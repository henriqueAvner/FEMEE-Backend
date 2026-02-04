using FEMEE.API.Configuration;
using FEMEE.API.Middleware;
using FEMEE.Application.Configurations;
using FEMEE.Application.DTOs.Auth;
using FEMEE.Application.DTOs.Campeonato;
using FEMEE.Application.DTOs.Conquista;
using FEMEE.Application.DTOs.InscricaoCampeonato;
using FEMEE.Application.DTOs.Jogador;
using FEMEE.Application.DTOs.Jogo;
using FEMEE.Application.DTOs.Noticia;
using FEMEE.Application.DTOs.Partida;
using FEMEE.Application.DTOs.Produto;
using FEMEE.Application.DTOs.Time;
using FEMEE.Application.DTOs.User;
using FEMEE.Application.Interfaces.Common;
using FEMEE.Application.Interfaces.Repositories;
using FEMEE.Application.Interfaces.Services;
using FEMEE.Application.Mappings;
using FEMEE.Application.Services;
using FEMEE.Application.Services.Auth;
using FEMEE.Application.Validators.Auth;
using FEMEE.Application.Validators.Campeonato;
using FEMEE.Application.Validators.Conquista;
using FEMEE.Application.Validators.InscricaoCampeonato;
using FEMEE.Application.Validators.Jogador;
using FEMEE.Application.Validators.Jogo;
using FEMEE.Application.Validators.Noticia;
using FEMEE.Application.Validators.Partida;
using FEMEE.Application.Validators.Produto;
using FEMEE.Application.Validators.Time;
using FEMEE.Application.Validators.User;
using FEMEE.Domain.Interfaces;
using FEMEE.Infrastructure.Data;
using FEMEE.Infrastructure.Data.Context;
using FEMEE.Infrastructure.Data.Repositories;
using FEMEE.Infrastructure.Extensions;
using FEMEE.Infrastructure.Security;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrWhiteSpace(connectionString))
    throw new InvalidOperationException("ConnectionStrings:DefaultConnection não está configurada em appsettings ou variáveis de ambiente.");

builder.Services.AddDbContext<FemeeDbContext>(options =>
    options.UseSqlServer(connectionString, sqlOptions => sqlOptions.MigrationsAssembly("FEMEE.Infrastructure"))
);

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITimeRepository, TimeRepository>();
builder.Services.AddScoped<ICampeonatoRepository, CampeonatoRepository>();
builder.Services.AddScoped<IJogadorRepository, JogadorRepository>();
builder.Services.AddAuthorizationPolicies();


// ===== CONFIGURAR FLUENTVALIDATION =====
builder.Services.AddValidatorsFromAssemblyContaining<CreateUserDtoValidator>();

//User
builder.Services.AddScoped<IValidator<CreateUserDto>, CreateUserDtoValidator>();
builder.Services.AddScoped<IValidator<UpdateUserDto>, UpdateUserDtoValidator>();
//Auth
builder.Services.AddScoped<IValidator<LoginRequest>, LoginRequestValidator>();
builder.Services.AddScoped<IValidator<RegisterRequest>, RegisterRequestValidator>();
//Time
builder.Services.AddScoped<IValidator<CreateTimeDto>, CreateTimeDtoValidator>();
builder.Services.AddScoped<IValidator<UpdateTimeDto>, UpdateTimeDtoValidator>();
//Jogador
builder.Services.AddScoped<IValidator<CreateJogadorDto>, CreateJogadorDtoValidator>();
builder.Services.AddScoped<IValidator<UpdateJogadorDto>, UpdateJogadorDtoValidator>();
//Partida
builder.Services.AddScoped<IValidator<CreatePartidaDto>, CreatePartidaDtoValidator>();
builder.Services.AddScoped<IValidator<UpdatePartidaDto>, UpdatePartidaDtoValidator>();
//Produto
builder.Services.AddScoped<IValidator<CreateProdutoDto>, CreateProdutoDtoValidator>();
builder.Services.AddScoped<IValidator<UpdateProdutoDto>, UpdateProdutoDtoValidator>();
//InscricaoCampeonato
builder.Services.AddScoped<IValidator<CreateInscricaoCampeonatoDto>, CreateInscricaoCampeonatoDtoValidator>();
builder.Services.AddScoped<IValidator<UpdateInscricaoCampeonatoDto>, UpdateInscricaoCampeonatoDtoValidator>();
//Jogo
builder.Services.AddScoped<IValidator<CreateJogoDto>, CreateJogoDtoValidator>();
builder.Services.AddScoped<IValidator<UpdateJogoDto>, UpdateJogoDtoValidator>();
//Conquista
builder.Services.AddScoped<IValidator<CreateConquistaDto>, CreateConquistaDtoValidator>();
builder.Services.AddScoped<IValidator<UpdateConquistaDto>, UpdateConquistaDtoValidator>();
//Noticia
builder.Services.AddScoped<IValidator<CreateNoticiaDto>, CreateNoticiaDtoValidator>();
builder.Services.AddScoped<IValidator<UpdateNoticiaDto>, UpdateNoticiaDtoValidator>();
//Campeonato
builder.Services.AddScoped<IValidator<CreateCampeonatoDto>, CreateCampeonatoDtoValidator>();
builder.Services.AddScoped<IValidator<UpdateCampeonatoDto>, UpdateCampeonatoDtoValidator>();

// ===== SERVIÇOS =====
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITimeService, TimeService>();
builder.Services.AddScoped<ICampeonatoService, CampeonatoService>();
builder.Services.AddScoped<IPartidaService, PartidaService>();
builder.Services.AddScoped<IJogadorService, JogadorService>();
builder.Services.AddScoped<INoticiaService, NoticiaService>();
builder.Services.AddScoped<IProdutoService, ProdutoService>();
builder.Services.AddScoped<IJogoService, JogoService>();
builder.Services.AddScoped<IConquistaService, ConquistaService>();
builder.Services.AddScoped<IInscricaoCampeonatoService, InscricaoCampeonatoService>();

// ===== SERVIÇOS OPCIONAIS =====
builder.Services.AddMemoryCache();
builder.Services.AddScoped<ICacheService, MemoryCacheService>();
builder.Services.AddScoped<IEmailService, SmtpEmailService>();
builder.Services.AddScoped<INotificationService, InMemoryNotificationService>();




var jwtSettings = new JwtSettings();
builder.Configuration.GetSection("JwtSettings").Bind(jwtSettings);

if (string.IsNullOrWhiteSpace(jwtSettings.SecretKey) || jwtSettings.SecretKey.Length < 32)
    throw new InvalidOperationException("JwtSettings:SecretKey é obrigatório e deve ter no mínimo 32 caracteres. Use User Secrets em desenvolvimento e variáveis de ambiente em produção.");

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPasswordHasher, BcryptPasswordHasher>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.SecretKey!)),
        ValidateIssuer = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidateAudience = true,
        ValidAudience = jwtSettings.Audience,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorizationPolicies();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var logger = LoggingConfiguration.ConfigureLogging();
Log.Logger = logger;

try
{
    builder.Host.UseSerilog();
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    
    // ===== CONFIGURAR SWAGGER COM JWT =====
    builder.Services.AddSwaggerWithJwt();
    
    // ===== CONFIGURAR CORS =====

    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowFrontend", policy =>
        {
            policy
                // Domínios permitidos
                .WithOrigins(
                    "https://femee-arena-hub.com",      // Produção
                    "http://localhost:3000",             // Desenvolvimento local
                    "http://localhost:5173"              // Vite dev server
                 )
                // Métodos HTTP permitidos
                .AllowAnyMethod()
                // Headers permitidos
                .AllowAnyHeader()
                // Permitir cookies/credenciais
                .AllowCredentials();
        });

        // Política alternativa para desenvolvimento (menos restritiva)
        options.AddPolicy("AllowAll", policy =>
        {
            policy
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
    });


    var app = builder.Build();

    // ===== ADICIONAR MIDDLEWARE DE TRATAMENTO DE ERROS =====

    app.UseMiddleware<ExceptionHandlingMiddleware>();
    app.UseMiddleware<RequestLoggingMiddleware>();
    // ===== ADICIONAR MIDDLEWARE DE CORS =====

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseCors("AllowFrontend");
    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();
    app.Run();

}
catch (Exception ex)
{
    Log.Fatal(ex, "Aplicação encerrada inesperadamente");
}
finally
{
    Log.CloseAndFlush();
}






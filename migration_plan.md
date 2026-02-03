# Plano de Migração de Arquivos - FEMEE Backend

## 1. Interfaces de Serviço
**De:** `src/FEMEE.Domain/Interfaces/IService/`
**Para:** `src/FEMEE.Application/Interfaces/Services/`

- `IAuthenticationService.cs`
- `ICampeonatoService.cs`
- `IPartidaService.cs`
- `IAuthorizationService.cs`
- `IEmailService.cs`
- `ICacheService.cs`
- `INotificationService.cs`

## 2. Implementações de Serviço
**De:** `src/FEMEE.Infrastructure/Security/Services/` (e outros locais se houver)
**Para:** `src/FEMEE.Application/Services/`

- `AuthenticationService.cs` (Mover de Infrastructure para Application)
- `UserService.cs` (Já está em Application/Services, manter e organizar)

## 3. Interfaces de Repositório
**De:** `src/FEMEE.Domain/Interfaces/IRepository/`
**Para:** `src/FEMEE.Application/Interfaces/Repositories/`

- `ICampeonatoRepository.cs`
- `IRepository.cs`
- `IJogadorRepository.cs`
- `ITimeRepository.cs`
- `IUnitOfWork.cs` (Mover de `src/FEMEE.Domain/Interfaces/IUnitOfWork.cs`)

## 4. Outras Interfaces
**De:** `src/FEMEE.Domain/Interfaces/`
**Para:** `src/FEMEE.Application/Interfaces/Common/` (ou local apropriado)

- `IPasswordHasher.cs`
- `IService` (Interface marcadora, se necessária, mover para Application/Interfaces)

## 5. Segurança e Infraestrutura
- `BcryptPasswordHasher.cs` e `PasswordHasher.cs` em `src/FEMEE.Infrastructure/Security/` devem permanecer em Infrastructure, mas implementar interfaces que agora estarão em Application.

## 6. DTOs e Validadores
- Já estão em `src/FEMEE.Application/`, apenas garantir que os namespaces estejam corretos após as mudanças de interfaces.

## 7. Refatoração de Namespaces
- Atualizar todos os arquivos para refletir os novos caminhos.
- Atualizar referências de projeto (csproj) se necessário.

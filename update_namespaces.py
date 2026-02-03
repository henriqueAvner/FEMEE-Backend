import os
import re

replacements = {
    "FEMEE.Domain.Interfaces.IService": "FEMEE.Application.Interfaces.Services",
    "FEMEE.Domain.Interfaces.IRepository": "FEMEE.Application.Interfaces.Repositories",
    "FEMEE.Domain.Interfaces": "FEMEE.Application.Interfaces.Repositories", # Para IUnitOfWork
    "namespace FEMEE.Domain.Interfaces": "namespace FEMEE.Application.Interfaces.Common", # Fallback para outros
    "FEMEE.Infrastructure.Security.Services": "FEMEE.Application.Services",
    "FEMEE.Domain.Interfaces.IPasswordHasher": "FEMEE.Application.Interfaces.Common.IPasswordHasher"
}

# Caso específico para IPasswordHasher que estava na raiz de Interfaces
specific_replacements = [
    ("namespace FEMEE.Domain.Interfaces", "namespace FEMEE.Application.Interfaces.Common"),
    ("using FEMEE.Domain.Interfaces;", "using FEMEE.Application.Interfaces.Common;\nusing FEMEE.Application.Interfaces.Repositories;\nusing FEMEE.Application.Interfaces.Services;")
]

def update_file(file_path):
    with open(file_path, 'r', encoding='utf-8') as f:
        content = f.read()
    
    original_content = content
    
    for old, new in replacements.items():
        content = content.replace(old, new)
    
    if content != original_content:
        with open(file_path, 'w', encoding='utf-8') as f:
            f.write(content)
        return True
    return False

root_dir = "/home/ubuntu/FEMEE-Backend/src"
files_updated = 0

for root, dirs, files in os.walk(root_dir):
    for file in files:
        if file.endswith(".cs"):
            if update_file(os.path.join(root, file)):
                files_updated += 1

print(f"Total de arquivos atualizados: {files_updated}")

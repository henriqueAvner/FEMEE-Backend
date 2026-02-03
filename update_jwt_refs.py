import os

replacements = {
    "FEMEE.Infrastructure.Security.Settings": "FEMEE.Application.Configurations",
    "using FEMEE.Infrastructure.Security.Settings;": "using FEMEE.Application.Configurations;"
}

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

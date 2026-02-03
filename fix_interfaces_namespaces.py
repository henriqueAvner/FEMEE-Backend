import os

def fix_namespace(file_path, new_namespace):
    with open(file_path, 'r', encoding='utf-8') as f:
        lines = f.readlines()
    
    new_lines = []
    for line in lines:
        if line.strip().startswith("namespace "):
            new_lines.append(f"namespace {new_namespace}\n")
        else:
            new_lines.append(line)
            
    with open(file_path, 'w', encoding='utf-8') as f:
        f.writelines(new_lines)

# Services
service_dir = "/home/ubuntu/FEMEE-Backend/src/FEMEE.Application/Interfaces/Services"
for file in os.listdir(service_dir):
    if file.endswith(".cs"):
        fix_namespace(os.path.join(service_dir, file), "FEMEE.Application.Interfaces.Services")

# Repositories
repo_dir = "/home/ubuntu/FEMEE-Backend/src/FEMEE.Application/Interfaces/Repositories"
for file in os.listdir(repo_dir):
    if file.endswith(".cs"):
        fix_namespace(os.path.join(repo_dir, file), "FEMEE.Application.Interfaces.Repositories")

# Common
common_dir = "/home/ubuntu/FEMEE-Backend/src/FEMEE.Application/Interfaces/Common"
for file in os.listdir(common_dir):
    if file.endswith(".cs"):
        fix_namespace(os.path.join(common_dir, file), "FEMEE.Application.Interfaces.Common")

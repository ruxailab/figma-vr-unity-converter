def write_glb_content_to_txt(file_path, txt_file_path):
    try:
        with open(file_path, 'rb') as glb_file:
            content = glb_file.read()
            with open(txt_file_path, 'w') as txt_file:
                txt_file.write(str(content))
        print("Conteúdo do arquivo GLB foi escrito com sucesso em", txt_file_path)
    except FileNotFoundError:
        print("Arquivo não encontrado.")
    except Exception as e:
        print("Erro ao escrever no arquivo:", e)

# Exemplo de uso:
glb_file_path = "./teeny_flat_t2.glb"
txt_file_path = "./arquivo.txt"
write_glb_content_to_txt(glb_file_path, txt_file_path)

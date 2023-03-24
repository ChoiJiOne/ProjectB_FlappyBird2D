import os
import shutil

def is_folder_exist(folder_path):
    return os.path.exists(folder_path) and os.path.isdir(folder_path)

def is_file_exist(folder_path, file_name):
    file_path = os.path.join(folder_path, file_name)
    return os.path.exists(file_path) and os.path.isfile(file_path)

def copy_file(source_path, destination_path, file_name):
    source_full_path = source_path + "\\" + file_name
    destination_full_path = destination_path + "\\" + file_name
    shutil.copy(source_full_path, destination_full_path)

if __name__ == "__main__":
    current_directory = os.getcwd()

    dll_library_path = os.path.join(current_directory, "ThirdParty\\Lib")

    binary_paths = [
        os.path.join(current_directory, "FlappyBird2D\\bin\\Win64\\Debug"),
        os.path.join(current_directory, "FlappyBird2D\\bin\\Win64\\Release"),
        os.path.join(current_directory, "FlappyBird2D\\bin\\Win64\\Shipping"),
    ]

    dlls = [
        "SDL2.dll",
        "SDL2_image.dll",
        "SDL2_mixer.dll",
        "SDL2_ttf.dll",
    ]

    for binary_path in binary_paths:
        if not is_folder_exist(binary_path):
            os.makedirs(binary_path)

        for dll in dlls:
            if is_file_exist(dll_library_path, dll) and not is_file_exist(binary_path, dll):
                copy_file(dll_library_path, binary_path, dll)

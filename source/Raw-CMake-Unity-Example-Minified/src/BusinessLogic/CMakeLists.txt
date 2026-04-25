# Register .C files with CMake as C++ files, so that they are compiled with a C++ compiler and can be linked with C++ code.
add_library(source_code STATIC
  sample.c
)

target_include_directories(source_code PUBLIC
  ${CMAKE_CURRENT_LIST_DIR}
)

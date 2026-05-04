cmake_minimum_required(VERSION 3.20)
project(my_project C)

set(CMAKE_C_STANDARD 11)

enable_testing()
include(CTest)

add_subdirectory(external/unity)
add_subdirectory(src)
add_subdirectory(tests)

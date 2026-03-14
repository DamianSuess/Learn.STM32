cmake_minimum_required(VERSION 3.22)

#
# This file is generated only once,
# and is not re-generated if converter is called multiple times.
#
# User is free to modify the file as much as necessary
#

# Setup compiler settings
set(CMAKE_C_STANDARD 11)
set(CMAKE_C_STANDARD_REQUIRED ON)
set(CMAKE_C_EXTENSIONS ON)


# Define the build type
if(NOT CMAKE_BUILD_TYPE)
    set(CMAKE_BUILD_TYPE "Debug")
endif()

# Set the project name
set(CMAKE_PROJECT_NAME Test-STM32H5-UnityUnitTest)

# Enable compile command to ease indexing with e.g. clangd
set(CMAKE_EXPORT_COMPILE_COMMANDS TRUE)

# Core project settings
project(${CMAKE_PROJECT_NAME})
message("Build type: " ${CMAKE_BUILD_TYPE})

# Enable CMake support for ASM and C languages
enable_language(C ASM)

# Create an executable object type
add_executable(${CMAKE_PROJECT_NAME})

# Add STM32CubeMX generated sources
add_subdirectory(cmake/stm32cubemx)

# Link directories setup
target_link_directories(${CMAKE_PROJECT_NAME} PRIVATE
    # Add user defined library search paths
)

# Add sources to executable
target_sources(${CMAKE_PROJECT_NAME} PRIVATE
    # Add user sources here
)

# Add include paths
target_include_directories(${CMAKE_PROJECT_NAME} PRIVATE
    # Add user defined include paths
)

# Add project symbols (macros)
target_compile_definitions(${CMAKE_PROJECT_NAME} PRIVATE
    # Add user defined symbols
)

# Remove wrong libob.a library dependency when using cpp files
list(REMOVE_ITEM CMAKE_C_IMPLICIT_LINK_LIBRARIES ob)

# Add linked libraries
target_link_libraries(${CMAKE_PROJECT_NAME}
    stm32cubemx

    # Add user defined libraries
)

## Unity Unit Testing Framework
if (CMAKE_CROSSCOMPILING)
  # message(WARNING "Unit testing is not supported when cross-compiling. Skipping Unity tests.")
  message(STATUS "Cross compiling for board. Skipping Unity tests.")
else()
  message(STATUS "Unit testing enabled. Adding Unity tests.")

  set(UNITY_SRC libs/Unity/src/)

  enable_testing()
  add_library(Unity STATIC ${UNITY_SRC}/unity.c)
  target_include_directories(Unity PUBLIC ${UNITY_SRC})

  ### Create Executable
  add_subdirectory(tests)
  add_executable(test_example tests/test_example.c)
  ## add_executable(test_example tests/test_example.c Core/Src/main.c)

  target_link_libraries(test_example Unity Core/Src)

  ## Add test file
  add_test(NAME test_example COMMAND test_example)

endif()

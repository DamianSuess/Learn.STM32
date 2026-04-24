cmake_minimum_required(VERSION 3.12)

project ("Sample CMake-CTest with Unity Project")

## Project/Module Configuration

## Note: Directory must contain a CMakeLists.txt file.
add_subdirectory(src/BusinessLogic)

## Don't set "add_executable" or else it will throw an error (unresolved external symbol _main)
#### add_executable (CMakeProject "src/BusinessLogic/sample.c")

#if (CMAKE_VERSION VERSION_GREATER 3.12)
#  set_property(TARGET CMakeProject PROPERTY CXX_STANDARD 20)
#endif()

## CTest Configuration

message("Building with CTest version ${CMAKE_VERSION}")

## enable_testing()
include(CTest)
#add_subdirectory(libs/Unity)
add_subdirectory(libs)          # allow the CMakeLists.txt in libs to add the Unity library and the tests

# find_package(GTest REQUIRED)
# find_package(Catch2 CONFIG REQUIRED)
add_subdirectory(tests)

## Don't set add_executable for the test, as it is already set in the tests/CMakeLists.txt file
#### add_executable(UnitTest "tests\\test.c")

#add_test(UnitTest0 UnitTest 0)
#add_test(UnitTest1 UnitTest 1)

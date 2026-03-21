# CMakeLists for Tests

# Register .c files with CMake
add_executable(suite_1_app
  "test.c"
)

# souce_code  - Defined in src/BusinessLogic/CMakeLists.txt
# Unity       - Defined in libs/CMakeLists.txt
target_link_libraries(suite_1_app
  source_code
  Unity
)

# Name of Test Group: suite_1_test
# Files to test:      suite_1_app
add_test(suite_1_test suite_1_app)

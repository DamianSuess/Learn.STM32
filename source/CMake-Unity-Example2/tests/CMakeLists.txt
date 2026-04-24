# CMakeLists for Tests

# Register .c files with CMake
add_executable(suite_1_app
  "test.c"
)

# souce_code  - Defined in src/BusinessLogic/CMakeLists.txt
# Unity       - Defined in libs/Unity-min/CMakeLists.txt
target_link_libraries(suite_1_app
  source_code
  Unity
)

# Name of Test Group: test_suite1_name
# Files to test:      suite_1_app
add_test(test_suite1_name suite_1_app)

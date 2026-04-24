# Executing entry point for the test suite
add_executable(suite_1_app
  test_sample.c
)

# ? Do we need to add a CMakeLists.txt in Src/business folder and define it as a library ("add_library business STATIC sample.c)")?
target_link_libraries(suite_1_app
  business
  Unity
)

add_test(suite_1_test suite_1_app)

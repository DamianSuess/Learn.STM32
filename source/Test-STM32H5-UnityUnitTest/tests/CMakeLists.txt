# Need to include CTest on every CMakeLists.txt which is going to use tests.
include(CTest)

list(APPEND CMAKE_CTEST_ARGUMENTS "--output-on-failure")
list(APPEND CMAKE_CTEST_ARGUMENTS "--verbose")
enable_testing()

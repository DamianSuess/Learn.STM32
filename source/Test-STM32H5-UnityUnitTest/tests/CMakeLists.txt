# Need to include CTest on every CMakeLists.txt which is going to use tests.

#[[
include(CTest)

set(THIS_LIBRARY_NAME businessTestLib)

list(APPEND CMAKE_CTEST_ARGUMENTS "--output-on-failure")
list(APPEND CMAKE_CTEST_ARGUMENTS "--verbose")

# Gather all the test files
file(GLOB FILES "*.c")

# add_library: With this declaration, you express the intent to build a library.
add_library(${THIS_LIBRARY_NAME} ${FILES})

# target_link_libraries: If you link with other libraries, list them here
#target_link_libraries(
#  ${THIS_LIBRARY_NAME}
#  PRIVATE srcLib
#  PRIVATE unityLib
#)

# target_include_directories: Libraries need to publish their header files
target_include_directories(
  ${THIS_LIBRARY_NAME}
  PUBLIC
  .
)

enable_testing()
]]#

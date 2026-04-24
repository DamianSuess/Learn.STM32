# CMakeLists.txt for the business module
add_library(business STATIC
  sample.c)

target_include_directories(business PUBLIC
  ${CMAKE_CURRENT_LIST_DIR}
  #${CMAKE_CURRENT_SOURCE_DIR}/../../Inc/business
)

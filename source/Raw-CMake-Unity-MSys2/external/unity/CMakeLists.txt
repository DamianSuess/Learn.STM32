add_library(unity STATIC
  #Unity/src/unity.c
  unity.c
)

target_include_directories(unity PUBLIC
  #Unity/src
  ${CMAKE_CURRENT_SOURCE_DIR}
)

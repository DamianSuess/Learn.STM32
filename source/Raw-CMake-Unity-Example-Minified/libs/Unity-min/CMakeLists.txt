# CMakeLists for Unity library
# Create a static library from the Unity source files
add_library(UnityLib STATIC
  unity.c
)

target_include_directories(UnityLib PUBLIC
  ${CMAKE_CURRENT_LIST_DIR}
  #${CMAKE_CURRENT_SOURCE_DIR}
)

# When checking out from GitHub into subfolder ("libs/Unity/src")
# the following path assumes this files in in the base, "libs/" folder.
#target_include_directories(UnityLib PUBLIC
#    Unity/src
#)

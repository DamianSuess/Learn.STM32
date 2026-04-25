
# Define a library called "Unity" that includes the Unity testing framework source files.
# This library can be linked to test executables that use Unity for unit testing.
add_library(UnityLib STATIC
  # Unity/src/unity.c   # Original Unity library source files
  Unity-min/unity.c     # Core Unity library source files
)

target_include_directories(UnityLib PUBLIC
  # Unity/src
  Unity-min
)

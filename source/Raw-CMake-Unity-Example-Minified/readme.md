## Steps to reproduce

1. Create folder with following files:
   1. sample.cpp, sample.h
   2. test.cpp, test.h
   3. CMakeLists.txt
2. Edit files with boiler-plate code (see below)
3. Launch VS Code
4. Allow STM32 to discover CMake
5. Build project
   1. Select "Visual Studio x86_amd64" for local builds
   2. This builds for 32-bit and is 64-bit compatible
6. "Testing Beaker" should now appear, showing 2 Unit Tests

### Key Takeaways

* `CMakeLists.txt` must
  * Include `enable_testing()` to enable CTest
  * Use `add_executable(UnitTest "test.cpp")` to specify test file
  * Add test suites `add_test(UnitTest0 UnitTest 0)`
* You must build the code first for CMake to discover CTest

## Boiler Plate Code (1st-Pass)

### CMakeLists.txt

```cmake
cmake_minimum_required(VERSION 3.12)

project ("CMakeProject")
add_executable (CMakeProject "sample.cpp" "sample.h")

if (CMAKE_VERSION VERSION_GREATER 3.12)
  set_property(TARGET CMakeProject PROPERTY CXX_STANDARD 20)
endif()

enable_testing()

# find_package(GTest REQUIRED)
# find_package(Catch2 CONFIG REQUIRED)
add_executable(UnitTest "test.cpp")

add_test(UnitTest0 UnitTest 0)
add_test(UnitTest1 UnitTest 1)
```

### Sample C/H

```c
#include "sample.h"

int do_stuff()
{
  printf("Hello, World!");
  return 0;
}

///////////////////////

#ifndef _TEST_H_
#define _TEST_H_
  #include<stdio.h>
  int do_stuff();
#endif
```

### Test C/H

```c
#include "test.h"
#include "sample.h"

int test()
{
  return 0;
}

/////////////////

#ifndef _SAMPLE_H_
#define _SAMPLE_H_
  #include<stdio.h>
  int do_stuff();
#endif
```

## References

* [Unity Test](https://www.throwtheswitch.org/unity/)
  * [CMake Unity Integration (HoneyTreeLabs.com)](https://honeytreelabs.com/posts/cmake-unity-integration/)
    * [Sample Code (GitHub)](https://github.com/rpoisel/cmake-unity-tutorial)
* Google Test
  * [Do you even test? (your code with CMake)](https://www.youtube.com/watch?v=pxJoVRfpRPE)
  * [Introduction to Google Test and CMake](https://www.youtube.com/watch?v=Lp1ifh9TuFI)
  * [Using CMake's CTest to create and run all your C++ tests](https://www.youtube.com/watch?v=mdkVnmj32ZQ)
    * Using WSL-Ubuntu on Windows

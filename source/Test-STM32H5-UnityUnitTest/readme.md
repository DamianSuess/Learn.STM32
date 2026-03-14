# STM32 with Unity Unit-Testing Framework

The Unity unit-testing framework _"is written in 100% pure ANSI C._" and is intended for tiny 8-bit MCU to 64-bit ones as well.

## Jump-Start Usage

## Configuring

1. Add "tests" folder to your project
   * `tests`
2. Create Git SubModule in the path `libs/unity/`
   * [Unity](https://github.com/ThrowTheSwitch/Unity/releases)
3. Modify the root `CMakeLists.txt` to use testing

```sh
## Unity Unit Testing Framework
if (CMAKE_CROSSCOMPILING)
  # message(WARNING "Unit testing is not supported when cross-compiling. Skipping Unity tests.")
  message(STATUS "Cross compiling for board. Skipping Unity tests.")
else()
  message(STATUS "Unit testing enabled. Adding Unity tests.")

  set(UNITY_SRC libs/Unity/src/)

  enable_testing()
  add_library(Unity STATIC ${UNITY_SRC}/unity.c)
  target_include_directories(Unity PUBLIC ${UNITY_SRC})

  ### Create Executable
  add_subdirectory(tests)
  add_executable(test_example tests/test_example.c)
  ## add_executable(test_example tests/test_example.c Core/Src/main.c)

  target_link_libraries(test_example Unity Core/Src)

  ## Add test file
  add_test(NAME test_example COMMAND test_example)

endif()
```

4. Add new file, `tests/CMakeLists.txt` (_empty file_)
5. Add new file, `tests/test_example.c` (_see file for reference_)
6. Execute command to build tests to folder `build/Tests` next to our `Debug` folder:
   1. What the following does:
      1. `-B <path-to-build>` - Explicitly specify a build directory.
      2. `--build <dir>`      - Build a CMake-generated project binary tree.
   2. `cube cmake -B build/Tests -DCMAKE_BUILD_TYPE=Release`
      1. You should see our custom message, "_Unit testing enabled. Adding Unity tests._"
   3. `cube cmake --build build/Tests/`

## Building Code

The following assumes "cube.exe" is in your PATH (Environment Variables).

```sh
# Clean project and build
cube cmake --build . --clean-first
```

## Further Reading

### Alternate Unit Testing Tools

* [Comparison of Unit Test Frameworks](https://www.throwtheswitch.org/comparison-of-unit-test-frameworks)
* [CPPUTEST (C/C++)](https://github.com/cpputest/cpputest) - _xUnit compatible C++ suite which has been designed with embedded developers in mind._
* [Google Test (aimed at C++)](https://github.com/google/googletest)
* [Catch2 (C++ only)](https://github.com/catchorg/Catch2)

### Building with Cube and CMake

As of v3.8.0 of the VS Code extension, STM32CubeCLT is no longer needed to be installed separately and comes bundled with STM32's VS Code Extensions. However, you usually have to track down the path yourself and open a terminal window.  Sometimes, VS Code does show a "File Complation" in the Terminal tab, which adds the path `cube.exe` command wrapper automagically, and you can execute `cmake` commands.

> ```txt
> C:\Users\USERNAME\.vscode\extensions\stmicroelectronics.stm32cube-ide-core-1.2.0-win32-x64\resources\binaries\win32\x86_64\
> ```

## References

* [Unity Unit-Test Framework]()
* [Blog: CMake on STM32 using 'Catch2' (dev.to)](https://dev.to/pgradot/cmake-on-smt32-episode-7-unit-tests-13gj)
* [Blog: Modern On-Target Embedded System Testing with CMake and CTest](https://mcuoneclipse.com/2023/12/18/modern-on-target-embedded-system-testing-with-cmake-and-ctest/) - Using _Unity_
  * [Blog: Building with CMake Presets](https://mcuoneclipse.com/2023/12/03/building-with-cmake-presets/)

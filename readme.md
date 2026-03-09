# STM32 Nucleo H563ZI - Blinky LEDs

## Project Setup

### Configuration

1. Run STM32 MX
2. Select STM32H563ZIT6
3. Click, Nucleo board link in list
4. Select, Project Manger tab
5. Fill in project details
   1. Toolchain: CMake (_default, EWARM_)
6. File > Save project as ...

### VS Code

1. File > Open Folder > Load Project
2. "Would you like to configure discovered CMake project(s) as STM32Cube project(s)?"
   * Click, YES
3. 

## Code Formatter

VS Code by default uses clang-format. The '_Xeno Innovations_' style used in the projects is based on Microsoft but with 2 spaces and maintains tidyness.

> Execute the formatter via `SHIFT-ALT-F`.

## References

* [Get started with STM32Cube for VS Code: from installation to debugging](https://www.youtube.com/watch?v=aWMni01XGeI)

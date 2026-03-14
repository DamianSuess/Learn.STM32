# STM32 Nucleo H563ZI - Blinky LEDs

## What's In The Repo

* **Template Files**
  * VS Code Profile
    * File: `suesslabs.clang-format`
  * CLang-Format - _Source code formatting rules_
    * File: `basic.clang-format` - _recommended ruleset based on Microsoft_
    * File: `suesslabs.clang-format` - _looser `{ }` rules alternative_
    * `test-ugly-1.c` - _test file for formatter before beautification_
* **STM32 Sample Projects**
  * Test-Nucleo-H563ZI-LEDs - _Blinking LEDs and Toggled LED (based on CubeMX Generated)_
  * Test-STM32-BlankProject - _Empty STM32 Project (VS Code generated)_

## Recommended Tools

* STMicro Tools Needed
  * [STM32CubeIDE (for VS Code)](https://www.st.com/en/development-tools/stm32cubeide.html)
  * [STM32CubeMX]()
* Nice to Have
  * [STM32CubeProgrammer](https://www.st.com/en/development-tools/stm32cubeprog.html)
  * [STM32CubeMonitor - Monitoring tool to test STM32 applications at run-time](https://www.st.com/en/development-tools/stm32cubemonitor.html)
  * ST-Link Progrmmer
    * [ST-Link Board Firmware upgrade](https://www.st.com/en/development-tools/stsw-link007.html)
    * [STLINK Programmer USB Drivers](https://www.st.com/en/development-tools/stsw-link009.html)
    * [USB driver for ST-LINK/V2, ST-LINK/V2-1 and STLINK-V3 (PDF)](https://www.st.com/resource/en/data_brief/stsw-link009.pdf)

## Project Setup

### WARNINGS

> Say "NO" to VS Code's _C/C++ Tools Extension_, it conflicts with STM32's built-in C/C++ intellisense.

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

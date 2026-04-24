# STM32 + Unity Unit Testing Framework

1. Create an example project and steps for how to integrate the Unity unit-testing framework (https://www.throwtheswitch.org/unity) with a STM32 projects built using VS Code and CMake written in C. I want the instructions to clearly define how I add the library to my STM32 VS Code project. Show examples of how to perform unit testing using the Unity unit-testing framework. I want the project to build for Debug, Release, and Unit-Testing. The Unit Tests should only test the business logic.

## Overview

Below is a **complete, copy‑pasteable example project layout + CMake setup** showing how to integrate the **Unity** C unit-testing framework into an **STM32** project built with **VS Code + CMake** (C language), with three build flavors:

*   **Debug** (firmware for STM32)
*   **Release** (firmware for STM32)
*   **Unit-Testing** (host-built unit tests that **only test business logic**, not HAL/LL/peripherals)

Unity is designed to be small/portable (core is essentially **`unity.c` + headers**) and works well in embedded contexts. [\[throwtheswitch.org\]](https://www.throwtheswitch.org/unity), [\[github.com\]](https://github.com/ThrowTheSwitch/Unity)

***

## 0) What “business logic only” means (and how we enforce it)

**Rule of thumb:** Anything you want to unit-test should **not include STM32 HAL headers** and should **not directly call hardware functions**. Instead:

*   Put business logic into a **pure C library** (e.g., `src/business/*`)
*   If it needs time/IO, define a tiny **platform interface** (e.g., `platform_time.h`)
*   Provide:
    *   a **real implementation** in firmware (calls HAL)
    *   a **fake/stub implementation** in unit tests

This keeps unit tests focused, deterministic, and runnable on your PC.

Unity’s test style is plain C functions + assertions (e.g., `TEST_ASSERT_EQUAL_INT`) executed through `UNITY_BEGIN()`, `RUN_TEST()`, and `UNITY_END()`. [\[throwtheswitch.org\]](https://www.throwtheswitch.org/unity), [\[docs.platformio.org\]](https://docs.platformio.org/en/stable/advanced/unit-testing/frameworks/unity.html)

***

## 1) Example project structure

```text
stm32-unity-cmake/
├─ CMakeLists.txt
├─ CMakePresets.json
├─ cmake/
│  ├─ toolchain-arm-none-eabi.cmake
│  └─ stm32_flags.cmake
├─ external/
│  └─ unity/                 # Option A: Git submodule OR Option B: empty (FetchContent)
├─ src/
│  ├─ business/
│  │  ├─ thermostat.c
│  │  ├─ thermostat.h
│  │  ├─ platform_time.h     # platform interface (no HAL includes)
│  ├─ platform_stm32/
│  │  └─ platform_time_stm32.c  # uses HAL_GetTick()
│  └─ firmware/
│     └─ main.c              # normal STM32 main
└─ tests/
   ├─ stubs/
   │  └─ platform_time_fake.c
   ├─ test_thermostat.c
   └─ test_main.c
```

***

## 2) Add Unity to your STM32 VS Code project (two clear options)

Unity can be added to an existing build easily because it’s essentially a small set of C sources/headers. [\[throwtheswitch.org\]](https://www.throwtheswitch.org/unity), [\[github.com\]](https://github.com/ThrowTheSwitch/Unity)

### Option A (recommended for embedded repos): **Git submodule**

This gives traceability and easy updates.

```bash
cd stm32-unity-cmake
git submodule add https://github.com/ThrowTheSwitch/Unity.git external/unity
git submodule update --init --recursive
```

Unity’s upstream repo is here: <https://github.com/ThrowTheSwitch/Unity> [\[github.com\]](https://github.com/ThrowTheSwitch/Unity)

### Option B: **CMake FetchContent** (no vendoring)

Fetch at configure time. FetchContent is intended for downloading dependencies during configuration and making targets available to your build. [\[cmake.org\]](https://cmake.org/cmake/help/latest/module/FetchContent.html), [\[coderefine....github.io\]](https://coderefinery.github.io/cmake-workshop/fetch-content/)

You’ll see this implemented in the CMake below.

***

## 3) Toolchain file for STM32 (arm-none-eabi)

Cross-compiling in CMake is typically done via a **toolchain file**. CMake can’t run target executables during configure, so embedded toolchains commonly set `CMAKE_TRY_COMPILE_TARGET_TYPE` to `STATIC_LIBRARY`. [\[cmake.org\]](https://cmake.org/cmake/help/book/mastering-cmake/chapter/Cross%20Compiling%20With%20CMake.html), [\[jchisholm2....github.io\]](https://jchisholm204.github.io/posts/stm32_cmake/)

**`cmake/toolchain-arm-none-eabi.cmake`**

```cmake
set(CMAKE_SYSTEM_NAME Generic)
set(CMAKE_SYSTEM_PROCESSOR arm)

set(TOOLCHAIN_PREFIX arm-none-eabi-)

set(CMAKE_C_COMPILER   ${TOOLCHAIN_PREFIX}gcc)
set(CMAKE_ASM_COMPILER ${TOOLCHAIN_PREFIX}gcc)

set(CMAKE_OBJCOPY ${TOOLCHAIN_PREFIX}objcopy)
set(CMAKE_SIZE    ${TOOLCHAIN_PREFIX}size)

# Important for embedded cross-compile: don't try to run executables
set(CMAKE_TRY_COMPILE_TARGET_TYPE STATIC_LIBRARY)

# Optional: avoid host paths polluting search
set(CMAKE_FIND_ROOT_PATH_MODE_PROGRAM NEVER)
set(CMAKE_FIND_ROOT_PATH_MODE_LIBRARY ONLY)
set(CMAKE_FIND_ROOT_PATH_MODE_INCLUDE ONLY)
```

This pattern is consistent with common STM32+CMake templates and cross-compiling guidance. [\[jchisholm2....github.io\]](https://jchisholm204.github.io/posts/stm32_cmake/), [\[cmake.org\]](https://cmake.org/cmake/help/book/mastering-cmake/chapter/Cross%20Compiling%20With%20CMake.html)

***

## 4) Top-level CMake: firmware + business library + unit tests

### 4.1 `CMakeLists.txt` (root)

> Key idea: **business logic builds in all configs**.
> Firmware builds only for STM32 presets.
> Unit test executable builds only for Unit-Testing preset.

```cmake
cmake_minimum_required(VERSION 3.20)
project(stm32_unity_example C)

# Options controlling what gets built
option(BUILD_FIRMWARE "Build STM32 firmware (cross-compiled)" ON)
option(UNIT_TESTING   "Build host-based unit tests"          OFF)

set(CMAKE_C_STANDARD 11)
set(CMAKE_C_STANDARD_REQUIRED ON)

# ---- Business Logic Library (no HAL includes here!) ----
add_library(business_logic STATIC
  src/business/thermostat.c
)

target_include_directories(business_logic PUBLIC
  ${CMAKE_CURRENT_SOURCE_DIR}/src/business
)

target_compile_definitions(business_logic PUBLIC
  $<$<BOOL:${UNIT_TESTING}>:UNIT_TESTING=1>
)

# ---- Firmware (STM32) ----
if(BUILD_FIRMWARE)
  add_executable(firmware.elf
    src/firmware/main.c
    src/platform_stm32/platform_time_stm32.c
    # Add your CubeMX-generated sources, startup, system_stm32xx.c, etc.
  )

  target_link_libraries(firmware.elf PRIVATE business_logic)

  # Example: add MCU flags/includes here (you will adapt to your STM32 part)
  include(cmake/stm32_flags.cmake)
  stm32_apply_flags(firmware.elf)

  # (Optional) create .bin/.hex
  add_custom_command(TARGET firmware.elf POST_BUILD
    COMMAND ${CMAKE_OBJCOPY} -O ihex  $<TARGET_FILE:firmware.elf> firmware.hex
    COMMAND ${CMAKE_OBJCOPY} -O binary $<TARGET_FILE:firmware.elf> firmware.bin
    COMMAND ${CMAKE_SIZE} $<TARGET_FILE:firmware.elf>
  )
endif()

# ---- Unit Tests (host build, business logic only) ----
if(UNIT_TESTING)
  include(CTest)        # enables testing + ctest integration
  enable_testing()

  # --- Unity dependency ---
  # Choose ONE approach:

  # Option A: If using Git submodule at external/unity
  # add_subdirectory(external/unity EXCLUDE_FROM_ALL)

  # Option B: FetchContent
  include(FetchContent)  # FetchContent usage per CMake docs
  FetchContent_Declare(unity
    GIT_REPOSITORY https://github.com/ThrowTheSwitch/Unity.git
    GIT_TAG        master
  )
  FetchContent_MakeAvailable(unity)

  add_executable(unit_tests
    tests/test_main.c
    tests/test_thermostat.c
    tests/stubs/platform_time_fake.c
  )

  target_include_directories(unit_tests PRIVATE
    ${CMAKE_CURRENT_SOURCE_DIR}/tests
    ${CMAKE_CURRENT_SOURCE_DIR}/src/business
  )

  target_link_libraries(unit_tests PRIVATE
    business_logic
    unity
  )

  add_test(NAME business_logic_tests COMMAND unit_tests)
endif()
```

**Notes:**

*   Unity provides a CMake build in its repo (and/or can be built as a small static lib); it’s commonly integrated via `add_subdirectory` or by building `unity.c` into a library. [\[github.com\]](https://github.com/ThrowTheSwitch/Unity), [\[throwtheswitch.org\]](https://www.throwtheswitch.org/build/cmake)
*   `FetchContent_MakeAvailable()` is the standard modern flow for pulling a dependency at configure time and adding it to the build. [\[cmake.org\]](https://cmake.org/cmake/help/latest/module/FetchContent.html), [\[coderefine....github.io\]](https://coderefinery.github.io/cmake-workshop/fetch-content/)

### 4.2 STM32 flags helper (example)

**`cmake/stm32_flags.cmake`**

```cmake
function(stm32_apply_flags tgt)
  # Example MCU flags: adjust to your device (e.g. cortex-m4/m7, FPU, etc.)
  target_compile_options(${tgt} PRIVATE
    -mcpu=cortex-m4
    -mthumb
    -ffunction-sections
    -fdata-sections
  )
  target_link_options(${tgt} PRIVATE
    -mcpu=cortex-m4
    -mthumb
    -Wl,--gc-sections
    # -T path/to/linker_script.ld
  )
endfunction()
```

***

## 5) Business logic module example (testable, no HAL includes)

### 5.1 Platform interface: `src/business/platform_time.h`

```c
#pragma once
#include <stdint.h>

/* Platform abstraction:
 * - Firmware implementation calls HAL_GetTick() or a timer.
 * - Unit tests provide a fake time source.
 */
uint32_t platform_millis(void);
```

### 5.2 Business logic: `src/business/thermostat.h`

```c
#pragma once
#include <stdint.h>
#include <stdbool.h>

typedef struct
{
    float setpoint_c;
    float hysteresis_c;
    uint32_t min_on_time_ms;
    bool heater_on;
    uint32_t last_on_ms;
} thermostat_t;

void thermostat_init(thermostat_t* t, float setpoint_c, float hysteresis_c, uint32_t min_on_time_ms);

/* Returns current heater state after update */
bool thermostat_update(thermostat_t* t, float current_temp_c);
```

### 5.3 Business logic: `src/business/thermostat.c`

```c
#include "thermostat.h"
#include "platform_time.h"

static bool below_on_threshold(const thermostat_t* t, float temp)
{
    return temp < (t->setpoint_c - t->hysteresis_c);
}

static bool above_off_threshold(const thermostat_t* t, float temp)
{
    return temp > (t->setpoint_c + t->hysteresis_c);
}

void thermostat_init(thermostat_t* t, float setpoint_c, float hysteresis_c, uint32_t min_on_time_ms)
{
    t->setpoint_c = setpoint_c;
    t->hysteresis_c = hysteresis_c;
    t->min_on_time_ms = min_on_time_ms;
    t->heater_on = false;
    t->last_on_ms = 0u;
}

bool thermostat_update(thermostat_t* t, float current_temp_c)
{
    const uint32_t now = platform_millis();

    if (!t->heater_on)
    {
        if (below_on_threshold(t, current_temp_c))
        {
            t->heater_on = true;
            t->last_on_ms = now;
        }
    }
    else
    {
        const uint32_t on_duration = now - t->last_on_ms;
        if (on_duration >= t->min_on_time_ms && above_off_threshold(t, current_temp_c))
        {
            t->heater_on = false;
        }
    }

    return t->heater_on;
}
```

***

## 6) Firmware platform implementation (uses HAL, not unit-tested)

**`src/platform_stm32/platform_time_stm32.c`**

```c
#include "platform_time.h"

// In real project, include "stm32xxxx_hal.h"
extern uint32_t HAL_GetTick(void);

uint32_t platform_millis(void)
{
    return HAL_GetTick();
}
```

***

## 7) Unit tests (Unity)

Unity tests are C functions (no args, no return) with assertions like `TEST_ASSERT_EQUAL_INT`, and a runner that calls `UNITY_BEGIN()`/`RUN_TEST()`/`UNITY_END()`. [\[throwtheswitch.org\]](https://www.throwtheswitch.org/unity), [\[docs.platformio.org\]](https://docs.platformio.org/en/stable/advanced/unit-testing/frameworks/unity.html)

### 7.1 Fake platform time: `tests/stubs/platform_time_fake.c`

```c
#include "platform_time.h"

static uint32_t g_fake_ms;

void fake_time_set(uint32_t ms) { g_fake_ms = ms; }
void fake_time_advance(uint32_t delta) { g_fake_ms += delta; }

uint32_t platform_millis(void)
{
    return g_fake_ms;
}
```

Add declarations for helper functions (only tests use them):

**`tests/stubs/platform_time_fake.h`**

```c
#pragma once
#include <stdint.h>
void fake_time_set(uint32_t ms);
void fake_time_advance(uint32_t delta);
```

### 7.2 Test file: `tests/test_thermostat.c`

```c
#include "unity.h"
#include "thermostat.h"
#include "stubs/platform_time_fake.h"

static thermostat_t t;

void setUp(void)
{
    fake_time_set(0);
    thermostat_init(&t, 25.0f, 0.5f, 1000u); // setpoint 25C, hysteresis 0.5C, min on 1s
}

void tearDown(void) { }

void test_heater_turns_on_below_lower_threshold(void)
{
    // Lower threshold = 24.5
    TEST_ASSERT_FALSE(thermostat_update(&t, 24.6f));
    TEST_ASSERT_TRUE (thermostat_update(&t, 24.4f));
}

void test_heater_respects_min_on_time_before_turning_off(void)
{
    (void)thermostat_update(&t, 24.4f); // turn on
    TEST_ASSERT_TRUE(t.heater_on);

    // Go above upper threshold (25.5), but not enough time elapsed
    fake_time_advance(500);
    TEST_ASSERT_TRUE(thermostat_update(&t, 26.0f)); // still on due to min_on_time

    // After min_on_time, it may turn off if above threshold
    fake_time_advance(600);
    TEST_ASSERT_FALSE(thermostat_update(&t, 26.0f));
}

void test_hysteresis_prevents_chatter(void)
{
    (void)thermostat_update(&t, 24.4f); // on
    fake_time_advance(1100);

    // Between thresholds -> should remain ON
    TEST_ASSERT_TRUE(thermostat_update(&t, 25.2f));

    // Above upper threshold -> off
    TEST_ASSERT_FALSE(thermostat_update(&t, 25.6f));
}
```

### 7.3 Test runner: `tests/test_main.c`

```c
#include "unity.h"

void test_heater_turns_on_below_lower_threshold(void);
void test_heater_respects_min_on_time_before_turning_off(void);
void test_hysteresis_prevents_chatter(void);

int main(void)
{
    UNITY_BEGIN();

    RUN_TEST(test_heater_turns_on_below_lower_threshold);
    RUN_TEST(test_heater_respects_min_on_time_before_turning_off);
    RUN_TEST(test_hysteresis_prevents_chatter);

    return UNITY_END();
}
```

This runner structure is exactly the intended Unity flow. [\[docs.platformio.org\]](https://docs.platformio.org/en/stable/advanced/unit-testing/frameworks/unity.html), [\[throwtheswitch.org\]](https://www.throwtheswitch.org/unity)

***

## 8) CMake Presets: Debug / Release / Unit-Testing

CMake presets are the recommended way to share common configure/build/test settings (`CMakePresets.json` at project root).
They were introduced in CMake 3.19+ and are supported by VS Code CMake Tools. [\[cmake.org\]](https://cmake.org/cmake/help/latest/manual/cmake-presets.7.html), [\[github.com\]](https://github.com/microsoft/vscode-cmake-tools/blob/main/docs/cmake-presets.md)

**`CMakePresets.json`**

```json
{
  "version": 6,
  "cmakeMinimumRequired": { "major": 3, "minor": 20, "patch": 0 },

  "configurePresets": [
    {
      "name": "stm32-debug",
      "displayName": "STM32 Debug (Firmware)",
      "generator": "Ninja",
      "binaryDir": "${sourceDir}/build/stm32-debug",
      "cacheVariables": {
        "CMAKE_BUILD_TYPE": "Debug",
        "CMAKE_TOOLCHAIN_FILE": "${sourceDir}/cmake/toolchain-arm-none-eabi.cmake",
        "BUILD_FIRMWARE": "ON",
        "UNIT_TESTING": "OFF"
      }
    },
    {
      "name": "stm32-release",
      "displayName": "STM32 Release (Firmware)",
      "generator": "Ninja",
      "binaryDir": "${sourceDir}/build/stm32-release",
      "cacheVariables": {
        "CMAKE_BUILD_TYPE": "Release",
        "CMAKE_TOOLCHAIN_FILE": "${sourceDir}/cmake/toolchain-arm-none-eabi.cmake",
        "BUILD_FIRMWARE": "ON",
        "UNIT_TESTING": "OFF"
      }
    },
    {
      "name": "unit-tests",
      "displayName": "Unit Tests (Host)",
      "generator": "Ninja",
      "binaryDir": "${sourceDir}/build/unit-tests",
      "cacheVariables": {
        "CMAKE_BUILD_TYPE": "Debug",
        "BUILD_FIRMWARE": "OFF",
        "UNIT_TESTING": "ON"
      }
    }
  ],

  "buildPresets": [
    { "name": "build-stm32-debug",   "configurePreset": "stm32-debug" },
    { "name": "build-stm32-release", "configurePreset": "stm32-release" },
    { "name": "build-unit-tests",    "configurePreset": "unit-tests" }
  ],

  "testPresets": [
    {
      "name": "run-unit-tests",
      "configurePreset": "unit-tests",
      "output": { "outputOnFailure": true }
    }
  ]
}
```

**Why this works well:**

*   STM32 presets use the **toolchain file** (cross-compile). Cross-compiling is normally configured via toolchain files in CMake. [\[cmake.org\]](https://cmake.org/cmake/help/book/mastering-cmake/chapter/Cross%20Compiling%20With%20CMake.html), [\[jchisholm2....github.io\]](https://jchisholm204.github.io/posts/stm32_cmake/)
*   Unit tests preset builds on host, so you can run tests locally via `ctest`.

***

## 9) How to build + run

### Command line

```bash
# Firmware Debug
cmake --preset stm32-debug
cmake --build --preset build-stm32-debug

# Firmware Release
cmake --preset stm32-release
cmake --build --preset build-stm32-release

# Unit tests
cmake --preset unit-tests
cmake --build --preset build-unit-tests
ctest --preset run-unit-tests
```

CTest is the standard runner CMake integrates with (`include(CTest)` / `enable_testing()`), and Unity integrates naturally by producing a test executable. [\[throwtheswitch.org\]](https://www.throwtheswitch.org/build/cmake), [\[cmake.org\]](https://cmake.org/cmake/help/latest/module/FetchContent.html)

### VS Code workflow (CMake Tools)

1.  Install **CMake Tools** extension.
2.  `Ctrl+Shift+P` → **CMake: Select Configure Preset** → pick:
    *   `stm32-debug`, `stm32-release`, or `unit-tests`
3.  **CMake: Build**
4.  For unit tests: **CMake: Run Tests** (CTest) or use the Testing UI.

VS Code CMake Tools supports CMake presets and can drive configure/build/test from them. [\[github.com\]](https://github.com/microsoft/vscode-cmake-tools/blob/main/docs/cmake-presets.md), [\[cmake.org\]](https://cmake.org/cmake/help/latest/manual/cmake-presets.7.html)

***

## 10) Keeping unit tests “business only” (practical checklist)

✅ **Do**:

*   Put unit-testable code in `src/business/` and compile into `business_logic` library
*   Keep headers HAL-free
*   Abstract hardware/time via small interfaces
*   Provide stubs/fakes in `tests/stubs/`

❌ **Avoid**:

*   Including `stm32xxxx_hal.h` in business modules
*   Calling `HAL_*` functions directly from business logic
*   Testing drivers/peripheral init in host unit tests (that’s better as integration tests / HIL)

Unity’s design goal is to let you test C code without “littering” production code with special requirements, and it’s commonly used in embedded environments. [\[throwtheswitch.org\]](https://www.throwtheswitch.org/unity), [\[github.com\]](https://github.com/ThrowTheSwitch/Unity)

***

## 11) Common STM32-specific integration notes

*   If you use **STM32CubeMX CMake output**, keep Cube-generated code in firmware-only targets and don’t pull it into unit test builds. ST’s community guidance emphasizes customizing at the top level and being mindful of generated interface libraries. [\[community.st.com\]](https://community.st.com/t5/stm32cubemx-mcus/how-to-customize-your-stm32cubemx-generated-cmake-project/td-p/857290)
*   For cross-compiling, the “try compile” behavior often trips people up; setting `CMAKE_TRY_COMPILE_TARGET_TYPE STATIC_LIBRARY` is a known fix for embedded toolchains. [\[jchisholm2....github.io\]](https://jchisholm204.github.io/posts/stm32_cmake/), [\[cmake.org\]](https://cmake.org/cmake/help/book/mastering-cmake/chapter/Cross%20Compiling%20With%20CMake.html)

***

# Want me to tailor this to your exact STM32 setup?

If you tell me:

1.  STM32 family/part (e.g., **F4**, **L4**, **H7**),
2.  whether you’re using **CubeMX-generated CMake** or a custom layout, and
3.  your linker script path (`.ld`),

…I can adjust the `stm32_apply_flags()` and firmware target (startup/system files, link options, memory layout) to match your project exactly—while keeping unit tests strictly business-logic-only.

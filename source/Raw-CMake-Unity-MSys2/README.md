# Unity + CMake + MSYS2 (UCRT64) Sample

## Build
```bash
cmake -S . -B build -G Ninja
cmake --build build
ctest --test-dir build --output-on-failure
```

## Unity dependency
Add Unity as a submodule:
```bash
git submodule add https://github.com/ThrowTheSwitch/Unity.git external/unity/Unity
```

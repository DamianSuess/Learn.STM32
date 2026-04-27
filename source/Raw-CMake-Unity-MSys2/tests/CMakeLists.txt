add_executable(test_calculator
    test_calculator.c
)

target_link_libraries(test_calculator
    unity
    calculator
)

add_test(NAME calculator_tests COMMAND test_calculator)

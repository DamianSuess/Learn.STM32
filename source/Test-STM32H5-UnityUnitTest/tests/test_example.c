// Copyright 2026 Suess Labs, Inc. All rights reserved.

#if ENABLE_UNIT_TESTS
#include "../libs/Unity/src/unity.h"
#include "../libs/Unity/src/unity_internals.h"
// #include "test_example.h"

#define UNITY_INCLUDE_PRINT_FORMATTED

void test_example(void);

int main()
{
  UNITY_BEGIN();
  RUN_TEST(test_example);

  ////// protect against infinite loops or long-running tests
  ////if (TEST_PROTECT())
  ////{
  ////  // Additional test cases can be added here
  ////  test_example();
  ////}

  return UNITY_END();
}

void setUp(void)
{
  // Setup before EACH TEST
}

void tearDown()
{
  // Cleanup after each test
}

void test_example(void)
{
  // Example test case
  TEST_ASSERT_EQUAL(1, 1);

  // TEST_PRINTF("This is an example test case.\n");
}

#endif // ENABLE_UNIT_TESTS

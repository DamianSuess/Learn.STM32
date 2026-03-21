//// #include "test.h"
//// #include "../libs/Unity/src/unity.h"
#include "../libs/Unity-min/unity.h"
#include "../src/BusinessLogic/sample.h"

// NOTE: Must place '(void)' after function name to avoid 'conflicting types' error.
int main(void);
void test_addition(void);
void test_printHello(void);
void test_nothing(void);

int main(void)
{
  UNITY_BEGIN();

  RUN_TEST(test_addition);
  RUN_TEST(test_printHello);
  RUN_TEST(test_nothing);

  return UNITY_END();
}

void setUp(void)
{
}

void tearDown(void)
{
}

void test_addition(void)
{
  int a = 1, b = 2;
  int c;

  c = add(a, b);
  TEST_ASSERT_EQUAL(3, c);
}

void test_printHello(void)
{
  printHello();
  TEST_ASSERT_EQUAL(1, 1);
}

void test_nothing(void)
{
  TEST_IGNORE();
}

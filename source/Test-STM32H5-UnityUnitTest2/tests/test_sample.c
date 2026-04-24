//// #include "../Core/Inc/sample.h"
#include "../Core/Src/business/sample.h"
#include "../libs/Unity/src/unity.h"

void test_add(void);

int main(void)
{
  UNITY_BEGIN();

  RUN_TEST(test_add);

  return UNITY_END();
}

void setUp(void)
{
}

void tearDown(void)
{
}

void test_add(void)
{
  int result = add(2, 3);
  TEST_ASSERT_EQUAL(5, result);
}

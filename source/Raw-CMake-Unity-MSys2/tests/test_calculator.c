#include "unity.h"
#include "calculator.h"

void setUp(void) {}
void tearDown(void) {}

void test_add(void)
{
    TEST_ASSERT_EQUAL_INT(5, add(2, 3));
}

void test_sub(void)
{
    TEST_ASSERT_EQUAL_INT(1, sub(3, 2));
}

int main(void)
{
    UNITY_BEGIN();
    RUN_TEST(test_add);
    RUN_TEST(test_sub);
    return UNITY_END();
}

// Includes are to be alphabetical and groupd
#include "test1.h"
#include "test2.h"
#include "zzz1.h"

#include <algorithm>
#include <cstdlib>
#include <ctime>
#include <functional>
#include <iostream>
#include <iterator>
#include <zz2.h>

#define BIT_MASK 0xDEADBEAF

#define MULTILINE_DEF(a, b) \
  if ((a) > 2) \
  { \
    auto temp = (b) / 2; \
    (b) += 10; \
    someFunctionCall((a), (b)); \
  }

namespace LevelOneNamespace
{
  namespace LevelTwoNamespace
  {

    // Note no space after template type
    template <typename T, int size> bool is_sorted(T (&array)[size])
    {
      return std::adjacent_find(array, array + size, std::greater<T>()) == array + size;
    }

    // Rules:
    //    ColumnLimit: 120
    //  - ContuationIndentWidth: 2
    //    IndentWidth: 2
    uint8_t lineGroup = LINE_PAST_120COL_MARK | SOME_CONSTANT2 | SOME_CONSTANT3 | SOME_CONDDDDDDDDDDDDDSTANT4 |
                        SOME_CONSTANT5 | SOME_CONSanntesTANT6;

    uint8_t lineWraps =
      LINE_SHY_120COL_MARK | SOME_CONSTANT2 | SOME_CONSTANT3 | SOME_CONDDDDDDDDDDddddDDDSTANT4 | SOME_CONSTANT5;

    std::vector<uint32_t> returnVector(uint32_t *LongNameForParameter1, double *LongNameForParameter2,
                                       const float &LongNameForParameter3,
                                       const std::map<std::string, int32_t> &LongNameForParameter4)
    {

      // TODO: This is a long comment that allows you to understand how long comments will be trimmed. Here should be
      // deep thought but it's just not right time for this

      for (auto &i : LongNameForParameter4)
      {
        auto b = someFunctionCall(static_cast<int16_t>(*LongNameForParameter2),
                                  reinterpret_cast<float *>(LongNameForParameter2));
        i.second++;
      }

      do
      {
        if (a)
          a--;
        else
        {
          a++;
        }
      } while (false);

      switch (a)
      {
        case 0:
          a = a;
          break;
        case 1:
        {
          a = a;
          break;
        }

          return {};
      }

    } // namespace LevelTwoNamespace
  }   // namespace LevelTwoNamespace

  int main()
  {
    std::srand(std::time(0));

    int list[] = {1, 2, 3, 4, 5, 6, 7, 8, 9};

    do
    {
      std::random_shuffle(list, list + 9);
    } while (is_sorted(list));

    int score = 0;

    do
    {
      std::cout << "Current list: ";
      std::copy(list, list + 9, std::ostream_iterator<int>(std::cout, " "));

      int rev;
      while (true)
      {
        std::cout << "\nDigits to reverse? ";
        std::cin >> rev;
        if (rev > 1 && rev < 10)
          break;
        std::cout << "Please enter a value between 2 and 9.";
      }

      ++score;
      std::reverse(list, list + rev);
    } while (!is_sorted(list));

    std::cout << "Congratulations, you sorted the list.\n"
              << "You needed " << score << " reversals." << std::endl;
    return 0;
  }

using System.Collections;
using System.Collections.Generic;

namespace CorrecaoFp.Business.Tests.Utils
{
    public class CalculatorTest
    {
        public static IEnumerable<object[]> ParametersToSum()
        {
            return new List<object[]>
      {
          new object[] {10, 20, 30}, // soma de 10 + 20 = 30,
          new object[] {12, 2, 14} // soma de 12 + 12 = 14
      };
        }
    }
}

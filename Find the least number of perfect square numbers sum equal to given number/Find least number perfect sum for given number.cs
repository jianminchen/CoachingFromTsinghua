using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindLeastNumber
{
    /*
     * May 16, 2018
    Given a number "n", find the least number of perfect square numbers sum needed to get "n" 
    Example: 
    n = 12, return 3 (4 + 4 + 4) = (2^2 + 2^2 + 2^2) NOT (3^2 + 1 + 1 + 1) 
    n =  6, return 3 (4 + 1 + 1) = (2^2 + 1^2 + 1^2)
    * 
    */
    class Program
    {
        static void Main(string[] args)
        {
            RunTestcase();
        }

        public static void RunTestcase()
        {
            var result = GetLeastNumberPerfectSquareSumForGivenNumber(12); 
        }

        /// <summary>
        /// use dynamic programming algorithm
        /// </summary>
        /// <param name="givenSum"></param>
        /// <returns></returns>
        public static int GetLeastNumberPerfectSquareSumForGivenNumber(int givenSum) // 12
        {
            if (givenSum <= 0) // false
            {
                return 0;
            }

            // 1  2   3
            // 1  4   9   perfect square
            var dp = new int[givenSum + 1]; 

            dp[0] = 0;             

            for (int index = 1; index <= givenSum; index++)
            {
                dp[index] = 1 + dp[index - 1];

                for (int number = 1; number <= Math.Sqrt(givenSum); number++)
                {
                    var diff = index - number * number;

                    if (diff < 0)  // should exclude = 
                    {
                        break;
                    }

                    dp[index] = Math.Min(dp[index], 1 + dp[diff]);                     
                }
            }

            return dp[givenSum];
        }
    }
}

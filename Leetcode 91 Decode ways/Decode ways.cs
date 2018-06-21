using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leetcode91_DecodeWays_dynamicProgramming
{
    /// <summary>
    /// Leetcode 91: Decode ways
    /// https://leetcode.com/problems/decode-ways/description/
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            // RunTestcase1();
            // RunTestcase2();
            RunTestcase3();
        }

        public static void RunTestcase1()
        {
            var result = NumDecodings("12");
            Debug.Assert(result == 2);
        }

        public static void RunTestcase2()
        {
            var result = NumDecodings("226");
            Debug.Assert(result == 3);
        }

        /// <summary>
        /// 
        /// </summary>
        public static void RunTestcase3()
        {
            var result = NumDecodings("102");
            Debug.Assert(result == 1);
        }

        /// <summary>
        /// work on May 29, 2018 mock interview 8:45 AM - 10:00 AM
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns></returns>
        public static int NumDecodings(string numbers)  // 226 
        {
            if (numbers == null || numbers.Length == 0)  // false 
            {
                return 0;
            }

            var length = numbers.Length;
            var dp = new int[length]; // 3 

            for (int index = 0; index < length; index++)  // index = 1
            {
                var current = numbers[index]; // 2
                var isZero = current == '0'; // '2'

                // base case 
                if (index == 0)
                {
                    dp[0] = isZero ? 0 : 1;    // 1 

                    if (isZero)
                    {
                        return 0;
                    }
                }

                if (index > 0 && !isZero) // true
                {
                    dp[index] += dp[index - 1]; // + dp[0] = 1
                }

                if (index >= 1 && numbers[index - 1] != '0' && // bug found: should be '0', not 0
                     toNumber(current, numbers[index - 1]) <= 26 &&
                     toNumber(current, numbers[index - 1]) > 0)
                {
                    if (index == 1)
                    {
                        dp[index] += 1;  // 22 ->  2 + 2
                    }

                    if (index >= 2)
                    {
                        dp[index] += dp[index - 2];
                    }
                }
            }

            return dp[length - 1];
        }

        private static int toNumber(char current, char left)
        {
            var number = left - '0';
            number *= 10;
            number += current - '0';

            return number;
        }
    }
}
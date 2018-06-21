using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainPermutationOfSubstring
{
    /*
    Given two strings s1 and s2, write a function to return true if s2 contains 
    the permutation of s1. In other words, one of the first string's permutations 
    is the substring of the second string.
    Example 1:
    Input:s1 = "ab" s2 = "eidbaooo"
    Output:True
    Explanation: s2 contains one permutation of s1 ("ba").
    Example 2:
    Input:s1= "ab" s2 = "eidboaoo"
    Output: False
    Note:
    The input strings only contain lower case letters.
    The length of both given strings is in range [1, 10,000].
    
    Julia's analysis in the mock interview on May 22, 2018 8:00 AM:
    Keywords:
    Two string s1 s2, 
    Ask: return true if s2 contains the permutation of s1, 
    requirement: contain a substring 
    Variation s1.Length! 
     
    Example: 
    String 1: had duplicate char -> 26, counting sort a :1, b: 1
    Slide window minimum -> eidboaoo
     
    -> left, right, index = 0, e not in keys{a,b}
    
    Julia wrote the C# code on May 23, 2018. 
    */
    class Program
    {
        public const int SIZE = 26;

        static void Main(string[] args)
        {
            RunTestcase1();
        }

        public static void RunTestcase1()
        {
            var result = FindSubstringContainS1Permutation("aab", "ab");
            Debug.Assert(result);

            var result2 = FindSubstringContainS1Permutation("acb", "ab");
            Debug.Assert(!result2);

            var result3 = FindSubstringContainS1Permutation("acba", "ab");
            Debug.Assert(result3);
        }

        /// <summary>
        /// Time complexity: O(N), N is the length of search string. 
        /// </summary>
        /// <param name="search"></param>
        /// <param name="keys"></param>
        /// <returns></returns>
        public static bool FindSubstringContainS1Permutation(string search, string keys)
        {
            if (search == null || keys == null)
            {
                return false;
            }

            var keysCount = addToKeysCount(keys);
            var totalKeysNeed = keysCount.Sum();

            var foundCount = new int[SIZE];
            var hashset = new HashSet<int>(keysCount);
            var left = 0;
            var searchLength = search.Length;

            var foundKeyCount = 0;

            for (int index = 0; index < searchLength; index++)
            {
                var lookupIndex = search[index] - 'a';
                if (!hashset.Contains(lookupIndex))
                {
                    left = index + 1;
                    foundKeyCount = 0;
                    Array.Clear(foundCount, 0, SIZE);
                    continue;
                }

                if (foundCount[lookupIndex] < keysCount[lookupIndex])
                {
                    foundKeyCount++;
                    foundCount[lookupIndex]++;
                }
                else
                {
                    while (foundCount[lookupIndex] == keysCount[lookupIndex])
                    {
                        var leftCharIndex = search[left] - 'a';

                        if (leftCharIndex == lookupIndex)
                        {
                            left++;
                            break;
                        }
                        else
                        {
                            foundCount[leftCharIndex]--;
                            left++;
                        }
                    }
                }

                if (foundKeyCount == totalKeysNeed)
                {
                    return true;
                }
            }

            return false;
        }

        private static int[] addToKeysCount(string keys)
        {
            var counted = new int[26];

            foreach (var item in keys)
            {
                counted[item - 'a']++;
            }

            return counted;
        }
    }
}
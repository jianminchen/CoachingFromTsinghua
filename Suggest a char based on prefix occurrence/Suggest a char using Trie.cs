using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuggestACharUsingTrie
{
    /*
     Suppose you are supplied with a file containing a list of words like ABC,ACD, ABC 
     ( say each word in new line ). now you have to suggest algorithm for this problem - 
     When a user type some character, we have to suggest him next character and basis of 
     suggestion is that the character you are going to suggest should have maximum occurrence at that position with the given prefix. 
    For example , Let's say words are 
    ABC 
    ACD
    ABC
     
    Now if user types 'A' we have to suggest him 'B' as next character with the prefix ‘A’, ‘B’ 
    has 2 times, and ‘C’ has 1. similarly if he types 'AB' then we need to suggest him third 
    character as 'C' as in third index since C has 2 times
     * 
     * Julia's analysis:
     * keywords:
     * Constraint: the given prefix -> check its occurrence 
     * ask: maximum occurrence char -> if there are more than one, check first occurrence with smaller index
            A                                B  ...  Z
    /                  \
    B                    C
   /  \                   \                    
  C   A                    D
     
[ABC count = 2, leaf = true]      [ACD 1]
     
    for example: 
    ABC
    ABC
    ABC (ABC prefix count = 3) C 
    ABA (ABA prefix count = 1)
    Work on three string and put into trie, add count variable 
    ABC 
    ACD
    ABC  
     May 24, 2018
     * 
     * 
     */
    class Program
    {
        public const int SIZE = 256;

        internal class Node
        {
            public char Item { get; set; }
            public int Number { get; set; }

            public Node[] Children; // 26, 256

            public Node(char c)
            {
                Item = c;
                Children = new Node[SIZE];
            }
        };

        static void Main(string[] args)
        {
            RunTestcase();
        }

        public static void RunTestcase()
        {
            var trie = addToTrie(new string[] { "ABC", "ACD", "ABC" });

            //var suggested = PrefixSuggest("A", trie);
            var suggestedAB = PrefixSuggest("AB", trie);
        }

        private static Node addToTrie(string[] words)
        {
            if (words == null)
            {
                return null;
            }

            // dummy node
            var dummyNode = new Node(' ');

            foreach (var word in words)
            {
                var iterate = dummyNode;

                for (int index = 0; index < word.Length; index++)
                {
                    var item = word[index];
                    var currentIndex = (int)item;

                    if (iterate.Children[currentIndex] == null)
                    {
                        iterate.Children[currentIndex] = new Node(item);
                        iterate.Children[currentIndex].Number = 1;
                    }
                    else
                    {
                        iterate.Children[currentIndex].Number++;
                    }

                    iterate = iterate.Children[currentIndex];
                }
            }

            return dummyNode;
        }

        /// <summary>
        /// the coach asked me to test the code using the test case:
        /// ABC 
        /// ACD
        /// ABC
        /// test case: given prefix “A”, find ‘B’
        /// add one more constraint: For same number occurrence, the order is given by 
        /// the smallest first occurrence index
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="trie"></param>
        /// <returns></returns>
        private static char PrefixSuggest(string prefix, Node trie)  // A
        {
            if (prefix == null || trie == null)
            {
                return ' ';
            }

            var iterate = trie;  // dummy node
            var length = prefix.Length;  // 1
            var index = 0;

            while (iterate != null && index < length) // find the prefix first, and then look up its children 
            // and find its maximum number
            {
                var visit = prefix[index];

                iterate = iterate.Children[(int)visit];

                if (index == length - 1)
                {
                    // find maximum value of children 
                    var maxIndex = 0;
                    var maxNo = 0;
                    for (int i = 0; i < SIZE; i++)
                    {
                        if (iterate.Children[i] != null && iterate.Children[i].Number > maxNo)
                        {
                            maxIndex = i;
                            maxNo = iterate.Children[i].Number;
                        }
                    }

                    return (char)maxIndex;
                }

                index++;
            }

            return ' ';    // default no found -> 
        }
    }
}
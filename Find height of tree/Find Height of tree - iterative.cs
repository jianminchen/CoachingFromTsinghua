using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindHeightOfTreeIterativeSolution
{
    /// <summary>
    /// May 18, 2018
    /// The code practice is based on the practice of a mock interview. Julia wrote a recursive solution 
    /// based on the coach's advice. 
    /// The mock interview transcript is here:
    /// https://gist.github.com/jianminchen/95729605a21ffaf070a546d746a9c726
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            RunTestcase();
        }

        public static void RunTestcase()
        {
            var heightOfTree = FindHeightOfTree(new int[] { 3, 3, 3, -1, 2 });
            Debug.Assert(heightOfTree == 3);
        }


        /*        
        A tree, (NOT NECESSARILY BINARY), has nodes numbered 0 to N-1. An array has indices 
        ranging from 0 to N-1. The indices denote the node ids and values denote the id of 
        parents. A value of -1 at some index k denotes that node with id k is the root. For ex:
        3 3 3 -1 2
        0 1 2 3 4
         * 
        In the above, nodes with ids 0, 1 & 2 have 3 as parent. 3 is the root as its parent = -1 
        and 2 is the parent of node id 4. 
        Given such an array, find the height of the tree.
        Julia's analysis in the mock interview:
        keywords:
        a tree - children maybe > 2
        0 - N - 1, total N nodes 
        customize array[i] - i - node id, array[i] is node id i's parent 
        -1 is special: root node
        ask: given such an array, find the hight of the tree
        3 3 3 -1 2 
        0 1 2 3  4
        reconstruct:
         parent <- child
          3 <-  0
          3 <-  1
          3 <-  2
          -1 <- 3  root
          2 <- 4
          above height of tree is 3
          0 -> 3 -> -1 -> 0 -> height[0] = 2, height[3] = 1, 
          1 -> 3 -> -1  -> 1 + height[3] = 2
          2 -> 3 -> - 1 -> 1 + height[3] = 2
          3 -> -1
          4 -> 2 -> 3-> -1  
          line 36 -> 3   brute force solution O(n * height of tree)
          line 34, 2 -> 3
          -1 0 1 2 3
          0  1 2 3 4
          0 - > -1 
          1 -> 0 -> -1 
          2 -> 1 -> look up height
          3 -> 2 -> look up height
          4->3 -> look up height
       */
        /// <summary>
        /// understand the iterative solution, how to write very efficient one. 
        /// So many bugs in my writing in mock interview. 
        /// I like to work on more than one version to improve them.
        /// Make sure that time complexity is O(N), N is size of the array. 
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns></returns>
        public static int FindHeightOfTree(int[] numbers)  // indexWithParentId
        {
            if (numbers == null)
            {
                return 0;
            }

            var length = numbers.Length;

            var heightIds = new int[length];

            for (int index = 0; index < length; index++)
            {
                if (heightIds[index] > 0)
                {
                    continue;
                }

                // find the path for id = index, save height along the path for each id
                // 3 3 3 -1 2 
                // 0 1 2 3  4
                var iterate = index; // 0
                var pathLength = 0;
                var pathList = new List<int>(); // think about removing the list using iterative solution

                while (iterate != -1)
                {
                    if (heightIds[iterate] > 0)
                    {
                        pathLength += heightIds[iterate];
                        break;
                    }

                    pathList.Add(iterate);
                    pathLength++;

                    // next iteration
                    // base case - added after mock interview
                    if (numbers[iterate] == -1)
                    {
                        heightIds[iterate] = 1;
                    }

                    iterate = numbers[iterate];
                }

                heightIds[index] = pathLength;

                // must have! otherwise time complexity will go up from O(N) to O(N^2). 
                for (int i = 0; i < pathList.Count; i++)
                {
                    if (heightIds[pathList[i]] > 0)
                    {
                        break;
                    }

                    heightIds[pathList[i]] = pathLength - i;
                }
            }

            return heightIds.Max();
        }
    }
}
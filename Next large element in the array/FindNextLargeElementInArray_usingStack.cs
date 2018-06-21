using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrayFindRightHandSideNextLargerElement
{
    /*
    Given an Array, replace each element in the Array with its Next Element(To its RHS) which is Larger than it. 
    If no such element exists, then no need to replace. 
    Ex: 
    i/p: {2,12,8,6,5,1,2,10,3,2} 
    o/p: {12,12,10,10,10,2,10,10,3,2}
    mock interview coding blog: 
     https://gist.github.com/jianminchen/b3062b12a24b23173fbb009803e442b0
    */
    class Program
    {
        static void Main(string[] args)
        {
            RunTestcases();
        }

        public static void RunTestcases()
        {
            var nextLarge = FindNextLargerElementInArray(new int[] { 1, 2, 3 });  // the result: [2, 3, 3]
            var nextLarge2 = FindNextLargerElementInArray(new int[] { 3, 2, 1 });  // the result: [3, 2, 1]
            var nextLarge3 = FindNextLargerElementInArray(new int[] { 2, 1, 3 });  // the result: [3, 3, 3]
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns></returns>
        public static int[] FindNextLargerElementInArray(int[] numbers)
        {
            if (numbers == null || numbers.Length == 0)
            {
                return new int[0];
            }

            var length = numbers.Length;

            var nextLargeElements = new int[length];

            var stack = new Stack<int>(); // push index to the stack

            stack.Push(0);

            // iterate the array first 
            for (int index = 1; index < length; index++)
            {
                var visit = numbers[index];

                while (stack.Count > 0 && numbers[stack.Peek()] < visit)
                {
                    nextLargeElements[stack.Peek()] = visit;
                    stack.Pop();       // we may continuously pop elements in the stack                                      
                }

                stack.Push(index); // push index into the stack, keep it descending order                
            }

            // edge case: [3, 2, 1], stack has [3, 2, 1]
            while (stack.Count > 0)
            {
                var topIndex = stack.Peek();
                stack.Pop();
                nextLargeElements[topIndex] = numbers[topIndex];
            }

            return nextLargeElements;
        }
    }
}
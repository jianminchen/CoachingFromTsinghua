using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindGroupWithMaximumNumber_UnionFind
{
    /// <summary>
    /// given a graph with edges, find maximum group with largest number 
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            RunTestcase();
        }

        public static void RunTestcase()
        {
            var pairs = new List<int[]>();
            pairs.Add(new int[] { 0, 1 });
            pairs.Add(new int[] { 1, 2 });

            pairs.Add(new int[] { 3, 4 });
            pairs.Add(new int[] { 5, 6 });
            pairs.Add(new int[] { 4, 5 });

            // two groups, {0, 1, 2}, another group {3, 4, 5, 6, }
            // make sure that id starts from 0 instead of 1. 
            var maximum = GetMaximumGroup(pairs, 7);
            Debug.Assert(maximum == 4);
        }

        public static int GetMaximumGroup(IList<int[]> pairs, int n)
        {
            if (n <= 0 || pairs == null || pairs.Count == 0)
            {
                return -1;
            }

            var unionFind = new QuickUnion(n);

            foreach (var item in pairs)
            {
                unionFind.Union(item[0], item[1]);
            }

            var groupCount = new Dictionary<int, int>();

            // quick find and also path compression
            for (int i = 0; i < n; i++)
            {
                unionFind.QuickFindAndPathCompression(i);
            }

            // Find maximum group number
            for (int i = 0; i < n; i++)
            {
                var rootId = unionFind.QuickFind(i);
                if (!groupCount.ContainsKey(rootId))
                {
                    groupCount.Add(rootId, 1);
                }
                else
                {
                    groupCount[rootId]++;
                }
            }

            return groupCount.Values.Max();
        }

        /// <summary>
        /// May 28, 2018
        /// 
        /// Julia likes to write her own union find algorithm
        /// The coach spent almost 100 minutes in the mock interview to show her 
        /// the solution, and also he wrote two functions called 
        /// QuickFind() and Union()
        /// 
        /// source code reference used:
        /// https://gist.github.com/jianminchen/cb889def70be4563581113daa8a8fb2a
        /// </summary>
        internal class QuickUnion
        {
            private int[] parent { get; set; }
            private int count { get; set; }

            public QuickUnion(int number)
            {
                if (number <= 0)
                {
                    return;
                }

                count = number;
                parent = new int[number];

                for (int i = 0; i < number; i++)
                {
                    parent[i] = i;
                }
            }

            public int GetCount()
            {
                return count;
            }

            /// <summary>
            /// Find group id given the node value
            /// </summary>
            /// <returns></returns>
            public int QuickFind(int search)
            {
                if (search < 0)
                {
                    return -1;
                }

                if (search == parent[search])
                {
                    return search;
                }

                return QuickFind(parent[search]);
            }

            /// <summary>
            /// Reset all parent node's to its original ancestor
            /// path compression - all node's parent will be reset to its ancestor
            /// </summary>
            /// <returns></returns>
            public int QuickFindAndPathCompression(int search)
            {
                if (search == parent[search])
                {
                    return search;
                }

                int root = QuickFindAndPathCompression(parent[search]);

                parent[search] = root;

                return root;
            }

            public void Union(int p, int q)
            {
                int pRoot = QuickFind(p);
                int qRoot = QuickFind(q);

                if (pRoot == qRoot)
                {
                    return;
                }

                // set one tree to another tree's subtree
                parent[pRoot] = qRoot;

                // update count of groups
                count--;
            }

            public bool Connected(int p, int q)
            {
                return QuickFind(p) == QuickFind(q);
            }
        }
    }
}
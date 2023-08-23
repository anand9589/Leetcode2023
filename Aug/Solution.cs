using Common;
using System.Globalization;
using System.Text;

namespace Aug
{
    public class Solution
    {
        #region Day 1 77. Combinations
        public IList<IList<int>> Combine(int n, int k)
        {
            if (n == k)
            {
                IList<int> lst = Enumerable.Range(1, n).ToList();
                return new List<IList<int>>() { lst };
            }
            IList<IList<int>> result = new List<IList<int>>();

            for (int i = 1; i <= n - k + 1; i++)
            {
                IList<int> list = new List<int>();
                list.Add(i);
                combineList(result, list, i + 1, n, k - 1);

            }

            return result;
        }

        private void combineList(IList<IList<int>> result, IList<int> lst, int i, int limit, int k)
        {
            if (k < 0) return;
            if (k == 0)
            {
                result.Add(new List<int>(lst));
                return;
            }

            while (i <= limit)
            {
                lst.Add(i);
                k--;
                combineList(result, lst, i + 1, limit, k);
                lst.Remove(i);
                i++;
                k++;
            }
        }
        #endregion

        #region Day 2 46. Permutations
        public IList<IList<int>> Permute(int[] nums)
        {
            IList<IList<int>> result = new List<IList<int>>();

            permute_helper(result, nums, 0);

            return result;
        }

        private void permute_helper(IList<IList<int>> result, int[] nums, int index)
        {
            if (index == nums.Length)
            {
                result.Add(new List<int>(nums));
                return;
            }

            for (int i = index; i < nums.Length; i++)
            {
                swap(nums, index, i);
                permute_helper(result, nums, index + 1);
                swap(nums, index, i);
            }
        }

        private void swap(int[] nums, int i, int j)
        {
            if (i < 0 || j < 0 || i >= nums.Length || j >= nums.Length) return;

            int temp = nums[i];
            nums[i] = nums[j];
            nums[j] = temp;
        }


        #endregion

        #region Day 3
        Dictionary<int, char[]> map_17;
        public IList<string> LetterCombinations(string digits)
        {
            IList<string> result = new List<string>();
            if (digits.Length > 0)
            {
                map_17 = new Dictionary<int, char[]>
                {
                    { 2, new char[] { 'a', 'b', 'c' } },
                    { 3, new char[] { 'd', 'e', 'f' } },
                    { 4, new char[] { 'g', 'h', 'i' } },
                    { 5, new char[] { 'j', 'k', 'l' } },
                    { 6, new char[] { 'm', 'n', 'o' } },
                    { 7, new char[] { 'p', 'q', 'r', 's' } },
                    { 8, new char[] { 't', 'u', 'v' } },
                    { 9, new char[] { 'w', 'x', 'y', 'z' } }
                };

                dfs_17(result, digits, new StringBuilder(), 0);
            }
            return result;
        }

        private void dfs_17(IList<string> result, string digits, StringBuilder stringBuilder, int len)
        {
            if (len == digits.Length)
            {
                result.Add(stringBuilder.ToString());
                return;
            }

            foreach (char c in map_17[digits[len] - '0'])
            {
                stringBuilder.Append(c);
                dfs_17(result, digits, stringBuilder, len + 1);
                stringBuilder.Remove(stringBuilder.Length - 1, 1);
            }
        }
        #endregion

        #region Day 4     139. Word Break  
        int[] dp139;
        public bool WordBreak(string s, IList<string> wordDict)
        {
            dp139 = Enumerable.Repeat(-1, s.Length + 1).ToArray();


            return help_139(0, s, wordDict) == 1;
        }

        private int help_139(int v, string s, IList<string> wordDict)
        {
            if (v == s.Length) return 1;

            if (dp139[v] != -1) return dp139[v];

            string temp = string.Empty;

            for (int i = v; i < s.Length; i++)
            {
                temp += s[i];

                if (wordDict.Contains(temp))
                {
                    if (help_139(i + 1, s, wordDict) == 1) return dp139[v] = 1;
                }
            }
            return dp139[v] = 0;
        }

        public bool WordBreak_v3(string s, IList<string> wordDict)
        {
            if (s.Length == 0) return true;
            foreach (var word in wordDict)
            {
                if (s.StartsWith(word))
                {
                    if (WordBreak_v3(s.Substring(word.Length), wordDict))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool WordBreak_v2(string s, IList<string> wordDict)
        {
            string s1 = string.Empty;
            for (int i = 0; i < wordDict.Count; i++)
            {
                s1 = s;

                s1 = s1.Replace(wordDict[i], "");

                if (s1.Length == 0) return true;
                for (int j = 0; j < wordDict.Count; j++)
                {
                    if (i == j) continue;

                    s1 = s1.Replace(wordDict[j], "");
                    if (s1.Length == 0) return true;
                }
            }

            return false;
        }
        public bool WordBreak_v1(string s, IList<string> wordDict)
        {
            if (s.Length == 0) return true;
            int i = 0;

            while (i < s.Length)
            {
                string x = s.Substring(0, i + 1);

                if (wordDict.Contains(x))
                {
                    if (WordBreak_v1(s.Substring(i + 1), wordDict))
                    {
                        return true;
                    }
                }
                i++;
            }
            return false;
        }
        #endregion

        #region Day 5 95. Unique Binary Search Trees II


        public IList<TreeNode> GenerateTrees(int n)
        {
            return GenerateTrees(1, n);
        }

        private IList<TreeNode> GenerateTrees(int left, int right)
        {
            IList<TreeNode> trees = new List<TreeNode>();
            if (left >= right)
            {
                if (left == right)
                {
                    trees.Add(new TreeNode(left, null, null));
                }
                else
                {
                    trees.Add(null);
                }
                return trees;
            }
            for (int i = left; i <= right; i++)
            {
                var leftNodes = GenerateTrees(left, i - 1);
                var rightNodes = GenerateTrees(i + 1, right);

                foreach (var leftNode in leftNodes)
                {
                    foreach (var rightNode in rightNodes)
                    {
                        trees.Add(new TreeNode(i, leftNode, rightNode));
                    }
                }
            }

            return trees;
        }


        #endregion

        #region Day 9 2616. Minimize the Maximum Difference of Pairs
        public int MinimizeMax(int[] nums, int p)
        {
            Array.Sort(nums);
            PriorityQueue<(int, int), int> diff = new PriorityQueue<(int, int), int>();
            for (int i = 1; i < nums.Length; i++)
            {
                diff.Enqueue((i - 1, i), Math.Abs(nums[i - 1] - nums[i]));
            }
            HashSet<int> visited = new HashSet<int>();
            int index1 = 0;
            int index2 = 0;

            while (p > 0 && diff.Count > 0)
            {
                (int, int) e = diff.Dequeue();
                if (!visited.Contains(e.Item1) && !visited.Contains(e.Item2))
                {
                    index1 = e.Item1;
                    index2 = e.Item2;
                    visited.Add(index1);
                    visited.Add(index2);
                    p--;

                }
            }

            if (p == 0)
            {
                return Math.Abs(nums[index1] - nums[index2]);
            }

            return MinimizeMax(nums.Except(visited).ToArray(), p);
        }
        #endregion

        #region Day 10 81. Search in Rotated Sorted Array II
        public bool Search(int[] nums, int target)
        {
            int low = 0;
            int high = nums.Length - 1;

            while (low <= high)
            {
                if (nums[low] == target || nums[high] == target) return true;

                int mid = (low + high) / 2;

                if (nums[mid] == target) return true;

                if (nums[mid] > target)
                {
                }
                else
                {

                }
            }

            return false;
        }
        #endregion

        #region Day 13. 2369. Check if There is a Valid Partition For The Array
        public bool ValidPartition(int[] nums)
        {
            if (nums.Length == 2) return nums[0] == nums[1];

            return solveValidPartition(Enumerable.Repeat(-1, nums.Length).ToArray(), nums, 0);
        }

        private bool solveValidPartition(int[] dp, int[] nums, int index)
        {
            if (index == nums.Length) return true;

            if (dp[index] != -1) return dp[index] > 0;

            bool res = false;

            if (index + 1 < nums.Length && nums[index] == nums[index + 1])
            {
                res = solveValidPartition(dp, nums, index + 2);

                if (index + 2 < nums.Length && nums[index + 2] == nums[index])
                {
                    res = res || solveValidPartition(dp, nums, index + 3);
                }
            }

            if (index + 2 < nums.Length && nums[index + 1] - nums[index] == 1 && nums[index + 2] - nums[index + 1] == 1)
            {
                res = res || solveValidPartition(dp, nums, index + 3);
            }
            dp[index] = res ? 1 : 0;
            return res;
        }
        #endregion

        #region Day 14. 215. Kth Largest Element in an Array
        public int FindKthLargest(int[] nums, int k)
        {
            PriorityQueue<int, int> pq = new PriorityQueue<int, int>();
            int i = 0;
            while (k-- > 0)
            {
                pq.Enqueue(nums[i], nums[i++]);
            }

            for (; i < nums.Length; i++)
            {
                if (nums[i] > pq.Peek())
                {
                    pq.Dequeue();
                    pq.Enqueue(nums[i], nums[i]);
                }
            }

            return pq.Dequeue();
        }


        public int FindKthLargest_V1(int[] nums, int k)
        {
            int result = 0;

            Array.Sort(nums, (a, b) => { return b - a; });

            return nums[k - 1];
        }
        #endregion

        #region Day 16 239. Sliding Window Maximum
        public int[] MaxSlidingWindow(int[] nums, int k)
        {
            int[] result = new int[nums.Length - k + 1];

            int i = 0;
            LinkedList<int> l = new LinkedList<int>();
            int tempK = k;
            while (k-- > 0)
            {
                while (l.Count > 0 && nums[l.Last()] < nums[i])
                {
                    l.RemoveLast();
                }
                l.AddLast(i);
                i++;
            }

            int index = -1;
            while (++index < result.Length)
            {
                result[index] = nums[l.First()];
                if (l.First() == index)
                {
                    l.RemoveFirst();
                }

                while (i < nums.Length && l.Count > 0 && nums[l.Last()] < nums[i])
                {
                    l.RemoveLast();
                }
                l.AddLast(i);
                i++;
            }


            return result;
        }
        #endregion

        #region Day 17 542. 01 Matrix

        public int[][] UpdateMatrix(int[][] mat)
        {
            Queue<(int x, int y)> q = new Queue<(int x, int y)>();
            for (int i = 0; i < mat.Length; i++)
            {
                for (int j = 0; j < mat[i].Length; j++)
                {
                    if (mat[i][j] == 1)
                    {
                        mat[i][j] = -1;
                    }
                    else
                    {
                        q.Enqueue((i, j));
                    }
                }
            }

            while (q.Count > 0)
            {
                var cell = q.Dequeue();
                int cellValue = mat[cell.x][cell.y];

                if (cell.x - 1 >= 0 && mat[cell.x - 1][cell.y] == -1)
                {
                    q.Enqueue((cell.x - 1, cell.y));
                    mat[cell.x - 1][cell.y] = cellValue + 1;
                }

                if (cell.y - 1 >= 0 && mat[cell.x][cell.y - 1] == -1)
                {
                    q.Enqueue((cell.x, cell.y - 1));
                    mat[cell.x][cell.y - 1] = cellValue + 1;
                }

                if (cell.x + 1 >= 0 && mat[cell.x + 1][cell.y] == -1)
                {
                    q.Enqueue((cell.x + 1, cell.y));
                    mat[cell.x + 1][cell.y] = cellValue + 1;
                }

                if (cell.y + 1 >= 0 && mat[cell.x][cell.y + 1] == -1)
                {
                    q.Enqueue((cell.x, cell.y + 1));
                    mat[cell.x][cell.y + 1] = cellValue + 1;
                }
            }
            return mat;
        }

        //private int calculateCell(int[][] mat, int i, int j)
        //{
        //    int min = int.MaxValue;
        //    if (i - 1 >= 0 && !visited[i - 1, j])
        //    {
        //        if (mat[i - 1][j] == 0) return 1;
        //        if (!visited[i - 1, j])
        //        {
        //            visited[i - 1, j] = true;
        //            mat[i - 1][j] = calculateCell(mat, i - 1, j);
        //        }
        //        min = Math.Min(min, mat[i - 1][j]);
        //    }
        //    if (j - 1 >= 0)
        //    {
        //        if (mat[i][j - 1] == 0) return 1;
        //        if (!visited[i, j - 1])
        //        {
        //            visited[i, j - 1] = true;
        //            mat[i][j - 1] = calculateCell(mat, i, j - 1);
        //        }
        //        min = Math.Min(min, mat[i][j - 1]);
        //    }
        //    if (i + 1 < mat.Length)
        //    {
        //        if (mat[i + 1][j] == 0) return 1;
        //        if (!visited[i + 1, j])
        //        {
        //            mat[i + 1][j] = calculateCell(mat, i + 1, j);
        //        }
        //        min = Math.Min(min, mat[i + 1][j]);
        //    }
        //    if (j + 1 < mat[i].Length)
        //    {
        //        if (mat[i][j + 1] == 0) return 1;
        //        mat[i][j + 1] = calculateCell(mat, i, j + 1);
        //        min = Math.Min(min, mat[i][j + 1]);
        //    }
        //    return min + 1;
        //}
        #endregion

        #region Day 19 1489. Find Critical and Pseudo-Critical Edges in Minimum Spanning Tree

        internal class UnionFind
        {
            int[] parent;
            int cost;
            int setCount;
            public UnionFind(int n)
            {
                this.parent = Enumerable.Range(0, n).ToArray();
                this.cost = 0;
                this.setCount = n;
            }

            public void ProcessEdge(int[] arr)
            {
                int p1 = findParent(arr[0]);
                int p2 = findParent(arr[1]);

                if (p1 != p2)
                {
                    parent[p1] = p2;
                    setCount--;

                    cost += arr[2];
                }
            }

            private int findParent(int c)
            {
                if (parent[c] == c) return c;

                return findParent(parent[c]);

            }

            internal int GetCost()
            {
                return setCount > 1 ? int.MaxValue : cost;
            }
        }

        public IList<IList<int>> FindCriticalAndPseudoCriticalEdges(int n, int[][] edges)
        {
            IList<IList<int>> result = new List<IList<int>>();
            IList<int> critical = new List<int>();
            IList<int> pseudoCritical = new List<int>();

            result.Add(critical);
            result.Add(pseudoCritical);

            UnionFind uf = new UnionFind(n);

            Dictionary<int, int[]> edgeDictionary = new Dictionary<int, int[]>();

            for (int i = 0; i < edges.Length; i++)
            {
                edgeDictionary.Add(i, edges[i]);
            }

            edgeDictionary = edgeDictionary.OrderBy(x => x.Value[2]).ToDictionary(x => x.Key, x => x.Value);
            foreach (int key in edgeDictionary.Keys)
            {
                uf.ProcessEdge(edgeDictionary[key]);
            }

            int mst = uf.GetCost();

            for (int i = 0; i < edgeDictionary.Keys.Count; i++)
            {

                uf = new UnionFind(n);
                foreach (var key in edgeDictionary.Keys)
                {
                    if (key == i) continue;
                    uf.ProcessEdge(edgeDictionary[key]);
                }

                int excludeCost = uf.GetCost();
                if (excludeCost > mst)
                {
                    critical.Add(i);
                }
                else
                {
                    uf = new UnionFind(n);
                    uf.ProcessEdge(edgeDictionary[i]);

                    foreach (var key in edgeDictionary.Keys)
                    {
                        if (key == i) continue;
                        uf.ProcessEdge(edgeDictionary[key]);
                    }
                    int includeCost = uf.GetCost();
                    if (includeCost == mst)
                    {
                        pseudoCritical.Add(i);
                    }
                }
            }

            return result;
        }
        #endregion

        #region Day 23 767. Reorganize String
        public string ReorganizeString(string s)
        {
            Dictionary<char, int> map = new Dictionary<char, int>();

            foreach (char c in s)
            {
                if (!map.ContainsKey(c))
                {
                    map.Add(c, 0);
                }
                map[c]++;
            }

            PriorityQueue<(char ch, int count), int> q = new PriorityQueue<(char, int), int>(Comparer<int>.Create((x, y) => y - x));

            foreach (char key in map.Keys)
            {
                q.Enqueue((key, map[key]), map[key]);
            }

            StringBuilder stringBuilder = new StringBuilder();

            while (q.Count > 1)
            {
                var char1 = q.Dequeue();
                var char2 = q.Dequeue();


                stringBuilder.Append(char1.ch);
                stringBuilder.Append(char2.ch);

                if (char1.count > 1)
                {
                    q.Enqueue((char1.ch, char1.count - 1), char1.count - 1);
                }

                if (char2.count > 1)
                {
                    q.Enqueue((char2.ch, char2.count - 1), char2.count - 1);
                }
            }

            if (q.Count == 1)
            {
                var char3 = q.Dequeue();
                stringBuilder.Append(char3.ch);

                if (char3.count > 1) { return string.Empty; }

            }

            return stringBuilder.ToString();
        }
        #endregion

        #region weekly-contest-357
        public string FinalString(string s)
        {
            int i = 0;
            List<char> chars = new List<char>();
            while (i < s.Length)
            {
                if (s[i] == 'i')
                {
                    chars.Reverse();
                }
                else
                {
                    chars.Add(s[i]);
                }
            }

            return new string(chars.ToArray());
        }

        public long FindMaximumElegance(int[][] items, int k)
        {
            long result = 0;

            Dictionary<int, List<int>> categories = new Dictionary<int, List<int>>();

            foreach (var item in items)
            {
                if (!categories.ContainsKey(item[1]))
                {
                    categories.Add(item[1], new List<int>());
                }
                categories[item[1]].Add(item[0]);
            }

            foreach (var key in categories.Keys)
            {

                categories[key] = categories[key].OrderByDescending(x => x).ToList();
            }

            int totalProfit = 0;
            int distinctCategories = 0;

            // Function to get the maximum elegance for a given list of profits and k
            int GetMaxElegance(List<int> profits, int k)
            {
                List<int> selectedProfits = profits.Take(k).ToList();
                totalProfit += selectedProfits.Sum();
                distinctCategories += Math.Min(k, profits.Count);
                return selectedProfits.Sum(p => p * p);
            }

            // Get the top k items from each group and find their maximum elegance
            List<int> eleganceValues = categories.Values.Select(profits => GetMaxElegance(profits, k)).ToList();

            // Sort the elegance values in descending order and select the top k
            eleganceValues.Sort((x, y) => y.CompareTo(x));
            return totalProfit + eleganceValues.Take(k).Sum();
            return result;
        }
        #endregion

        #region weekly-contest-359
        public bool IsAcronym(IList<string> words, string s)
        {
            if (s.Length != words.Count) return false;

            int i = 0;

            while (i < s.Length)
            {
                if (words[i][0] != s[i]) return false; i++;
            }
            return true;
        }

        public int MinimumSum(int n, int k)
        {
            HashSet<int> nums = new HashSet<int>();
            int next = 1;
            while (n-- > 0)
            {
                int d = k - next;
                if (nums.Contains(d))
                {
                    n++;
                }
                else
                {
                    nums.Add(next);

                }
                next++;
            }

            return nums.Sum();
        }

        public int LongestEqualSubarray(IList<int> nums, int k)
        {
            Dictionary<int, List<int>> map = new Dictionary<int, List<int>>();


            for (int i = 0; i < nums.Count; i++)
            {
                if (!map.ContainsKey(nums[i]))
                {
                    map.Add(nums[i], new List<int>());
                }
                map[nums[i]].Add(i);
            }


            foreach (int i in map.Keys)
            {

            }

            int startIndex = 0, endIndex = 0;
            int result = 0;
            while (startIndex < nums.Count - result)
            {
                int num = nums[startIndex];
                endIndex = startIndex + 1;
                int x = k;
                int count = 1;
                while (endIndex < nums.Count && x >= 0)
                {
                    if (nums[endIndex] != num)
                    {
                        x--;

                    }
                    else
                    {
                        count++;
                    }
                    endIndex++;
                }
                result = Math.Max(result, count);
                startIndex++;
            }
            return result;

        }
        #endregion


        public bool RepeatedSubstringPattern(string s)
        {
            if (s.Length <= 1) return false;

            int i = 0;

            while (i < s.Length)
            {
                int count = i + 1;
                string ss = s.Substring(0, count);
                int j = count;
                while (j + count <= s.Length)
                {
                    if (ss != s.Substring(j, count))
                    {
                        break;
                    }

                    j += count;
                    if (j == s.Length) return true;
                }
                i++;
            }
            return false;
        }


        public int[] SortItems(int n, int m, int[] group, List<List<int>> beforeItems)
        {
            int groupId = m;
            for (int i = 0; i < n; i++)
            {
                if (group[i] == -1)
                {
                    group[i] = groupId;
                    groupId++;
                }
            }

            Dictionary<int, List<int>> itemGraph = new Dictionary<int, List<int>>();
            int[] itemIndegree = new int[n];
            for (int i = 0; i < n; ++i)
            {
                itemGraph.Add(i, new List<int>());
            }

            Dictionary<int, List<int>> groupGraph = new Dictionary<int, List<int>>();
            int[] groupIndegree = new int[groupId];
            for (int i = 0; i < groupId; ++i)
            {
                groupGraph[i] = new List<int>();
            }

            for (int curr = 0; curr < n; curr++)
            {
                foreach (int prev in beforeItems[curr])
                {
                    itemGraph[prev].Add(curr);
                    itemIndegree[curr]++;

                    if (group[curr] != group[prev])
                    {
                        groupGraph[group[prev]].Add(group[curr]);
                        groupIndegree[group[curr]]++;
                    }
                }
            }

            List<int> itemOrder = TopologicalSort(itemGraph, itemIndegree);
            List<int> groupOrder = TopologicalSort(groupGraph, groupIndegree);

            if (itemOrder.Count == 0 || groupOrder.Count == 0)
            {
                return new int[0];
            }

            Dictionary<int, List<int>> orderedGroups = new Dictionary<int, List<int>>();
            foreach (int item in itemOrder)
            {
                orderedGroups[group[item]] = orderedGroups.GetValueOrDefault(group[item], new List<int>());
                orderedGroups[group[item]].Add(item);
            }

            List<int> answerList = new List<int>();
            foreach (int groupIndex in groupOrder)
            {
                answerList.AddRange(orderedGroups.GetValueOrDefault(groupIndex, new List<int>()));
            }

            return answerList.ToArray();
        }

        private List<int> TopologicalSort(Dictionary<int, List<int>> graph, int[] indegree)
        {
            List<int> visited = new List<int>();
            Stack<int> stack = new Stack<int>();
            foreach (int key in graph.Keys)
            {
                if (indegree[key] == 0)
                {
                    stack.Push(key);
                }
            }

            while (stack.Count > 0)
            {
                int curr = stack.Pop();
                visited.Add(curr);

                foreach (int prev in graph[curr])
                {
                    indegree[prev]--;
                    if (indegree[prev] == 0)
                    {
                        stack.Push(prev);
                    }
                }
            }

            return visited.Count == graph.Count ? visited : new List<int>();
        }
        public static int countGroups(List<string> related)
        {
            int n = related.Count;

            Dictionary<int, IList<int>> map = new Dictionary<int, IList<int>>();

            for (int i = 0; i < n; i++)
            {
                map.Add(i, new List<int>());
            }

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (i == j) continue;

                    if (related[i][j] == '1')
                    {
                        map[i].Add(j);
                    }
                }
            }

            bool[] visited = new bool[n];
            int count = 0;
            for (int i = 0; i < n; i++)
            {
                if (!visited[i])
                {
                    count++;
                    Stack<int> stack = new Stack<int>();
                    stack.Push(i);

                    while (stack.Count > 0)
                    {
                        int k = stack.Pop();
                        visited[k] = true;

                        foreach (var item in map[k])
                        {
                            if (!visited[item])
                            {
                                stack.Push(item);
                            }
                        }
                    }
                }
            }

            return count;
        }

        public static List<string> processLogs(List<string> logs, int threshold)
        {
            List<string> result = new List<string>();
            Dictionary<int, int> counts = new Dictionary<int, int>();
            foreach (string log in logs)
            {
                int[] arr = Array.ConvertAll(log.Split(' '), int.Parse);

                checkAndAdd(counts, arr[0]);
                if (arr[0] != arr[1])
                {
                    checkAndAdd(counts, arr[1]);
                }
            }

            int[] arr1 = counts.Where(x => x.Value >= threshold).Select(x => x.Key).ToArray();
            Array.Sort(arr1);

            foreach (int i in arr1)
            {
                result.Add(i.ToString());
            }
            return result;
        }

        private static void checkAndAdd(Dictionary<int, int> counts, int num)
        {
            if (!counts.ContainsKey(num))
            {
                counts.Add(num, 0);
            }
            counts[num]++;
        }
    }
}
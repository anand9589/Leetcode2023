using Common;
using System.Collections;
using System.Text;

namespace April
{
    public class Solution
    {

        #region Problem Day 1 704. Binary Search
        public int Search(int[] nums, int target)
        {
            int high = nums.Length - 1;
            int low = 0;

            while (low <= high)
            {
                int mid = (low + high) / 2;

                if (nums[mid] == target) return mid;

                if (nums[mid] < target) low = mid + 1;
                else high = mid - 1;
            }

            return -1;
        }
        #endregion

        #region Problem Day 2 2300. Successful Pairs of Spells and Potions
        public int[] SuccessfulPairs(int[] spells, int[] potions, long success)
        {
            int[] result = new int[spells.Length];
            Array.Sort(potions);
            for (int i = 0; i < spells.Length; i++)
            {
                long k = success / (long)spells[i];

                if (success % (long)spells[i] != 0)
                {
                    k++;
                }

                int index = getFirstValidIndex(potions, k);
                result[i] = potions.Length - index;
            }

            return result;
        }

        private int getFirstValidIndex(int[] potions, long k)
        {
            int i = 0;
            int low = 0;
            int high = potions.Length - 1;
            while (low <= high)
            {
                if (potions[low] >= k) return low;
                if (potions[high] < k) return high + 1;

                int mid = (low + high) / 2;
                if (potions[mid] == k)
                {
                    int res = mid;
                    while (potions[mid] == k)
                    {
                        res = mid--;
                    }
                    return res;
                }
                if (potions[mid] > k)
                {
                    high = mid - 1;
                }
                else
                {
                    low = mid + 1;
                }
            }

            return i;
        }
        #endregion

        #region Problem Day 3 881. Boats to Save People
        public int NumRescueBoats(int[] people, int limit)
        {
            Array.Sort(people);

            int left = 0;
            int right = people.Length - 1;
            int result = 0;
            while (left <= right)
            {
                if (people[left] + people[right] <= limit)
                {
                    left++;
                    right--;
                }
                else
                {
                    right--;
                }

                result++;
            }
            return result;
        }
        #endregion

        #region Problem Day 4 2405. Optimal Partition of String
        public int PartitionString(string s)
        {
            if (string.IsNullOrEmpty(s)) return 0;
            int result = 1;

            HashSet<char> chars = new HashSet<char>();

            foreach (char c in s)
            {
                if (chars.Contains(c))
                {
                    result++;
                    chars = new HashSet<char>() { c };

                }
                else
                {
                    chars.Add(c);
                }
            }

            return result;
        }
        #endregion

        #region Problem Day 5 2439. Minimize Maximum of Array
        public int MinimizeArrayValue(int[] nums)
        {
            int result = 0;
            int low = nums.Min();
            int high = nums.Max();

            while (low <= high)
            {
                long mid = ((long)low + (long)high) / 2;

                if (isValidMid(nums, mid))
                {
                    result = (int)mid;
                    high = result - 1;
                }
                else
                {
                    low = (int)mid + 1;
                }
            }
            return result;
        }

        private bool isValidMid(int[] nums, long mid)
        {
            long tempRem = 0;

            for (int i = nums.Length - 1; i >= 0; i--)
            {
                if (nums[i] <= mid)
                {
                    tempRem -= Math.Min(tempRem, mid - nums[i]);
                }
                else
                {
                    tempRem += nums[i] - mid;
                }
            }
            return tempRem <= 0;
        }
        #endregion

        #region Problem Day 6 1254. Number of Closed Islands
        public int ClosedIsland(int[][] grid)
        {
            int result = 0;
            int m = grid.Length;
            int n = grid[0].Length;
            for (int i = 0; i < m; i++)
            {
                checkEdge_1254(grid, i, 0);
                checkEdge_1254(grid, i, n - 1);
            }

            for (int i = 0; i < n; i++)
            {
                checkEdge_1254(grid, 0, i);
                checkEdge_1254(grid, m - 1, i);
            }

            for (int i = 1; i < grid.Length - 1; i++)
            {
                for (int j = 1; j < grid[i].Length - 1; j++)
                {
                    if (grid[i][j] == 0)
                    {
                        result++;
                        Queue<(int, int)> q = new Queue<(int, int)>();
                        q.Enqueue((i, j));
                        bfs_1254(q, grid);
                    }
                }
            }

            return result;
        }

        private void checkEdge_1254(int[][] grid, int x, int y)
        {
            if (grid[x][y] == 0)
            {
                Queue<(int, int)> q = new Queue<(int, int)>();
                q.Enqueue((x, y));
                bfs_1254(q, grid);
            }
        }

        private void bfs_1254(Queue<(int, int)> q, int[][] grid)
        {
            if (q.Count == 0) return;

            (int x, int y) = q.Dequeue();

            grid[x][y] = 1;

            //left x-1 y
            checkAndAddToQueue_1254(q, grid, x - 1, y);
            //right x+1 y
            checkAndAddToQueue_1254(q, grid, x + 1, y);
            //top x y-1
            checkAndAddToQueue_1254(q, grid, x, y - 1);
            //bottom x y+1
            checkAndAddToQueue_1254(q, grid, x, y + 1);

            bfs_1254(q, grid);

        }

        private void checkAndAddToQueue_1254(Queue<(int, int)> q, int[][] grid, int x, int y)
        {
            if (x < 0 || y < 0 || x == grid.Length || y == grid[x].Length || grid[x][y] == 1) return;
            q.Enqueue((x, y));
        }

        #endregion

        #region Problem Day 7 1020. Number of Enclaves
        public int NumEnclaves(int[][] grid)
        {
            int result = 0;

            int m = grid.Length;
            int n = grid[0].Length;

            for (int i = 0; i < m; i++)
            {
                runbfs(grid, i, 0);
                runbfs(grid, i, n - 1);
            }

            for (int i = 0; i < n; i++)
            {
                runbfs(grid, 0, i);
                runbfs(grid, m - 1, i);
            }

            for (int i = 1; i < m - 1; i++)
            {
                for (int j = 1; j < n - 1; j++)
                {
                    result += runbfs(grid, i, j);
                }
            }

            return result;
        }

        private int runbfs(int[][] grid, int i, int j)
        {
            if (grid[i][j] == 1)
            {
                Queue<(int, int)> q = new Queue<(int, int)>();
                q.Enqueue((i, j));
                grid[i][j] = 0;
                return 1 + bfs(grid, q);
            }
            return 0;
        }

        private int bfs(int[][] grid, Queue<(int, int)> q)
        {
            if (q.Count == 0) return 0;
            int count = 0;
            (int x, int y) = q.Dequeue();


            count += checkAndAddToQueue(grid, q, x - 1, y);
            count += checkAndAddToQueue(grid, q, x + 1, y);
            count += checkAndAddToQueue(grid, q, x, y - 1);
            count += checkAndAddToQueue(grid, q, x, y + 1);

            return count + bfs(grid, q);
        }

        private int checkAndAddToQueue(int[][] grid, Queue<(int, int)> q, int x, int y)
        {
            if (x == -1 || y == -1 || x == grid.Length || y == grid[x].Length || grid[x][y] == 0) return 0;
            q.Enqueue((x, y));
            grid[x][y] = 0;
            return 1;
        }
        #endregion

        #region Problem Day 8 133. Clone Graph

        HashSet<NodeV1> lst = new HashSet<NodeV1>();
        public NodeV1 CloneGraph(NodeV1 node)
        {

            if (node != null)
            {
                NodeV1 node1 = new NodeV1(node.val);

                lst.Add(node1);
                foreach (NodeV1 node2 in node.neighbors)
                {
                    var p = lst.FirstOrDefault(x => x.val == node2.val);
                    if (p == null)
                    {
                        p = CloneGraph(node2);
                    }
                    node1.neighbors.Add(p);
                }
                return node1;
            }

            return null;
        }
        #endregion

        #region Problem Day 9 1857. Largest Color Value in a Directed Graph
        public int LargestPathValue(string colors, int[][] edges)
        {

            var parentToChildren = new List<HashSet<int>>();
            var childToParents = new List<HashSet<int>>();
            var n = colors.Length;

            var outDegree = new int[n];
            for (int i = 0; i < n; i++)
            {
                parentToChildren.Add(new HashSet<int>());
                childToParents.Add(new HashSet<int>());
            }
            foreach (var edge in edges)
            {
                parentToChildren[edge[0]].Add(edge[1]);
                childToParents[edge[1]].Add(edge[0]);

                outDegree[edge[0]]++;
            }

            var colorMap = new int[n, 26];

            var q = new Queue<int>();
            for (int i = 0; i < n; i++)
            {
                if (outDegree[i] == 0)
                    q.Enqueue(i);
            }

            var result = 0;
            var count = n;
            while (q.Count > 0)
            {
                count--;
                var node = q.Dequeue();

                for (int i = 0; i < 26; i++)
                {
                    foreach (var child in parentToChildren[node])
                    {
                        colorMap[node, i] = Math.Max(colorMap[node, i], colorMap[child, i]);
                    }
                }
                colorMap[node, colors[node] - 'a']++;
                result = Math.Max(result, colorMap[node, colors[node] - 'a']);

                foreach (var parent in childToParents[node])
                {
                    outDegree[parent]--;
                    if (outDegree[parent] == 0)
                    {
                        q.Enqueue(parent);
                    }
                }
            }
            return count > 0 ? -1 : result;
        }
        #endregion

        #region Problem Day 10 20. Valid Parentheses
        public bool IsValid(string s)
        {
            Stack<char> stack = new Stack<char>();
            int i = 0;

            while (i < s.Length)
            {
                switch (s[i])
                {
                    case '}':
                        if (stack.Count == 0 || stack.Count == 0 || stack.Pop() != '{') return false;
                        break;
                    case ')':
                        if (stack.Count == 0 || stack.Pop() != '(') return false;
                        break;
                    case ']':
                        if (stack.Count == 0 || stack.Pop() != '[') return false;
                        break;
                    default:
                        stack.Push(s[i]);
                        break;
                }
                i++;
            }
            return stack.Count == 0;
        }
        #endregion

        #region Problem Day 11 2390. Removing Stars From a String
        public string RemoveStars(string s)
        {

            int j = -1;
            StringBuilder s1 = new StringBuilder();
            int i = 0;

            while (i < s.Length)
            {
                if (s[i] == '*')
                {
                    s1.Remove(j, 1);
                    j--;
                }
                else
                {
                    s1.Append(s[i]);
                    j++;
                }
                i++;
            }
            return s1.ToString();
        }

        public string RemoveStars_v1(string s)
        {
            Stack<char> stack = new Stack<char>();
            int i = 0;

            while (i < s.Length)
            {
                if (s[i] == '*')
                {
                    stack.Pop();
                }
                else
                {
                    stack.Push(s[i]);
                }
                i++;
            }


            StringBuilder s1 = new StringBuilder();

            while (stack.Count > 0)
            {
                s1.Insert(0, stack.Pop());
            }
            return s1.ToString();
        }
        #endregion

        #region Problem Day 12 71. Simplify Path
        public string SimplifyPath(string path)
        {
            if (!path.StartsWith("/")) return "";

            string[] strs = path.Split('/', StringSplitOptions.RemoveEmptyEntries);
            Stack<string> stack = new Stack<string>();
            foreach (string str in strs)
            {
                if (str == ".") continue;
                if (str == "..")
                {
                    if (stack.Count > 0)
                    {
                        stack.Pop();
                    }

                    continue;
                }
                stack.Push(str);

            }
            StringBuilder stringBuilder = new StringBuilder();

            while (stack.Count > 0)
            {
                stringBuilder.Insert(0, stack.Pop());
                stringBuilder.Insert(0, "/");
            }

            return stringBuilder.Length == 0 ? "/" : stringBuilder.ToString();
        }
        #endregion

        #region Problem Day 13 946. Validate Stack Sequences
        public bool ValidateStackSequences(int[] pushed, int[] popped)
        {
            int n = pushed.Length;
            Stack<int> stack = new Stack<int>();
            int pushIndex = 0;
            int popIndex = 0;
            while (pushIndex < n)
            {
                while (stack.Count > 0 && stack.Peek() == popped[popIndex])
                {
                    stack.Pop();
                    popIndex++;
                }
                stack.Push(pushed[pushIndex]);
                pushIndex++;
            }

            while (popIndex < n && stack.Peek() == popped[popIndex])
            {
                stack.Pop();
                popIndex++;
            }

            return stack.Count == 0;
        }
        #endregion

        #region Problem Day 14 516. Longest Palindromic Subsequence
        public int LongestPalindromeSubseq(string s)
        {

            int length = s.Length;

            int[][] dp = new int[length][];

            for (int i = 0; i < length; i++)
            {
                dp[i] = new int[length];
                dp[i][i] = 1;
            }


            for (int i = 1; i < length; i++)
            {
                for (int j = 0; j < length - i; j++)
                {
                    if (s[j] == s[j + i])
                    {
                        dp[j][j + i] = 2 + dp[j + 1][j + i - 1];
                    }
                    else
                    {
                        dp[j][j + i] = Math.Max(dp[j][j + i - 1], dp[j + 1][j + i]);
                    }
                }
            }
            return dp[0][length - 1];
        }
        #endregion

        #region Problem Day 15 2218. Maximum Value of K Coins From Piles
        int[][] dp;
        public int MaxValueOfCoins(IList<IList<int>> piles, int k)
        {
            int len = piles.Count;
            dp = new int[len + 1][];

            for (int i = 0; i <= len; i++)
            {
                dp[i] = new int[k + 1];
            }
            return knapsack(piles, len - 1, k);
        }

        private int knapsack(IList<IList<int>> piles, int v, int k)
        {
            if (v < 0 || k == 0) return 0;
            if (dp[v][k] != 0) return dp[v][k];

            int m = Math.Min(piles[v].Count, k);

            int exclude = knapsack(piles, v - 1, k);

            int include = 0;

            for (int i = 0, sum = 0; i < m; i++)
            {
                sum += piles[v][i];
                include = Math.Max(include, sum + knapsack(piles, v - 1, k - i - 1));
            }

            return dp[v][k] = Math.Max(include, exclude);
        }
        #endregion

        #region Problem Day 16 1431. Kids With the Greatest Number of Candies
        public IList<bool> KidsWithCandies(int[] candies, int extraCandies)
        {
            int max = candies.Max();
            return candies.Select(x => x + extraCandies >= max).ToList<bool>();
        }
        #endregion

        #region Problem Day 17 1768. Merge Strings Alternately
        public string MergeAlternately(string word1, string word2)
        {
            if (string.IsNullOrEmpty(word1)) return word2;
            if (string.IsNullOrEmpty(word2)) return word1;

            StringBuilder stringBuilder = new StringBuilder();

            int i = 0;
            while (i < word1.Length && i < word2.Length)
            {
                stringBuilder.Append(word1[i]);
                stringBuilder.Append(word2[i]);
                i++;
            }

            while (i < word1.Length)
            {
                stringBuilder.Append(word1[i]);
                i++;
            }

            while (i < word2.Length)
            {
                stringBuilder.Append(word2[i]);
                i++;
            }

            return stringBuilder.ToString();
        }
        #endregion

        #region Problem Day 18 1372. Longest ZigZag Path in a Binary Tree
        public int LongestZigZag(TreeNode root)
        {

            int result = 0;
            Queue<(TreeNode node, bool isLeft, int count)> q = new Queue<(TreeNode node, bool isLeft, int count)>();

            if (root.left != null)
            {
                q.Enqueue((root.left, true, 1));
            }

            if (root.right != null)
            {
                q.Enqueue((root.right, false, 1));
            }

            while (q.Count > 0)
            {
                var p = q.Dequeue();
                result = Math.Max(result, p.count);
                if (p.isLeft)
                {
                    if (p.node.right != null)
                    {
                        q.Enqueue((p.node.right, false, p.count + 1));
                    }

                    if (p.node.left != null)
                    {
                        q.Enqueue((p.node.left, true, 1));
                    }
                }
                else
                {
                    if (p.node.left != null)
                    {
                        q.Enqueue((p.node.left, true, p.count + 1));
                    }

                    if (p.node.right != null)
                    {
                        q.Enqueue((p.node.right, false, 1));
                    }
                }
            }

            return result;
        }
        #endregion

        #region Problem Day 19 662. Maximum Width of Binary Tree
        public int WidthOfBinaryTree(TreeNode root)
        {
            Stack<(TreeNode node, int level, int index)> stack = new Stack<(TreeNode node, int level, int index)>();
            stack.Push((root, 0, 0));

            Dictionary<int, List<int>> map = new Dictionary<int, List<int>>();

            while (stack.Count > 0)
            {
                var p = stack.Pop();

                if (!map.ContainsKey(p.level))
                {
                    map.Add(p.level, new List<int>());
                }

                map[p.level].Add(p.index);

                if (p.node.left != null)
                {
                    stack.Push((p.node.left, p.level + 1, (p.index * 2 + 1)));
                }
                if (p.node.right != null)
                {
                    stack.Push((p.node.right, p.level + 1, (p.index * 2 + 2)));
                }
            }

            int maxDiff = 0;

            foreach (var key in map.Keys)
            {
                maxDiff = Math.Max(maxDiff,Math.Abs( map[key].Last() - map[key].First()));
            }


            return maxDiff+1;
        }
        #endregion

        #region Problem Day 20
        #endregion

        #region Problem Day 21
        #endregion

        #region Problem Day 22
        #endregion

        #region Problem Day 23
        #endregion

        #region Problem Day 24
        #endregion

        #region Problem Day 25
        #endregion

        #region Problem Day 26
        #endregion

        #region Problem Day 27
        #endregion

        #region Problem Day 28
        #endregion

        #region Problem Day 29
        #endregion

        #region Problem Day 30
        #endregion

        #region Problem 10. Regular Expression Matching
        public bool IsMatch(string s, string p)
        {
            if (s.Length == 0 || p.Length == 0) return false;

            bool[][] dp = new bool[s.Length + 1][];

            for (int i = 0; i < dp.Length; i++)
            {
                dp[i] = new bool[p.Length + 1];
            }

            dp[0][0] = true;

            for (int i = 0; i < p.Length; i++)
            {
                if (p[i] == '*' && dp[0][i - 1])
                {
                    dp[0][i + 1] = true;
                }
            }

            for (int i = 0; i < s.Length; i++)
            {
                for (int j = 0; j < p.Length; j++)
                {
                    if (p[j] == '.' || p[j] == s[i])
                    {
                        dp[i + 1][j + 1] = dp[i][j];
                    }
                    else if (p[j] == '*')
                    {
                        if (p[j - 1] != s[i] && p[j - 1] != '.')
                        {
                            dp[i + 1][j + 1] = dp[i + 1][j - 1];
                        }
                        else
                        {
                            dp[i + 1][j + 1] = (dp[i + 1][j] || dp[i][j + 1] || dp[i + 1][j - 1]);
                        }
                    }
                }
            }

            return dp[s.Length][p.Length];
        }
        #endregion

        #region Problem 138. Copy List with Random Pointer
        public Node CopyRandomList(Node head)
        {
            Node node = new Node(-1);
            Node temp = node.next;
            while (head != null)
            {
            }

            return node.next;
        }
        #endregion

        #region Weekly 339
        public int FindTheLongestBalancedSubstring(string s)
        {
            int res = 0;
            int i = 0;

            while (i < s.Length)
            {

                if (s[i] == '0')
                {
                    int zeroCount = 0;
                    while (i < s.Length && s[i] == '0')
                    {
                        i++;
                        zeroCount++;
                    }
                    int oneCount = 0;
                    while (i < s.Length && s[i] == '1')
                    {
                        i++;
                        oneCount++;
                    }

                    res = Math.Max(res, Math.Min(zeroCount, oneCount));

                }
                else
                {
                    i++;
                }
            }

            return res * 2;
        }

        public IList<IList<int>> FindMatrix(int[] nums)
        {
            IList<IList<int>> matrix = new List<IList<int>>();

            IDictionary<int, int> map = new Dictionary<int, int>();

            for (int i = 0; i < nums.Length; i++)
            {
                if (!map.ContainsKey(nums[i]))
                {
                    map.Add(nums[i], 0);
                }
                map[nums[i]]++;
            }

            while (map.Keys.Count > 0)
            {
                IList<int> list = new List<int>();
                IList<int> keysToRemove = new List<int>();
                foreach (var k in map.Keys)
                {
                    list.Add(k);
                    map[k]--;
                    if (map[k] == 0) keysToRemove.Add(k);
                }
                matrix.Add(list);
                foreach (var item in keysToRemove)
                {
                    map.Remove(item);
                }
            }

            return matrix;
        }

        public int MiceAndCheese(int[] reward1, int[] reward2, int k)
        {
            int result = 0;

            PriorityQueue<int, (int, int)> q = new PriorityQueue<int, (int, int)>(new CustomComparer());

            for (int i = 0; i < reward1.Length; i++)
            {
                q.Enqueue(i, (reward1[i], reward2[i]));
            }

            while (k > 0 && q.Count > 0)
            {
                result += reward1[q.Dequeue()];
                k--;
            }

            while (q.Count > 0)
            {
                int index = q.Dequeue();
                result += Math.Max(reward1[index], reward2[index]);
            }
            return result;
            //Dictionary<int, IList<int>> matrix = new Dictionary<int, IList<int>>();

            //for (int i = 0; i < reward1.Length; i++)
            //{
            //    if (!matrix.ContainsKey(reward1[i]))
            //    {
            //        matrix.Add(reward1[i], new List<int>());
            //    }

            //    matrix[reward1[i]].Add(i);
            //}

            //matrix = matrix.OrderByDescending(x => x.Key).ToDictionary(x => x.Key, y => y.Value);

            //bool[] eaten = new bool[reward1.Length];

            //while (k > 0 && matrix.Count > 0)
            //{
            //    int key = matrix.Keys.FirstOrDefault();

            //    int count = matrix[key].Count;

            //    if (count <= k)
            //    {

            //    }
            //    else
            //    {

            //    }
            //}

            //for (int i = 0; i < reward1.Length; i++)
            //{
            //    queue.Enqueue(i, reward1[i]);
            //}

            //if (k < reward1.Length)
            //{
            //    int p = reward1.Length - k;

            //    for (int i = 0; i < p; i++)
            //    {
            //        var x = queue.Dequeue();
            //        result += reward2[x];
            //    }
            //}

            //while (queue.Count > 0)
            //{
            //    result += reward1[queue.Dequeue()];
            //}

        }

        class CustomComparer : IComparer<(int, int)>
        {
            public int Compare((int, int) x, (int, int) y)
            {
                if (x.Item1 == y.Item1)
                {
                    if (x.Item2 == y.Item2) return 0;

                    if (x.Item2 < y.Item2) return -1;

                    return 1;
                }

                if (x.Item1 < y.Item1) return 1;

                return -1;
            }
        }

        #endregion

        #region Weekly 340
        public int DiagonalPrime(int[][] nums)
        {
            int result = 0;
            int j = nums.Length - 1;
            int i = 0;
            while (i < nums.Length && j >= 0)
            {
                if (IsPrime(nums[i][i]))
                {
                    result = Math.Max(result, nums[i][i]);
                }

                if (IsPrime(nums[i][j]))
                {
                    result = Math.Max(result, nums[i][j]);
                }
                i++;
                j--;
            }

            return result;
        }

        public static bool IsPrime(int number)
        {
            if (number <= 1) return false;
            if (number == 2) return true;
            if (number % 2 == 0) return false;

            var boundary = (int)Math.Floor(Math.Sqrt(number));

            for (int i = 3; i <= boundary; i += 2)
                if (number % i == 0)
                    return false;

            return true;
        }

        //public long[]
        public long[] Distance_V1(int[] nums)
        {
            long[] result = new long[nums.Length];
            Dictionary<int, List<int>> map = new Dictionary<int, List<int>>();
            for (int i = 0; i < nums.Length; i++)
            {
                if (map.ContainsKey(nums[i]))
                {
                    long sum = 0;
                    foreach (var item in map[nums[i]])
                    {
                        sum += Math.Abs(item - i);
                        result[item] += Math.Abs(item - i);
                    }

                    map[nums[i]].Add(i);
                    result[i] = sum;
                }
                else
                {
                    map.Add(nums[i], new List<int>() { i });
                }
            }

            return result;
        }
        #endregion

        #region Weekly 341
        public int[] RowAndMaximumOnes(int[][] mat)
        {
            int[] result = new int[] { -1, -1 };
            for (int i = 0; i < mat.Length; i++)
            {
                int sum = 0;
                for (int j = 0; j < mat[i].Length; j++)
                {
                    sum += mat[i][j];
                }

                if (sum > result[1])
                {
                    result[0] = i;
                    result[1] = sum;
                }
            }

            return result;
        }

        public int MaxDivScore(int[] nums, int[] divisors)
        {
            int result = int.MaxValue;

            IDictionary<int, int> map = new Dictionary<int, int>();

            int count = 0;

            for (int i = 0; i < divisors.Length; i++)
            {
                if (!map.ContainsKey(divisors[i]))
                {
                    map.Add(divisors[i], 0);
                    for (int j = 0; j < nums.Length; j++)
                    {
                        if (nums[j] % divisors[i] == 0) map[divisors[i]]++;
                    }

                    if (count < map[divisors[i]])
                    {
                        count = map[divisors[i]];
                        result = divisors[i];
                    }
                    else if (count == map[divisors[i]])
                    {
                        result = Math.Min(result, divisors[i]);
                    }
                }
            }

            return result;
        }

        public int AddMinimum(string word)
        {
            char[] chars = new char[] { 'a', 'b', 'c' };

            int i = 0;
            StringBuilder resultString = new StringBuilder();

            while (resultString.Length % 3 != 0 || i < word.Length)
            {
                int k = resultString.Length % 3;
                if (i < word.Length && chars[k] == word[i])
                {
                    i++;
                }
                resultString.Append(chars[k]);
            }

            return resultString.Length - word.Length;
        }

        //2646. Minimize the Total Price of the Trips
        public int MinimumTotalPrice(int n, int[][] edges, int[] price, int[][] trips)
        {
            int result = 0;

            return result;
        }
        #endregion
    }
}
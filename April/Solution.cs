using Common;

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

        #region Problem Day 9
        #endregion

        #region Problem Day 10
        #endregion

        #region Problem Day 11
        #endregion

        #region Problem Day 12
        #endregion

        #region Problem Day 13
        #endregion

        #region Problem Day 14
        #endregion

        #region Problem Day 15
        #endregion

        #region Problem Day 16
        #endregion

        #region Problem Day 17
        #endregion

        #region Problem Day 18
        #endregion

        #region Problem Day 19
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
    }
}
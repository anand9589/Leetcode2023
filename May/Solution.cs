using Common;
using System.Collections;
using System.Text.RegularExpressions;

namespace May
{
    public class Solution
    {
        #region Day 1 Problem  1491. Average Salary Excluding the Minimum and Maximum Salary

        public double Average(int[] salary)
        {
            Array.Sort(salary);

            int n = salary.Length;
            int sum = 0;
            for (int i = 1; i < n - 1; i++)
            {
                sum += salary[i];
            }

            return (double)sum / n - 2;
        }

        public double Average_V1(int[] salary)
        {
            int maxSalary = int.MinValue;
            int minSalary = int.MaxValue;

            int n = salary.Length;
            int salarySum = 0;
            foreach (int i in salary)
            {
                salarySum += i;

                minSalary = Math.Min(minSalary, i);
                maxSalary = Math.Max(maxSalary, i);
            }

            return (double)(salarySum - maxSalary - minSalary) / (n - 2);
            //return (double)(salary.Sum() - salary.Max() - salary.Min()) / (salary.Length - 2);
        }
        #endregion

        #region Day 2 Problem  1822. Sign of the Product of an Array
        public int ArraySign(int[] nums)
        {
            Array.Sort(nums);

            int low = 0;

            int high = nums.Length - 1;

            while (low <= high)
            {
                int mid = (low + high) / 2;

                if (nums[mid] == 0) return 0;

                if (nums[mid] > 0)
                {
                    high = mid - 1;
                }
                else
                {
                    low = mid + 1;
                }
            }

            return low % 2 == 0 ? 1 : -1;
        }
        #endregion

        #region Day 3 Problem  2215. Find the Difference of Two Arrays
        public IList<IList<int>> FindDifference(int[] nums1, int[] nums2)
        {
            var p = nums1.Except(nums1.Intersect(nums2));

            var q = nums2.Except(nums2.Intersect(nums1));
            return new List<IList<int>>() { p.ToList(), q.ToList() };
        }
        #endregion

        #region Day 4 Problem 649. Dota2 Senate
        public string PredictPartyVictory(string senate)
        {
            int n = senate.Length;
            int[] counts = new int[2];  // counts[0] is the count of Radiant senators, counts[1] is the count of Dire senators
            int[] bans = new int[2];    // bans[0] is the number of times Radiant has banned Dire, bans[1] is the number of times Dire has banned Radiant
            char[] cans = senate.ToCharArray();
            foreach (char c in senate)
            {
                if (c == 'R')
                {
                    counts[0]++;
                }
                else
                {
                    counts[1]++;
                }
            }

            while (counts[0] > 0 && counts[1] > 0)
            {
                for (int i = 0; i < n; i++)
                {
                    if (cans[i] == ' ') continue;  // this senator has lost their right
                    if (cans[i] == 'R')
                    {
                        if (bans[0] > 0)
                        {
                            bans[0]--;
                            cans[i] = ' ';
                            counts[0]--;
                        }
                        else
                        {
                            bans[1]++;
                        }
                    }
                    else
                    {
                        if (bans[1] > 0)
                        {
                            bans[1]--;
                            cans[i] = ' ';
                            counts[1]--;
                        }
                        else
                        {
                            bans[0]++;
                        }
                    }
                }
            }

            return counts[0] > 0 ? "Radiant" : "Dire";
        }
        public string PredictPartyVictory_V1(string senate)
        {
            char c = senate[0];
            int n = senate.Length;

            int rCount = senate.Where(x => x == 'R').Count();

            int dCount = n - rCount;

            if (rCount != dCount)
            {
                c = rCount > dCount ? 'R' : 'D';
            }

            return c == 'R' ? "Radiant" : "Dire";
        }
        #endregion

        #region Day 5 Problem 1456. Maximum Number of Vowels in a Substring of Given Length
        public int MaxVowels(string s, int k)
        {
            int vowel_count = 0;
            for (int i = 0; i < k; i++)
            {
                switch (s[i])
                {
                    case 'a':
                    case 'e':
                    case 'i':
                    case 'o':
                    case 'u':
                        vowel_count++;
                        break;
                    default: break;
                }
            }

            int result = vowel_count;
            int startIndex = 1;
            while (startIndex + k - 1 < s.Length)
            {
                switch (s[startIndex - 1])
                {
                    case 'a':
                    case 'e':
                    case 'i':
                    case 'o':
                    case 'u':
                        vowel_count--;
                        break;
                    default: break;
                }

                switch (s[startIndex + k - 1])
                {
                    case 'a':
                    case 'e':
                    case 'i':
                    case 'o':
                    case 'u':
                        vowel_count++;
                        result = Math.Max(result, vowel_count);
                        break;
                    default: break;
                }

                startIndex++;


            }

            return result;
        }
        #endregion

        #region Day 6 Problem 1498. Number of Subsequences That Satisfy the Given Sum Condition
        public int NumSubseq(int[] nums, int target)
        {
            int mod = 1000000007;
            int n = nums.Length;
            Array.Sort(nums);
            int i = 0, j = n - 1;
            int res = 0;
            int[] pows = new int[n];
            pows[0] = 1;
            for (int k = 1; k < n; k++)
            {
                pows[k] = (pows[k - 1] * 2) % mod;
            }
            while (i <= j)
            {
                if (nums[i] + nums[j] <= target)
                {
                    res = (res + pows[j - i]) % mod;
                    i++;
                }
                else
                {
                    j--;
                }
            }
            return res;
        }
        #endregion

        #region Day 7 Problem 1964. Find the Longest Valid Obstacle Course at Each Position
        public int[] LongestObstacleCourseAtEachPosition(int[] obstacles)
        {
            int n = obstacles.Length;
            int[] ans = new int[n];
            List<int> courses = new List<int>();
            for (int i = 0; i < n; i++)
            {
                int h = obstacles[i];
                int lo = 0, hi = courses.Count - 1;
                while (lo <= hi)
                {
                    int mid = (lo + hi) / 2;
                    if (courses[mid] <= h)
                    {
                        lo = mid + 1;
                    }
                    else
                    {
                        hi = mid - 1;
                    }
                }
                int len = lo + 1;
                if (lo < courses.Count)
                {
                    courses[lo] = h;
                }
                else
                {
                    courses.Add(h);
                }
                ans[i] = len;
            }
            return ans;
        }
        #endregion

        #region Day 8 Problem 1572. Matrix Diagonal Sum
        public int DiagonalSum(int[][] mat)
        {
            HashSet<(int, int)> values = new HashSet<(int, int)>();

            for (int i = 0; i < mat.Length; i++)
            {
                values.Add((i, i));
            }

            int j = 0;
            for (int i = mat.Length - 1; i >= 0; i--, j++)
            {
                values.Add((j, i));
            }
            int sum = 0;
            foreach (var item in values)
            {
                sum += mat[item.Item1][item.Item2];
            }
            return sum;
        }
        #endregion

        #region Day 9 Problem 54. Spiral Matrix

        public IList<int> SpiralOrder(int[][] matrix)
        {
            IList<int> lst = new List<int>();

            int m = matrix.Length;
            int n = matrix[0].Length;
            int totalCount = m * n;
            int round = 0;
            while (lst.Count < totalCount)
            {
                int x = round;
                int y = round;
                int y1 = n - round;
                int x1 = m - round;

                while (y < y1)
                {
                    lst.Add(matrix[x][y]);
                    y++;
                }

                if (lst.Count == totalCount) break;

                y--;
                x++;

                while (x < x1)
                {
                    lst.Add(matrix[x][y]);
                    x++;
                }

                if (lst.Count == totalCount) break;

                x--;
                y--;


                while (y >= round)
                {
                    lst.Add(matrix[x][y]);
                    y--;
                }

                if (lst.Count == totalCount) break;
                y++;
                x--;


                while (x > round)
                {
                    lst.Add(matrix[x][y]);
                    x--;
                }

                round++;
            }

            return lst;
        }

        public IList<int> SpiralOrder_V1(int[][] matrix)
        {
            IList<int> lst = new List<int>();
            bool[][] visited = new bool[matrix.Length][];
            for (int i = 0; i < matrix.Length; i++)
            {
                visited[i] = new bool[matrix[i].Length];
            }
            int m = matrix.Length;
            int n = matrix[0].Length;
            int x = 0;
            int y = 0;
            int round = 0;
            while (lst.Count < m * n)
            {
                x = round;
                y = round;
                while (y < n && !visited[x][y])
                {
                    lst.Add(matrix[x][y]);
                    visited[x][y] = true;
                    y++;
                }
                y = n - 1;
                x = x + 1;

                while (x < m && !visited[x][y])
                {
                    lst.Add(matrix[x][y]);
                    visited[x][y] = true;
                    x++;
                }
                x = m - 1;
                y = y - 1;

                while (y >= round && !visited[x][y])
                {
                    lst.Add(matrix[x][y]);
                    visited[x][y] = true;
                    y--;
                }
                y = round;
                x = x - 1;

                while (x > round && !visited[x][y])
                {
                    lst.Add(matrix[x][y]);
                    visited[x][y] = true;
                    x--;
                }

                round++;
            }

            return lst;
        }
        #endregion

        #region Day 10 Problem 59. Spiral Matrix II
        public int[][] GenerateMatrix(int n)
        {
            int[][] arr = new int[n][];
            for (int i = 0; i < n; i++)
            {
                arr[i] = new int[n];
            }
            int cur = 1;
            int round = 0;
            while (cur <= n * n)
            {
                int x = round;

                int y = round;

                int x1 = n - round;

                int y1 = n - round;

                while (y < y1)
                {
                    arr[x][y] = cur;
                    cur++;
                    y++;
                }

                y--;
                x++;

                while (x < x1)
                {
                    arr[x][y] = cur;
                    cur++;
                    x++;
                }


                x--;
                y--;
                while (y >= round)
                {
                    arr[x][y] = cur;
                    cur++;
                    y--;
                }

                x--;
                y++;

                while (x > round)
                {
                    arr[x][y] = cur;
                    cur++;
                    x--;
                }
                round++;
            }

            return arr;
        }
        #endregion

        #region Day 11 Problem 1035. Uncrossed Lines
        public int MaxUncrossedLines(int[] nums1, int[] nums2)
        {
            int m = nums1.Length;
            int n = nums2.Length;
            int[,] dp = new int[m + 1, n + 1];

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (nums1[i] == nums2[j])
                    {
                        dp[i + 1, j + 1] = dp[i, j] + 1;
                    }
                    else
                    {
                        dp[i + 1, j + 1] = Math.Max(dp[i + 1, j], dp[i, j + 1]);
                    }
                }
            }

            return dp[m, n];
        }
        #endregion

        #region Day 12 Problem 2140. Solving Questions With Brainpower
        public long MostPoints(int[][] questions)
        {
            long[] dp = new long[questions.Length];
            for (int i = 0; i < questions.Length; i++)
            {
                dp[i] = questions[i][0];
            }

            for (int i = dp.Length - 2; i >= 0; i--)
            {
                dp[i] = dp[i + 1];

                long cur = questions[i][0];

                int nextIndex = i + 1 + questions[i][1];

                if (nextIndex < dp.Length)
                {
                    cur += dp[nextIndex];
                }

                dp[i] = Math.Max(cur, dp[i]);
            }

            return dp[0];
        }
        #endregion

        #region Day 13 Problem 2466. Count Ways To Build Good Strings
        public int CountGoodStrings(int low, int high, int zero, int one)
        {
            const int Mod = 1000000007;

            long[] dp = new long[100001];
            dp[0] = 1;

            for (int i = 1; i <= 100000; i++)
            {
                if (i - zero >= 0)
                {
                    dp[i] += dp[i - zero];
                }
                if (i - one >= 0)
                {
                    dp[i] += dp[i - one];
                }
                dp[i] %= Mod;
            }

            int res = 0;

            for (int i = low; i <= high; i++)
            {
                res = (int)((res + dp[i]) % Mod);
            }

            return res;
        }
        #endregion

        #region Day 14 Problem 1799. Maximize Score After N Operations
        public int MaxScore(int[] nums)
        {
            var n = nums.Length;
            var gcdVal = CalculateGCDValues(nums);

            var dp = new int[1 << n];

            for (var i = 0; i < 1 << n; ++i)
            {
                var bits = GetSetBitsCount(i);
                if (bits % 2 != 0)
                {
                    // Skip odd numbers
                    continue;
                }

                foreach (var (k, v) in gcdVal)
                {
                    if ((k & i) != 0)
                    {
                        // Skip overlapping numbers
                        continue;
                    }

                    dp[i ^ k] = Math.Max(dp[i ^ k], dp[i] + v * (bits / 2 + 1));
                }
            }

            return dp[(1 << n) - 1];
        }

        private static Dictionary<int, int> CalculateGCDValues(int[] nums)
        {
            var gcdVal = new Dictionary<int, int>();
            var n = nums.Length;

            for (var i = 0; i < n; ++i)
            {
                for (var j = i + 1; j < n; ++j)
                {
                    var key = (1 << i) + (1 << j);
                    var value = GCD(nums[i], nums[j]);
                    gcdVal.Add(key, value);
                }
            }

            return gcdVal;
        }

        private static int GetSetBitsCount(int num)
        {
            var count = 0;
            while (num > 0)
            {
                if ((num & 1) == 1)
                    count++;
                num >>= 1;
            }
            return count;
        }

        private static int GCD(int a, int b)
        {
            return b == 0 ? a : GCD(b, a % b);
        }

        #endregion

        #region Day 15 Problem 1721. Swapping Nodes in a Linked List

        public ListNode SwapNodes(ListNode head, int k)
        {
            k = k - 1;

            ListNode node1 = head;

            while (k > 0)
            {
                node1 = node1.next;
                k--;
            }

            ListNode first = node1;

            ListNode node2 = head;

            while (first.next != null)
            {
                node2 = node2.next;
                first = first.next;
            }

            int temp = node1.val;
            node1.val = node2.val;
            node2.val = temp;

            return head;
        }
        #endregion

        #region Day 16 Problem 24. Swap Nodes in Pairs
        public ListNode SwapPairs(ListNode head)
        {

            if (head != null && head.next != null)
            {
                int temp = head.val;
                head.val = head.next.val;
                head.next.val = temp;

                head.next.next = SwapPairs(head.next.next);
            }
            return head;
        }
        #endregion

        #region Day 17 Problem 2130. Maximum Twin Sum of a Linked List
        public int PairSum(ListNode head)
        {
            int res = 0;
            List<int> lst = new List<int>();

            ListNode node = head;

            while (node != null)
            {
                lst.Add(node.val);
                node = node.next;
                lst.Add(node.val);
                node = node.next;
            }

            for (int i = 0; i < lst.Count / 2; i++)
            {
                res = Math.Max(res, lst[i] + lst[lst.Count - i - 1]);
            }

            return res;
        }
        #endregion

        #region Day 18 Problem 1557. Minimum Number of Vertices to Reach All Nodes

        public IList<int> FindSmallestSetOfVertices(int n, IList<IList<int>> edges)
        {
            HashSet<int> unreachableNodes = new HashSet<int>();

            foreach (var edge in edges)
            {
                int to = edge[1];
                unreachableNodes.Add(to);
            }

            List<int> result = new List<int>();

            for (int i = 0; i < n; i++)
            {
                if (!unreachableNodes.Contains(i))
                {
                    result.Add(i);
                }
            }

            return result;
        }

        #endregion

        #region Day 19 Problem
        #endregion

        #region Day 20 Problem
        #endregion

        #region Day 21 Problem 934. Shortest Bridge
        public int ShortestBridge(int[][] grid)
        {
            int result = 0;


            Queue<(int x, int y)> q = new Queue<(int x, int y)>();
            int[][] dp = new int[grid.Length][];
            for (int i = 0; i < grid.Length; i++)
            {
                dp[i] = new int[grid[i].Length];
                for (int j = 0; j < grid[i].Length; j++)
                {
                    if (grid[i][j] == 1)
                    {
                        q.Enqueue((i, j));
                        dp[i][j] = 0;

                    }
                    else
                    {
                        dp[i][j] = int.MaxValue;
                    }
                }
            }


            while (q.Count > 0)
            {
                var p = q.Dequeue();
                int val = dp[p.x][p.y];
                result = Math.Max(result, val);
                int newVal = val + 1;

                checkAndAdd(dp, q, p.x - 1, p.y, newVal);
                checkAndAdd(dp, q, p.x, p.y - 1, newVal);
                checkAndAdd(dp, q, p.x + 1, p.y, newVal);
                checkAndAdd(dp, q, p.x, p.y + 1, newVal);
            }


            //Queue<(int x, int y, int w)> q2 = new Queue<(int x, int y, int w)>();
            //while (q.Count > 0)
            //{
            //    var p = q.Dequeue();
            //    q2.Enqueue((p.x, p.y, 0));
            //    //i-1 j
            //    //i+1 j
            //    //
            //    checkAndAdd(grid, q, p.x - 1, p.y);
            //    checkAndAdd(grid, q, p.x + 1, p.y);
            //    checkAndAdd(grid, q, visited, p.x, p.y - 1);
            //    checkAndAdd(grid, q, visited, p.x, p.y + 1);

            //}

            //while (q2.Count > 0)
            //{
            //    var s = q2.Dequeue();
            //}

            return result;
        }

        private void checkAndAdd(int[][] dp, Queue<(int x, int y)> q, int x, int y, int val)
        {
            if (x < 0 || y < 0 || x >= dp.Length || y >= dp[0].Length || dp[x][y] <= val) return;

            dp[x][y] = val;
            q.Enqueue((x, y));
        }


        #endregion

        #region Day 22 Problem  347. Top K Frequent Elements

        public int[] TopKFrequent(int[] nums, int k)
        {
            Dictionary<int, int> map = new Dictionary<int, int>();

            foreach (int num in nums)
            {
                if (!map.ContainsKey(num))
                {
                    map.Add(num, 0);
                }
                map[num]++;
            }

            PriorityQueue<int, int> pq = new PriorityQueue<int, int>(Comparer<int>.Create((a, b) => b - a));

            foreach (var key in map.Keys)
            {
                pq.Enqueue(key, map[key]);
            }
            int[] res = new int[k];

            for (int i = 0; i < k; i++)
            {
                res[i] = pq.Dequeue();
            }
            return res;
        }

        #endregion

        #region Day 23 Problem 703. Kth Largest Element in a Stream
        public class KthLargest
        {

            PriorityQueue<int, int> pq;
            readonly int size = 0;
            public KthLargest(int k, int[] nums)
            {
                size = k;
                pq = new PriorityQueue<int, int>();

                for (int i = 0; i < nums.Length; i++)
                {
                    pq.Enqueue(nums[i], nums[i]);
                }

                while (pq.Count > k)
                {
                    pq.Dequeue();
                }
            }

            public int Add(int val)
            {
                if (pq.Count < size)
                {
                    pq.Enqueue(val, val);
                }
                else if (pq.Peek() < val)
                {

                    pq.EnqueueDequeue(val, val);
                }
                return pq.Peek();
            }
        }
        #endregion

        #region Day 24 Problem 2542. Maximum Subsequence Score
        public long MaxScore(int[] nums1, int[] nums2, int k)
        {
            long result = 0;


            List<(int val, int rank)> lst = new List<(int val, int rank)>();

            for (int i = 0; i < nums1.Length; i++)
            {
                lst.Add((nums1[i], nums2[i]));
            }

            lst = lst.OrderByDescending(x => x.rank).ToList();

            long sum = 0;
            PriorityQueue<int, int> pq = new PriorityQueue<int, int>();
            for (int i = 0; i < k; i++)
            {
                sum += lst[i].val;
                pq.Enqueue(lst[i].val, lst[i].val);
            }

            result = sum * lst[k - 1].rank;


            for (int i = k; i < lst.Count; i++)
            {
                int v = lst[i].val;
                int r = lst[i].rank;

                var c = pq.Dequeue();

                sum -= c;
                sum += v;
                pq.Enqueue(v, v);

                result = Math.Max(result, sum * r);
            }

            return result;
        }
        #endregion

        #region Day 25 Problem 837. New 21 Game
        public double New21Game(int n, int k, int maxPts)
        {
            if (k == 0) return 1;
            double result = 0;
            double[] dp = new double[n + 1];

            dp[0] = 1;
            double temp = 1;
            for (int i = 1; i <= n; i++)
            {
                dp[i] = temp / maxPts;

                if (i < k)
                {
                    temp += dp[i];
                }

                if (i - maxPts >= 0 && i - maxPts < k)
                {
                    temp -= dp[i - maxPts];
                }
            }

            for (int i = k; i <= n; i++)
            {
                result += dp[i];
            }

            return result;
        }
        #endregion

        #region Day 26 Problem 1140. Stone Game II
        public int StoneGameII(int[] piles)
        {
            int[,,] dp = new int[2, piles.Length + 1, piles.Length + 1];

            for (int p = 0; p < 2; p++)
            {
                for (int i = 0; i <= piles.Length; i++)
                {
                    for (int j = 0; j <= piles.Length; j++)
                    {
                        dp[p, i, j] = -1;
                    }
                }
            }

            return StoneGameII_Helper(dp, piles, 0, 0, 1);
        }

        private int StoneGameII_Helper(int[,,] dp, int[] piles, int p, int i, int j)
        {
            if (i == piles.Length) return 0;

            if (dp[p, i, j] != -1) return dp[p, i, j];

            int result = p == 1 ? 1000000 : -1;
            int temp = 0;

            for (int x = 1; x <= Math.Min(2 * j, piles.Length - i); x++)
            {
                temp += piles[i + x - 1];

                if (p == 0)
                {
                    result = Math.Max(result, temp + StoneGameII_Helper(dp, piles, 1, i + x, Math.Max(j, x)));
                }
                else
                {
                    result = Math.Min(result, StoneGameII_Helper(dp, piles, 0, i + x, Math.Max(j, x)));
                }
            }

            return dp[p, i, j] = result;
        }
        #endregion

        #region Day 27 Problem 1406. Stone Game III

        public string StoneGameIII(int[] stoneValue)
        {
            int[] dp = new int[stoneValue.Length + 1];

            for (int i = stoneValue.Length - 1; i >= 0; i--)
            {
                dp[i] = stoneValue[i] - dp[i + 1];

                if (i + 2 <= stoneValue.Length)
                {
                    dp[i] = Math.Max(dp[i], stoneValue[i + 1] + stoneValue[i] - dp[i + 2]);
                }
                if (i + 3 <= stoneValue.Length)
                {
                    dp[i] = Math.Max(dp[i], stoneValue[i] + stoneValue[i + 1] + stoneValue[i + 2] - dp[i + 3]);

                }
            }

            return dp[0] == 0 ? "Tie" : dp[0] > 0 ? "Alice" : "Bob";
        }
        #endregion

        #region Day 28 Problem 1547. Minimum Cost to Cut a Stick

        int[,] dp;
        int[] newCuts;
        public int MinCost(int n, int[] cuts)
        {
            int m = cuts.Length;
            newCuts = new int[m + 2];

            Array.Copy(cuts, 0, newCuts, 1, m);
            newCuts[m + 1] = n;
            Array.Sort(newCuts);

            dp = new int[m + 2, m + 2];

            for (int i = 0; i < m + 2; i++)
            {
                for (int j = 0; j < m + 2; j++)
                {
                    dp[i, j] = -1;
                }
            }
            return cost(0, newCuts.Length - 1);
        }

        private int cost(int left, int right)
        {
            if (dp[left, right] != -1) return dp[left, right];

            if (right - left == 1) return 0;

            int result = int.MaxValue;

            for (int mid = left+1; mid < right; mid++)
            {
                int c = cost(left, mid) + cost(mid, right) + newCuts[right] - newCuts[left];

                result = Math.Min(result, c);   
            }

            return dp[left,right] = result;
        }
        #endregion

        #region Day 29 Problem 1603. Design Parking System
        public class ParkingSystem
        {
            private readonly int big;
            private readonly int medium;
            private readonly int small;

            private int bigCars;
            private int mediumCars;
            private int smallCars;
            public ParkingSystem(int big, int medium, int small)
            {
                this.big = big;
                this.medium = medium;
                this.small = small;
                bigCars = 0;
                mediumCars = 0;
                smallCars = 0;
            }

            public bool AddCar(int carType)
            {
                switch (carType)
                {
                    case 1:
                        if (bigCars == big) return false;
                        bigCars++;
                        break;
                    case 2:
                        if (mediumCars == medium) return false;
                        mediumCars++;
                        break;
                    case 3:
                        if(smallCars == small) return false;
                        smallCars++;
                        break;
                    default:
                        return false;
                }
                return true;
            }
        }
        #endregion

        #region Day 30 Problem 705. Design HashSet
        public class MyHashSet
        {
            BitArray arr;

            public MyHashSet()
            {
                arr = new BitArray(1000001);
            }

            public void Add(int key)
            {
                arr[key] = true;
            }

            public void Remove(int key)
            {
                arr[key] = false;
            }

            public bool Contains(int key)
            {
                return arr[key];
            }
        }

        /**
         * Your MyHashSet object will be instantiated and called as such:
         * MyHashSet obj = new MyHashSet();
         * obj.Add(key);
         * obj.Remove(key);
         * bool param_3 = obj.Contains(key);
         */
        #endregion

        #region Day 31 Problem 1396. Design Underground System
        public class UndergroundSystem
        {
            Dictionary<int, (string station, int time)> dct;
            Dictionary<string, List<int>> dctTimes;
            public UndergroundSystem()
            {
                dct = new Dictionary<int, (string station, int time)>();
                dctTimes = new Dictionary<string, List<int>>();
            }

            public void CheckIn(int id, string stationName, int t)
            {
                dct.Add(id,(stationName, t));
            }

            public void CheckOut(int id, string stationName, int t)
            {
                var passenger = dct[id];

                string route = getRoute(passenger.station, stationName);

                if(!dctTimes.ContainsKey(route)) dctTimes.Add(route, new List<int>());

                dctTimes[route].Add(t-passenger.time);

                dct.Remove(id);
            }

            public double GetAverageTime(string startStation, string endStation)
            {

                string route = getRoute(startStation, endStation);

                int total = 0;

                foreach (var item in dctTimes[route])
                {
                    total += item;
                }

                return total / dctTimes[route].Count;
            }

            private string getRoute(string fromStation, string toStation)
            {
                return $"{fromStation}-{toStation}";
            }
        }
        #endregion

        #region  Problem 2670. Find the Distinct Difference Array
        public int[] DistinctDifferenceArray(int[] nums)
        {
            int n = nums.Length;
            int[] diff = new int[n];

            Dictionary<int, List<int>> map = new Dictionary<int, List<int>>();
            for (int i = 0; i < n; i++)
            {
                if (!map.ContainsKey(nums[i]))
                {
                    map.Add(nums[i], new List<int>());
                }
                map[nums[i]].Add(i);
            }
            HashSet<int> prefix = new HashSet<int>();
            for (int i = 0; i < n; i++)
            {
                int k = nums[i];
                prefix.Add(k);

                map[k].Remove(i);

                if (map[k].Count == 0) map.Remove(k);

                diff[i] = prefix.Count - map.Count;
            }
            return diff;
        }

        #endregion

        #region Problem 2671. Frequency Tracker
        public class FrequencyTracker
        {
            readonly Dictionary<int, int> numbermap;
            readonly Dictionary<int, List<int>> frequencyMap;
            public FrequencyTracker()
            {
                numbermap = new Dictionary<int, int>();
                frequencyMap = new Dictionary<int, List<int>>();
            }

            public void Add(int number)
            {
                int freq = 1;
                if (!numbermap.ContainsKey(number))
                {
                    numbermap.Add(number, 0);
                }
                else
                {
                    freq = numbermap[number];

                    frequencyMap[freq].Remove(number);
                    if (frequencyMap[freq].Count == 0)
                    {
                        frequencyMap.Remove(freq);
                    }
                }
                numbermap[number]++;

                freq = numbermap[number];
                updatefrequencymap(freq, number);
            }

            public void DeleteOne(int number)
            {
                if (numbermap.ContainsKey(number))
                {
                    int oldfreq = numbermap[number];
                    frequencyMap[oldfreq].Remove(number);

                    if (frequencyMap[oldfreq].Count == 0) frequencyMap.Remove(oldfreq);

                    if (oldfreq == 1)
                    {
                        numbermap.Remove(number);
                    }
                    else
                    {
                        numbermap[number]--;

                        updatefrequencymap(oldfreq - 1, number);
                    }


                }
            }

            public bool HasFrequency(int frequency)
            {
                return frequencyMap.ContainsKey(frequency);
            }

            private void updatefrequencymap(int freq, int number)
            {
                if (!frequencyMap.ContainsKey(freq))
                {
                    frequencyMap.Add(freq, new List<int>());
                }
                frequencyMap[freq].Add(number);
            }
        }
        #endregion

        #region Problem 2672. Number of Adjacent Elements With the Same Color
        public int[] ColorTheArray(int n, int[][] queries)
        {
            int[] nums = new int[n]; // initialize the array with zeros
            int[] ans = new int[queries.Length]; // initialize the answer array


            return ans;
        }
        #endregion

        #region Weekly 345
        public int[] CircularGameLosers(int n, int k)
        {
            List<int> list = Enumerable.Range(2, n - 1).ToList();


            int initSteps = k;
            if (k >= n)
            {
                k %= n;
            }

            if (k == 0) return list.ToArray();


            int round = 2;

            while (list.Contains(k + 1))
            {
                list.Remove(k + 1);
                k = k + round * initSteps;

                if (k >= n)
                {
                    k %= n;
                }
                round++;
            }

            return list.ToArray();
        }

        public int[] CircularGameLosers_V1(int n, int k)
        {
            bool[] visited = new bool[n];

            visited[0] = true;

            int initSteps = k;
            if (k >= n)
            {
                k %= n;
            }

            if (k == 0) return Enumerable.Range(2, n - 1).ToArray();


            int round = 2;

            while (!visited[k])
            {
                visited[k] = true;
                k = round * initSteps;

                if (k >= n)
                {
                    k %= n;
                }
                round++;
            }
            IList<int> lst = new List<int>();
            for (int i = 0; i < n; i++)
            {
                if (!visited[i]) lst.Add(i + 1);
            }

            return lst.ToArray();
        }

        public bool DoesValidArrayExist(int[] derived)
        {
            return false;
        }

        bool[][] dp2684;
        int m2684;
        Queue<(int x, int y, int w)> q = new Queue<(int x, int y, int w)>();

        public int MaxMoves(int[][] grid)
        {
            int res = 0;
            m2684 = grid.Length;
            int n = grid[0].Length;

            dp2684 = new bool[m2684][];

            for (int i = 0; i < m2684; i++)
            {
                dp2684[i] = new bool[n];
                q.Enqueue((i, 0, 0));
                dp2684[i][0] = true;
            }

            while (q.Count > 0)
            {
                var p = q.Dequeue();

                res = Math.Max(res, p.w);

                int nextCellCol = p.y + 1;

                if (nextCellCol == n) continue;

                int nextWeight = p.w + 1;

                int nextCellRow = p.x;
                checkAndAddv1(grid, grid[p.x][p.y], nextCellRow, nextCellCol, nextWeight);

                nextCellRow = p.x + 1;
                checkAndAddv1(grid, grid[p.x][p.y], nextCellRow, nextCellCol, nextWeight);

                nextCellRow = p.x - 1;
                checkAndAddv1(grid, grid[p.x][p.y], nextCellRow, nextCellCol, nextWeight);
            }

            return res;
        }

        private void checkAndAddv1(int[][] grid, int currentValue, int nextCellRow, int nextCellCol, int nextWeight)
        {
            if (nextCellRow < 0 || nextCellRow >= m2684 || currentValue >= grid[nextCellRow][nextCellCol] || dp2684[nextCellRow][nextCellCol]) return;

            q.Enqueue((nextCellRow, nextCellCol, nextWeight));

            dp2684[nextCellRow][nextCellCol] = true;

        }

        public int MaxMoves_V1(int[][] grid)
        {
            int m = grid.Length;
            int n = grid[0].Length;
            bool[][] dp = new bool[grid.Length][];

            for (int i = 0; i < grid.Length; i++)
            {
                dp[i] = new bool[grid[i].Length];
            }

            Queue<(int x, int y, int w)> q = new Queue<(int x, int y, int w)>();

            for (int i = 0; i < grid.Length; i++)
            {
                q.Enqueue((i, 0, 0));
                dp[i][0] = true;

            }
            int res = 0;
            while (q.Count > 0)
            {
                var p = q.Dequeue();

                res = Math.Max(p.w, res);
                int curVal = grid[p.x][p.y];
                int nextCol = p.y + 1;

                if (nextCol == n) continue;

                if (!dp[p.x][nextCol] && curVal < grid[p.x][nextCol])
                {
                    q.Enqueue((p.x, nextCol, p.w + 1));
                    dp[p.x][nextCol] = true;
                }

                if (p.x - 1 >= 0 && !dp[p.x - 1][nextCol] && curVal < grid[p.x - 1][nextCol])
                {
                    q.Enqueue((p.x - 1, nextCol, p.w + 1));
                    dp[p.x - 1][nextCol] = true;
                }

                if (p.x + 1 < m && !dp[p.x + 1][nextCol] && curVal < grid[p.x + 1][nextCol])
                {
                    q.Enqueue((p.x + 1, nextCol, p.w + 1));
                    dp[p.x + 1][nextCol] = true;
                }

            }

            return res;
        }

        public int CountCompleteComponents(int n, int[][] edges)
        {


            return 0;
        }

        public int CountCompleteComponents_V1(int n, int[][] edges)
        {
            Dictionary<int, List<int>> map = new Dictionary<int, List<int>>();

            for (int i = 0; i < n; i++)
            {
                map.Add(i, new List<int>());
            }

            foreach (var arr in edges)
            {
                map[arr[0]].Add(arr[1]);
                map[arr[1]].Add(arr[0]);
            }

            List<List<int>> lst = new List<List<int>>();

            bool[] visited = new bool[n];
            int count = 0;
            for (int i = 0; i < visited.Length; i++)
            {
                if (!visited[i])
                {
                    List<int> ll = new List<int>();
                    visited[i] = true;
                    ll.Add(i);
                    Queue<int> q = new Queue<int>();

                    foreach (var val in map[i])
                    {
                        if (!visited[val])
                        {
                            q.Enqueue(val);
                            visited[val] = true;
                            ll.Add(val);
                        }
                    }

                    while (q.Count > 0)
                    {
                        var p = q.Dequeue();

                        foreach (var item in map[p])
                        {
                            if (!visited[item])
                            {
                                visited[item] = true;
                                q.Enqueue(item);
                                ll.Add(item);
                            }
                        }
                    }

                    bool isComplete = true;

                    foreach (var item in ll)
                    {
                        if (map[item].Count != ll.Count - 1)
                        {
                            isComplete = false;
                            break;
                        }
                    }

                    if (isComplete) count++;

                }
            }

            return count;
        }
        #endregion

        #region Weekly 346
        public int MinLength(string s)
        {

            Stack<char> stack = new Stack<char>();

            int i = 0;

            while (i < s.Length)
            {
                if (stack.Count == 0)
                {
                    stack.Push(s[i++]);
                    continue;
                }

                switch (s[i])
                {
                    case 'B':
                        if (stack.Peek() == 'A')
                        {
                            stack.Pop();
                            i++;
                        }
                        else
                        {
                            stack.Push(s[i++]);

                        }
                        break;
                    case 'D':
                        if (stack.Peek() == 'C')
                        {
                            stack.Pop();
                            i++;
                        }
                        else
                        {
                            stack.Push(s[i++]);

                        }
                        break;
                    default:
                        stack.Push(s[i++]);
                        break;
                }
            }

            return stack.Count;
            //List<char> lst = s.ToCharArray().ToList();

            //for (int i = 0; i < lst.Count-1; i++)
            //{
            //    if (lst[i] == 'A')
            //    {
            //        if (lst[i+1] == 'B')
            //        {
            //            lst.RemoveAt(i + 1);
            //            lst.RemoveAt(i);
            //        }
            //        i--;
            //    }
            //    else if (lst[i] == 'C')
            //    {

            //        if (lst[i + 1] == 'D')
            //        {
            //            lst.RemoveAt(i + 1);
            //            lst.RemoveAt(i);
            //        }
            //        i--;
            //    }
            //}

            //return lst.Count;
        }

        public string MakeSmallestPalindrome(string s)
        {
            int start = 0;
            int end = s.Length - 1;
            char[] chars = s.ToCharArray();
            while (start <= end)
            {
                if (chars[start] == chars[end]) continue;

                if (chars[start] > chars[end])
                {
                    chars[start] = chars[end];

                }
                else
                {
                    chars[end] = chars[start];
                }
                start++;
                end--;
            }

            return new string(chars);
        }

        public int PunishmentNumber(int n)
        {
            int sum = 0;

            for (int i = 1; i <= n; i++)
            {
                int square = i * i;
                string squareString = square.ToString();

                int substringSum = 0;
                bool isValid = true;

                for (int j = 0; j < squareString.Length; j++)
                {
                    substringSum += squareString[j] - '0';

                    if (substringSum > i)
                    {
                        isValid = false;
                        break;
                    }
                }

                if (isValid && substringSum == i)
                {
                    sum += square;
                }
            }

            return sum;
        }


        public int[][] ModifiedGraphEdges(int n, int[][] edges, int source, int destination, int target)
        {
            // Create a adjacency list representation of the graph
            Dictionary<int, List<int[]>> graph = new Dictionary<int, List<int[]>>();
            foreach (var edge in edges)
            {
                int u = edge[0];
                int v = edge[1];
                int weight = edge[2];

                if (!graph.ContainsKey(u))
                {
                    graph[u] = new List<int[]>();
                }
                graph[u].Add(new int[] { v, weight });

                if (!graph.ContainsKey(v))
                {
                    graph[v] = new List<int[]>();
                }
                graph[v].Add(new int[] { u, weight });
            }

            // Perform Dijkstra's algorithm to find the shortest distance from source to all other nodes
            int[] distances = new int[n];
            Array.Fill(distances, Int32.MaxValue);
            distances[source] = 0;

            PriorityQueue1<(int, int)> pq = new PriorityQueue1<(int, int)>((a, b) => a.Item2.CompareTo(b.Item2));
            pq.Enqueue((source, 0));

            while (pq.Count > 0)
            {
                (int x, int y) node = pq.Dequeue();
                int u = node.x;
                int dist = node.y;

                if (dist > distances[u])
                {
                    continue;
                }

                if (graph.ContainsKey(u))
                {
                    foreach (int[] edge in graph[u])
                    {
                        int v = edge[0];
                        int weight = edge[1];

                        if (weight == -1)
                        {
                            weight = 1;
                        }

                        int newDist = dist + weight;
                        if (newDist < distances[v])
                        {
                            distances[v] = newDist;
                            pq.Enqueue((v, newDist));
                        }
                    }
                }
            }

            // Check if it is possible to make the shortest distance from source to destination equal to target
            if (distances[destination] == target)
            {
                return edges;
            }

            return new int[][] { };
        }

        // Implementation of PriorityQueue using Binary Heap
        public class PriorityQueue1<T> where T : IComparable<T>
        {
            private List<T> heap;
            private Comparison<T> compare;

            public PriorityQueue1()
            {
                heap = new List<T>();
                compare = Comparer<T>.Default.Compare;
            }

            public PriorityQueue1(Comparison<T> comparison)
            {
                heap = new List<T>();
                compare = comparison;
            }

            public int Count
            {
                get { return heap.Count; }
            }

            public void Enqueue(T item)
            {
                heap.Add(item);
                int i = heap.Count - 1;

                while (i > 0)
                {
                    int parent = (i - 1) / 2;

                    if (compare(heap[parent], item) <= 0)
                    {
                        break;
                    }

                    heap[i] = heap[parent];
                    i = parent;
                }

                heap[i] = item;
            }

            public T Dequeue()
            {
                T item = heap[0];
                int lastIndex = heap.Count - 1;
                T lastItem = heap[lastIndex];
                heap.RemoveAt(lastIndex);

                if (lastIndex > 0)
                {
                    int i = 0;
                    while (i < lastIndex)
                    {
                        int child = 2 * i + 1;

                        if (child < lastIndex && compare(heap[child], heap[child + 1]) > 0)
                        {
                            child++;
                        }

                        if (compare(lastItem, heap[child]) <= 0)
                        {
                            break;
                        }

                        heap[i] = heap[child];
                        i = child;
                    }

                    heap[i] = lastItem;
                }

                return item;
            }
        }

        #endregion

        #region Weekly 347
        public string RemoveTrailingZeros(string num)
        {
            int i = num.Length - 1;

            while (i >= 0)
            {
                if (num[i] != '0') break;
                i--;
            }

            return num.Substring(0, i + 1);
        }

        public int[][] DiagonalDifference(int[][] grid)
        {
            int m = grid.Length;
            int n = grid[0].Length;

            int[][] topLeft = new int[m][];
            int[][] bottomRight = new int[m][];
            int[][] answer = new int[m][];

            for (int i = 0; i < m; i++)
            {
                topLeft[i] = new int[n];
                bottomRight[i] = new int[n];
                answer[i] = new int[n];
            }

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    // Calculate top-left diagonal values
                    int r = i - 1;
                    int c = j - 1;
                    HashSet<int> distinctTopLeft = new HashSet<int>();
                    while (r >= 0 && c >= 0)
                    {
                        distinctTopLeft.Add(grid[r][c]);
                        r--;
                        c--;
                    }
                    topLeft[i][j] = distinctTopLeft.Count;

                    // Calculate bottom-right diagonal values
                    r = i + 1;
                    c = j + 1;
                    HashSet<int> distinctBottomRight = new HashSet<int>();
                    while (r < m && c < n)
                    {
                        distinctBottomRight.Add(grid[r][c]);
                        r++;
                        c++;
                    }
                    bottomRight[i][j] = distinctBottomRight.Count;

                    // Calculate answer
                    answer[i][j] = Math.Abs(topLeft[i][j] - bottomRight[i][j]);
                }
            }

            return answer;
        }

        public long MinimumCost(string s)
        {
            int n = s.Length;
            long cost = 0;
            int countOnes = 0;
            int countZeros = 0;

            // Count the number of ones and zeros in the string
            foreach (char c in s)
            {
                if (c == '1')
                    countOnes++;
                else
                    countZeros++;
            }

            int minCost = Math.Min(countOnes, countZeros);

            // Calculate the cost of making all characters equal to '0'
            for (int i = 0; i < n; i++)
            {
                if (s[i] != '0')
                    cost += i + 1;
            }

            // Calculate the cost of making all characters equal to '1'
            long currCost = cost;
            for (int i = n - 1; i >= 0; i--)
            {
                if (s[i] != '1')
                    currCost += n - i;

                cost = Math.Min(cost, currCost);
            }

            return Math.Min(cost, minCost);
        }

        #endregion
    }
}
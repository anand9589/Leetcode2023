﻿using Common;
using System;

namespace Jun
{
    public class Solution
    {
        #region Day 1 Problem
        #endregion

        #region Day 2 Problem 2101. Detonate the Maximum Bombs
        public int MaximumDetonation(int[][] bombs)
        {
            int result = 0;
            Dictionary<int, List<int>> map = new Dictionary<int, List<int>>();

            int len = bombs.Length;

            for (int i = 0; i < len; i++)
            {
                map.Add(i, new List<int>());
            }

            for (int i = 0; i < len; i++)
            {
                for (int j = 0; j < len; j++)
                {
                    if (i == j) continue;

                    int x1 = bombs[i][0];
                    int y1 = bombs[i][1];
                    int r1 = bombs[i][2];
                    int x2 = bombs[j][0];
                    int y2 = bombs[j][1];
                    int r2 = bombs[j][2];

                    if ((long)(r1 * r2) >= (long)(x1 - x2) * (long)(x1 - x2) + (long)(y1 - y2) * (long)(y1 - y2))
                    {
                        map[i].Add(j);
                    }
                }
            }


            return result;
        }

        private int dfs(int cur, bool[] visited, Dictionary<int, List<int>> map)
        {
            visited[cur] = true;

            int count = 1;

            foreach (var item in map[cur])
            {
                if (!visited[cur])
                {
                    count += dfs(item, visited, map);
                }
            }
            return count;
        }
        #endregion

        #region Day 3 Problem
        #endregion

        #region Day 4 Problem 547. Number of Provinces
        public int FindCircleNum(int[][] isConnected)
        {
            int n = isConnected.Length;

            Dictionary<int, List<int>> dct = new Dictionary<int, List<int>>();

            for (int i = 0; i < n; i++)
            {
                dct.Add(i, new List<int>());

                for (int j = 0; j < n; j++)
                {
                    if (i == j) continue;

                    if (isConnected[i][j] == 1) dct[i].Add(j);
                }
            }

            bool[] visited = new bool[n];

            int count = 0;

            Queue<int> queue = new Queue<int>();

            foreach (var key in dct.Keys)
            {
                if (!visited[key])
                {
                    count++;
                    queue.Enqueue(key);
                    visited[key] = true;
                    while (queue.Count > 0)
                    {
                        var p = queue.Dequeue();

                        foreach (var item in dct[p])
                        {
                            if (!visited[item])
                            {
                                queue.Enqueue(item);
                                visited[item] = true;
                            }
                        }

                    }

                }
            }

            return count;



        }
        #endregion

        #region Day 5 Problem 1232. Check If It Is a Straight Line


        public bool CheckStraightLine(int[][] coordinates)
        {
            int diffX = coordinates[1][0] - coordinates[0][0];
            int diffY = coordinates[1][1] - coordinates[0][1];

            for (int i = 2; i < coordinates.Length; i++)
            {
                if (diffY * (coordinates[i][0] - coordinates[0][0]) != (diffX * (coordinates[i][1] - coordinates[0][1]))) return false;
            }

            return true;
        }
        #endregion

        #region Day 6 Problem 1502. Can Make Arithmetic Progression From Sequence
        public bool CanMakeArithmeticProgression(int[] arr)
        {
            Array.Sort(arr);

            int n = arr.Length;
            if (n == 2) return true;

            int diff = arr[1] - arr[0];

            int i = 2;
            int j = n - 1;
            while (i < j)
            {
                if (arr[i] - arr[i - 1] != diff) return false;
                if (arr[j] - arr[j - 1] != diff) return false;
                i++;
                j--;
            }

            return true;
        }
        #endregion

        #region Day 7 Problem
        #endregion

        #region Day 8 Problem
        #endregion

        #region Day 9 Problem
        #endregion

        #region Day 10 Problem 1802. Maximum Value at a Given Index in a Bounded Array
        public int MaxValue(int n, int index, int maxSum)
        {
            int left = 1;
            int right = maxSum;

            while (left < right)
            {
                int mid = (left + right + 1) / 2;
                if (getSum(index, mid, n) <= maxSum)
                {
                    left = mid;
                }
                else
                {
                    right = mid - 1;
                }
            }

            return left;
        }

        private long getSum(int index, int value, int n)
        {
            long count = 0;

            if (value > index)
            {
                count += (long)(value + value - index) * (index + 1) / 2;
            }
            else
            {
                count += (long)(value + 1) * value / 2 + index - value + 1;
            }


            if (value >= n - index)
            {
                count += (long)(value + value - n + 1 + index) * (n - index) / 2;
            }
            else
            {
                count += (long)(value + 1) * value / 2 + n - index - value;
            }


            return count - value;
        }
        #endregion

        #region Day 11 Problem 1146. Snapshot Array
        public class SnapshotArray
        {
            int snapCount = 0;
            readonly Dictionary<int, int>[] versions;
            public SnapshotArray(int length)
            {
                versions = new Dictionary<int, int>[length];
            }

            public void Set(int index, int val)
            {
                if (versions[index] == null) versions[index] = new Dictionary<int, int>();

                if (!versions[index].ContainsKey(snapCount))
                {
                    versions[index].Add(snapCount, val);
                }
                else
                {
                    versions[index][snapCount] = val;
                }
            }

            public int Snap()
            {
                return snapCount++;
            }

            public int Get(int index, int snap_id)
            {
                if (versions[index] == null) return 0;

                var keys = versions[index].Keys.ToArray();

                int low = 0;
                int high = keys.Length - 1;
                if (keys[low] > snap_id)
                {
                    return 0;
                }

                while (low <= high)
                {
                    if (keys[low] == snap_id) return versions[index][keys[low]];

                    if (keys[low] > snap_id) return versions[index][keys[low - 1]];

                    if (keys[high] <= snap_id) return versions[index][keys[high]];

                    int mid = (low + high) / 2;

                    if (keys[mid] == snap_id) return versions[index][keys[mid]];


                    if (keys[mid] > snap_id)
                    {
                        high = mid - 1;
                    }
                    else
                    {
                        low = mid + 1;
                    }
                }
                return versions[index][keys[low]];
            }
        }
        #endregion

        #region Day 12 Problem
        #endregion

        #region Day 13 Problem
        #endregion

        #region Day 14 Problem 530. Minimum Absolute Difference in BST
        int diff_530 = int.MaxValue;
        TreeNode prev_530;
        public int GetMinimumDifference(TreeNode root)
        {
            inorderTraversal(root);
            return diff_530;
        }

        void inorderTraversal(TreeNode root)
        {
            if (root == null) return;

            inorderTraversal(root.left);

            if (prev_530 != null)
            {
                diff_530 = Math.Min(diff_530, root.val - prev_530.val);
            }

            prev_530 = root;

            inorderTraversal(root.right);
        }
        #endregion

        #region Day 15 Problem 1161. Maximum Level Sum of a Binary Tree
        public int MaxLevelSum(TreeNode root)
        {
            int resultLevel = 1;

            Dictionary<int, int> maps = new Dictionary<int, int>();
            Queue<(TreeNode node, int level)> queue = new Queue<(TreeNode node, int level)>();

            queue.Enqueue((root, resultLevel));
            while (queue.Count > 0)
            {
                var p = queue.Dequeue();

                if (!maps.ContainsKey(p.level))
                {
                    maps.Add(p.level, 0);
                }

                maps[p.level] += p.node.val;

                if (p.node.left != null)
                {
                    queue.Enqueue((p.node.left, p.level + 1));
                }

                if (p.node.right != null)
                {
                    queue.Enqueue((p.node.right, p.level + 1));
                }
            }


            return maps.OrderByDescending(x => x.Value).First().Key;
        }
        #endregion

        #region Day 16 Problem 1569. Number of Ways to Reorder Array to Get Same BST
        private long[,] table_1569;

        public int NumOfWays(int[] nums)
        {
            int m = nums.Length;

            // Table of Pascal's triangle
            BuildPascalsTriangle(m);

            List<int> arrList = nums.ToList();
            return (int)((DFS(arrList) - 1) % Mod);
        }

        private void BuildPascalsTriangle(int m)
        {
            table_1569 = new long[m, m];
            for (int i = 0; i < m; i++)
            {
                table_1569[i, 0] = table_1569[i, i] = 1;
                for (int j = 1; j < i; j++)
                {
                    table_1569[i, j] = (table_1569[i - 1, j - 1] + table_1569[i - 1, j]) % Mod;
                }
            }
        }

        private long DFS(List<int> nums)
        {
            int m = nums.Count;
            if (m < 3)
            {
                return 1;
            }

            List<int> leftNodes = new List<int>();
            List<int> rightNodes = new List<int>();
            int root = nums[0];

            for (int i = 1; i < m; i++)
            {
                if (nums[i] < root)
                {
                    leftNodes.Add(nums[i]);
                }
                else
                {
                    rightNodes.Add(nums[i]);
                }
            }

            long leftWays = DFS(leftNodes) % Mod;
            long rightWays = DFS(rightNodes) % Mod;
            int leftSize = leftNodes.Count;

            return (((leftWays * rightWays) % Mod) * table_1569[m - 1, leftSize]) % Mod;
        }


        #endregion

        #region Day 17 Problem
        #endregion

        #region Day 18 Problem 2328. Number of Increasing Paths in a Grid


        int Mod = 1_000_000_007;
        int[][] dp;
        public int CountPaths(int[][] grid)
        {
            int result = 0;
            dp = new int[grid.Length][];

            for (int i = 0; i < grid.Length; i++)
            {
                dp[i] = new int[grid[0].Length];
            }

            for (int i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid[0].Length; j++)
                {
                    result = (result + DFS(grid, i, j)) % Mod;
                }
            }
            return result;
        }
        int DFS(int[][] grid, int i, int j)
        {
            if (dp[i][j] != 0) return dp[i][j];

            int answer = 1;

            foreach (int[] dir in directions)
            {
                int pi = i + dir[0];
                int pj = j + dir[1];

                if (0 <= pi && pi < grid.Length && 0 <= pj && pj < grid[0].Length && grid[pi][pj] < grid[i][j])
                {
                    answer += (DFS(grid, pi, pj) % Mod);
                    answer %= Mod;
                }
            }

            return dp[i][j] = answer;
        }
        #endregion

        #region Day 19 Problem 1732. Find the Highest Altitude
        public int LargestAltitude(int[] gain)
        {
            int result = 0;
            int K = 0;
            foreach (int x in gain)
            {
                K += x;

                result = Math.Max(result, K);
            }

            return result;
        }
        #endregion

        #region Day 20 Problem 2090. K Radius Subarray Averages
        public int[] GetAverages(int[] nums, int k)
        {
            if (k == 0) return nums;

            int[] result = Enumerable.Repeat(-1, nums.Length).ToArray();

            int high = k * 2;
            if (high + 1 > nums.Length) return result;

            int low = 0;
            long sum = 0;
            while (low <= high)
            {
                sum += nums[low];
                low++;
            }

            low = 0;
            int divider = high - low + 1;

            int r = (int)(sum / divider);

            result[low + k] = r;
            low++;
            high++;
            while (high < nums.Length)
            {
                sum -= nums[low - 1];
                sum += nums[high];

                r = (int)(sum / divider);

                result[low + k] = r;
                low++;
                high++;
            }
            return result;


        }
        #endregion

        #region Day 21 Problem 2448. Minimum Cost to Make Array Equal

        public long MinCost(int[] nums, int[] cost)
        {
            long result = 0;

            int n = nums.Length;

            List<(int v, int w)> lst = new List<(int v, int w)>();


            for (int i = 0; i < n; i++)
            {
                lst.Add((nums[i], cost[i]));
            }

            lst = lst.OrderBy(x => x.v).ToList();

            long[] prefixCost = new long[n];

            prefixCost[0] = lst[0].w;

            for (int i = 1; i < n; i++)
            {
                prefixCost[i] = lst[i].w + prefixCost[i - 1];
            }

            long totalCost = 0;

            for (int i = 1; i < n; i++)
            {
                totalCost += (long)lst[i].w * (long)(lst[i].v - lst[0].v);
            }

            result = totalCost;

            for (int i = 1; i < n; i++)
            {
                int gap = lst[i].v - lst[i - 1].v;

                totalCost += (long)prefixCost[i - 1] * (long)gap;

                totalCost -= (long)(prefixCost[n - 1] - prefixCost[i - 1]) * (long)gap;

                result = Math.Min(result, totalCost);
            }

            return result;
        }

        public long MinCost_v1(int[] nums, int[] cost)
        {
            int l = 1000001;
            int r = 0;

            foreach (int num in nums)
            {
                l = Math.Min(l, num);
                r = Math.Max(r, num);
            }

            long result = getCost(nums, cost, nums[0]);

            while (l < r)
            {
                int mid = (l + r) / 2;

                long cost1 = getCost(nums, cost, nums[mid]);
                long cost2 = getCost(nums, cost, nums[mid + 1]);

                result = Math.Min(cost1, cost2);

                if (cost1 > cost2)
                {
                    l = mid + 1;
                }
                else
                {
                    r = mid;
                }
            }

            return result;
        }

        private long getCost(int[] nums, int[] cost, int v)
        {
            long result = 0L;

            for (int i = 0; i < nums.Length; i++)
            {
                result += 1L * Math.Abs(nums[i] - v) * cost[i];
            }

            return result;
        }
        #endregion

        #region Day 22 Problem 714. Best Time to Buy and Sell Stock with Transaction Fee
        public int MaxProfit(int[] prices, int fee)
        {
            int cash = 0;
            int hold = -prices[0];

            for (int i = 1; i < prices.Length; i++)
            {
                int prevCash = cash;
                cash = Math.Max(cash, hold + prices[i] - fee);
                hold = Math.Max(hold, prevCash - prices[i]);
            }

            return cash;
        }
        #endregion

        #region Day 23 Problem 1027. Longest Arithmetic Subsequence
        public int LongestArithSeqLength(int[] nums)
        {
            int n = nums.Length;
            Dictionary<int, Dictionary<int, int>> dp = new Dictionary<int, Dictionary<int, int>>();
            int maxLen = 2;

            for (int i = 0; i < n; i++)
            {
                dp[i] = new Dictionary<int, int>();

                for (int j = 0; j < i; j++)
                {
                    int diff = nums[i] - nums[j];

                    if (dp[j].ContainsKey(diff))
                    {
                        dp[i][diff] = dp[j][diff] + 1;
                    }
                    else
                    {
                        dp[i][diff] = 2;
                    }

                    maxLen = Math.Max(maxLen, dp[i][diff]);
                }
            }

            return maxLen;

        }

        #endregion

        #region Day 24 Problem 956. Tallest Billboard
        public int TallestBillboard(int[] rods)
        {
            // dp[taller - shorter] = taller
            Dictionary<int, int> dp = new Dictionary<int, int>();
            dp.Add(0, 0);

            foreach (int r in rods)
            {
                // newDp means we don't add r to these stands.
                Dictionary<int, int> newDp = new Dictionary<int, int>(dp);

                foreach (KeyValuePair<int, int> entry in dp)
                {
                    int diff = entry.Key;
                    int taller = entry.Value;
                    int shorter = taller - diff;

                    // Add r to the taller stand, thus the height difference is increased to diff + r.
                    int newTaller = newDp.GetValueOrDefault(diff + r, 0);
                    newDp[diff + r] = Math.Max(newTaller, taller + r);

                    // Add r to the shorter stand, the height difference is expressed as abs(shorter + r - taller).
                    int newDiff = Math.Abs(shorter + r - taller);
                    int newTaller2 = Math.Max(shorter + r, taller);
                    newDp[newDiff] = Math.Max(newTaller2, newDp.GetValueOrDefault(newDiff, 0));
                }

                dp = newDp;
            }

            // Return the maximum height with 0 difference.
            return dp.GetValueOrDefault(0, 0);
        }


        #endregion

        #region Day 25 Problem
        #endregion

        #region Day 26 Problem 2462. Total Cost to Hire K Workers
        public long TotalCost(int[] costs, int k, int candidates)
        {
            PriorityQueue<int, int> headWorkers = new PriorityQueue<int, int>();
            PriorityQueue<int, int> tailWorkers = new PriorityQueue<int, int>();

            for (int i = 0; i < candidates; i++)
            {
                headWorkers.Enqueue(costs[i], costs[i]);
            }

            for (int i = Math.Max(candidates, costs.Length - candidates); i < costs.Length; i++)
            {
                tailWorkers.Enqueue(costs[i], costs[i]);
            }

            int nextHead = candidates;
            int nextTail = costs.Length - 1 - candidates;

            long result = 0;

            for (int i = 0; i < k; i++)
            {
                if (tailWorkers.Count == 0 || headWorkers.Count > 0 && headWorkers.Peek() <= tailWorkers.Peek())
                {
                    result += headWorkers.Dequeue();

                    if (nextHead <= nextTail)
                    {
                        headWorkers.Enqueue(costs[nextHead], costs[nextHead++]);
                    }
                }
                else
                {
                    result += tailWorkers.Dequeue();

                    if (nextHead <= nextTail)
                    {
                        tailWorkers.Enqueue(costs[nextTail], costs[nextTail--]);
                    }
                }
            }

            return result;
        }
        #endregion

        #region Day 27 Problem 373. Find K Pairs with Smallest Sums

        public IList<IList<int>> KSmallestPairs_v1(int[] nums1, int[] nums2, int k)
        {
            IList<IList<int>> result = new List<IList<int>>();

            PriorityQueue<(int n1, int n2), int> q = new PriorityQueue<(int n1, int n2), int>(Comparer<int>.Create((x, y) => y - x));


            q.Enqueue((nums1[0], nums2[0]), nums1[0] + nums2[0]);
            for (int i = 0; i < nums1.Length; i++)
            {
                for (int j = 0; j < nums1.Length; j++)
                {
                    if (i == 0 && j == 0) continue;
                    int csum = nums1[i] + nums2[j];

                    if (q.Count < k)
                    {
                        q.Enqueue((nums1[i], nums2[j]), csum);
                    }
                    else
                    {
                        q.EnqueueDequeue((nums1[i], nums2[j]), csum);
                    }
                }
            }

            while (q.Count > 0)
            {
                var c = q.Dequeue();

                result.Add(new List<int>() { c.n1, c.n2 });
            }

            return result;
        }
        public IList<IList<int>> KSmallestPairs(int[] nums1, int[] nums2, int k)
        {
            int m = nums1.Length;
            int n = nums2.Length;

            IList<IList<int>> result = new List<IList<int>>();
            bool[,] visited = new bool[m, n];
            //HashSet<KeyValuePair<int, int>> visited = new HashSet<KeyValuePair<int, int>>();

            //PriorityQueue<(int i, int j), int> pq = new PriorityQueue<(int i, int j), int>(Comparer<int>.Create((x, y) => y - x));
            PriorityQueue<(int i, int j), int> pq = new PriorityQueue<(int i, int j), int>();
            pq.Enqueue((0, 0), nums1[0] + nums2[0]);
            visited[0, 0] = true;

            while (k-- > 0 && pq.Count > 0)
            {
                (int i, int j) = pq.Dequeue();

                result.Add(new List<int> { nums1[i], nums2[j] });

                if (i + 1 < m && !visited[i + 1, j])
                {
                    pq.Enqueue((i + 1, j), nums1[i + 1] + nums2[j]);
                    visited[i + 1, j] = true;
                }

                if (j + 1 < n && !visited[i, j + 1])
                {
                    pq.Enqueue((i, j + 1), nums1[i] + nums2[j + 1]);
                    visited[i, j + 1] = true;
                }
            }

            return result;


        }
        public IList<IList<int>> KSmallestPairs_v4(int[] nums1, int[] nums2, int k)
        {
            int m = nums1.Length;
            int n = nums2.Length;

            IList<IList<int>> result = new List<IList<int>>();
            result.Add(new List<int> { nums1[0], nums2[0] });
            k--;
            if (m == 1 && n == 1 || k == 0) return result;

            if (m == 1)
            {
                int i = 1;
                while (k-- > 0 && i < n)
                {
                    result.Add(new List<int> { nums1[0], nums2[i++] });
                }
                return result;
            }

            if (n == 1)
            {
                int i = 1;
                while (k-- > 0 && i < m)
                {
                    result.Add(new List<int> { nums1[i++], nums2[0] });
                }
                return result;
            }

            (int i, int j, int sum) top1 = (0, 1, nums1[0] + nums1[1]);
            (int i, int j, int sum) top2 = (1, 0, nums1[1] + nums1[0]);

            bool[,] visited = new bool[m, n];
            visited[0, 0] = true;

            while (k-- > 0)
            {
                if (top1.sum <= top2.sum)
                {
                    visited[top1.i, top1.j] = true;
                    result.Add(new List<int> { nums1[top2.i], nums2[top2.j] });

                    top1.j++;
                }
                else
                {
                    visited[top2.i, top2.j] = true;
                    result.Add(new List<int> { nums1[top2.i], nums2[top2.j] });

                    top2.i++;
                }
            }

            return result;
        }
        public IList<IList<int>> KSmallestPairs_v3(int[] nums1, int[] nums2, int k)
        {
            int m = nums1.Length;
            int n = nums2.Length;


            IList<IList<int>> result = new List<IList<int>>();
            HashSet<KeyValuePair<int, int>> visited = new HashSet<KeyValuePair<int, int>>();

            PriorityQueue<(int n1, int n2), int> pq = new PriorityQueue<(int n1, int n2), int>(Comparer<int>.Create((x, y) => y - x));
            pq.Enqueue((0, 0), nums1[0] + nums2[0]);
            visited.Add(new KeyValuePair<int, int>(0, 0));

            while (k-- > 0 && pq.Count != 0)
            {
                (int i, int j) = pq.Dequeue();

                result.Add(new List<int> { nums1[i], nums2[j] });

                if (i + 1 < m && !visited.Contains(new KeyValuePair<int, int>(i + 1, j)))
                {
                    pq.Enqueue((i + 1, j), nums1[i + 1] + nums2[j]);
                    visited.Add(new KeyValuePair<int, int>(i + 1, j));
                }

                if (j + 1 < n && !visited.Contains(new KeyValuePair<int, int>(i, j + 1)))
                {
                    pq.Enqueue((i, j + 1), nums1[i] + nums2[j + 1]);
                    visited.Add(new KeyValuePair<int, int>(i, j + 1));
                }
            }

            return result;
        }

        public IList<IList<int>> KSmallestPairs_v2(int[] nums1, int[] nums2, int k)
        {
            int n1Len = nums1.Length;
            int n2Len = nums2.Length;

            if (n1Len == 1 && n2Len == 1) return new List<IList<int>>() { new List<int> { nums1[0], nums2[0] } };

            IList<IList<int>> result = new List<IList<int>>();
            if (n1Len == 1)
            {
                for (int i = 0; i < k; i++)
                {
                    result.Add(new List<int> { nums1[0], nums2[i] });
                }

                return result;
            }

            if (n2Len == 1)
            {

                for (int i = 0; i < k; i++)
                {
                    result.Add(new List<int> { nums1[0], nums2[i] });
                }

                return result;
            }

            //PriorityQueue<(int n1, int n2), int> q = new PriorityQueue<(int n1, int n2), int>(Comparer<int>.Create((x, y) => y - x));

            int n1Index1 = 0;
            int n1Index2 = 1;
            int n2Index1 = 0;
            int n2Index2 = 1;

            int valSum = nums1[n1Index1] + nums2[n2Index1];

            //q.Enqueue((nums1[n1Index1], nums2[n2Index1]), valSum);
            result.Add(new List<int>() { nums1[n1Index1], nums2[n2Index1] });
            k--;
            while (k > 0 && n2Index2 < nums2.Length && n1Index2 < nums1.Length)
            {
                int n11 = nums1[n1Index1];
                int n12 = nums1[n1Index2];
                int n21 = nums2[n2Index1];
                int n22 = nums2[n2Index2];

                int sum1 = n11 + n22;
                int sum2 = n21 + n12;

                k--;
                if (sum1 <= sum2)
                {
                    result.Add(new List<int> { n11, n22 });

                    n2Index2++;
                    if (n2Index2 == nums2.Length)
                    {
                        n1Index1++;
                        n2Index2 = n2Index1 + 1;

                        if (n1Index1 == nums1.Length) break;
                    }

                }
                else
                {

                    result.Add(new List<int> { n21, n12 });

                    n1Index2++;
                    if (n1Index2 == nums1.Length)
                    {
                        n2Index1++;
                        n1Index2 = n1Index1 + 1;

                        if (n2Index1 == nums2.Length) break;
                    }
                }

            }

            if (k > 0)
            {
                //if(n2Index2 == )
            }


            //while (q.Count > 0)
            //{
            //    var c = q.Dequeue();

            //    result.Add(new List<int>() { c.n1, c.n2 });
            //}

            return result;
        }

        #endregion

        #region Day 28 Problem 864. Shortest Path to Get All Keys
        public int ShortestPathAllKeys(string[] grid)
        {
            int m = grid.Length;
            int n = grid[0].Length;
            Queue<int[]> queue = new Queue<int[]>();
            int[][] moves = new int[][] { new int[] { 0, 1 }, new int[] { 1, 0 }, new int[] { -1, 0 }, new int[] { 0, -1 } };

            Dictionary<int, HashSet<(int, int)>> seen = new Dictionary<int, HashSet<(int, int)>>();

            HashSet<char> keySet = new HashSet<char>();
            HashSet<char> lockSet = new HashSet<char>();
            int allKeys = 0;
            int startR = -1, startC = -1;
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    char cell = grid[i][j];
                    if (cell >= 'a' && cell <= 'f')
                    {
                        allKeys += (1 << (cell - 'a'));
                        keySet.Add(cell);
                    }
                    if (cell >= 'A' && cell <= 'F')
                    {
                        lockSet.Add(cell);
                    }
                    if (cell == '@')
                    {
                        startR = i;
                        startC = j;
                    }
                }
            }

            queue.Enqueue(new int[] { startR, startC, 0, 0 });
            seen[0] = new HashSet<(int, int)>();
            seen[0].Add((startR, startC));

            while (queue.Count > 0)
            {
                int[] cur = queue.Dequeue();
                int curR = cur[0], curC = cur[1], keys = cur[2], dist = cur[3];
                foreach (int[] move in moves)
                {
                    int newR = curR + move[0], newC = curC + move[1];

                    if (newR >= 0 && newR < m && newC >= 0 && newC < n && grid[newR][newC] != '#')
                    {
                        char cell = grid[newR][newC];

                        if (keySet.Contains(cell))
                        {
                            if (((1 << (cell - 'a')) & keys) != 0)
                            {
                                continue;
                            }

                            int newKeys = (keys | (1 << (cell - 'a')));

                            if (newKeys == allKeys)
                            {
                                return dist + 1;
                            }

                            if (!seen.ContainsKey(newKeys))
                            {
                                seen[newKeys] = new HashSet<(int, int)>();
                            }

                            seen[newKeys].Add((newR, newC));
                            queue.Enqueue(new int[] { newR, newC, newKeys, dist + 1 });
                        }

                        if (lockSet.Contains(cell) && ((keys & (1 << (cell - 'A'))) == 0))
                        {
                            continue;
                        }

                        else if (!seen.ContainsKey(keys) || !seen[keys].Contains((newR, newC)))
                        {
                            if (!seen.ContainsKey(keys))
                            {
                                seen[keys] = new HashSet<(int, int)>();
                            }

                            seen[keys].Add((newR, newC));
                            queue.Enqueue(new int[] { newR, newC, keys, dist + 1 });
                        }
                    }
                }
            }

            return -1;
        }
        #endregion

        #region Day 29 Problem
        #endregion

        #region Day 30 Problem 1970. Last Day Where You Can Still Cross
        int[][] directions = new int[][] { new int[] { 0, 1 }, new int[] { 0, -1 }, new int[] { 1, 0 }, new int[] { -1, 0 } };
        public int LatestDayToCross(int row, int col, int[][] cells)
        {
            int left = 1;
            int right = row * col;

            while (left < right)
            {
                int mid = right - (right - left) / 2;

                if (canCross(row, col, cells, mid))
                {
                    left = mid;
                }
                else
                {
                    right = mid - 1;
                }
            }
            return left;
        }

        private bool canCross(int row, int col, int[][] cells, int mid)
        {
            int[,] grid = new int[row, col];

            Queue<(int, int)> q = new Queue<(int, int)>();

            for (int i = 0; i < mid; i++)
            {
                grid[cells[i][0] - 1, cells[i][1] - 1] = 1;
            }

            for (int i = 0; i < col; i++)
            {
                if (grid[0, i] == 0)
                {
                    q.Enqueue((0, i));
                    grid[0, i] = -1;
                }
            }

            while (q.Count > 0)
            {
                (int r, int c) = q.Dequeue();

                if(r==row-1) { return true; }

                foreach (int[] dir in directions)
                {
                    int newRow = r + dir[0];
                    int newCol = c + dir[1];

                    if(newRow>=0 && newRow<row && newCol>=0&&newCol < col && grid[newRow,newCol] == 0)
                    {
                        grid[newRow,newCol] = -1;
                        q.Enqueue((newRow, newCol));
                    }
                }
            }
            return false;
        }
        #endregion

        #region Weekly 348

        //Minimize String Length

        public int MinimizedStringLength(string s)
        {
            if (s.Length <= 1) return s.Length;
            int i = 0;
            HashSet<char> vis = new HashSet<char>();
            while (i < s.Length)
            {
                vis.Add(s[i]);
                i++;
            }
            return vis.Count;
        }

        public int SemiOrderedPermutation(int[] nums)
        {
            int n = nums.Length;
            if (nums[0] == 1 && nums[n - 1] == n) return 0;

            int res = 0;
            int i = -1;
            int j = -1;
            if (nums[0] != 1)
            {
                i = Array.IndexOf(nums, 1);
                while (i != 0)
                {
                    res++;
                    j = i - 1;
                    swap(nums, i, j);

                    i--;
                }

            }
            if (nums[n - 1] != n)
            {
                i = Array.IndexOf(nums, n);
                while (i != n - 1)
                {
                    res++;
                    j = i + 1;
                    swap(nums, i, j);
                    i++;
                }

            }

            return res;
        }

        private static void swap(int[] nums, int i, int j)
        {
            int temp = nums[j];
            nums[j] = nums[i];
            nums[i] = temp;
        }
        public long MatrixSumQueries(int n, int[][] queries)
        {
            long[] arr = new long[n * n];

            long res = 0;

            foreach (var item in queries)
            {
                int k = item[1];
                int v = item[2];

                if (item[0] == 0)
                {
                    for (int i = 0; i < n; i++)
                    {
                        if (arr[k * n + i] != v)
                        {
                            res -= arr[k * n + i];
                            arr[k * n + i] = v;
                            res += v;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < n; i++)
                    {
                        if (arr[i * n + k] != v)
                        {
                            res -= arr[i * n + k];
                            arr[i * n + k] = v;
                            res += v;
                        }
                    }
                }
            }

            return res;
        }

        public long MatrixSumQueries_v1(int n, int[][] queries)
        {
            long res = 0;

            long[][] arr = new long[n][];

            for (int i = 0; i < n; i++)
            {
                arr[i] = new long[n];
            }

            foreach (var item in queries)
            {
                int k = item[1];
                int v = item[2];
                if (item[0] == 0)
                {
                    for (int i = 0; i < n; i++)
                    {
                        arr[k][i] = v;
                    }
                }
                else
                {
                    for (int i = 0; i < n; i++)
                    {
                        arr[i][k] = v;
                    }
                }
            }

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    res += arr[i][j];
                }
            }

            return res;

        }

        #endregion

        #region Weekly 350
        public int DistanceTraveled(int mainTank, int additionalTank)
        {
            int totalDistance = 0;

            while (mainTank >= 5 && additionalTank >= 1)
            {
                totalDistance += 50;

                mainTank -= 4;
                additionalTank--;
            }

            totalDistance += mainTank * 10;
            return totalDistance;
        }

        public int FindValueOfPartition(int[] nums)
        {
            Array.Sort(nums); // Sort the array in ascending order

            int minValue = int.MaxValue;
            int n = nums.Length;

            for (int i = 1; i < n; i++)
            {
                int maxNums1 = nums[i - 1]; // Maximum element of nums1
                int minNums2 = nums[i]; // Minimum element of nums2

                int diff = Math.Abs(maxNums1 - minNums2); // Calculate the difference

                minValue = Math.Min(minValue, diff); // Update the minimum value if necessary
            }

            return minValue;
        }

        #endregion

        #region Problem
        #endregion

        #region Problem
        #endregion

        #region BiWeekly 106
        public bool IsFascinating(int n)
        {
            bool[] arr = new bool[9];
            int k = n;
            if (!isFascHelper(arr, k)) return false;

            k = n * 2;

            if (!isFascHelper(arr, k)) return false;


            k = n * 3;
            if (!isFascHelper(arr, k)) return false;
            return true;
        }

        private bool isFascHelper(bool[] arr, int n)
        {
            int k = n;

            int r = k % 10;

            if (r == 0) return false;

            arr[r - 1] = true;

            k /= 10;

            r = k % 10;


            if (r == 0 || arr[r - 1]) return false;
            arr[r - 1] = true;

            k /= 10;

            r = k % 10;


            if (r == 0 || arr[r - 1]) return false;
            arr[r - 1] = true;

            return true;
        }
        #endregion
    }
}
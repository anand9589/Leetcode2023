using Common;
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

                    if (keys[low] > snap_id) return versions[index][keys[low-1]];

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

        #region Day 15 Problem
        #endregion

        #region Day 16 Problem
        #endregion

        #region Day 17 Problem
        #endregion

        #region Day 18 Problem
        #endregion

        #region Day 19 Problem
        #endregion

        #region Day 20 Problem
        #endregion

        #region Day 21 Problem
        #endregion

        #region Day 22 Problem
        #endregion

        #region Day 23 Problem
        #endregion

        #region Day 24 Problem
        #endregion

        #region Day 25 Problem
        #endregion

        #region Day 26 Problem
        #endregion

        #region Day 27 Problem
        #endregion

        #region Day 28 Problem
        #endregion

        #region Day 29 Problem
        #endregion

        #region Day 30 Problem
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
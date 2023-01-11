using Common;

namespace Jan
{
    public class Solution
    {
        #region Problem Day 1 290. Word Pattern
        public bool WordPattern(string pattern, string s)
        {
            char[] chars = pattern.ToCharArray();

            string[] words = s.Split(' ');

            if (words.Length == chars.Length && words.Distinct().Count() == chars.Distinct().Count())
            {
                Dictionary<char, string> map = new Dictionary<char, string>();


                for (int i = 0; i < words.Length; i++)
                {
                    char c = chars[i];
                    string word = words[i];

                    if (map.ContainsKey(c))
                    {
                        if (map[c] != word) return false;
                    }
                    else
                    {
                        map.Add(c, word);
                    }
                }
                return true;

            }

            return false;
        }
        #endregion

        #region Problem Day 2 520. Detect Capital
        public bool DetectCapitalUse(string word)
        {
            if (word.Length == 1) return true;

            if (!isCap(word[0]))
            {
                return word.Where(x => isCap(x)).Count() == 0;
            }
            else if (isCap(word[0]) && isCap(word[1]))
            {
                return word.Where(x => !isCap(x)).Count() == 0;
            }
            else
            {
                return word.Skip(1).Where(x => isCap(x)).Count() == 0;
            }
        }

        private bool isCap(char c)
        {
            return c >= 'A' && c <= 'Z';
        }
        #endregion

        #region Problem Day 3 944. Delete Columns to Make Sorted
        public int MinDeletionSize(string[] strs)
        {
            if (strs.Length == 1) return 0;
            int cnt = 0;
            for (int i = 0; i < strs[0].Length; i++)
            {
                int o = -1;
                for (int j = 0; j < strs.Length; j++)
                {
                    char p = strs[j][i];

                    if (p >= o)
                    {
                        o = p;
                    }
                    else
                    {
                        cnt++;
                        break;
                    }
                }
            }
            return cnt;
        }
        #endregion

        #region Problem Day 4 2244. Minimum Rounds to Complete All Tasks
        public int MinimumRounds(int[] tasks)
        {
            int days = 0;
            IDictionary<int, int> map = new Dictionary<int, int>();

            foreach (int task in tasks)
            {
                if (!map.ContainsKey(task))
                {
                    map.Add(task, 0);
                }
                map[task]++;
            }

            foreach (var key in map.Keys)
            {
                if (map[key] < 2) return -1;

                int p = map[key] % 3;
                days += map[key] / 3;

                if (p > 0) days++;
            }

            return days;
        }
        public int MinimumRounds_V1(int[] tasks)
        {
            int days = 0;
            IDictionary<int, int> map = new Dictionary<int, int>();

            foreach (int task in tasks)
            {
                if (!map.ContainsKey(task))
                {
                    map.Add(task, 0);
                }
                map[task]++;
            }


            foreach (var key in map.Keys)
            {
                if (map[key] < 2) return -1;
                while (map[key] > 4)
                {
                    days++;
                    map[key] -= 3;
                }

                if (map[key] != 0)
                {
                    if (map[key] == 4)
                    {
                        days += 2;
                    }
                    else
                    {
                        days++;
                    }
                    map[key] = 0;
                }
            }

            return days;
        }
        #endregion

        #region Problem Day 5 452. Minimum Number of Arrows to Burst Balloons
        public int FindMinArrowShots(int[][] points)
        {
            int count = 1;
            Array.Sort(points, (x, y) =>
            {
                if (x[1] < y[1]) return -1;
                if (x[1] > y[1]) return 1;
                return 0;
            });

            int p1 = points[0][1];

            for (int i = 1; i < points.Length; i++)
            {
                if (points[i][0] > p1 && points[i][1] > p1)
                {
                    p1 = points[i][1];
                    count++;
                }
            }
            return count;
        }


        #endregion

        #region Problem Day 6 1833. Maximum Ice Cream Bars
        public int MaxIceCream(int[] costs, int coins)
        {
            Array.Sort(costs);

            int count = 0;

            foreach (var cost in costs)
            {
                if (coins < cost) break;
                count++;
                coins -= cost;
            }

            return count;
        }
        #endregion

        #region Problem Day 7 134. Gas Station
        public int CanCompleteCircuit(int[] gas, int[] cost)
        {
            int result = 0, diff = 0, sum = 0;

            for (int i = 0; i < gas.Length; i++)
            {
                sum += gas[i] - cost[i];
                if (sum < 0)
                {
                    diff += sum;
                    sum = 0;
                    result = i + 1;
                }
            }

            diff += sum;
            return diff >= 0 ? result : -1;
        }
        #endregion

        #region Problem Day 8 149. Max Points on a Line
        public int MaxPoints(int[][] points)
        {
            int n = points.Length;

            if (n == 1) return 1;

            int result = 2;

            for (int i = 0; i < n; i++)
            {
                Dictionary<double, int> map = new Dictionary<double, int>();

                for (int j = 0; j < n; j++)
                {
                    if (j != i)
                    {
                        var p = Math.Atan2(points[j][1] - points[i][1], points[j][0] - points[i][0]);

                        if (!map.ContainsKey(p))
                        {
                            map.Add(p, 1);
                        }
                        map[p]++;

                        result = Math.Max(result, map[p]);
                    }
                }

            }

            return result;
        }
        #endregion

        #region Problem Day 9 144. Binary Tree Preorder Traversal
        public IList<int> PreorderTraversal(TreeNode root)
        {
            IList<int> list = new List<int>();
            preOrder(root, list);
            return list;
        }

        private void preOrder(TreeNode node, IList<int> list)
        {
            if (node == null) return;

            list.Add(node.val);
            preOrder(node.left, list);
            preOrder(node.right, list);
        }
        #endregion

        #region Problem Day 10 100. Same Tree
        public bool IsSameTree(TreeNode p, TreeNode q)
        {
            if (p == null && q == null) return true;
            if (p == null && q != null) return false;
            if (p != null && q == null) return false;
            if(p.val != q.val) return false;
            return IsSameTree(p.left, q.left) && IsSameTree(p.right, q.right);
        }
        #endregion

        #region Problem Day 11 1443. Minimum Time to Collect All Apples in a Tree
        public int MinTime(int n, int[][] edges, IList<bool> hasApple)
        {
            Dictionary<int, List<int>> map = new Dictionary<int, List<int>>();

            foreach (int[] arr in edges)
            {
                int u = arr[0];
                int v = arr[1];

                if (!map.ContainsKey(u))
                {
                    map.Add(u, new List<int>());
                }
                map[u].Add(v);

                if (!map.ContainsKey(v))
                {
                    map.Add(v, new List<int>());
                }
                map[v].Add(u);

            }
            return dfs_1443(0,-1,map,hasApple);
        }

        private int dfs_1443(int u, int v, Dictionary<int, List<int>> map, IList<bool> hasApple)
        {
            if(!map.ContainsKey(u)) return 0;

            int total = 0, child = 0;

            foreach (int c in map[u])
            {
                if (c == v) continue;

                child = dfs_1443(c, u, map, hasApple);

                if(child>0 || hasApple[c])
                {
                    total += child + 2;
                }
            }

            return total;
        }
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

        #region Problem Day 31
        #endregion

        #region Problem 2520. Count the Digits That Divide a Number
        public int CountDigits(int num)
        {
            int n = num;
            int cnt = 0;

            while (n > 0)
            {
                int k = n % 10;
                n /= 10;

                if (num % k == 0) cnt++;
            }
            return cnt;
        }
        #endregion

        #region Problem 2523. Closest Prime Numbers in Range
        public int[] ClosestPrimes(int left, int right)
        {
            int num1 = -1, num2 = -1, x = -1, y = -1;

            right = right % 2 == 0 ? right - 1 : right;
            left = left % 2 == 0 ? left + 1 : left;
            int diff = int.MaxValue;

            for (int i = right; i >= left; i = i - 2)
            {
                if (isPrime(i))
                {
                    if (x == -1)
                    {
                        x = i;
                    }
                    else if (y == -1)
                    {
                        y = i;

                        diff = x - y;
                        num1 = y;
                        num2 = x;
                    }
                    else
                    {
                        x = y;
                        y = i;
                        if (diff >= x - y)
                        {
                            diff = x - y;
                            num1 = y;
                            num2 = x;
                        }
                    }
                }
            }


            return new int[] { num1, num2 };
        }

        private bool isPrime(int num)
        {
            if (num <= 1) return false;
            if (num <= 3) return true;
            if (num % 2 == 0 || num % 3 == 0) return false;
            for (int i = 5; i * i <= num; i = i + 6)
            {
                if (num % i == 0 || num % (i + 2) == 0)
                    return false;
            }
            return true;
        }
        #endregion

        #region Problem 2529. Maximum Count of Positive Integer and Negative Integer
        public int MaximumCount(int[] nums)
        {
            int num = 0;

            int low = 0;
            int high = nums.Length - 1;
            if (nums[high] == 0 && nums[low] == 0) return 0;
            if (nums[low] > 0 || nums[high] < 0) return nums.Length;

            while (low <= high)
            {
                int mid = (low + high) / 2;

                if (nums[mid] == 0)
                {
                    return Math.Max(mid, nums.Length - 1 - mid);
                }

                if (nums[mid] > 0)
                {
                    if (nums[mid - 1] < 0)
                    {
                        return Math.Max(mid, nums.Length - mid);
                    }

                    high = mid - 1;
                }
                else
                {
                    if (nums[mid + 1] > 0)
                    {
                        return Math.Max(mid + 1, nums.Length - mid - 1);
                    }
                    low = mid + 1;
                }
            }

            return num;
        }
        #endregion

        #region Problem 2530. Maximal Score After Applying K Operations
        public long MaxKelements(int[] nums, int k)
        {
            PriorityQueue<int, int> pq = new PriorityQueue<int, int>(new MaxComparer());
            long result = 0;
            foreach (int v in nums)
            {
                pq.Enqueue(v, v);
            }

            while (k > 0)
            {
                var v = pq.Dequeue();

                result += v;

                int b = (int)Math.Ceiling((double)v / 3);
                pq.Enqueue(b, b);
                k--;
            }
            return result;
        }

        public class MaxComparer : IComparer<int>
        {
            public int Compare(int x, int y)
            {
                if (x < y) return 1;
                if (x > y) return -1;
                return 0;
            }
        }
        #endregion

        #region Problem 2531. Make Number of Distinct Characters Equal
        public bool IsItPossible(string word1, string word2)
        {
            Dictionary<char, int> map1 = createMap(word1);
            Dictionary<char, int> map2 = createMap(word2);

            if (map1.Count == map2.Count) return true;




            return false;
        }

        private static Dictionary<char, int> createMap(string word)
        {
            Dictionary<char, int> map = new Dictionary<char, int>();
            int i = 0;
            while (i < word.Length)
            {
                if (!map.ContainsKey(word[i]))
                {
                    map.Add(word[i], 0);
                }
                map[word[i]]++;
            }
            return map;
        }
        #endregion
    }
}
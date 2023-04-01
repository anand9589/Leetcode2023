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
            if (p.val != q.val) return false;
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
            return dfs_1443(0, -1, map, hasApple);
        }

        private int dfs_1443(int u, int v, Dictionary<int, List<int>> map, IList<bool> hasApple)
        {
            if (!map.ContainsKey(u)) return 0;

            int total = 0, child = 0;

            foreach (int c in map[u])
            {
                if (c == v) continue;

                child = dfs_1443(c, u, map, hasApple);

                if (child > 0 || hasApple[c])
                {
                    total += child + 2;
                }
            }

            return total;
        }
        #endregion

        #region Problem Day 12 1519. Number of Nodes in the Sub-Tree With the Same Label
        public int[] CountSubTrees(int n, int[][] edges, string labels)
        {
            int[] result = new int[n];

            Dictionary<int, List<int>> map = new Dictionary<int, List<int>>();

            foreach (int[] arr in edges)
            {
                if (!map.ContainsKey(arr[0]))
                {
                    map.Add(arr[0], new List<int>());
                }
                map[arr[0]].Add(arr[1]);
                if (!map.ContainsKey(arr[1]))
                {
                    map.Add(arr[1], new List<int>());
                }
                map[arr[1]].Add(arr[0]);
            }
            dfs_1519(0, -1, map, labels.ToCharArray(), result);
            return result;
        }

        private int[] dfs_1519(int node, int parent, Dictionary<int, List<int>> map, char[] labels, int[] result)
        {
            int[] nodeCounts = new int[26];

            nodeCounts[labels[node] - 'a'] = 1;
            if (!map.ContainsKey(node)) return nodeCounts;

            foreach (int child in map[node])
            {
                if (child == parent) continue;

                int[] childCounts = dfs_1519(child, node, map, labels, result);

                for (int i = 0; i < 26; i++)
                {
                    nodeCounts[i] += childCounts[i];
                }
            }
            result[node] = nodeCounts[labels[node] - 'a'];

            return nodeCounts;
        }
        #endregion

        #region Problem Day 13 2246. Longest Path With Different Adjacent Characters
        int LongestPath_2246 = 1;
        public int LongestPath(int[] parent, string s)
        {

            Dictionary<int, List<int>> map_2246 = new Dictionary<int, List<int>>();

            for (int i = 1; i < parent.Length; i++)
            {
                if (!map_2246.ContainsKey(parent[i]))
                {
                    map_2246.Add(parent[i], new List<int>());
                }
                map_2246[parent[i]].Add(i);
            }

            dfs_2246(0, map_2246, s);
            return LongestPath_2246;
        }

        private int dfs_2246(int v, Dictionary<int, List<int>> map_2246, string s)
        {
            if (!map_2246.ContainsKey(v)) return 1;

            int longestChain = 0, secondLongestChain = 0;

            foreach (var val in map_2246[v])
            {
                int longestChainStartingFromChild = dfs_2246(val, map_2246, s);

                if (s[v] == s[val]) continue;

                if (longestChainStartingFromChild > longestChain)
                {
                    secondLongestChain = longestChain;
                    longestChain = longestChainStartingFromChild;
                }
                else if (longestChainStartingFromChild > secondLongestChain)
                {
                    secondLongestChain = longestChainStartingFromChild;
                }

                LongestPath_2246 = Math.Max(LongestPath_2246, longestChain + secondLongestChain + 1);
                return longestChain + 1;
            }
            return 0;
        }
        #endregion

        #region Problem Day 14
        #endregion

        #region Problem Day 15
        #endregion

        #region Problem Day 16
        #endregion

        #region Problem Day 17 926. Flip String to Monotone Increasing
        public int MinFlipsMonoIncr(string s)
        {
            int m = 0;
            for (int i = 0; i < s.Length; ++i)
            {
                if (s[i] == '0')
                {
                    ++m;
                }
            }
            int result = m;
            for (int i = 0; i < s.Length; ++i)
            {
                if (s[i] == '0')
                {
                    result = Math.Min(result, --m);
                }
                else
                {
                    ++m;
                }
            }
            return result;
        }
        #endregion

        #region Problem Day 18 918. Maximum Sum Circular Subarray
        public int MaxSubarraySumCircular(int[] nums)
        {
            int result = 0;
            int curMax = 0, curMin = 0, maxSum = nums[0], minSum = nums[0];

            foreach (var num in nums)
            {
                curMax = Math.Max(curMax, 0) + num;
                maxSum = Math.Max(maxSum, curMax);

                curMin = Math.Min(curMin, 0) + num;
                minSum = Math.Min(minSum, curMin);

                result += num;
            }

            return result == minSum ? maxSum : Math.Max(maxSum, result - minSum);
        }
        #endregion

        #region Problem Day 19 974. Subarray Sums Divisible by K
        public int SubarraysDivByK(int[] nums, int k)
        {
            int prefixMod = 0, result = 0;

            int[] modGroups = new int[k];
            modGroups[0] = 1;

            foreach (int num in nums)
            {
                // Take modulo twice to avoid negative remainders.
                prefixMod = (prefixMod + num % k + k) % k;
                // Add the count of subarrays that have the same remainder as the current
                // one to cancel out the remainders.
                result += modGroups[prefixMod];
                modGroups[prefixMod]++;
            }

            return result;
        }
        #endregion

        #region Problem Day 20 491. Non-decreasing Subsequences
        public IList<IList<int>> FindSubsequences(int[] nums)
        {
            //IList<IList<int>> result = new List<IList<int>>();

            HashSet<IList<int>> set = new HashSet<IList<int>>();

            IList<int> subsequesnce = new List<int>();

            backTrack_491(nums, 0, subsequesnce, set);

            return set.ToList();
        }

        private void backTrack_491(int[] nums, int index, IList<int> subsequesnce, HashSet<IList<int>> set)
        {
            if (index == nums.Length)
            {
                if (subsequesnce.Count > 1)
                {
                    set.Add(new List<int>(subsequesnce));
                }
                return;
            }

            if (subsequesnce.Count == 0 || subsequesnce.Last() <= nums[index])
            {
                subsequesnce.Add(nums[index]);

                backTrack_491(nums, index + 1, subsequesnce, set);

                subsequesnce.Remove(nums[index]);
            }

            if (index + 1 < nums.Length && nums[index] != nums[index + 1]) backTrack_491(nums, index + 1, subsequesnce, set);
        }
        #endregion

        #region Problem Day 21
        #endregion

        #region Problem Day 22 131. Palindrome Partitioning
        public IList<IList<string>> Partition(string s)
        {
            IList<IList<string>> list = new List<IList<string>>();

            dfs_131(list, new List<string>(), s, 0);

            return list;
        }

        private void dfs_131(IList<IList<string>> list, List<string> currlist, string s, int i)
        {
            if (i >= s.Length) list.Add(new List<string>(currlist));

            for (int j = i; j < s.Length; j++)
            {
                if (isPalindrome(s, i, j))
                {
                    currlist.Add(s.Substring(i, j - i + 1));
                    dfs_131(list, currlist, s, j + 1);

                    currlist.RemoveAt(currlist.Count - 1);
                }
            }


        }

        private bool isPalindrome(string s, int low, int high)
        {
            while (low <= high)
            {
                if (s[low++] != s[high--]) return false;
            }

            return true;
        }
        #endregion

        #region Problem Day 23 997. Find the Town Judge
        public int FindJudge(int n, int[][] trust)
        {
            int result = -1;

            Dictionary<int, (IList<int>, IList<int>)> dct = new Dictionary<int, (IList<int>, IList<int>)>();

            foreach (int[] i in trust)
            {
                var j = i[1];
                var k = i[0];
                if (!dct.ContainsKey(j))
                {
                    dct.Add(j, (new List<int>(), new List<int>()));
                }

                dct[j].Item1.Add(k);

                if (!dct.ContainsKey(k))
                {
                    dct.Add(k, (new List<int>(), new List<int>()));
                }

                dct[k].Item2.Add(j);
            }

            foreach (var key in dct.Keys)
            {
                if (dct[key].Item1.Count == n - 1)
                {
                    if (dct[key].Item2.Count == 0) return key;
                }
            }

            return result;
        }
        #endregion

        #region Problem Day 24 909. Snakes and Ladders
        public int SnakesAndLadders(int[][] board)
        {
            int moves = -1;

            int n = board.Length;
            int boxLength = n * n + 1;

            bool[] visited = new bool[boxLength];
            int[] arr = new int[boxLength];
            int x = 1;
            bool one = true;

            for (int i = n - 1; i >= 0; i--)
            {
                if (one)
                {
                    one = false;
                    for (int j = 0; j < n; j++)
                    {
                        arr[x++] = board[i][j];
                    }
                }
                else
                {
                    one = true;
                    for (int j = n - 1; j >= 0; j--)
                    {
                        arr[x++] = board[i][j];

                    }
                }
            }


            Queue<(int cur, int mov)> q = new Queue<(int, int)>();

            q.Enqueue((1, 0));

            while (q.Count > 0)
            {
                var p = q.Dequeue();
                visited[p.cur] = true;
                if (p.cur == boxLength - 1) return p.mov;
                for (int i = 1; i <= 6; i++)
                {
                    int next = p.cur + i;
                    if (next < boxLength)
                    {
                        if (arr[next] != -1)
                        {
                            next = arr[next];
                        }
                        if (!visited[next])
                        {
                            q.Enqueue((next, p.mov + 1));
                        }
                    }
                }
            }

            return moves;
        }
        #endregion

        #region Problem Day 25
        #endregion

        #region Problem Day 26
        #endregion

        #region Problem Day 27 352. Data Stream as Disjoint Intervals
        public class SummaryRanges
        {
            private SortedSet<int> values;
            private IList<int[]> lst;

            public SummaryRanges()
            {
                values = new SortedSet<int>();
                lst = new List<int[]>();
            }

            public void AddNum(int value)
            {
                values.Add(value);

                var x = lst.FirstOrDefault(x => x[1] == value - 1);
                var y = lst.FirstOrDefault(x => x[0] == value + 1);
                if (x != null)
                {

                }
            }

            public int[][] GetIntervals()
            {
                if (lst.Count == 0)
                {
                    lst.Add(new int[2]);
                }
                return lst.ToArray();
            }

            //private
        }
        #endregion

        #region Problem Day 28
        #endregion

        #region Problem Day 29
        public class LFUCache
        {
            private Dictionary<int, int> cache;
            private Dictionary<int, (int frequency, System.DateTime lastlyUsed)> lfu;
            private readonly int capacity;
            private int frequency = 0;

            public LFUCache(int capacity)
            {
                this.capacity = capacity;
                cache = new Dictionary<int, int>(capacity);
                lfu = new Dictionary<int, (int frequency, System.DateTime lastlyUsed)>();
            }

            public int Get(int key)
            {
                if (!cache.TryGetValue(key, out var value))
                {
                    return -1;
                }

                (var f, var d) = lfu[key];
                lfu[key] = (f + 1, System.DateTime.UtcNow);
                return value;
            }

            public int GetLfuKey()
            {
                var min = int.MaxValue;
                int key = 0;
                DateTime dt = DateTime.MaxValue;
                foreach (var kvp in lfu)
                {
                    if (kvp.Value.frequency < min)
                    {
                        min = kvp.Value.frequency;
                        key = kvp.Key;
                        dt = kvp.Value.lastlyUsed;
                    }
                    else if (kvp.Value.frequency == min && kvp.Value.lastlyUsed < dt)
                    {
                        key = kvp.Key;
                        dt = kvp.Value.lastlyUsed;
                    }
                }

                return key;
            }

            public void Put(int key, int value)
            {
                if (capacity == 0)
                {
                    return;
                }
                if (!cache.TryGetValue(key, out var _) && cache.Count == capacity)
                {
                    var lfuKey = GetLfuKey();
                    cache.Remove(lfuKey);
                    lfu.Remove(lfuKey);
                }

                if (lfu.TryGetValue(key, out var res))
                {
                    lfu[key] = (res.frequency + 1, DateTime.UtcNow);
                }
                else
                {
                    lfu[key] = (0, DateTime.UtcNow);
                }



                cache[key] = value;
            }
        }
        public class LFUCache_1
        {
            int counter;
            Dictionary<int, (int value, int usedCount, int lastUsed)> cache;
            //Dictionary<int, int> dctKeyValue;
            //Dictionary<int, int> dctKeyCounter;
            //Dictionary<int, int> dctKeyLastUsed;
            int capacity;
            public LFUCache_1(int capacity)
            {
                this.capacity = capacity;
                counter = 0;
                cache = new Dictionary<int, (int value, int usedCount, int lastUsed)>();
                //dctKeyCounter = new Dictionary<int, int>();
                //dctKeyLastUsed = new Dictionary<int, int>();
                //dctKeyValue = new Dictionary<int, int>();
            }

            public int Get(int key)
            {
                //if (dctKeyValue.ContainsKey(key))
                //{
                //    dctKeyCounter[key]++;
                //    dctKeyLastUsed[key] = ++counter;
                //    return dctKeyValue[key];
                //}

                if (cache.ContainsKey(key))
                {
                    var p = cache[key];
                    p.usedCount++;
                    p.lastUsed = ++counter;

                    cache[key] = p;
                    return p.value;
                }
                return -1;
            }

            public void Put(int key, int value)
            {
                if (capacity == 0) return;
                //if (dctKeyValue.ContainsKey(key))
                //{
                //    dctKeyValue[key] = value;

                //    dctKeyCounter[key]++;

                //    dctKeyLastUsed[key]= ++counter;
                //}
                //else
                //{
                //    if(capacity == dctKeyValue.Count)
                //    {

                //    }
                //    dctKeyValue.Add(key, value);
                //    dctKeyLastUsed.Add(key, ++counter);
                //    dctKeyCounter.Add(key, 1);
                //}
                if (cache.ContainsKey(key))
                {
                    var p = cache[key];

                    p.value = value;
                    p.usedCount++;
                    p.lastUsed = ++counter;

                    cache[key] = p;
                }
                else
                {
                    if (cache.Count == capacity)
                    {
                        //operation for remove
                        //var c = cache.OrderBy(x => x.Value.lastUsed).OrderBy(x => x.Value.usedCount).FirstOrDefault().Key;

                        int k = -1;
                        (int value, int usedCount, int lastUsed) x = (int.MaxValue, int.MaxValue, int.MaxValue);
                        foreach (var kv in cache.Keys)
                        {
                            var c = cache[kv];

                            if (c.usedCount < x.Item2 || (c.usedCount == x.Item2 && c.lastUsed < x.lastUsed))
                            {
                                x = c;
                                k = kv;
                            }

                        }
                        cache.Remove(k);
                    }
                    cache.Add(key, (value, 1, ++counter));
                }
            }
        }
        #endregion

        #region Problem Day 30 1137. N-th Tribonacci Number
        public int Tribonacci(int n)
        {
            if (n == 0) return 0;

            if (n <= 2) return 1;

            int[] dp = new int[n + 1];

            dp[0] = 0;
            dp[1] = 1;
            dp[2] = 1;
            for (int i = 3; i < n + 1; i++)
            {
                dp[i] = dp[i - 1] + dp[i - 2] + dp[i - 3];
            }

            return dp[n];
        }

        #endregion

        #region Problem Day 31 1626. Best Team With No Conflicts
        public int BestTeamScore(int[] scores, int[] ages)
        {
            //SortedList<>//
            int n = scores.Length;

            int[][] agescore = new int[n][];

            for (int i = 0; i < n; i++)
            {
                agescore[i] = new int[] { ages[i], scores[i] };
            }

            Array.Sort(agescore, (a, b) => a[0] == b[0] ? a[1]-b[1]:a[0]-b[0]);


            return findBestScore(agescore);
        }

        private int findBestScore(int[][] agescore)
        {
            int n = agescore.Length;

            int ans = 0;

            int[] dp=new int[n];

            for (int i = 0; i < n; i++)
            {
                dp[i] = agescore[i][1];
                ans = Math.Max(ans, dp[i]);
            }

            for (int i = 0; i < n; i++)
            {
                for (int j = i-1; j >=0 ; j--)
                {
                    if (agescore[i][1] >= agescore[j][1])
                    {
                        dp[i] = Math.Max(dp[i], agescore[i][1]+dp[j]);
                    }
                }
                ans = Math.Max(ans,dp[i]);
            }

            return ans;
        }
        #endregion

        #region Problem 133. Clone Graph
        HashSet<Node_133> lst_133 = new HashSet<Node_133>();
        public Node_133 CloneGraph(Node_133 node)
        {

            if (node != null)
            {
                Node_133 node1 = new Node_133(node.val);

                lst_133.Add(node1);
                foreach (Node_133 node2 in node.neighbors)
                {
                    var p = lst_133.FirstOrDefault(x => x.val == node2.val);
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

        #region Problem 137. Single Number II
        public int SingleNumber(int[] nums)
        {
            int one = 0, two = 0;

            foreach (int num in nums)
            {
                one = (one ^ num) & ~two;
                two = (two ^ num) & ~one;
            }
            return one;
        }
        #endregion

        #region Problem 138. Copy List with Random Pointer
        HashSet<Node> nodes = new HashSet<Node>();
        public Node CopyRandomList(Node head)
        {
            if (head != null)
            {
                Node node = new Node(head.val);
                nodes.Add(node);

                if (head.next != null)
                {
                    var k = nodes.FirstOrDefault(x => x.val == head.next.val);

                    if (k == null)
                    {
                        k = CopyRandomList(head.next);
                    }
                    node.next = k;
                }

                if (head.random != null)
                {
                    var k = nodes.FirstOrDefault(x => x.val == head.random.val);

                    if (k == null)
                    {
                        k = CopyRandomList(head.random);
                    }
                    node.random = k;
                }
                return node;
            }
            return null;
        }
        #endregion

        #region Problem 217. Contains Duplicate

        public bool ContainsDuplicate_V2(int[] nums)
        {
            Array.Sort(nums);

            for (int i = 1; i < nums.Length; i++)
            {
                if (nums[i] == nums[i - 1]) return true;

            }
            return false;
        }
        public bool ContainsDuplicate_V1(int[] nums)
        {
            IList<int> lst = new List<int>();

            foreach (int num in nums)
            {
                if (lst.Contains(num)) return true;
                lst.Add(num);
            }

            return false;
        }
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
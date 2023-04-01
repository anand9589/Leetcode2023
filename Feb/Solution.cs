using System.Text;
using System;
using Common;

namespace Feb
{
    public class Solution
    {
        #region Problem Day 1 1071. Greatest Common Divisor of Strings
        public string GcdOfStrings(string str1, string str2)
        {
            if (!(str1 + str2).Equals(str2 + str1))
            {
                return "";
            }

            // Get the GCD of the two lengths.
            int gcdLength = gcd(str1.Length, str2.Length);
            return str1.Substring(0, gcdLength);
        }

        private int gcd(int x, int y)
        {
            if (y == 0)
            {
                return x;
            }
            else
            {
                return gcd(y, x % y);
            }
        }
        #endregion

        #region Problem Day 2 953. Verifying an Alien Dictionary
        public bool IsAlienSorted(string[] words, string order)
        {
            Dictionary<char, int> dic = new Dictionary<char, int>();

            for (int i = 0; i < 26; i++)
            {
                dic.Add(order[i], i + 1);
            }

            int[][] arr = new int[words.Length][];

            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = new int[20];

                for (int j = 0; j < words[i].Length; j++)
                {
                    arr[i][j] = dic[words[i][j]];
                }
            }

            for (int i = 1; i < arr.Length; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    if (arr[i - 1][j] == arr[i][j]) continue;
                    if (arr[i - 1][j] > arr[i][j]) return false;
                    break;
                }
            }

            return true;
        }
        public bool IsAlienSorted_V1(string[] words, string order)
        {
            Dictionary<char, int> dic = new Dictionary<char, int>();

            for (int i = 0; i < 26; i++)
            {
                dic.Add(order[i], i);
            }

            for (int i = 1; i < words.Length; i++)
            {
                string w1 = words[i - 1];
                string w2 = words[i];

                if (w1 == w2) continue;

                int j = 0;
                while (j < w1.Length)
                {
                    if (j == w2.Length && j < w1.Length) return false;
                    if (w1[j] != w2[j])
                    {
                        if (dic[w1[j]] > dic[w2[j]]) return false;
                        break;
                    }
                    j++;
                }



            }

            return true;

        }
        #endregion

        #region Problem Day 3 6. Zigzag Conversion
        public string Convert(string s, int numRows)
        {
            IList<char>[] lst = new List<char>[numRows];
            for (int k = 0; k < numRows; k++)
            {
                lst[k] = new List<char>();
            }
            int i = 0;
            int row = 0;
            bool incr = true;
            while (i < s.Length)
            {
                lst[row].Add(s[i]);
                i++;
                if (incr)
                {
                    row++;
                }
                else
                {
                    row--;
                }

                if (row == numRows)
                {
                    incr = false;
                    row -= 2;
                }
                else if (row == -1)
                {
                    incr = true;
                    row += 2;
                }
            }

            StringBuilder stringBuilder = new StringBuilder();
            foreach (var item in lst)
            {
                stringBuilder.Append(string.Join("", item));
            }

            return stringBuilder.ToString();
        }
        #endregion

        #region Problem Day 4 567. Permutation in String
        public bool CheckInclusion(string s1, string s2)
        {
            if (s1.Length > s2.Length) return false;
            Dictionary<char, int> dic = new Dictionary<char, int>();
            int i = 0;
            while (i < s1.Length)
            {
                if (!dic.ContainsKey(s1[i]))
                {
                    dic.Add(s1[i], 0);
                }
                dic[s1[i]]++;
                i++;
            }


            Dictionary<char, int> dct;
            i = 0;
            while (i < s2.Length)
            {
                if (dic.ContainsKey(s2[i]))
                {
                    dct = new Dictionary<char, int>(dic);
                    int j = i;

                    while (j < s2.Length)
                    {
                        if (!dct.ContainsKey(s2[j]))
                        {
                            i = j;
                            break;
                        }
                        else
                        {

                            if (dct[s2[j]] == 1)
                            {
                                dct.Remove(s2[j]);

                                if (dct.Count == 0) return true;
                            }
                            else
                            {
                                dct[s2[j]]--;
                            }
                        }
                        j++;

                    }
                }
                i++;
            }

            return false;
        }
        #endregion

        #region Problem Day 5 438. Find All Anagrams in a String
        public IList<int> FindAnagrams(string s, string p)
        {
            IList<int> list = new List<int>();



            return list;
        }
        #endregion

        #region Problem Day 6 1470. Shuffle the Array
        public int[] Shuffle(int[] nums, int n)
        {
            int[] res = new int[nums.Length];
            //int mid = n / 2;
            int j = 0;
            for (int i = 0; i < nums.Length / 2; i++)
            {
                res[j++] = nums[i];
                res[j++] = nums[i + n];
            }
            return res;
        }

        #endregion

        #region Problem Day 7 904. Fruit Into Baskets    
        public int TotalFruit(int[] fruits)
        {
            int result = 0;
            Dictionary<int, int> map = new Dictionary<int, int>();
            int lastAccessKey = fruits[0];
            int lastAccessIndex = 0;
            int removeKey = -1;
            map.Add(lastAccessKey, 1);
            for (int i = 1; i < fruits.Length; i++)
            {
                if (map.ContainsKey(fruits[i]))
                {
                    if (lastAccessKey != fruits[i])
                    {
                        removeKey = lastAccessKey;
                        lastAccessKey = fruits[i];
                        lastAccessIndex = i;
                    }
                    map[lastAccessKey]++;
                }
                else
                {
                    if (map.Count < 2)
                    {
                        map.Add(fruits[i], 1);
                        removeKey = lastAccessKey;
                        lastAccessKey = fruits[i];
                        lastAccessIndex = i;
                    }
                    else
                    {
                        result = Math.Max(result, map.Values.Sum());
                        map.Remove(removeKey);
                        map.Add(fruits[i], 1);
                        removeKey = lastAccessKey;
                        lastAccessKey = fruits[i];
                        int cnt = i - lastAccessIndex;
                        lastAccessIndex = i;
                        map[removeKey] = cnt;
                    }
                }
            }
            result = Math.Max(result, map.Values.Sum());

            return result;
        }
        #endregion

        #region Problem Day 8 45. Jump Game II
        public int Jump(int[] nums)
        {
            int[] dp = Enumerable.Repeat(int.MaxValue, nums.Length).ToArray();
            dp[0] = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                for (int j = 1; j <= nums[i]; j++)
                {
                    if (i + j >= nums.Length) break;
                    dp[i + j] = Math.Min(dp[i + j], dp[i] + 1);
                }
            }

            return dp[dp.Length - 1];
        }
        #endregion

        #region Problem Day 9 2306. Naming a Company
        public long DistinctNames(string[] ideas)
        {
            long result = 0;

            HashSet<string>[] names = new HashSet<string>[26];

            for (int i = 0; i < 26; i++)
            {
                names[i] = new HashSet<string>();
            }
            foreach (string idea in ideas)
            {
                names[idea[0] - 'a'].Add(idea.Substring(1));
            }
            for (int i = 0; i < 25; i++)
            {
                for (int j = i + 1; j < 26; j++)
                {
                    long mutual = 0;

                    foreach (string ideaA in names[i])
                    {
                        if (names[j].Contains(ideaA)) { mutual++; }
                    }
                    result += 2 * (names[i].Count - mutual) * (names[j].Count - mutual);
                }
            }
            return result;
        }
        #endregion

        #region Problem Day 10 1162. As Far from Land as Possible
        public int MaxDistance(int[][] grid)
        {
            int result = 0;



            return result;
        }
        #endregion

        #region Problem Day 11 
        #endregion

        #region Problem Day 12 2477. Minimum Fuel Cost to Report to the Capital

        long result_2477 = 0;
        Dictionary<int, IList<int>> map_2477;
        public long MinimumFuelCost(int[][] roads, int seats)
        {
            int n = roads.Length + 1;

            map_2477 = new Dictionary<int, IList<int>>();

            foreach (int[] road in roads)
            {
                if (!map_2477.ContainsKey(road[0]))
                {
                    map_2477.Add(road[0], new List<int>());
                }
                if (!map_2477.ContainsKey(road[1]))
                {
                    map_2477.Add(road[1], new List<int>());
                }
                map_2477[road[0]].Add(road[1]);
                map_2477[road[1]].Add(road[0]);
            }

            dfs_2477(0, -1, seats);



            return result_2477;
        }

        private long dfs_2477(int node, int parent, int seats)
        {
            long rep = 1;

            if (!map_2477.ContainsKey(node)) return rep;

            foreach (var child in map_2477[node])
            {
                if (child != parent)
                {
                    rep += dfs_2477(child, node, seats);
                }
            }
            if (node != 0)
            {
                result_2477 += (long)Math.Ceiling((double)rep / seats);
            }
            return rep;

        }
        #endregion

        #region Problem Day 13 1523. Count Odd Numbers in an Interval Range
        public int CountOdds(int low, int high)
        {
            low = low % 2 == 0 ? low + 1 : low;
            high = high % 2 == 0 ? high - 1 : high;

            if (low == high) return 1;

            int diff = high - low;


            return diff / 2 + 1;
        }
        #endregion

        #region Problem Day 14 67. Add Binary
        //public string AddBinary(string a, string b)
        //{
        //    int k = Convert.ToInt32(a);
        //}
        #endregion

        #region Problem Day 15 989. Add to Array-Form of Integer
        public IList<int> AddToArrayForm(int[] num, int k)
        {
            int i = num.Length - 1;
            int carryon = 0;
            for (; i >= 0 && k > 0; i--)
            {
                int x = k % 10;

                int sum = x + carryon + num[i];

                num[i] = sum % 10;

                carryon = sum / 10;
                k /= 10;
            }

            while (carryon == 1)
            {
                if (i >= 0)
                {
                    int sum = num[i] + carryon;

                    num[i] = sum % 10;

                    carryon = sum / 10;
                }
                else
                {
                    break;
                }
                i--;
            }
            var lst = new List<int>(num);
            if (carryon == 1) lst.Insert(0, 1);
            return lst;
        }
        public IList<int> AddToArrayForm_V1(int[] num, int k)
        {
            List<int> result = new List<int>();


            int resultIndex = num.Length;
            int carryon = 0;
            while (k > 0)
            {
                resultIndex--;
                int n1 = 0, n2 = 0;
                if (resultIndex >= 0)
                {
                    n1 = num[resultIndex];
                }
                n2 = k % 10;
                k /= 10;
                int sum = n1 + n2 + carryon;
                if (sum > 9)
                {
                    result.Insert(0, sum % 10);
                    carryon = 1;
                }
                else
                {
                    result.Insert(0, sum);
                    carryon = 0;
                }
            }

            while (carryon == 1)
            {
                if (resultIndex > 0)
                {
                    resultIndex--;
                    int sum = num[resultIndex] + 1;

                    if (sum > 9)
                    {
                        result.Insert(0, sum % 10);
                        carryon = 1;
                    }
                    else
                    {
                        result.Insert(0, sum);
                        carryon = 0;
                    }
                }
                else
                {
                    result.Insert(0, 1);
                    carryon = 0;

                }
            }

            if (resultIndex > 0)
            {
                resultIndex--;
                result.InsertRange(0, num.Take(resultIndex + 1));
            }

            return result;
        }
        #endregion

        #region Problem Day 16 104. Maximum Depth of Binary Tree
        public int MaxDepth(TreeNode root)
        {
            if (root == null) return 0;
            return 1 + Math.Max(MaxDepth(root.left), MaxDepth(root.right));
        }
        #endregion

        #region Problem Day 17 783. Minimum Distance Between BST Nodes
        public int MinDiffInBST(TreeNode root)
        {
            List<int> lst = new List<int>();

            inorder_783(root, lst);

            int result = int.MaxValue;

            for (int i = 1; i < lst.Count; i++)
            {
                result = Math.Min(result, lst[i] - lst[i - 1]);
            }
            return result;
        }

        private void inorder_783(TreeNode root, List<int> lst)
        {
            if (root == null) return;

            inorder_783(root.left, lst);
            lst.Add(root.val);
            inorder_783(root.right, lst);
        }
        #endregion

        #region Problem Day 18 226. Invert Binary Tree
        public TreeNode InvertTree(TreeNode root)
        {
            if (root == null) return null;

            return new TreeNode(root.val, InvertTree(root.right), InvertTree(root.left));
        }
        #endregion

        #region Problem Day 19 103. Binary Tree Zigzag Level Order Traversal
        public IList<IList<int>> ZigzagLevelOrder(TreeNode root)
        {
            IList<IList<int>> lst = new List<IList<int>>();

            Queue<(int index, TreeNode node)> q = new Queue<(int, TreeNode)>();
            if (root != null)
            {
                q.Enqueue((0, root));

                while (q.Count > 0)
                {
                    var p = q.Dequeue();
                    if (lst.Count == p.index)
                    {
                        lst.Add(new List<int>());
                    }
                    if (p.index % 2 == 0)
                    {
                        lst[p.index].Add(p.node.val);
                    }
                    else
                    {
                        lst[p.index].Insert(0, p.node.val);
                    }

                    if (p.node.left != null) q.Enqueue((p.index + 1, p.node.left));
                    if (p.node.right != null) q.Enqueue((p.index + 1, p.node.right));
                }
            }
            return lst;
        }
        #endregion

        #region Problem Day 20 35. Search Insert Position
        public int SearchInsert(int[] nums, int target)
        {
            int index = -1;

            int low = 0, high = nums.Length - 1;

            while (low <= high)
            {
                if (nums[low] >= target)
                {
                    return low;
                }

                if (nums[high] < target)
                {
                    return high + 1;
                }

                int mid = (low + high) / 2;

                if (nums[mid] == target)
                {
                    return mid;
                }

                if (nums[mid] > target)
                {
                    high = mid - 1;
                }
                else
                {
                    low = mid + 1;
                }
            }

            return index;
        }
        #endregion

        #region Problem Day 21
        #endregion

        #region Problem Day 22 1011. Capacity To Ship Packages Within D Days
        public int ShipWithinDays(int[] weights, int days)
        {
            int totalload = 0, maxload = 0;
            for (int i = 0; i < weights.Length; i++)
            {
                totalload += weights[i];
                maxload = Math.Max(maxload, weights[i]);
            }

            int l = maxload, r = totalload;

            while (l < r)
            {
                int mid = (l + r) / 2;

                if (feasible_1011(weights, mid, days))
                {
                    r = mid;
                }
                else
                {
                    l = mid + 1;
                }
            }
            return l;
            //int result = weights.Max();

            //(int sum, IList<int> vals)[] list = new (int sum, IList<int> vals)[days];

            //for (int i = 0; i < days; i++)
            //{
            //    list[i] = (0, new List<int>());
            //}

            //int accomodated = 0;
            //int day = 0;
            //for (; accomodated < weights.Length; accomodated++)
            //{
            //    if (list[day].sum + weights[accomodated] > result)
            //    {
            //        day++;

            //        if (day == days) break;
            //    }

            //    list[day].sum += weights[accomodated];
            //    list[day].vals.Add(weights[accomodated]);
            //}

            //while (accomodated < weights.Length)
            //{
            //    result++;
            //    for (day = 0; day < days - 1; day++)
            //    {
            //        while (list[day + 1].vals.Count>0 && list[day].sum + list[day + 1].vals[0] <= result)
            //        {
            //            int k = list[day + 1].vals[0];
            //            list[day + 1].vals.RemoveAt(0);
            //            list[day].vals.Add(k);
            //            list[day].sum += k;
            //            list[day + 1].sum -= k;
            //        }
            //    }

            //    while (accomodated < weights.Length && list[day].sum + weights[accomodated] <= result)
            //    {
            //        list[day].vals.Add(weights[accomodated]);
            //        list[day].sum += weights[accomodated];
            //        accomodated++;
            //    }
            //}
            //return result;
        }

        private bool feasible_1011(int[] weights, int mid, int days)
        {
            int daysNeeded = 1, currentLoad = 0;
            foreach (var weight in weights)
            {
                currentLoad += weight;

                if (currentLoad > mid)
                {
                    daysNeeded++;
                    currentLoad = weight;
                }
            }

            return daysNeeded <= days;
        }
        #endregion

        #region Problem Day 23 502. IPO
        public int FindMaximizedCapital(int k, int w, int[] profits, int[] capital)
        {
            (int capital, int profit)[] projects = new (int capital, int profit)[profits.Length];

            for (int i = 0; i < profits.Length; i++)
            {
                projects[i] = (capital[i], profits[i]);
            }

            Array.Sort(projects, (x, y) => { return x.capital - y.capital; });

            PriorityQueue<int, int> queue = new PriorityQueue<int, int>();
            int completed = 0;
            while (k-- > 0)
            {
                while (completed < projects.Length && projects[completed].capital <= w)
                {
                    queue.Enqueue(projects[completed].profit, int.MaxValue - projects[completed].profit);
                    completed++;
                }

                if (queue.Count == 0)
                {
                    break;
                }

                w += queue.Dequeue();
            }
            return w;
        }
        #endregion

        #region Problem Day 24 1675. Minimize Deviation in Array
        public int MinimumDeviation(int[] nums)
        {
            return 0;
        }
        #endregion

        #region Problem Day 25
        #endregion

        #region Problem Day 26
        #endregion

        #region Problem Day 27
        public Node1 Construct(int[][] grid)
        {
            int n = grid.Length;
            if (n == 1) return new Node1(grid[0][0] == 1, true);
            int subN = n / 2;

            //TopLeft
            Node1 topLeft = getNode(grid, 0, 0);
            Node1 topRight = getNode(grid, 0, subN);
            Node1 bottomLeft = getNode(grid, subN, 0);
            Node1 bottomRight = getNode(grid, subN, subN);

            if (isEqual(topLeft, topRight) && isEqual(bottomLeft, topRight) && isEqual(bottomLeft, bottomRight)) return new Node1(bottomRight.val, true);
            return new Node1(true, false, topLeft, topRight, bottomLeft, bottomRight);
        }

        private bool isEqual(Node1 node1, Node1 node2)
        {
            return node1.isLeaf && node2.isLeaf && (node1.val == node2.val);
        }

        private Node1 getNode(int[][] grid, int startXIndex, int startYIndex)
        {
            int sum = 0;
            int n = grid.Length / 2;
            int[][] subgrid = new int[n][];
            for (int i = 0; i < n; i++)
            {
                subgrid[i] = new int[n];
                for (int j = 0; j < n; j++)
                {
                    subgrid[i][j] = grid[i + startXIndex][j + startYIndex];
                    sum += subgrid[i][j];
                }
            }

            return sum == 0 || sum == n * n ? new Node1(subgrid[0][0] == 1, true) : Construct(subgrid);
        }
        public class Node1
        {
            public bool val;
            public bool isLeaf;
            public Node1 topLeft;
            public Node1 topRight;
            public Node1 bottomLeft;
            public Node1 bottomRight;

            public Node1()
            {
                val = false;
                isLeaf = false;
                topLeft = null;
                topRight = null;
                bottomLeft = null;
                bottomRight = null;
            }

            public Node1(bool _val, bool _isLeaf)
            {
                val = _val;
                isLeaf = _isLeaf;
                topLeft = null;
                topRight = null;
                bottomLeft = null;
                bottomRight = null;
            }

            public Node1(bool _val, bool _isLeaf, Node1 _topLeft, Node1 _topRight, Node1 _bottomLeft, Node1 _bottomRight)
            {
                val = _val;
                isLeaf = _isLeaf;
                topLeft = _topLeft;
                topRight = _topRight;
                bottomLeft = _bottomLeft;
                bottomRight = _bottomRight;
            }
        }
        #endregion

        #region Problem Day 28 652. Find Duplicate Subtrees
        Dictionary<string, int>? map_652;
        public IList<TreeNode> FindDuplicateSubtrees(TreeNode root)
        {
            map_652 = new Dictionary<string, int>();
            IList<TreeNode> subtrees = new List<TreeNode>();

            helper_652(root, subtrees);

            return subtrees;
        }

        private string helper_652(TreeNode root, IList<TreeNode> treeNodes)
        {
            if (root == null) return string.Empty;
            StringBuilder sb = new StringBuilder();

            sb.Append(helper_652(root.left, treeNodes));
            sb.Append(",");
            sb.Append(helper_652(root.right, treeNodes));
            sb.Append(",");
            sb.Append(root.val);

            string s = sb.ToString();
            if (!map_652.ContainsKey(s))
            {
                map_652.Add(s, 0);
            }
            map_652[s]++;

            if (map_652[s] == 2)
            {
                treeNodes.Add(root);
            }
            return s;
        }
        #endregion

        #region Problem Day 29
        #endregion

        #region Problem Day 30
        #endregion

        #region Problem Day 31
        #endregion

        #region Problem 2264. Largest 3-Same-Digit Number in String
        public string LargestGoodInteger(string num)
        {
            int k = int.MinValue;

            int i = 0;

            while (i < num.Length - 2)
            {
                if (num[i] == num[i + 1] && num[i + 2] == num[i])
                {
                    k = Math.Max(k, int.Parse(num.Substring(i, 3)));
                }
                i++;
            }

            return k == int.MinValue ? string.Empty : k == 0 ? "000" : k.ToString();
        }
        #endregion

        #region Problem 721. Accounts Merge
        HashSet<string> set_721;
        Dictionary<string, List<string>> dictionary_721;
        public IList<IList<string>> AccountsMerge(IList<IList<string>> accounts)
        {
            set_721 = new HashSet<string>();
            dictionary_721 = new Dictionary<string, List<string>>();
            IList<IList<string>> accountsMerge = new List<IList<string>>();

            foreach (var account in accounts)
            {
                var p = account[1];

                if (!dictionary_721.ContainsKey(p))
                {
                    dictionary_721.Add(p, new List<string>());
                }
                for (int j = 2; j < account.Count; j++)
                {
                    var q = account[j];
                    dictionary_721[p].Add(q);

                    if (!dictionary_721.ContainsKey(q))
                    {
                        dictionary_721.Add(q, new List<string>());
                    }
                    dictionary_721[q].Add(p);

                }
            }


            foreach (var account in accounts)
            {
                var person = account[0];
                var firstEmail = account[1];

                if (!set_721.Contains(firstEmail))
                {
                    List<string> list = new List<string>();

                    DFS_721(list, firstEmail);
                    list = list.OrderBy(x => x, StringComparer.Ordinal).ToList();
                    list.Insert(0, person);
                    accountsMerge.Add(list);
                }
            }

            return accountsMerge;
        }

        private void DFS_721(List<string> list, string firstEmail)
        {
            set_721.Add(firstEmail);
            // Add the email vector that contains the current component's emails
            list.Add(firstEmail);

            if (!dictionary_721.ContainsKey(firstEmail))
            {
                return;
            }

            foreach (var item in dictionary_721[firstEmail])
            {
                if (!set_721.Contains(item))
                {
                    DFS_721(list, item);
                }
            }
        }

        public IList<IList<string>> AccountsMerge_V1(IList<IList<string>> accounts)
        {
            IList<IList<string>> accountsMerge = new List<IList<string>>();

            IDictionary<string, int> dct = new Dictionary<string, int>();

            foreach (IList<string> account in accounts)
            {
                int k = accountsMerge.Count;
                HashSet<string> lst = new HashSet<string>();

                for (int i = 1; i < account.Count; i++)
                {
                    if (lst.Contains(account[i])) continue;
                    if (dct.ContainsKey(account[i]))
                    {
                        int existingIndex = dct[account[i]];

                        var l = accountsMerge[existingIndex].Skip(1).ToList();
                        accountsMerge[existingIndex] = new List<string>(accountsMerge[existingIndex].Take(1));

                        foreach (var x1 in l)
                        {
                            lst.Add(x1);

                            dct[x1] = k;
                        }

                    }
                    else
                    {
                        lst.Add(account[i]);
                        dct.Add(account[i], k);
                    }
                }

                var q = lst.OrderBy(x => x, StringComparer.Ordinal).ToList();
                q.Insert(0, account[0]);
                accountsMerge.Add(q);
            }


            return accountsMerge.Where(x => x.Count > 1).OrderBy(x => x[0]).ToList();
        }
        #endregion

        #region Weekly 332
        public long FindTheArrayConcVal(int[] nums)
        {
            int low = 0;
            int high = nums.Length - 1;

            long result = 0;

            while (low < high)
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(nums[low++]);
                stringBuilder.Append(nums[high--]);

                result += long.Parse(stringBuilder.ToString());
            }

            if (low == high) result += nums[high];
            return result;

        }

        public long CountFairPairs(int[] nums, int lower, int upper)
        {
            Array.Sort(nums);
            long result = 0;

            for (int i = 0; i < nums.Length - 1; i++)
            {
                int n1 = nums[i];

                int low = i + 1;
                int high = nums.Length - 1;

                int lowerLimit = lower - n1;
                //if (lowerLimit < n1) continue;
                int upperLimit = upper - n1;

                int x = findlowerlimitIndex(nums, lowerLimit, i);
                int y = findhigherlimitIndex(nums, upperLimit, x);

                if (x == -1 || y == -1) continue;
                result += y - x + 1;

            }

            return result;
        }

        private int findhigherlimitIndex(int[] nums, int upperLimit, int i)
        {
            int low = i;
            int high = nums.Length - 1;

            if (nums[high] <= upperLimit) return high;

            if (nums[low] > upperLimit) return -1;

            if (nums[low] == upperLimit)
            {
                while (++low < high && nums[low] == upperLimit)
                {

                }
                return low - 1;
            }

            while (low < high)
            {
                int mid = (low + high) / 2;
                if (high == mid) return high - 1;

                if (nums[mid] == upperLimit)
                {
                    while (nums[++mid] == upperLimit) { }
                    return mid - 1;
                }

                if (nums[mid] < upperLimit)
                {
                    low = mid + 1;
                }
                else
                {
                    high = mid;
                }
            }

            return high - 1;
        }

        private int findlowerlimitIndex(int[] nums, int lowerLimit, int i)
        {
            int low = i + 1;
            int high = nums.Length - 1;

            if (nums[low] >= lowerLimit) return low;

            if (nums[high] < lowerLimit) return -1;
            if (nums[high] == lowerLimit)
            {
                while (--high > low && nums[high] == lowerLimit)
                {
                }
                return high + 1;
            }
            while (low < high)
            {
                int mid = (low + high) / 2;
                if (low == mid) return low + 1;

                if (nums[mid] == lowerLimit)
                {
                    while (nums[--mid] == lowerLimit) { }
                    return mid + 1;
                }

                if (nums[mid] > lowerLimit)
                {
                    high = mid;
                }
                else
                {
                    low = mid;
                }
            }

            return 0;
        }
        #endregion

        #region Weekly 333
        public int[][] MergeArrays(int[][] nums1, int[][] nums2)
        {
            Dictionary<int, int> result = new Dictionary<int, int>();

            foreach (var item in nums1)
            {
                if (!result.ContainsKey(item[0]))
                {
                    result.Add(item[0], 0);
                }
                result[item[0]] += item[1];
            }

            foreach (var item in nums2)
            {
                if (!result.ContainsKey(item[0]))
                {
                    result.Add(item[0], 0);
                }
                result[item[0]] += item[1];
            }

            int[][] res = new int[result.Count][];

            int i = 0;

            foreach (var item in result.Keys.OrderBy(x => x).ToList())
            {
                res[i++] = new int[] { item, result[item] };
            }

            return res;
        }

        public int MinOperations(int n)
        {
            if (n <= 3) return n;

            int[] dp = new int[n + 1];
            for (int i = 0; i < n + 1; i++)
                dp[i] = -1;

            dp[0] = 0;
            dp[1] = 1;
            dp[2] = 2;
            dp[3] = 3;

            int sqr;
            for (int i = 4; i <= n; i++)
            {
                sqr = (int)Math.Sqrt(i);

                int best = int.MaxValue;

                while (sqr > 1)
                {
                    if (i % sqr == 0)
                    {
                        best = Math.Min(best, 1 + dp[sqr]);
                    }
                    sqr--;
                }

                best = Math.Min(best, 1 + dp[i - 1]);

                dp[i] = best;
            }

            return dp[n];
        }
        #endregion

        #region Weekly 334
        public int[] LeftRigthDifference(int[] nums)
        {
            int[] result = new int[nums.Length];

            int[] left = new int[nums.Length];
            int[] right = new int[nums.Length];

            for (int i = 1; i < nums.Length; i++)
            {
                left[i] = left[i - 1] + nums[i - 1];
                right[nums.Length - i - 1] = right[nums.Length - i] + nums[nums.Length - i];
            }

            for (int i = 0; i < nums.Length; i++)
            {
                result[i] = Math.Abs(left[i] - right[i]);
            }

            return result;
        }
        #endregion
    }
}
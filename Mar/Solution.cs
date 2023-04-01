using Common;

namespace Mar
{
    public class Solution
    {
        List<int> list;
        public Solution()
        {

        }

        public Solution(ListNode head)
        {
            list = new List<int>();

            while (head != null)
            {
                list.Add(head.val);
                head = head.next;
            }
        }
        #region Problem Day 1 912. Sort an Array
        public int[] SortArray(int[] nums)
        {
            int n = nums.Length;

            for (int i = (n / 2) - 1; i >= 0; i--)
            {
                heapify(nums, n, i);
            }

            for (int i = n - 1; i > 0; i--)
            {
                swap(nums, 0, i);
                heapify(nums, i, 0);
            }
            return nums;
        }

        private void heapify(int[] nums, int n, int i)
        {
            int maxIndex = i;
            int l = 2 * i + 1;
            int r = 2 * i + 2;

            if (l < n && nums[l] > nums[maxIndex]) maxIndex = l;
            if (r < n && nums[r] > nums[maxIndex]) maxIndex = r;

            if (maxIndex != i)
            {
                swap(nums, i, maxIndex);
                heapify(nums, i, maxIndex);
            }
        }


        private void swap(int[] nums, int v, int i)
        {
            int temp = nums[i];

            nums[i] = nums[v];
            nums[v] = temp;
        }
        #endregion

        #region Problem Day 2 443. String Compression
        public int Compress(char[] chars)
        {
            int index = 0;
            int len = 0;

            while (index < chars.Length)
            {
                chars[len] = chars[index++];

                int count = 1;
                while (index < chars.Length && chars[len] == chars[index])
                {
                    count++;
                    index++;
                }

                if (count > 1)
                {
                    foreach (char c in count.ToString())
                    {
                        chars[++len] = c;
                    }
                }
                len++;
            }
            return len;
        }
        #endregion

        #region Problem Day 3 28. Find the Index of the First Occurrence in a String
        public int StrStr(string haystack, string needle)
        {
            int start = 0;
            int len = needle.Length;

            while (start + len <= haystack.Length)
            {
                if (haystack[start] == needle[0])
                {
                    if (haystack.Substring(start, len).Equals(needle)) return start;
                }
                start++;
            }
            return -1;
        }
        #endregion

        #region Problem Day 4
        #endregion

        #region Problem Day 5 1345. Jump Game IV
        public int MinJumps(int[] arr)
        {
            int n = arr.Length;

            if (n <= 1) return 0;

            return n;
        }
        #endregion

        #region Problem Day 6 1539. Kth Missing Positive Number
        public int FindKthPositive(int[] arr, int k)
        {
            int low = 0, n = arr.Length;

            int high = n - 1;

            if (arr[0] > k)
            {
                return k;
            }

            if (arr[n - 1] == n || arr[n - 1] - n < k)
            {
                return n + k;
            }

            int diff;


            while (high - low >= 2)
            {

                int mid = (low + high) / 2;
                diff = arr[mid] - (mid + 1);

                if (diff >= k)
                {
                    high = mid;
                }
                else
                {
                    low = mid;
                }
            }

            return arr[low] + k - (arr[low] - (low + 1));
        }

        #endregion

        #region Problem Day 7 2187. Minimum Time to Complete Trips
        public long MinimumTime(int[] time, int totalTrips)
        {
            long result = long.MaxValue;

            long left = 1;

            long right = (long)time.Max() * (long)totalTrips;

            while (left <= right)
            {
                long mid = (left + right) / 2;

                long tripcount = 0;

                for (int i = 0; i < time.Length; i++)
                {
                    tripcount += mid / time[i];
                }

                if (tripcount >= totalTrips)
                {
                    result = Math.Min(result, mid);

                    right = mid - 1;
                }
                else
                {
                    left = mid + 1;
                }

            }

            return result;
        }
        #endregion

        #region Problem Day 8 875. Koko Eating Bananas
        public int MinEatingSpeed(int[] piles, int h)
        {
            if (piles.Length == h) return piles.Max();

            int left = 1;
            int right = piles.Max();
            int k = int.MaxValue;
            while (left <= right)
            {
                int mid = (left + right) / 2;

                long p = 0;

                for (int i = 0; i < piles.Length; i++)
                {
                    p += ((long)piles[i] / (long)mid);

                    int rem = piles[i] % mid;

                    if (rem > 0) p++;
                }

                if (p <= h)
                {
                    k = Math.Min(k, mid);
                    right = mid - 1;
                }
                else
                {
                    left = mid + 1;
                }
            }

            return k;
        }
        #endregion

        #region Problem Day 9 142. Linked List Cycle II
        public ListNode DetectCycle(ListNode head)
        {

            if (head == null || head.next == null) return null;

            ListNode fast = head;
            ListNode slow = head;

            while (fast != null && fast.next != null)
            {
                slow = slow.next;
                fast = fast.next.next;

                if (slow == fast) break;
            }

            if (fast == null || fast.next == null) return null;

            fast = head;

            while (fast != slow)
            {
                fast = fast.next;
                slow = slow.next;
            }
            return fast;
        }
        #endregion

        #region Problem Day 10 382. Linked List Random Node
        public int GetRandom()
        {
            return list[new Random().Next(list.Count)];
        }
        #endregion

        #region Problem Day 11
        #endregion

        #region Problem Day 12 23. Merge k Sorted Lists
        public ListNode MergeKLists(ListNode[] lists)
        {
            return null;
        }
        #endregion

        #region Problem Day 13 101. Symmetric Tree
        public bool IsSymmetric(TreeNode root)
        {
            return IsSymmetric_Helper(root.left, root.right);
        }

        public bool IsSymmetric_Helper(TreeNode node1, TreeNode node2)
        {
            if (node1 == null && node2 == null) return true;

            if ((node1 != null && node2 == null) || (node1 == null && node2 != null)) return false;

            return node1.val == node2.val && IsSymmetric_Helper(node1.left, node2.right) && IsSymmetric_Helper(node1.right, node2.left);
        }
        #endregion

        #region Problem Day 14
        #endregion

        #region Problem Day 15 958. Check Completeness of a Binary Tree
        public bool IsCompleteTree(TreeNode root)
        {
            int[] arr = new int[101];

            Queue<(TreeNode node, int index)> queue = new Queue<(TreeNode node, int index)>();
            arr[0] = int.MaxValue;
            queue.Enqueue((root, 1));
            int lastIndex = -1;
            while (queue.Count > 0)
            {
                var p = queue.Dequeue();

                if (p.node.right != null && p.node.left == null) return false;
                if (arr[p.index - 1] == 0) return false;
                arr[p.index] = p.node.val;
                lastIndex = Math.Max(lastIndex, p.index);
                if (p.node.left != null)
                {
                    queue.Enqueue((p.node.left, 2 * p.index));

                }


                if (p.node.right != null)
                {
                    queue.Enqueue((p.node.right, (2 * p.index) + 1));
                }
            }


            for (int i = 1; i <= lastIndex; i++)
            {
                if (arr[i] == 0) return false;
            }

            return true;
        }
        #endregion

        #region Problem Day 16 106. Construct Binary Tree from Inorder and Postorder Traversal
        public TreeNode BuildTree(int[] inorder, int[] postorder)
        {
            int len = inorder.Length;
            int rootVal = postorder[len - 1];
            TreeNode root = new TreeNode(rootVal, null, null);

            int index = Array.IndexOf(inorder, rootVal);
            int secLen = len - 1 - index;
            if (index > 0)
            {
                int[] inorder1 = new int[index];
                int[] postorder1 = new int[index];
                Array.Copy(inorder, inorder1, index);
                Array.Copy(postorder, postorder1, index);
                root.left = BuildTree(inorder1, postorder1);
            }
            if (secLen > 0)
            {
                int[] inorder2 = new int[secLen];
                int[] postorder2 = new int[secLen];
                Array.Copy(inorder, index + 1, inorder2, 0, secLen);
                Array.Copy(postorder, index, postorder2, 0, secLen);
                root.right = BuildTree(inorder2, postorder2);
            }
            return root;
        }
        #endregion

        #region Problem Day 17 208. Implement Trie (Prefix Tree)
        public class Trie1
        {
            Trie1[] childs;
            bool ends = false;
            public Trie1()
            {
                childs = new Trie1[26];
            }

            public void Insert(string word)
            {
                Trie1 trie = this;


                int i = 0;

                while (i < word.Length)
                {
                    int index = word[i] - 'a';
                    if (trie.childs[index] == null)
                    {
                        trie.childs[index] = new Trie1();
                    }

                    trie = trie.childs[index];
                    i++;
                }
                trie.ends = true;
            }

            public bool Search(string word)
            {
                Trie1 trie = this;
                int i = 0;

                int index = -1;
                while (i < word.Length)
                {
                    index = word[i] - 'a';
                    if (trie.childs[index] == null) return false;
                    trie = trie.childs[index];
                    i++;
                }
                return trie.ends;
            }

            public bool StartsWith(string prefix)
            {
                Trie1 trie = this;
                int i = 0;

                int index = -1;
                while (i < prefix.Length)
                {
                    index = prefix[i] - 'a';
                    if (trie.childs[index] == null) return false;
                    trie = trie.childs[index];
                    i++;
                }
                return true;
            }
        }
        #endregion

        #region Problem Day 18 1472. Design Browser History
        public class BrowserHistory
        {
            Stack<string> history;
            Stack<string> history2;
            public BrowserHistory(string homepage)
            {
                history2 = new Stack<string>();
                history = new Stack<string>();
                history.Push(homepage);
            }

            public void Visit(string url)
            {
                history.Push(url);
                history2.Clear();
            }

            public string Back(int steps)
            {
                while (steps-- > 0 && history.Count > 1)
                {
                    history2.Push(history.Pop());
                }
                return history.Peek();
            }

            public string Forward(int steps)
            {
                while (history2.Count > 0 && steps-- > 0)
                {
                    history.Push(history2.Pop());
                }
                return history.Peek();
            }
        }
        #endregion 

        #region Problem Day 19 211. Design Add and Search Words Data Structure
        public class WordDictionary
        {
            class TrieNode
            {
                internal Dictionary<char, TrieNode> childrens;
                internal bool end;
                public TrieNode()
                {
                    childrens = new Dictionary<char, TrieNode>();
                    end = false;
                }
            }

            TrieNode root;

            public WordDictionary()
            {
                root = new TrieNode();
            }

            public void AddWord(string word)
            {
                TrieNode node = root;

                int i = 0;

                while (i < word.Length)
                {
                    char c = word[i];

                    if (!node.childrens.ContainsKey(c))
                    {
                        node.childrens.Add(c, new TrieNode());
                    }

                    node = node.childrens[c];
                    i++;
                }
                node.end = true;
            }

            public bool Search(string word)
            {
                TrieNode node = root;
                return helper(root, 0, word);
            }

            private bool helper(TrieNode root, int i, string word)
            {
                while (i < word.Length)
                {
                    char ch = word[i];

                    if (ch == '.')
                    {
                        foreach (char c in root.childrens.Keys)
                        {
                            TrieNode root1 = root.childrens[c];

                            if (helper(root1, i + 1, word)) return true;
                        }

                        return false;
                    }
                    else
                    {
                        if (!root.childrens.ContainsKey(ch)) return false;

                        root = root.childrens[ch];
                    }
                    i++;
                }
                return root.end;
            }
        }
        public class WordDictionary2
        {
            class TrieNode2
            {
                internal TrieNode2[] children = null;
                internal bool end;
                public TrieNode2()
                {
                    children = new TrieNode2[26];
                    end = false;
                }
            }

            TrieNode2 root;

            public WordDictionary2()
            {
                root = new TrieNode2();
            }

            public void AddWord(string word)
            {
                TrieNode2 node = root;
                int i = 0;
                int index;
                while (i < word.Length)
                {
                    index = word[i] - 'a';

                    if (node.children[index] == null)
                    {
                        node.children[index] = new TrieNode2();
                    }
                    node = node.children[index];
                    i++;
                }
                node.end = true;
            }

            public bool Search(string word)
            {
                return false;
            }

            bool helper(TrieNode2 node, int i, char[] word)
            {
                while (i < word.Length)
                {
                    if (word[i] == '.')
                    {
                        for (int j = 0; j < 26; j++)
                        {
                            if (node.children[j] != null)
                            {
                                if (helper(node.children[j], i + 1, word))
                                {
                                    return true;
                                }
                            }
                        }
                        return false;
                    }
                    else
                    {
                        if (node.children[word[i] - 'a'] != null)
                        {
                            node = node.children[word[i] - 'a'];
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                return node.end;
            }
        }

        public class WordDictionary1
        {
            Dictionary<char, WordDictionary1> words;
            bool ends = false;
            public WordDictionary1()
            {
                words = new Dictionary<char, WordDictionary1>();
            }

            public void AddWord(string word)
            {
                WordDictionary1 trie = this;


                int i = 0;

                while (i < word.Length)
                {
                    if (!words.ContainsKey(word[i]))
                    {
                        words.Add(word[i], new WordDictionary1());
                    }

                    trie = trie.words[word[i]];
                    i++;
                }
                trie.ends = true;
            }

            public bool Search(string word)
            {
                WordDictionary1 trie = this;
                int i = 0;
                while (i < word.Length)
                {
                    char c = word[i];
                    if (c == '.')
                    {
                        WordDictionary1 temp = null;
                        foreach (var item in trie.words)
                        {

                        }
                    }
                    else
                    {
                        if (!trie.words.ContainsKey(c)) break;

                        trie = trie.words[c];
                    }
                }
                return false;
            }
        }
        #endregion

        #region Problem Day 20 605. Can Place Flowers
        public bool CanPlaceFlowers(int[] flowerbed, int n)
        {
            if (n == 0) return true;
            int length = flowerbed.Length;
            int maxFlowerPlant = length / 2;
            if (length % 2 == 1)
            {
                maxFlowerPlant++;
            }

            if (maxFlowerPlant < n) return false;

            if (length == 1)
            {
                if (n == 1 && flowerbed[0] == 0) return true;
                return false;
            }

            if (flowerbed[0] == 0 && flowerbed[1] == 0)
            {
                flowerbed[0] = 1;
                n--;
            }

            if (flowerbed[length - 1] == 0 && flowerbed[length - 2] == 0)
            {
                flowerbed[length - 1] = 1;
                n--;
            }

            if (n <= 0) return true;

            for (int i = 2; i < flowerbed.Length - 1; i = i + 2)
            {
                if (flowerbed[i] == 0 && flowerbed[i + 1] == 0)
                {
                    flowerbed[i] = 1;
                    n--;
                }

                if (n == 0) break;
            }


            return n == 0;
        }
        #endregion

        #region Problem Day 21 2348. Number of Zero-Filled Subarrays
        public long ZeroFilledSubarray(int[] nums)
        {
            long result = 0;

            long count = 0;

            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] == 0)
                {
                    count++;
                }
                else
                {
                    result += (count * (count + 1)) / 2;
                    count = 0;
                }
            }
            if (count > 0)
            {
                result += (count * (count + 1)) / 2;
            }
            return result;
        }
        #endregion

        #region Problem Day 22 2492. Minimum Score of a Path Between Two Cities
        private int[] parent_2492;
        private int[] rank_2492;
        public int MinScore(int n, int[][] roads)
        {
            parent_2492 = new int[n];
            rank_2492 = new int[n];

            for (int i = 0; i < n; i++)
            {
                parent_2492[i] = i;
            }

            for (int i = 0; i < n; i++)
            {
                union_2492(roads[i][0] - 1, roads[i][1] - 1);
            }

            int res = int.MaxValue;

            for (int i = 0; i < roads.Length; i++)
            {
                if (find_2492(roads[i][0] - 1) == find_2492(0) && find_2492(roads[i][0] - 1) == find_2492(roads[i][1] - 1))
                {
                    res = Math.Min(roads[i][2], res);
                }
            }

            return res;
        }

        private int find_2492(int x)
        {
            if (parent_2492[x] != x)
            {
                parent_2492[x] = find_2492(parent_2492[x]);
            }
            return parent_2492[x];
        }

        private void union_2492(int x, int y)
        {
            int x1 = find_2492(x);
            int y1 = find_2492(y);

            if (x1 == y1) return;

            if (rank_2492[x1] < rank_2492[y1])
            {
                parent_2492[x1] = y1;
            }
            else if (rank_2492[x1] > rank_2492[y1])
            {
                parent_2492[y1] = x1;
            }
            else
            {
                parent_2492[y1] = x1;
                rank_2492[x1] = rank_2492[x1] + 1;
            }
        }
        #endregion

        #region Problem Day 23 1319. Number of Operations to Make Network Connected

        public class DisjointSet_1319
        {
            public int[] Parent;
            public int[] Rank;
            public int[] Size;
            public DisjointSet_1319(int length)
            {
                Parent = new int[length];
                Rank = new int[length];
                Size = new int[length];

                for (int i = 0; i < length; i++)
                {
                    Parent[i] = i;
                    Size[i] = i;
                }
            }

            public int FindParent(int v)
            {
                if (v == Parent[v]) return v;

                return Parent[v] = FindParent(Parent[v]);
            }

            public void UnionByRank(int u, int v)
            {
                int u1 = FindParent(u);
                int v1 = FindParent(v);

                if (u1 == v1) return;

                if (Rank[u1] < Rank[v1])
                {
                    Parent[u1] = v1;
                }
                else if (Rank[u1] > Rank[v1])
                {
                    Parent[v1] = u1;
                }
                else
                {
                    Parent[v1] = u1;
                    Rank[u1]++;
                }
            }

            public void UnionBySize(int u, int v)
            {
                int u1 = FindParent(u);
                int v1 = FindParent(v);

                if (u1 == v1) return;

                if (Size[u1] < Size[v1])
                {
                    Parent[u1] = v1;
                    Size[v1] += Size[u1];
                }
                else
                {
                    Parent[v1] = u1;
                    Size[u1] += Size[v1];
                }
            }
        }

        public int MakeConnected(int n, int[][] connections)
        {
            DisjointSet_1319 disjointSet = new DisjointSet_1319(n + 1);
            int countExtraEdges = 0;

            foreach (var edge in connections)
            {
                int u = edge[0];
                int v = edge[1];

                if (disjointSet.FindParent(u) == disjointSet.FindParent(v))
                {
                    countExtraEdges++;
                }
                else
                {
                    disjointSet.UnionBySize(u, v);
                }
            }

            int countComponents = 0;

            for (int i = 0; i < n; i++)
            {
                if (disjointSet.Parent[i] == i) countComponents++;
            }

            return countExtraEdges >= countComponents - 1 ? countComponents - 1 : -1;
        }
        #endregion

        #region Problem Day 24 1466. Reorder Routes to Make All Paths Lead to the City Zero
        //public int MinReorder(int n, int[][] connections)
        //{
        //    IDictionary<int, IList<int>> nodes = new Dictionary<int, IList<int>>();

        //    for (int i = 0; i < n; i++)
        //    {
        //        nodes.Add(i, new List<int>());
        //    }

        //    foreach (int[] arr in connections)
        //    {
        //        nodes[arr[0]].Add(arr[1]);
        //        nodes[arr[1]].Add(-arr[0]);
        //    }

        //    return dfs12(nodes, new bool[n], 0);
        //}

        //private int dfs12(int[] edgedistance, IDictionary<int, IList<int>> nodes, bool[] vs)
        //{
        //    int count = 0;
        //    vs[v] = true;

        //    foreach (int val in nodes[v])
        //    {
        //        if (!vs[Math.Abs(val)])
        //        {
        //            count += dfs12(nodes, vs, Math.Abs(val)) + (val > 0 ? 1 : 0);
        //        }
        //    }
        //    return count;
        //}
        #endregion

        #region Problem Day 25 2316. Count Unreachable Pairs of Nodes in an Undirected Graph
        public long CountPairs(int n, int[][] edges)
        {
            return 0;
        }
        public long CountPairs_V2(int n, int[][] edges)
        {
            long result = 0;
            bool[] visited = new bool[n];
            IDictionary<int, HashSet<int>> nodes = new Dictionary<int, HashSet<int>>();
            for (int i = 0; i < n; i++)
            {
                nodes.Add((int)i, new HashSet<int>());
            }

            foreach (int[] edge in edges)
            {
                nodes[edge[0]].Add(edge[1]);
                nodes[edge[1]].Add(edge[0]);
            }
            IList<IList<int>> components = new List<IList<int>>();
            int k = n;
            for (int i = 0; i < n; i++)
            {
                if (!visited[i])
                {
                    Stack<int> stack = new Stack<int>();
                    stack.Push(i);
                    visited[i] = true;
                    List<int> s = new List<int>();
                    while (stack.Count > 0)
                    {
                        var p = stack.Pop();
                        s.Add(p);
                        foreach (int item in nodes[p])
                        {
                            if (!visited[item])
                            {
                                stack.Push(item);
                                visited[item] = true;
                            }
                        }
                    }
                    k -= s.Count;
                    result += ((s.Count) * k);
                    components.Add(s);
                }
            }

            //int k = n;


            //foreach (var item in components)
            //{
            //    int a = item.Count;
            //    k -= a;
            //    result += (k * a);
            // }

            return result;
        }
        public long CountPairs_V1(int n, int[][] edges)
        {
            long result = 0;
            IDictionary<int, HashSet<int>> nodes = new Dictionary<int, HashSet<int>>();
            for (int i = 0; i < n; i++)
            {
                nodes.Add((int)i, new HashSet<int>());
            }

            foreach (int[] edge in edges)
            {
                nodes[edge[0]].Add(edge[1]);
                nodes[edge[1]].Add(edge[0]);
            }

            foreach (int key in nodes.Keys)
            {
                Queue<int> queue = new Queue<int>();
                queue.Enqueue(key);
                HashSet<int> visited = new HashSet<int>();
                while (queue.Count > 0)
                {
                    int v = queue.Dequeue();

                    visited.Add(v);

                    foreach (int c in nodes[v])
                    {
                        if (!visited.Contains(c)) queue.Enqueue(c);
                    }
                }
                nodes[key] = new HashSet<int>(visited);
                result += n - visited.Count;
            }

            return result / 2;
        }
        #endregion

        #region Problem Day 26 2360. Longest Cycle in a Graph
        int result = -1;
        public int LongestCycle(int[] edges)
        {

            bool[] visited = new bool[edges.Length];
            bool[] dfsvisited = new bool[edges.Length];
            int[] edgedistance = new int[edges.Length];

            for (int i = 0; i < edges.Length; i++)
            {
                if (!visited[i])
                {
                    dfs_2360(edgedistance, edges, visited, dfsvisited, 0, i);
                }
            }

            return result;
        }

        private void dfs_2360(int[] edgedistance, int[] edges, bool[] visited, bool[] dfsvisited, int d, int i)
        {
            if (i != -1)
            {
                if (!visited[i])
                {
                    visited[i] = true;
                    dfsvisited[i] = true;
                    edgedistance[i] = d;

                    dfs_2360(edgedistance, edges, visited, dfsvisited, d + 1, edges[i]);
                }
                else if (dfsvisited[i])
                {
                    result = Math.Max(result, d - edgedistance[i]);
                }
                dfsvisited[i] = false;
            }
        }


        #endregion

        #region Problem Day 27 64. Minimum Path Sum
        public int MinPathSum(int[][] grid)
        {
            int m = grid.Length;
            int n = grid[0].Length;


            for (int i = 1; i < n; i++)
            {
                grid[0][i] += grid[0][i - 1];
            }

            for (int i = 1; i < m; i++)
            {
                grid[i][0] += grid[i - 1][0];
            }

            for (int i = 1; i < m; i++)
            {
                for (int j = 1; j < n; j++)
                {
                    grid[i][j] += Math.Min(grid[i][j - 1], grid[i - 1][j]);
                }
            }

            return grid[m - 1][n - 1];
        }
        #endregion

        #region Problem Day 28 983. Minimum Cost For Tickets
        public int MincostTickets(int[] days, int[] costs)
        {
            int[] dp = new int[days.Max() + 1];
            int dayIndex = 0;
            for (int i = 1; i <= days.Max(); i++)
            {
                if (i == days[dayIndex])
                {
                    int onedayticket = dp[i - 1] + costs[0];
                    int week7dayticket = (i - 7 > 0 ? dp[i - 7] : 0) + costs[1];
                    int month30dayticket = (i - 30 > 0 ? dp[i - 30] : 0) + costs[2];

                    dp[i] = Math.Min(onedayticket, Math.Min(week7dayticket, month30dayticket));
                    dayIndex++;
                }
                else
                {
                    dp[i] = dp[i - 1];
                }
            }
            return dp[days.Max()];
        }
        #endregion

        #region Problem Day 29 1402. Reducing Dishes
        public int MaxSatisfaction(int[] satisfaction)
        {
            Array.Sort(satisfaction);

            int count = 0;

            int startIndex = satisfaction.Length - 1;

            for (int i = startIndex; i >= 0; i--)
            {
                count += satisfaction[i];

                if (count < 0) break;
                startIndex--;
            }

            startIndex++;
            int x = 1;
            count = 0;

            for (int i = startIndex; i < satisfaction.Length; i++)
            {
                count += satisfaction[i] * x++;
            }
            return count;
        }
        #endregion

        #region Problem Day 30 87. Scramble String
        Dictionary<string, bool> map87;
        public bool IsScramble(string s1, string s2)
        {
            map87 = new Dictionary<string, bool>();
            return solve(s1, s2);
        }
        private bool solve(string s1, string s2)
        {
            if (s1.Length == 1) return s1 == s2;

            if (s1 == s2) return true;

            if (map87.ContainsKey(s1 + s2)) return map87[s1 + s2];

            for (int i = 1; i < s1.Length; i++)
            {
                if (
                    (solve(s1.Substring(0, i), s2.Substring(0, i))
                    && solve(s1.Substring(i), s2.Substring(i)))
                    || (solve(s1.Substring(0, i), s2.Substring(s2.Length - i)) &&
                    solve(s1.Substring(i), s2.Substring(0, s2.Length - i))))
                {
                    if (!map87.ContainsKey(s1 + s2))
                    {
                        map87[s1 + s2] = true;
                    }
                    return true;
                }
            }
            map87[s1 + s2] = false;
            return false;
        }

        private bool solve_v1(string s1, string s2)
        {
            if (s1.Length == 1) return s1 == s2;

            if (s1 == s2) return true;

            for (int i = 1; i < s1.Length; i++)
            {
                if (
                    (solve_v1(s1.Substring(0, i), s2.Substring(0, i))
                    && solve_v1(s1.Substring(i), s2.Substring(i)))
                    || (solve_v1(s1.Substring(0, i), s2.Substring(s2.Length - i)) &&
                    solve_v1(s1.Substring(i), s2.Substring(0, s2.Length - i))))
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region Problem Day 31
        #endregion

        #region weekly 335
        public int PassThePillow(int n, int time)
        {
            int k = n - 1;
            int d = time / k;
            int r = time % k;

            return d % 2 == 0 ? r + 1 : n - r;
        }

        public long KthLargestLevelSum(TreeNode root, int k)
        {
            long result = 0;

            Dictionary<int, IList<int>> map = new Dictionary<int, IList<int>>();

            Queue<(int level, TreeNode node)> q = new Queue<(int level, TreeNode node)>();

            q.Enqueue((0, root));

            while (q.Count > 0)
            {
                var p = q.Dequeue();

                if (!map.ContainsKey(p.level))
                {
                    map.Add(p.level, new List<int>());
                }
                map[p.level].Add(p.node.val);

                if (p.node.left != null)
                {
                    q.Enqueue((p.level + 1, p.node.left));
                }

                if (p.node.right != null)
                {
                    q.Enqueue((p.level + 1, p.node.right));
                }
            }

            if (map.Count < k) return -1;

            long[] arr = new long[map.Count];
            int i = 0;
            foreach (var le in map.Keys)
            {
                foreach (int val in map[le])
                {
                    arr[i] += val;
                }
                i++;
            }

            Array.Sort(arr);

            return arr[map.Count - k];
        }

        public int FindValidSplit(int[] nums)
        {
            int n = nums.Length;
            double[] prefix = new double[n];
            double[] suffix = new double[n];
            int i, k;

            prefix[0] = nums[0];

            // Update the prefix array
            for (i = 1; i < n; i++)
            {
                prefix[i] = prefix[i - 1] *
                            nums[i];
            }

            suffix[n - 1] = nums[n - 1];

            // Update the suffix array
            for (i = n - 2; i >= 0; i--)
            {
                suffix[i] = suffix[i + 1] *
                            nums[i];
            }

            // Iterate the given array
            for (k = 0; k < n - 1; k++)
            {
                // Check if prefix[k] and
                // suffix[k+1] are co-prime
                if (gcd(prefix[k],
                        suffix[k + 1]) == 1)
                {
                    return k;
                }
            }

            // If no index for partition
            // exists, then return -1
            return -1;
        }
        double gcd(double a, double b)
        {
            // Base Case
            if (b == 0)
                return a;

            // Find the GCD
            // recursively
            return gcd(b, a % b);
        }


        #endregion

        #region weekly 336
        public int VowelStrings(string[] words, int left, int right)
        {
            int maxLen = Math.Min(words.Length, right + 1);
            int res = 0;
            List<char> vowels = new List<char> { 'a', 'e', 'i', 'o', 'u' };
            for (int i = left; i < maxLen; i++)
            {
                if (vowels.Contains(words[i][0]) && vowels.Contains(words[i][words[i].Length - 1])) res++;
            }


            return res;
        }

        public int MaxScore(int[] nums)
        {
            int max = 0;
            Array.Sort(nums, (a, b) => { return a - b; });

            int i = 0;

            while (i < nums.Length && nums[i] > 0)
            {
                max += nums[i];
            }
            return max;
        }
        #endregion

        #region weekly 337
        public int[] EvenOddBit(int n)
        {
            string output = Convert.ToString(n, 2);
            char[] chs = output.Reverse().ToArray();
            int[] res = new int[2];
            int i = 0;
            while (i < chs.Length)
            {
                if (output[i] == '1')
                {
                    int k = i % 2;
                    res[k]++;
                }
                i++;
            }
            return res;
        }
        #endregion

        #region weekly 338
        public int KItemsWithMaximumSum(int numOnes, int numZeros, int numNegOnes, int k)
        {
            int result = 0;
            if (k <= numOnes) return k;

            result += numOnes;
            k -= numOnes;

            if (k <= numZeros) return result;

            k -= numZeros;


            return result - k;
        }

        public bool PrimeSubOperation(int[] nums)
        {
            for (int i = 0; i < nums.Length; i++)
            {

            }

            return true;
        }

        private bool IsPrime(int number)
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

        public IList<long> MinOperations(int[] nums, int[] queries)
        {
            IList<long> result = new List<long>();

            for (int i = 0; i < queries.Length; i++)
            {
                long count = 0;
                int k = queries[i];
                for (int j = 0; j < nums.Length; j++)
                {
                    count += Math.Abs(k - nums[j]);


                }
                result.Add(count);
            }

            return result;
        }

        public int CollectTheCoins(int[] coins, int[][] edges)
        {
            return 0;
        }
        #endregion

        #region Problem 2407. Longest Increasing Subsequence II
        int[] st;
        private void build(int val, int left, int right)
        {
            if (val < 0 || val > 100000) return;
            if (left == right)
            {
                st[val] = 0;
            }
            else
            {
                int mid = (left + right) / 2;
                build(2 * val + 1, left, mid);
                build(2 * val + 2, mid, right);
                st[val] = Math.Max(st[2 * val + 1], st[2 * val + 2]);
            }
        }

        private int query(int val, int left, int right, int qs, int qe)
        {
            if (qs > qe) return 0;

            if (left == qs && right == qe) return st[val];

            int mid = (left + right) / 2;

            return Math.Max(
                query(2 * val + 1, left, mid, qs, Math.Min(qe, mid)),
                query(2 * val + 2, mid + 1, right, Math.Max(qs, mid + 1), qe));
        }

        private void update(int val, int left, int right, int index, int value)
        {
            if (left == right) st[val] = value;
            else
            {
                int mid = (left + right) / 2;

                if (index <= mid)
                {
                    update(2 * val + 1, left, mid, index, value);
                }
                else
                {
                    update(2 * val + 2, mid + 1, right, index, value);
                }
                st[val] = Math.Max(st[2 * val + 1], st[2 * val + 2]);
            }
        }

        public int LengthOfLIS(int[] nums, int k)
        {
            st = new int[400004];
            build(0, 0, 100000);
            int result = 0;

            for (int i = nums.Length - 1; i >= 0; i--)
            {
                int left = nums[i] + 1;
                int right = nums[i] + k;

                right = Math.Min(right, 100000);

                int getMax = query(0, 0, 100000, left, right);

                result = Math.Max(result, getMax + 1);

                update(0, 0, 100000, left - 1, getMax + 1);
            }
            return result;
        }
        #endregion
    }

}
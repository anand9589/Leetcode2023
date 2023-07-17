using Common;

namespace Jul
{
    public class Solution
    {

        int[][] dp2d;
        #region Day 1 Problem 2305. Fair Distribution of Cookies
        public int DistributeCookies(int[] cookies, int k)
        {
            int[] distribution = new int[k];

            return dfs_2305(0, distribution, cookies, k, k);
        }

        private int dfs_2305(int i, int[] distribution, int[] cookies, int k, int zero)
        {
            if (cookies.Length - i < zero)
            {
                return int.MaxValue;
            }

            if (i == cookies.Length)
            {
                return distribution.Max();
            }

            int result = int.MaxValue;

            for (int j = 0; j < k; j++)
            {
                zero -= distribution[j] == 0 ? 1 : 0;
                distribution[j] += cookies[i];

                result = Math.Min(result, dfs_2305(i + 1, distribution, cookies, k, zero));

                distribution[j] -= cookies[i];

                zero += distribution[j] == 0 ? 1 : 0;
            }

            return result;

        }
        #endregion

        #region Day 2 Problem 1601. Maximum Number of Achievable Transfer Requests
        int result_1601 = 0;
        public int MaximumRequests(int n, int[][] requests)
        {
            int[] buildings = new int[n];
            helper_1601(0, requests, 0, buildings);
            return result_1601;
        }

        private void helper_1601(int i, int[][] requests, int j, int[] buildings)
        {
            if (i == requests.Length)
            {
                foreach (int n in buildings)
                {
                    if (n != 0) return;
                }

                result_1601 = Math.Max(result_1601, j);
                return;
            }

            helper_1601(i + 1, requests, j, buildings);
            buildings[requests[i][0]]--;
            buildings[requests[i][1]]++;
            helper_1601(i + 1, requests, j + 1, buildings);
            buildings[requests[i][0]]++;
            buildings[requests[i][1]]--;

        }
        #endregion

        #region Day 5 Problem 1493. Longest Subarray of 1's After Deleting One Element
        public int LongestSubarray(int[] nums)
        {
            int result = 0;
            //Dictionary<int, int> map = new Dictionary<int, int>();
            List<(int start, int end)> lst = new List<(int start, int end)>();
            int i = 0;

            while (i < nums.Length)
            {
                if (nums[i] == 1)
                {
                    int j = i;
                    //map.Add(i, j++);

                    while (j < nums.Length && nums[j] == 1)
                    {
                        j++;
                    }

                    lst.Add((i, j - 1));

                    i = j;
                }
                else
                {
                    i++;
                }
            }

            if (lst.Count == 0) return 0;

            if (lst.Count == 1)
            {
                result = lst[0].end - lst[0].start + 1;

                if (result == nums.Length) return result - 1;

                return result;
            }

            for (int k = 1; k < lst.Count; k++)
            {
                if (lst[k].start - 2 == lst[k - 1].end)
                {
                    result = Math.Max(result, lst[k].end - lst[k - 1].start);
                }
                else
                {
                    result = Math.Max(result, lst[k].end - lst[k].start + 1);
                }
            }

            return result;
        }
        #endregion

        #region Day 6 Problem 209. Minimum Size Subarray Sum

        public int MinSubArrayLen(int target, int[] nums)
        {
            int result = int.MaxValue;
            if (nums[0] >= target) return 1;
            int left = 0;
            int sum = nums[0];
            for (int right = left + 1; right < nums.Length; right++)
            {
                if (nums[right] >= target) return 1;

                sum += nums[right];

                if (sum >= target)
                {
                    while (sum >= target)
                    {
                        result = Math.Min(result, right - left + 1);

                        sum -= nums[left++];
                    }
                }
            }

            return result == int.MaxValue ? 0 : result;
        }

        public int MinSubArrayLen_V1(int target, int[] nums)
        {
            int result = int.MaxValue;

            int i = 0;

            while (nums[i] > target)
            {
                i++;
            }

            int sum = nums[i];

            if (nums[i] == target) return 1;

            for (int j = i + 1; j < nums.Length; j++)
            {
                while (nums[j] > target)
                {
                    j++;
                    i = j;
                }

                if (nums[j] == target)
                {
                    return 1;
                }
                else
                {
                    sum += nums[j];
                    if (sum == target)
                    {
                        result = Math.Min(result, j - i + 1);
                    }
                    else if (sum > target)
                    {
                        sum -= nums[i];
                        sum -= nums[j];
                        i++;
                        j--;
                    }
                }
            }


            return result;
        }
        #endregion

        #region Day 7 Problem 2024. Maximize the Confusion of an Exam
        public int MaxConsecutiveAnswers(string answerKey, int k)
        {

            int result = k;

            Dictionary<char, int> dic = new Dictionary<char, int>();
            dic.Add('T', 0);
            dic.Add('F', 0);

            int left = 0;

            while (left < answerKey.Length)
            {
                dic[answerKey[left++]]++;
            }

            left = 0;

            for (int right = k; right < answerKey.Length; right++)
            {
                dic[answerKey[right]]++;

                while (Math.Min(dic['T'], dic['F']) > k)
                {
                    dic[answerKey[left]]--;
                }
                result = Math.Max(result, right - left + 1);
            }

            return result;
        }
        #endregion

        #region Day 8 Problem 2551. Put Marbles in Bags
        public long PutMarbles(int[] weights, int k)
        {
            int n = weights.Length;
            int[] pairweights = new int[n - 1];

            for (int i = 0; i < n - 1; i++)
            {
                pairweights[i] = weights[i] + weights[i + 1];
            }

            Array.Sort(pairweights, 0, n - 1);

            long result = 0;

            for (int i = 0; i < k - 1; i++)
            {
                result += pairweights[n - 2 - i] - pairweights[i];
            }

            return result;
        }
        #endregion

        #region Day 9 Problem 2272. Substring With Largest Variance
        public int LargestVariance(string s)
        {
            int[] counter = new int[26];
            int k = 0;
            while (k < s.Length)
            {
                counter[s[k++] - 'a']++;
            }

            int globalMax = 0;

            for (int i = 0; i < 26; i++)
            {

            }
            return 0;
        }
        #endregion

        #region Day 11 Problem 863. All Nodes Distance K in Binary Tree
        public IList<int> DistanceK(TreeNode root, TreeNode target, int k)
        {
            IList<int> result = new List<int>();
            if (k == 0) return new List<int>() { target.val };

            Queue<(TreeNode node, int distance)> q = new Queue<(TreeNode node, int distance)>();
            if (root.val == target.val)
            {
                q.Enqueue((root, 0));
                while (q.Count > 0)
                {

                    (TreeNode node, int distance) = q.Dequeue();
                    if (node != null)
                    {
                        if (distance == k)
                        {
                            result.Add(node.val);
                        }
                        else
                        {
                            distance++;
                            q.Enqueue((node.left, distance));
                            q.Enqueue((node.right, distance));

                        }
                    }
                }
            }
            else
            {
                int targetNodeFromRoot = -1;
                bool leftFloag = false;

                if (root.left != null)
                {
                    q.Enqueue((root.left, 1));

                    while (q.Count > 0)
                    {
                        (TreeNode node, int distance) = q.Dequeue();

                        if (node.val == target.val)
                        {
                            targetNodeFromRoot = distance;
                        }
                        else
                        {
                        }
                    }
                }

                if (root.right != null)
                {

                }
            }
            return result;
        }
        #endregion

        #region Day 12 Problem 802. Find Eventual Safe States
        public IList<int> EventualSafeNodes(int[][] graph)
        {
            IList<int> result = new List<int>();

            Dictionary<int, int[]> map = new Dictionary<int, int[]>();
            Dictionary<int, bool> safeNodeMap = new Dictionary<int, bool>();
            List<int> terminalNodes = new List<int>();
            for (int i = 0; i < graph.Length; i++)
            {
                map.Add(i, graph[i]);
            }

            map = map.OrderBy(x => x.Value.Length).ToDictionary(x => x.Key, x => x.Value);
            foreach (var key in map.Keys)
            {
                if (terminalNodes.Contains(key)) continue;
                bool safeNode = true;

                foreach (var node in map[key])
                {
                }

                if (safeNode)
                {
                    result.Add(key);
                }
            }

            return result.OrderBy(x => x).ToList();
        }
        #endregion

        #region Day 14 Problem 1218. Longest Arithmetic Subsequence of Given Difference
        public int LongestSubsequence(int[] arr, int difference)
        {
            int result = 1;
            Dictionary<int, int> keyValuePairs = new Dictionary<int, int>();

            foreach (int x in arr)
            {
                keyValuePairs.TryGetValue(x - difference, out int p);

                if (!keyValuePairs.ContainsKey(x))
                {
                    keyValuePairs.Add(x, 0);
                }

                keyValuePairs[x] = p + 1;

                if (result < p + 1)
                {
                    result = p + 1;
                }
            }
            return result;
        }
        public int LongestSubsequence_V1(int[] arr, int difference)
        {
            int n = arr.Length; Dictionary<int, int> keyValuePairs = null;

            if (difference == 0)
            {

                keyValuePairs = arr.GroupBy(x => x).ToDictionary(x => x.Key, y => y.Count());
            }
            else
            {
                keyValuePairs = new Dictionary<int, int>();
                for (int i = 0; i < n; i++)
                {
                    int k = arr[i] - difference;

                    if (!keyValuePairs.ContainsKey(arr[i]))
                    {
                        keyValuePairs.Add(arr[i], 1);
                    }

                    if (keyValuePairs.ContainsKey(k))
                    {
                        keyValuePairs[arr[i]] = keyValuePairs[k] + 1;
                    }
                }
            }


            return keyValuePairs.Values.Max();

        }
        #endregion

        #region Day 15 Problem 1751. Maximum Number of Events That Can Be Attended II
        public int MaxValue(int[][] events, int k)
        {
            Array.Sort(events, (a, b) => (a[0] - b[0]));
            dp2d = new int[k + 1][];

            for (int i = 0; i < dp2d.Length; i++)
            {
                dp2d[i] = Enumerable.Repeat(-1, dp2d[0].Length).ToArray();
            }

            return dfs(0, k, events);
        }

        private int dfs(int curIndex, int count, int[][] events)
        {
            if (count == 0 || curIndex == events.Length) return 0;

            if (dp2d[count][curIndex] != -1) return dp2d[count][curIndex];

            int nextIndex = bisectRight(events, events[curIndex][1]);
            dp2d[count][curIndex] = Math.Max(dfs(curIndex + 1, count, events), events[curIndex][2] + dfs(nextIndex, count - 1, events));
            return dp2d[count][curIndex];
        }

        public int bisectRight(int[][] events, int target)
        {
            int left = 0, right = events.Length;
            while (left < right)
            {
                int mid = (left + right) / 2;
                if (events[mid][0] <= target)
                {
                    left = mid + 1;
                }
                else
                {
                    right = mid;
                }
            }
            return left;
        }
        #endregion

        #region Day 16 Problem 1125. Smallest Sufficient Team
        private int n;
        private int[] skillsMaskOfPerson;
        private long[] dp;
        private long F(int skillsMask)
        {
            if (skillsMask == 0)
            {
                return 0L;
            }
            if (dp[skillsMask] != -1)
            {
                return dp[skillsMask];
            }
            for (int i = 0; i < n; i++)
            {
                int smallerSkillsMask = skillsMask & ~skillsMaskOfPerson[i];
                if (smallerSkillsMask != skillsMask)
                {
                    long peopleMask = F(smallerSkillsMask) | (1L << i);
                    if (dp[skillsMask] == -1 || CountBits(peopleMask) < CountBits(dp[skillsMask]))
                    {
                        dp[skillsMask] = peopleMask;
                    }
                }
            }
            return dp[skillsMask];
        }

        private int CountBits(long mask)
        {
            int count = 0;
            while (mask != 0)
            {
                count += (int)(mask & 1);
                mask >>= 1;
            }
            return count;
        }

        public int[] SmallestSufficientTeam(string[] req_skills, IList<IList<string>> people)
        {
            n = people.Count;
            int m = req_skills.Length;
            Dictionary<string, int> skillId = new Dictionary<string, int>();
            for (int i = 0; i < m; i++)
            {
                skillId[req_skills[i]] = i;
            }
            skillsMaskOfPerson = new int[n];
            for (int i = 0; i < n; i++)
            {
                foreach (string skill in people[i])
                {
                    skillsMaskOfPerson[i] |= 1 << skillId[skill];
                }
            }
            dp = new long[1 << m];
            for (int i = 0; i < dp.Length; i++)
            {
                dp[i] = -1;
            }
            long answerMask = F((1 << m) - 1);
            int[] ans = new int[CountBits(answerMask)];
            int ptr = 0;
            for (int i = 0; i < n; i++)
            {
                if (((answerMask >> i) & 1) == 1)
                {
                    ans[ptr++] = i;
                }
            }
            return ans;
        }
        #endregion

        #region Day 17 Problem 445. Add Two Numbers II
        public ListNode AddTwoNumbers(ListNode l1, ListNode l2)
        {
            Stack<int> s1 = new Stack<int>();
            Stack<int> s2 = new Stack<int>();
            ListNode resultNode = new ListNode();
            ListNode nextNode = null;
            fillStack(l1, s1);
            fillStack(l2, s2);

            int carryOn = 0;
            while (s1.Count > 0 || s2.Count > 0 || carryOn ==1)
            {
                s1.TryPop(out int num1);
                s2.TryPop(out int num2);

                int num = num1 + num2 + carryOn;
                carryOn = 0;
                if (num > 9)
                {
                    carryOn = 1;
                    num -= 10;
                }
                resultNode = new ListNode(num, nextNode);
                nextNode = resultNode;
            }

            return resultNode;
        }

        private void fillStack(ListNode l1, Stack<int> s1)
        {
            ListNode temp = new ListNode();

            temp.next = l1;

            while (temp.next != null)
            {
                s1.Push(temp.next.val);
                temp = temp.next;
            }
        }

        #endregion

        #region weekly-contest-352
        //Problem 1 6909. Longest Even Odd Subarray With Threshold

        public int LongestAlternatingSubarray(int[] nums, int threshold)
        {
            int l = 0;
            int r = nums.Length - 1;
            int ans = 0;
            while (l < nums.Length)
            {
                int res = 0;
                int k = l;
                bool rsupdate = false;
                while (k < nums.Length && nums[k] <= threshold && nums[k] % 2 == res % 2)
                {
                    rsupdate = true;
                    k++;
                    res++;
                }
                if (rsupdate)
                {

                    ans = Math.Max(ans, res);
                    l = k;
                }
                else
                {
                    l++;
                }
            }

            return ans;
        }


        //Problem 2 6916. Prime Pairs With Target Sum
        public IList<IList<int>> FindPrimePairs(int n)
        {

            IList<IList<int>> res = new List<IList<int>>();

            int l = 2;
            while (l <= n / 2)
            {

                int r = n - l;
                if (isPrime(r))
                {
                    res.Add(new List<int> { l, r });

                }

                if (l == 2)
                {
                    l++;
                }
                else
                {
                    l += 2;

                }

                while (!isPrime(l))
                {

                    l += 2;
                }
            }

            return res;
        }


        private bool isPrime(int n)
        {
            for (int i = 2; i <= Math.Sqrt(n); i++)
            {
                if (n % i == 0) return false;
            }

            return true;
        }

        public IList<IList<int>> FindPrimePairs_V1(int n)
        {
            List<int> primeList = getPrimeNumber(n).ToList();
            primeList.Insert(0, 2);

            IList<IList<int>> res = new List<IList<int>>();

            int l = 0;
            int r = primeList.Count - 1;

            while (l <= r)
            {
                int n1 = primeList[l];
                int n2 = primeList[r];

                if (n1 + n2 == n)
                {
                    res.Add(new List<int> { n1, n2 });
                    l++;
                    r--;
                }
                else if (n1 + n2 > n)
                {
                    r--;
                }
                else
                {
                    l++;
                }
            }

            return res;
        }

        private IEnumerable<int> getPrimeNumber(int n)
        {
            for (int i = 3; i <= n; i += 2)
            {
                if (isPrime(i))
                {
                    yield return i;
                }
            }
        }


        //Problem 3 6911. Continuous Subarrays

        //Problem 4 6894. Sum of Imbalance Numbers of All Subarrays
        #endregion

        #region Problem 2762. Continuous Subarrays
        public long ContinuousSubarrays(int[] nums)
        {
            long result = nums.Length;

            int k = 0;

            while (k < nums.Length)
            {
                int l = k;
                int max = nums[l];
                int min = nums[l];
                while (l < nums.Length)
                {

                }
            }

            return result;
        }
        #endregion

    }
}
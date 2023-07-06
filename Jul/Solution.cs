namespace Jul
{
    public class Solution
    {
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
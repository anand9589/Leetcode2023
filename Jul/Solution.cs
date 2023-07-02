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
            List<int> primeList = getPrimeNumber(n).ToList();
            primeList.Insert(0, 2);

            IList<IList<int>> res = new List<IList<int>>();

            int l = 0;
            int r = primeList.Count-1;

            while (l<=r)
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

        private bool isPrime(int n)
        {
            for (int i = 2; i <= Math.Sqrt(n); i++)
            {
                if (n % i == 0) return false;
            }

            return true;
        }

        //Problem 3

        //Problem 4
        #endregion
    }
}
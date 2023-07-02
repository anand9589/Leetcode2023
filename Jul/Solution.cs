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
        int result = 0;
        public int MaximumRequests(int n, int[][] requests)
        {
            int[] buildings = new int[n];
            helper(0, requests, 0, buildings);
            return result;
        }

        private void helper(int i, int[][] requests, int j, int[] buildings)
        {
            if (i == requests.Length)
            {
                foreach (int n in buildings)
                {
                    if (n != 0) return;
                }

                result = Math.Max(result, j);
                return;
            }

            helper(i + 1, requests, j, buildings);
            buildings[requests[i][0]]--;
            buildings[requests[i][1]]++;
            helper(i + 1, requests, j + 1, buildings);
            buildings[requests[i][0]]++;
            buildings[requests[i][1]]--;

        }
        #endregion
    }
}
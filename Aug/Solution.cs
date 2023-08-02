namespace Aug
{
    public class Solution
    {
        #region Day 1 77. Combinations
        public IList<IList<int>> Combine(int n, int k)
        {
            if (n == k)
            {
                IList<int> lst = Enumerable.Range(1, n).ToList();
                return new List<IList<int>>() { lst };
            }
            IList<IList<int>> result = new List<IList<int>>();

            for (int i = 1; i <= n - k + 1; i++)
            {
                IList<int> list = new List<int>();
                list.Add(i);
                combineList(result, list, i + 1, n, k - 1);

            }

            return result;
        }

        private void combineList(IList<IList<int>> result, IList<int> lst, int i, int limit, int k)
        {
            if (k < 0) return;
            if (k == 0)
            {
                result.Add(new List<int>(lst));
                return;
            }

            while (i <= limit)
            {
                lst.Add(i);
                k--;
                combineList(result, lst, i + 1, limit, k);
                lst.Remove(i);
                i++;
                k++;
            }
        }
        #endregion

        #region Day 2 46. Permutations
        public IList<IList<int>> Permute(int[] nums)
        {
            IList<IList<int>> result = new List<IList<int>>();

            permute_helper(result, nums, 0);

            return result;
        }

        private void permute_helper(IList<IList<int>> result, int[] nums, int index)
        {
            if (index == nums.Length)
            {
                result.Add(new List<int>(nums));
                return;
            }

            for (int i = index+1; i < nums.Length; i++)
            {
                swap(nums, index, i);
                permute_helper(result, nums, i + 1);
                swap(nums, index, i);
            }
        }

        private void swap(int[] nums, int i, int j)
        {
            if (i < 0 || j < 0 || i >= nums.Length || j >= nums.Length) return;

            int temp = nums[i];
            nums[i] = nums[j];
            nums[j] = temp;
        }


        #endregion

    }
}
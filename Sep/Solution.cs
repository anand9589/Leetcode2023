namespace Sep
{
    public class Solution
    {
        #region Day 1  Problem 338. Counting Bits
        public int[] CountBits(int n)
        {
            int[] result = new int[n + 1];
            result[0] = 0;
            for (int i = 1; i <= n; i++)
            {
                int y = i / 2;


                result[i] = result[y];

                if (i % 2 == 1)
                {
                    result[i]++;
                }
            }

            return result;
        }
        #endregion
    }
}
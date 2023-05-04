using System.Collections;

namespace May
{
    public class Solution
    {
        #region Day 1 Problem  1491. Average Salary Excluding the Minimum and Maximum Salary

        public double Average(int[] salary)
        {
            Array.Sort(salary);

            int n = salary.Length;
            int sum = 0;
            for (int i = 1; i < n - 1; i++)
            {
                sum += salary[i];
            }

            return (double)sum / n - 2;
        }

        public double Average_V1(int[] salary)
        {
            int maxSalary = int.MinValue;
            int minSalary = int.MaxValue;

            int n = salary.Length;
            int salarySum = 0;
            foreach (int i in salary)
            {
                salarySum += i;

                minSalary = Math.Min(minSalary, i);
                maxSalary = Math.Max(maxSalary, i);
            }

            return (double)(salarySum - maxSalary - minSalary) / (n - 2);
            //return (double)(salary.Sum() - salary.Max() - salary.Min()) / (salary.Length - 2);
        }
        #endregion

        #region Day 2 Problem  1822. Sign of the Product of an Array
        public int ArraySign(int[] nums)
        {
            Array.Sort(nums);

            int low = 0;

            int high = nums.Length - 1;

            while (low <= high)
            {
                int mid = (low + high) / 2;

                if (nums[mid] == 0) return 0;

                if (nums[mid] > 0)
                {
                    high = mid - 1;
                }
                else
                {
                    low = mid + 1;
                }
            }

            return low % 2 == 0 ? 1 : -1;
        }
        #endregion

        #region Day 3 Problem  2215. Find the Difference of Two Arrays
        public IList<IList<int>> FindDifference(int[] nums1, int[] nums2)
        {
            var p = nums1.Except(nums1.Intersect(nums2));

            var q = nums2.Except(nums2.Intersect(nums1));
            return new List<IList<int>>() { p.ToList(), q.ToList() };
        }
        #endregion

        #region Day 4 Problem 649. Dota2 Senate
        public string PredictPartyVictory(string senate)
        {
            int n = senate.Length;
            int[] counts = new int[2];  // counts[0] is the count of Radiant senators, counts[1] is the count of Dire senators
            int[] bans = new int[2];    // bans[0] is the number of times Radiant has banned Dire, bans[1] is the number of times Dire has banned Radiant
            char[] cans = senate.ToCharArray();
            foreach (char c in senate)
            {
                if (c == 'R')
                {
                    counts[0]++;
                }
                else
                {
                    counts[1]++;
                }
            }

            while (counts[0] > 0 && counts[1] > 0)
            {
                for (int i = 0; i < n; i++)
                {
                    if (cans[i] == ' ') continue;  // this senator has lost their right
                    if (cans[i] == 'R')
                    {
                        if (bans[0] > 0)
                        {
                            bans[0]--;
                            cans[i] = ' ';
                            counts[0]--;
                        }
                        else
                        {
                            bans[1]++;
                        }
                    }
                    else
                    {
                        if (bans[1] > 0)
                        {
                            bans[1]--;
                            cans[i] = ' ';
                            counts[1]--;
                        }
                        else
                        {
                            bans[0]++;
                        }
                    }
                }
            }

            return counts[0] > 0 ? "Radiant" : "Dire";
        }
        public string PredictPartyVictory_V1(string senate)
        {
            char c = senate[0];
            int n = senate.Length;

            int rCount = senate.Where(x => x == 'R').Count();

            int dCount = n - rCount;

            if (rCount != dCount)
            {
                c = rCount > dCount ? 'R' : 'D';
            }

            return c == 'R' ? "Radiant" : "Dire";
        }
        #endregion

        #region Day 5 Problem
        #endregion
        #region Day 6 Problem
        #endregion
        #region Day 7 Problem
        #endregion
        #region Day 8 Problem
        #endregion
        #region Day 9 Problem
        #endregion
        #region Day 10 Problem
        #endregion
        #region Day 11 Problem
        #endregion
        #region Day 12 Problem
        #endregion
        #region Day 13 Problem
        #endregion
        #region Day 14 Problem
        #endregion
        #region Day 15 Problem
        #endregion
        #region Day 16 Problem
        #endregion
        #region Day 17 Problem
        #endregion
        #region Day 18 Problem
        #endregion
        #region Day 19 Problem
        #endregion
        #region Day 20 Problem
        #endregion
        #region Day 21 Problem
        #endregion
        #region Day 22 Problem
        #endregion
        #region Day 23 Problem
        #endregion
        #region Day 24 Problem
        #endregion
        #region Day 25 Problem
        #endregion
        #region Day 26 Problem
        #endregion
        #region Day 27 Problem
        #endregion
        #region Day 28 Problem
        #endregion
        #region Day 29 Problem
        #endregion
        #region Day 30 Problem
        #endregion
        #region Day 31 Problem
        #endregion
    }
}
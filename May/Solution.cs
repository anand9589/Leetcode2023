using System.Collections;
using System.Collections.ObjectModel;

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

        #region Day 5 Problem 1456. Maximum Number of Vowels in a Substring of Given Length
        public int MaxVowels(string s, int k)
        {
            int vowel_count = 0;
            for (int i = 0; i < k; i++)
            {
                switch (s[i])
                {
                    case 'a':
                    case 'e':
                    case 'i':
                    case 'o':
                    case 'u':
                        vowel_count++;
                        break;
                    default: break;
                }
            }

            int result = vowel_count;
            int startIndex = 1;
            while (startIndex + k - 1 < s.Length)
            {
                switch (s[startIndex - 1])
                {
                    case 'a':
                    case 'e':
                    case 'i':
                    case 'o':
                    case 'u':
                        vowel_count--;
                        break;
                    default: break;
                }

                switch (s[startIndex + k - 1])
                {
                    case 'a':
                    case 'e':
                    case 'i':
                    case 'o':
                    case 'u':
                        vowel_count++;
                        result = Math.Max(result, vowel_count);
                        break;
                    default: break;
                }

                startIndex++;


            }

            return result;
        }
        #endregion

        #region Day 6 Problem 1498. Number of Subsequences That Satisfy the Given Sum Condition
        public int NumSubseq(int[] nums, int target)
        {
            int mod = 1000000007;
            int n = nums.Length;
            Array.Sort(nums);
            int i = 0, j = n - 1;
            int res = 0;
            int[] pows = new int[n];
            pows[0] = 1;
            for (int k = 1; k < n; k++)
            {
                pows[k] = (pows[k - 1] * 2) % mod;
            }
            while (i <= j)
            {
                if (nums[i] + nums[j] <= target)
                {
                    res = (res + pows[j - i]) % mod;
                    i++;
                }
                else
                {
                    j--;
                }
            }
            return res;
        }
        #endregion

        #region Day 7 Problem 1964. Find the Longest Valid Obstacle Course at Each Position
        public int[] LongestObstacleCourseAtEachPosition(int[] obstacles)
        {
            int n = obstacles.Length;
            int[] ans = new int[n];
            List<int> courses = new List<int>();
            for (int i = 0; i < n; i++)
            {
                int h = obstacles[i];
                int lo = 0, hi = courses.Count - 1;
                while (lo <= hi)
                {
                    int mid = (lo + hi) / 2;
                    if (courses[mid] <= h)
                    {
                        lo = mid + 1;
                    }
                    else
                    {
                        hi = mid - 1;
                    }
                }
                int len = lo + 1;
                if (lo < courses.Count)
                {
                    courses[lo] = h;
                }
                else
                {
                    courses.Add(h);
                }
                ans[i] = len;
            }
            return ans;
        }
        #endregion

        #region Day 8 Problem 1572. Matrix Diagonal Sum
        public int DiagonalSum(int[][] mat)
        {
            HashSet<(int, int)> values = new HashSet<(int, int)>();

            for (int i = 0; i < mat.Length; i++)
            {
                values.Add((i, i));
            }

            int j = 0;
            for (int i = mat.Length - 1; i >= 0; i--, j++)
            {
                values.Add((j, i));
            }
            int sum = 0;
            foreach (var item in values)
            {
                sum += mat[item.Item1][item.Item2];
            }
            return sum;
        }
        #endregion

        #region Day 9 Problem 54. Spiral Matrix

        public IList<int> SpiralOrder(int[][] matrix)
        {
            IList<int> lst = new List<int>();

            int m = matrix.Length;
            int n = matrix[0].Length;
            int totalCount = m * n;
            int round = 0;
            while (lst.Count < totalCount)
            {
                int x = round;
                int y = round;
                int y1 = n - round;
                int x1 = m - round;

                while (y < y1)
                {
                    lst.Add(matrix[x][y]);
                    y++;
                }

                if (lst.Count == totalCount) break;

                y--;
                x++;

                while (x < x1)
                {
                    lst.Add(matrix[x][y]);
                    x++;
                }

                if (lst.Count == totalCount) break;

                x--;
                y--;


                while (y >= round)
                {
                    lst.Add(matrix[x][y]);
                    y--;
                }

                if (lst.Count == totalCount) break;
                y++;
                x--;


                while (x > round)
                {
                    lst.Add(matrix[x][y]);
                    x--;
                }

                round++;
            }

            return lst;
        }

        public IList<int> SpiralOrder_V1(int[][] matrix)
        {
            IList<int> lst = new List<int>();
            bool[][] visited = new bool[matrix.Length][];
            for (int i = 0; i < matrix.Length; i++)
            {
                visited[i] = new bool[matrix[i].Length];
            }
            int m = matrix.Length;
            int n = matrix[0].Length;
            int x = 0;
            int y = 0;
            int round = 0;
            while (lst.Count < m * n)
            {
                x = round;
                y = round;
                while (y < n && !visited[x][y])
                {
                    lst.Add(matrix[x][y]);
                    visited[x][y] = true;
                    y++;
                }
                y = n - 1;
                x = x + 1;

                while (x < m && !visited[x][y])
                {
                    lst.Add(matrix[x][y]);
                    visited[x][y] = true;
                    x++;
                }
                x = m - 1;
                y = y - 1;

                while (y >= round && !visited[x][y])
                {
                    lst.Add(matrix[x][y]);
                    visited[x][y] = true;
                    y--;
                }
                y = round;
                x = x - 1;

                while (x > round && !visited[x][y])
                {
                    lst.Add(matrix[x][y]);
                    visited[x][y] = true;
                    x--;
                }

                round++;
            }

            return lst;
        }
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

        #region  Problem 2670. Find the Distinct Difference Array
        public int[] DistinctDifferenceArray(int[] nums)
        {
            int n = nums.Length;
            int[] diff = new int[n];

            Dictionary<int, List<int>> map = new Dictionary<int, List<int>>();
            for (int i = 0; i < n; i++)
            {
                if (!map.ContainsKey(nums[i]))
                {
                    map.Add(nums[i], new List<int>());
                }
                map[nums[i]].Add(i);
            }
            HashSet<int> prefix = new HashSet<int>();
            for (int i = 0; i < n; i++)
            {
                int k = nums[i];
                prefix.Add(k);

                map[k].Remove(i);

                if (map[k].Count == 0) map.Remove(k);

                diff[i] = prefix.Count - map.Count;
            }
            return diff;
        }

        #endregion

        #region Problem 2671. Frequency Tracker
        public class FrequencyTracker
        {
            readonly Dictionary<int, int> numbermap;
            readonly Dictionary<int, List<int>> frequencyMap;
            public FrequencyTracker()
            {
                numbermap = new Dictionary<int, int>();
                frequencyMap = new Dictionary<int, List<int>>();
            }

            public void Add(int number)
            {
                int freq = 1;
                if (!numbermap.ContainsKey(number))
                {
                    numbermap.Add(number, 0);
                }
                else
                {
                    freq = numbermap[number];

                    frequencyMap[freq].Remove(number);
                    if (frequencyMap[freq].Count == 0)
                    {
                        frequencyMap.Remove(freq);
                    }
                }
                numbermap[number]++;

                freq = numbermap[number];
                updatefrequencymap(freq, number);
            }

            public void DeleteOne(int number)
            {
                if (numbermap.ContainsKey(number))
                {
                    int oldfreq = numbermap[number];
                    frequencyMap[oldfreq].Remove(number);

                    if (frequencyMap[oldfreq].Count == 0) frequencyMap.Remove(oldfreq);

                    if (oldfreq == 1)
                    {
                        numbermap.Remove(number);
                    }
                    else
                    {
                        numbermap[number]--;

                        updatefrequencymap(oldfreq - 1, number);
                    }


                }
            }

            public bool HasFrequency(int frequency)
            {
                return frequencyMap.ContainsKey(frequency);
            }

            private void updatefrequencymap(int freq, int number)
            {
                if (!frequencyMap.ContainsKey(freq))
                {
                    frequencyMap.Add(freq, new List<int>());
                }
                frequencyMap[freq].Add(number);
            }
        }
        #endregion

        #region Problem 2672. Number of Adjacent Elements With the Same Color
        public int[] ColorTheArray(int n, int[][] queries)
        {
            int[] nums = new int[n]; // initialize the array with zeros
            int[] ans = new int[queries.Length]; // initialize the answer array


            return ans;
        }
        #endregion
    }
}
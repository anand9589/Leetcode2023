namespace Jan
{
    public class Solution
    {
        #region Problem Day 1 290. Word Pattern
        public bool WordPattern(string pattern, string s)
        {
            char[] chars = pattern.ToCharArray();

            string[] words = s.Split(' ');

            if (words.Length == chars.Length && words.Distinct().Count() == chars.Distinct().Count())
            {
                Dictionary<char, string> map = new Dictionary<char, string>();


                for (int i = 0; i < words.Length; i++)
                {
                    char c = chars[i];
                    string word = words[i];

                    if (map.ContainsKey(c))
                    {
                        if (map[c] != word) return false;
                    }
                    else
                    {
                        map.Add(c, word);
                    }
                }
                return true;

            }

            return false;
        }
        #endregion

        #region Problem Day 2 520. Detect Capital
        public bool DetectCapitalUse(string word)
        {
            if (word.Length == 1) return true;

            if (!isCap(word[0]))
            {
                return word.Where(x => isCap(x)).Count() == 0;
            }
            else if (isCap(word[0]) && isCap(word[1]))
            {
                return word.Where(x => !isCap(x)).Count() == 0;
            }
            else
            {
                return word.Skip(1).Where(x => isCap(x)).Count() == 0;
            }
        }

        private bool isCap(char c)
        {
            return c >= 'A' && c <= 'Z';
        }
        #endregion

        #region Problem Day 3 944. Delete Columns to Make Sorted
        public int MinDeletionSize(string[] strs)
        {
            if (strs.Length == 1) return 0;
            int cnt = 0;
            for (int i = 0; i < strs[0].Length; i++)
            {
                int o = -1;
                for (int j = 0; j < strs.Length; j++)
                {
                    char p = strs[j][i];

                    if (p >= o)
                    {
                        o = p;
                    }
                    else
                    {
                        cnt++;
                        break;
                    }
                }
            }
            return cnt;
        }
        #endregion

        #region Problem Day 4 2244. Minimum Rounds to Complete All Tasks
        public int MinimumRounds(int[] tasks)
        {
            int days = 0;
            IDictionary<int, int> map = new Dictionary<int, int>();

            foreach (int task in tasks)
            {
                if (!map.ContainsKey(task))
                {
                    map.Add(task, 0);
                }
                map[task]++;
            }

            foreach (var key in map.Keys)
            {
                if (map[key] < 2) return -1;

                int p = map[key] % 3;
                days += map[key] / 3;

                if (p > 0) days++;
            }

            return days;
        }
        public int MinimumRounds_V1(int[] tasks)
        {
            int days = 0;
            IDictionary<int, int> map = new Dictionary<int, int>();

            foreach (int task in tasks)
            {
                if (!map.ContainsKey(task))
                {
                    map.Add(task, 0);
                }
                map[task]++;
            }


            foreach (var key in map.Keys)
            {
                if (map[key] < 2) return -1;
                while (map[key] > 4)
                {
                    days++;
                    map[key] -= 3;
                }

                if (map[key] != 0)
                {
                    if (map[key] == 4)
                    {
                        days += 2;
                    }
                    else
                    {
                        days++;
                    }
                    map[key] = 0;
                }
            }

            return days;
        }
        #endregion

        #region Problem Day 5 452. Minimum Number of Arrows to Burst Balloons
        public int FindMinArrowShots(int[][] points)
        {
            int count = 1;
            Array.Sort(points, (x, y) => { 
                if(x[1] < y[1]) return -1;
                if(x[1] > y[1]) return 1;
                return 0; });

            int p1 = points[0][1];

            for (int i = 1; i < points.Length; i++)
            {
                if(points[i][0] > p1 && points[i][1] > p1)
                {
                    p1 = points[i][1];
                    count++;
                }
            }
            return count;
        }


        #endregion

        #region Problem Day 6
        #endregion

        #region Problem Day 7
        #endregion

        #region Problem Day 8
        #endregion

        #region Problem Day 9
        #endregion

        #region Problem Day 10
        #endregion

        #region Problem Day 11
        #endregion

        #region Problem Day 12
        #endregion

        #region Problem Day 13
        #endregion

        #region Problem Day 14
        #endregion

        #region Problem Day 15
        #endregion

        #region Problem Day 16
        #endregion

        #region Problem Day 17
        #endregion

        #region Problem Day 18
        #endregion

        #region Problem Day 19
        #endregion

        #region Problem Day 20
        #endregion

        #region Problem Day 21
        #endregion

        #region Problem Day 22
        #endregion

        #region Problem Day 23
        #endregion

        #region Problem Day 24
        #endregion

        #region Problem Day 25
        #endregion

        #region Problem Day 26
        #endregion

        #region Problem Day 27
        #endregion

        #region Problem Day 28
        #endregion

        #region Problem Day 29
        #endregion

        #region Problem Day 30
        #endregion

        #region Problem Day 31
        #endregion

        #region Problem 2520. Count the Digits That Divide a Number
        public int CountDigits(int num)
        {
            int n = num;
            int cnt = 0;

            while (n > 0)
            {
                int k = n % 10;
                n /= 10;

                if (num % k == 0) cnt++;
            }
            return cnt;
        }
        #endregion
        #region Problem 2523. Closest Prime Numbers in Range
        public int[] ClosestPrimes(int left, int right)
        {
            int num1 = -1, num2 = -1, x = -1, y = -1;

            right = right % 2 == 0 ? right - 1 : right;
            left = left % 2 == 0 ? left + 1 : left;
            int diff = int.MaxValue;

            for (int i = right; i >= left; i = i - 2)
            {
                if (isPrime(i))
                {
                    if (x == -1)
                    {
                        x = i;
                    }
                    else if (y == -1)
                    {
                        y = i;

                        diff = x - y;
                        num1 = y;
                        num2 = x;
                    }
                    else
                    {
                        x = y;
                        y = i;
                        if (diff >= x - y)
                        {
                            diff = x - y;
                            num1 = y;
                            num2 = x;
                        }
                    }
                }
            }


            return new int[] { num1, num2 };
        }

        private bool isPrime(int num)
        {
            if (num <= 1) return false;
            if (num <= 3) return true;
            if (num % 2 == 0 || num % 3 == 0) return false;
            for (int i = 5; i * i <= num; i = i + 6)
            {
                if (num % i == 0 || num % (i + 2) == 0)
                    return false;
            }
            return true;
        }
        #endregion
    }
}
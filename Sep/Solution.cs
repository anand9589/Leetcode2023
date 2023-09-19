using Common;

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

        #region Day 6 Problem 725. Split Linked List in Parts
        public ListNode[] SplitListToParts(ListNode head, int k)
        {
            ListNode[] listNodes = new ListNode[k];
            List<ListNode> list = new List<ListNode>();

            while (head != null)
            {
                list.Add(new ListNode(head.val));
                head = head.next;
            }

            int minSize = list.Count / k;
            int rem = list.Count % k;
            int counter = 0;
            for (int i = 0; i < k; i++)
            {
                ListNode temp1 = new ListNode(-1);

                ListNode temp = temp1;
                int m = i < rem ? minSize + 1 : minSize;
                for (int j = 0; j < m; j++)
                {
                    temp.next = new ListNode(list[counter++].val);
                    temp = temp.next;
                }

                listNodes[i] = temp1.next;
            }

            return listNodes;

        }
        #endregion

        #region Day 9 Problem 377. Combination Sum IV
        public int CombinationSum4(int[] nums, int target)
        {
            int[] dp = Enumerable.Repeat(-1, target + 1).ToArray();
            return solve(dp, nums, target);
        }

        private int solve(int[] dp, int[] nums, int target)
        {
            if (target == 0) return 1;

            if (dp[target] != -1) return dp[target];

            dp[target] = 0;

            foreach (int n in nums)
            {
                if (n <= target)
                {
                    dp[target] += solve(dp, nums, target - n);
                }
            }
            return dp[target];
        }
        #endregion

        #region Day 10 Problem 1359. Count All Valid Pickup and Delivery Options
        public int CountOrders(int n)
        {
            const int MOD = 1000_000_007;

            long places = 2 * n;

            long seq = 1;

            for (int i = n; i >= 1; i--)
            {
                seq = (seq * ((places * (places - 1))) / 2) % MOD;
                places -= 2;
            }

            return (int)seq;
        }
        #endregion

        #region Day 18 Problem 1337. The K Weakest Rows in a Matrix
        public int[] KWeakestRows(int[][] mat, int k)
        {
            PriorityQueue<int, (int, int)> pq = new PriorityQueue<int, (int, int)>(Comparer<(int, int)>.Create((x, y) =>
            {
                if (x.Item2 == y.Item2)
                {
                    return x.Item1 - y.Item1;
                }
                return x.Item2 - y.Item2;
            }));

            for (int i = 0; i < mat.Length; i++)
            {
                pq.Enqueue(i, (i, mat[i].Sum()));
            }

            int[] res = new int[k];

            for (int i = 0; i < k; i++)
            {
                res[i] = pq.Dequeue();
            }

            return res;
        }
        #endregion


        #region Day 19 Problem 287. Find the Duplicate Number
        public int FindDuplicate(int[] nums)
        {
            int slow = nums[0];
            int fast = nums[0];

            do
            {
                slow = nums[slow];
                fast = nums[nums[fast]];
            } while (slow != fast);

            fast = nums[0];

            while (fast!=slow)
            {
                fast = nums[fast];
                slow = nums[slow];
            }

            return slow;
        }
        #endregion

        #region weekly-contest-361
        public int CountSymmetricIntegers(int low, int high)
        {
            if ((low >= 1 && high <= 9) || (low >= 100 && high <= 999) || low > high) return 0;


            int mid = (low + high) / 2;
            int count = isAsymmtric(mid) ? 1 : 0;


            return count + CountSymmetricIntegers(low, mid - 1) + CountSymmetricIntegers(mid + 1, high);
        }

        public bool isAsymmtric(int num)
        {
            if (num < 10 || (num >= 100 && num <= 999) || num == 10000) return false;

            IList<int> list = new List<int>();

            while (num > 0)
            {
                int rem = num % 10;
                list.Add(rem);
                num /= 10;
            }

            int sum1 = 0;
            int sum2 = 0;
            int len = list.Count;
            while (list.Count > 0)
            {
                sum1 += list[0];
                sum2 += list[len - 1];
                list.RemoveAt(len - 1);
                list.RemoveAt(0);
                len = list.Count;
            }

            return sum1 == sum2;
        }
        //public int MinimumOperations(string num)
        //{

        //}
        #endregion

        #region weekly-contest-362
        public int NumberOfPoints(IList<IList<int>> nums)
        {
            if (nums.Count == 1) return nums[0][1] - nums[0][0] + 1;

            nums = nums.OrderByDescending(x => x[1]).OrderBy(x => x[0]).ToList();
            int count = 0;
            int start = nums[0][0];
            int end = nums[0][1];

            for (int i = 1; i < nums.Count; i++)
            {

                if (nums[i][0] <= end && nums[i][1] <= end) continue;

                if (nums[i][0] <= end && nums[i][1] > end)
                {

                    end = nums[i][1];
                    continue;
                }

                if (end < nums[i][0])
                {
                    count += (end - start) + 1;
                    start = nums[i][0];
                    end = nums[i][1];
                }
            }


            return count + end - start + 1;

        }

        public bool IsReachableAtTime(int sx, int sy, int fx, int fy, int t)
        {

            if (sx == fx && sy == fy)
            {
                return t % 2 == 0;
            }
            int m = Math.Max(sx, fx);
            int n = Math.Max(sy, fy);

            bool[][] visited = new bool[m + 1][];
            for (int i = 0; i <= m; i++)
            {
                visited[i] = new bool[n + 1];
            }

            Queue<(int x, int y, int t)> timeq = new Queue<(int x, int y, int t)>();

            timeq.Enqueue((sx, sy, 0));
            visited[sx][sy] = true;

            while (timeq.Count > 0)
            {
                var q = timeq.Dequeue();
                if (q.x == fx && q.y == fy)
                {
                    return t >= q.t;
                }
                if (q.t == t) continue;

                visit(timeq, q.x + 1, q.y, q.t + 1, visited, sx, fx, sy, fy);
                visit(timeq, q.x - 1, q.y, q.t + 1, visited, sx, fx, sy, fy);
                visit(timeq, q.x, q.y + 1, q.t + 1, visited, sx, fx, sy, fy);
                visit(timeq, q.x, q.y - 1, q.t + 1, visited, sx, fx, sy, fy);
                visit(timeq, q.x + 1, q.y + 1, q.t + 1, visited, sx, fx, sy, fy);
                visit(timeq, q.x + 1, q.y - 1, q.t + 1, visited, sx, fx, sy, fy);
                visit(timeq, q.x - 1, q.y + 1, q.t + 1, visited, sx, fx, sy, fy);
                visit(timeq, q.x - 1, q.y - 1, q.t + 1, visited, sx, fx, sy, fy);
            }

            return false;
        }

        private void visit(Queue<(int x, int y, int t)> timeq, int x, int y, int t, bool[][] visited, int sx, int fx, int sy, int fy)
        {
            if ((x < sx && x < fx) || (x > sx && x > fx) || (y < sy && y < fy) || (y > sy && y > fy) || visited[x][y]) return;
            visited[x][y] = true;
            timeq.Enqueue((x, y, t));
        }
        #endregion

        #region weekly-contest-363
        public int SumIndicesWithKSetBits(IList<int> nums, int k)
        {
            int result = 0;

            for (int i = 0; i < nums.Count; i++)
            {
                int count = CountSetBits(i);
                if (count == k)
                {
                    result += nums[i];
                }
            }

            return result;
        }

        // Function to count the number of set bits in an integer
        private int CountSetBits(int num)
        {
            int count = 0;
            while (num > 0)
            {
                count += num & 1;
                num >>= 1;
            }
            return count;
        }
        #endregion
    }
}
namespace Jun
{
    public class Solution
    {
        #region Day 1 Problem
        #endregion

        #region Day 2 Problem 2101. Detonate the Maximum Bombs
        public int MaximumDetonation(int[][] bombs)
        {
            int result = 0;
            Dictionary<int, List<int>> map = new Dictionary<int, List<int>>();

            int len = bombs.Length;

            for (int i = 0; i < len; i++)
            {
                map.Add(i, new List<int>());
            }

            for (int i = 0; i < len; i++)
            {
                for (int j = 0; j < len; j++)
                {
                    if (i == j) continue;

                    int x1 = bombs[i][0];
                    int y1 = bombs[i][1];
                    int r1 = bombs[i][2];
                    int x2 = bombs[j][0];
                    int y2 = bombs[j][1];
                    int r2 = bombs[j][2];

                    if ((long)(r1 * r2) >= (long)(x1 - x2) * (long)(x1 - x2) + (long)(y1 - y2) * (long)(y1 - y2))
                    {
                        map[i].Add(j);
                    }
                }
            }


            return result;
        }

        private int dfs(int cur, bool[] visited, Dictionary<int, List<int>> map)
        {
            visited[cur] = true;

            int count = 1;

            foreach (var item in map[cur])
            {
                if (!visited[cur])
                {
                    count += dfs(item, visited, map);
                }
            }
            return count;
        }
        #endregion

        #region Day 3 Problem
        #endregion

        #region Day 4 Problem 547. Number of Provinces
        public int FindCircleNum(int[][] isConnected)
        {
            int n = isConnected.Length;

            Dictionary<int, List<int>> dct = new Dictionary<int, List<int>>();

            for (int i = 0; i < n; i++)
            {
                dct.Add(i, new List<int>());

                for (int j = 0; j < n; j++)
                {
                    if (i == j) continue;

                    if (isConnected[i][j] == 1) dct[i].Add(j);
                }
            }

            bool[] visited = new bool[n];

            int count = 0;

            Queue<int> queue = new Queue<int>();

            foreach (var key in dct.Keys)
            {
                if (!visited[key])
                {
                    count++;
                    queue.Enqueue(key);
                    visited[key] = true;
                    while (queue.Count > 0)
                    {
                        var p = queue.Dequeue();

                        foreach (var item in dct[p])
                        {
                            if (!visited[item])
                            {
                                queue.Enqueue(item);
                                visited[item] = true;
                            }
                        }

                    }

                }
            }

            return 0;



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

        #region Problem Forest Fire


        #endregion

        #region Problem
        #endregion

        #region Problem
        #endregion
    }
}
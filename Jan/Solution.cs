namespace Jan
{
    public class Solution
    {
        #region Problem Day 1 290. Word Pattern
        public bool WordPattern(string pattern, string s)
        {
            char[] chars = pattern.ToCharArray();

            string[] words = s.Split(' ');

            if(words.Length == chars.Length && words.Distinct().Count() == chars.Distinct().Count())
            {
                Dictionary<char, string> map = new Dictionary<char, string>();


                for(int i = 0; i < words.Length; i++)
                {
                    char c = chars[i];
                    string word = words[i];

                    if (map.ContainsKey(c))
                    {
                        if(map[c] != word) return false;
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

        #region Problem Day 2
        #endregion

        #region Problem Day 3
        #endregion

        #region Problem Day 4
        #endregion

        #region Problem Day 5
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
    }
}
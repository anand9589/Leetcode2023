// See https://aka.ms/new-console-template for more information

using Jan;


Solution solution = new Solution();

var p = solution.FindMinArrowShots(new int[][] { new int[] { 10, 16 }, new int[] { 2, 8 }, new int[] { 1, 6 }, new int[] { 7, 12 }, new int[] { 10, 20 } });
Console.WriteLine(p);
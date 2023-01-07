// See https://aka.ms/new-console-template for more information

using Jan;


Solution solution = new Solution();

var p = solution.CanCompleteCircuit(new int[] { 6, 1, 4, 3, 5 }, new int[] { 3, 8, 2, 4, 2 });
Console.WriteLine(p);
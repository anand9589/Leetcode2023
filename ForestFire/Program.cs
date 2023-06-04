// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

string[] input1 = Console.ReadLine().Split();
int N = int.Parse(input1[0]);
int X = int.Parse(input1[1]);


long[] energyLevels = Console.ReadLine().Split().Select(long.Parse).ToArray();
long minEnergyLevel = FindMinEnergyLevel(N, X, energyLevels);

static long FindMinEnergyLevel(int N, int X, long[] energyLevels)
{
    if (N == X) return energyLevels[0];
    //Array.Sort(energyLevels);

    SortedList<long, long> list = new SortedList<long, long>();
    for (int i = 0; i < N; i++)
    {
        list.Add(energyLevels[i], energyLevels[i]);
    }




    if (list.ElementAt(N - X).Value != list.ElementAt(N - X - 1).Value) return list.ElementAt(N - X).Value;

    //for(int i = 1; i < N; i++)
    //{
    //    if (energyLevels[i] == energyLevels[i - 1]) continue;

        //    if(N-i==X) return energyLevels[i];

        //    if (N - i < X) break;
        //}
    return -1;
}

Console.WriteLine(minEnergyLevel);

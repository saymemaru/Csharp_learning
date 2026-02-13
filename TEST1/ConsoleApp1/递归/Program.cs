using G2048;
using 递归;
using static G2048.Game2048;

/*int[] numsA = [0,2,0,2,8,2];
//int[] nums4 = Game2048.MoveZeroToEnd(numsA);
int[] nums4 = Game2048.CombineSameNumbers(numsA);
foreach (int i in nums4)
{
    Console.Write(i + ",");
}*/

int[,] numsC = { {2, 2, 0 },{4, 2, 4 },{4, 0, 2 } };
Game2048.Move(MoveDirection.Up, numsC);
Game2048.PrintDoubleArray(numsC);

Recursion_.汉诺塔(3, 'A', 'B', 'C');

Console.WriteLine(Recursion_.times);
Console.WriteLine(Recursion_.累加(10));

Console.WriteLine(Recursion_.奇正偶负和(10));

Console.WriteLine();

int[] numsB = [5,2,7,10,34];
int[] nums1 = Recursion_.选择排序(numsB);
int[] nums2 = Recursion_.冒泡排序(numsB);
foreach (int i in nums1)
{
    Console.Write(i + ",");
}
Console.WriteLine();

foreach (int i in nums2)
{
    Console.Write(i + ",");
}
Console.WriteLine();
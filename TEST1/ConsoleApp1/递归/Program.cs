using 递归;

Recursion_.汉诺塔(3, 'A', 'B', 'C');

Console.WriteLine(Recursion_.times);
Console.WriteLine(Recursion_.累加(10));

Console.WriteLine(Recursion_.奇正偶负和(10));

Console.WriteLine();

int[] nums = [5,2,7,10,34];
int[] nums1 = Recursion_.选择排序(nums);
int[] nums2 = Recursion_.冒泡排序(nums);
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
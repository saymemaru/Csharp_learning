
DateTime now = DateTime.Now;
Console.WriteLine(now.ToString("yy-MM-dd"));
Console.WriteLine($"{now:yyyy年MM月dd日}");
Console.WriteLine($"{now:F}");

double pi = 3141.5926;
Console.WriteLine(pi.ToString("F2")); // 保留两位小数
Console.WriteLine($"{pi:N2}"); // N千位分隔符 2小数位数
Console.WriteLine($"{pi:0.001}"); // 自定义格式
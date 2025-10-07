﻿
属性 p = new();
p.VerifyName = "A";
Console.WriteLine(p.VerifyName = "Correct name");


class 属性
{
    //标准的封装字段的属性
    //隐藏字段
    private string name = "Null";
    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    //自动实现属性
    //会自动创建一个隐藏的字段
    //但是无法在set或get中添加逻辑,添加了逻辑会导致编译错误
    public string AutoName { get; set; } = "Null";

    //控制读写字段
    public string GetName1 { get { return name; } }
    public string GetName2 => name; //表达式体

    public string SetName { set { name = value; } }


    //添加字段数据校验
    public string VerifyName
    {
        get => name;
        set 
        { 
            if (value.Length < 2)
            {
                Console.WriteLine("Name too short");
            }
            else
            {
                name = value;
            }
        }
    }
}


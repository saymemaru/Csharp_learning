
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

    //控制读写字段
    public string GetName1 { get { return name; } }
    public string GetName2 => name; //表达式体

    public string SetName { set { name = value; } }

    //自动实现属性
    public string AutoName { get; set; } = "Null";

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


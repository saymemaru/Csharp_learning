using 枚举;
using static 枚举.Enum_;

//多项枚举
PrintAnimal(Animal.Cat | Animal.Dog);
PrintAnimal(Animal.Fish | Animal.Rabbit | Animal.Pig);

//数据类型转换
//int -> enum
Animal animal1 = (Animal)2;
//enum -> int
int enumNum = (int)Animal.Cat;
//string -> enum
Animal animal2 = (Animal)Enum.Parse(typeof(Animal),"Cat");
//enum -> string
string strEnum = Animal.Cat.ToString();


枚举日期("星期一");
枚举日期(日期.星期三);
枚举日期(5);

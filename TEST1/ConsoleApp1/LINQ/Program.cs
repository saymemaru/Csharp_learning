using System.Data.SqlTypes;
using System.Xml.Linq;
using 对象与类;

List<人类> peopleList1 = 人类.GetPeople();
var filtered1 = from 人类 person in peopleList1
               where person.年龄 > 21
               orderby person.年龄
               select person;
foreach (人类 person in filtered1)
{
    Console.WriteLine(person.年龄);
}

//从XML文件中查询
XDocument doc = XDocument.Load("data.xml");
var filtered2 = from p in doc.Descendants("Product")
                join s in doc.Descendants("Supplier")
                on (int)p.Attribute("SupplierID")
                equals (int)s.Attribute("SupplierID")
                where (decimal)p.Attribute("Price") > 10
                orderby (string)s.Attribute("Name"),
                    (string)p.Attribute("Name")
                select new
                {
                    SupplierName = (string)s.Attribute("Name"),
                    ProductName = (string)p.Attribute("Name")
                };

foreach (var item in filtered2)
{
    Console.WriteLine("Supplier={0}; Product={1}",
                       item.SupplierName, item.ProductName);
}



//linq to sql
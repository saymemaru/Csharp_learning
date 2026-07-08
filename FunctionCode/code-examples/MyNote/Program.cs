using MyNote;
using System.Collections.Immutable;

/*Example e = new Example();
ImmutableList<int> l1 = [1, 2, 3, 3];
ImmutableList<int> l2 = e.AddNumToList(l1);
Console.WriteLine(l1);
Console.WriteLine(l2);*/


string savePath = "person.json";

/*List<Person> personList= 
    [ 
    new Person() { PersonName = "Jack", Age = 10, Birthday = new DateTime(2000), Weight = 50 },
    new Person() { PersonName = "Tom", Age = 52, Birthday = new DateTime(1989), Weight = 58 },
    new Person() { PersonName = "Tom", Age = 23, Birthday = new DateTime(1989), Weight = 70 },
    new Person() { PersonName = "Tom", Age = 35, Birthday = new DateTime(1989), Weight = 100 }
    ];
JsonIO.Store(personList, savePath);*/

ImmutableList<Person> people = JsonIO.Load<Person>(savePath);

var getName = ImmutableList.Create(people
    .Select(x => new Person { PersonName = x.PersonName })
    .Distinct()
    .ToArray());
var agefiltedPeople = people
    .Where(p => p.Age >= 18)
    .ToImmutableList();

Console.WriteLine(Person.GetTotalWeight(people));
Console.WriteLine(Person.GetTotalWeight(agefiltedPeople));

people.Take(3).ToList().ForEach(p => Console.WriteLine(p));


using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNote
{
    public class Person
    {
        public string PersonName { get; set; }
        public int Age { get; set; }
        public DateTime Birthday { get; set; }
        public double Weight {  get; set; }

        public static double GetTotalWeight(ImmutableList<Person> people) 
        {
            return people
                .Sum( x => x.Weight);
        }
    }
}

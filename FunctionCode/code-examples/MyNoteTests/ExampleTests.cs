using MyNote;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyNote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNote.Tests
{
    [TestClass()]
    public class ExampleTests
    {
        [TestMethod()]
        public void CalcDiscountTest()
        {
            Example example = new();
            decimal amout = 100m;
            decimal discountRate = .2m;

            var result1 = example.CalcDiscount(amout, discountRate);
            var result2 = example.CalcDiscount(amout, discountRate);

            Assert.AreEqual(result1, result2);
            Assert.AreEqual(amout * (1 - discountRate), result1);
        }
    }
}
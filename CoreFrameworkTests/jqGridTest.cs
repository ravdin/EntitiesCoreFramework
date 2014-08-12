using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EntitiesCoreFramework.jqGrid;

namespace CoreFrameworkTests
{
    [TestClass]
    public class jqGridTest
    {
        //[TestInitialize]

        [TestMethod]
        public void TestGridSort()
        {
            var fakeEntities = TestData.GetFakeEntities();
            var sorted = fakeEntities.AsQueryable().OrderBy("LastName", "asc");
            Assert.IsTrue(sorted.First().FirstName == "Jayne");
        }

        [TestMethod]
        public void TestGridFilterEquals()
        {
            var fakeEntities = TestData.GetFakeEntities();
            var filter = new Filter
            {
                groupOp = "AND",
                rules = new Rule[] { new Rule { data = "Tam", field = "LastName", op = "eq" } }
            };

            var filtered = fakeEntities.AsQueryable().Where(filter);
            Assert.IsTrue(filtered.Count() == 2);
        }

        [TestMethod]
        public void TestGridFilterNotEquals()
        {
            var fakeEntities = TestData.GetFakeEntities();
            var filter = new Filter
            {
                groupOp = "AND",
                rules = new Rule[] { new Rule { data = "Tam", field = "LastName", op = "ne" } }
            };

            var filtered = fakeEntities.AsQueryable().Where(filter);
            Assert.IsTrue(filtered.Count() == 3);
        }


        [TestMethod]
        public void TestGridFilterContains()
        {
            var fakeEntities = TestData.GetFakeEntities();
            var filter = new Filter
            {
                groupOp = "OR",
                rules = new Rule[] { new Rule { data = "a", field = "LastName", op = "cn" } }
            };

            var filtered = fakeEntities.AsQueryable().Where(filter);
            Assert.IsTrue(filtered.Count() == 3);
        }
    }
}

using System;
using System.Data.Entity;
using System.Collections.Generic;
using EntitiesCoreFramework.Data;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using EntitiesCoreFramework.Repository;

namespace CoreFrameworkTests
{
    [TestClass]
    public class RepositoryTest
    {
        private Mock<IDbContext> _mockDbContext;
        private EFRepository<FakeEntity> _testRepository;

        [TestMethod]
        public void TestGetById()
        {
            var fake = _testRepository.GetById(2);
            Assert.IsTrue(fake.FirstName == "Zoe" && fake.LastName == "Washburne");
        }

        [TestMethod]
        public void TestUpdate()
        {
            var fake = _testRepository.GetById(3);
            fake.LastLogin = DateTime.Now;
            _testRepository.Update(fake);
            _mockDbContext.Verify(m => m.SaveChanges());
        }

        [TestMethod]
        public void TestDelete()
        {
            var fake = _testRepository.GetById(1);
            _testRepository.Delete(fake);
            _mockDbContext.Verify(m => m.SaveChanges());
        }

        [TestInitialize]
        public void Setup()
        {
            var mockDbSet = new MockDbSet<FakeEntity>(TestData.GetFakeEntities());
            _mockDbContext = new Mock<IDbContext>();
            _mockDbContext.Setup(m => m.Set<FakeEntity>()).Returns(mockDbSet);
            _testRepository = new EFRepository<FakeEntity>(_mockDbContext.Object);
        }
    }
}

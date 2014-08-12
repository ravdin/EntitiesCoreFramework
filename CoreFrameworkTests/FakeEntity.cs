using System;
using System.Collections.Generic;
using EntitiesCoreFramework.Data;

namespace CoreFrameworkTests
{
    public class FakeEntity : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime LastLogin { get; set; }
    }

    public static class TestData
    {
        public static IEnumerable<FakeEntity> GetFakeEntities()
        {
            yield return new FakeEntity
            {
                Id = 1,
                FirstName = "Malcolm",
                LastName = "Reynolds",
                LastLogin = DateTime.Today
            };

            yield return new FakeEntity
            {
                Id = 2,
                FirstName = "Zoe",
                LastName = "Washburne",
                LastLogin = DateTime.Today.AddDays(-7)
            };

            yield return new FakeEntity
            {
                Id = 3,
                FirstName = "Jayne",
                LastName = "Cobb",
                LastLogin = DateTime.Today.AddDays(-20)
            };

            yield return new FakeEntity
            {
                Id = 4,
                FirstName = "River",
                LastName = "Tam",
                LastLogin = DateTime.Today.AddYears(-2)
            };

            yield return new FakeEntity
            {
                Id = 5,
                FirstName = "Simon",
                LastName = "Tam",
                LastLogin = DateTime.Today.AddDays(-1)
            };
        }
    }
}

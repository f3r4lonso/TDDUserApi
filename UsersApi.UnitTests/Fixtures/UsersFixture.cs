namespace NewWebApi.UnitTests.Fixtures
{
    using UsersApi.API.Model;
    public static class UsersFixture
    {
        public static List<User> GetTestUsers() =>
            new()
            {
                new User()
                {
                    Id = 1,
                    Name = "Test",
                    Email = "email@test.com",
                    Address = new Address()
                    {
                        StreetName ="test",
                        City = "test",
                        ZipCode = "test",
                    }
                },
                new User()
                {
                    Id = 2,
                    Name = "Test1",
                    Email = "email@test.com",
                    Address = new Address()
                    {
                        StreetName ="test",
                        City = "test",
                        ZipCode = "test",
                    }
                },
                new User()
                {
                    Id = 3,
                    Name = "Test2",
                    Email = "email@test.com",
                    Address = new Address()
                    {
                        StreetName ="test",
                        City = "test",
                        ZipCode = "test",
                    }
                },
            };
    }
}

using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Xunit;
using KingPriceUserManagementWebApp.Data;
using KingPriceUserManagementWebApp.Models;
using Microsoft.EntityFrameworkCore.InMemory;

namespace KingPriceUserManagementWebApp.Tests
{
    public class AppDbContextTests
    {
        [Fact]
        public void Can_Create_AppDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var context = new AppDbContext(options))
            {
                Assert.NotNull(context);
            }
        }

        [Fact]
        public void Can_Add_User_To_Db()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var context = new AppDbContext(options))
            {
                var user = new User { Username = "TestUser" };
                context.Users.Add(user);
                context.SaveChanges();

                Assert.Equal(1, context.Users.Count());
                Assert.Equal("TestUser", context.Users.First().Username);
            }
        }
    }
}

using System.Data.Common;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Lab07.UnitTesting.DAL.Core.Context;
using Lab07.UnitTesting.DAL.Models;
using Lab07.UnitTesting.DAL.Repositories;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Moq;
using NUnit.Framework;

namespace Lab07.UnitTesting.IntegrationTests
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class StoreTypeRepositoryTest
    {
        private StoreTypeReposiroty storeTypeReposiroty;
        private GodelBenefitContext context;
        private SqlConnection connection;

        [SetUp]
        public void SetUp()
        {
            connection = new SqlConnection(@"Server=(LocalDb)\MSSQLLocalDB;Database=TestGodelBenefitBase;Trusted_Connection=True;");
            context = new GodelBenefitContext(connection);
            storeTypeReposiroty = new StoreTypeReposiroty(context);
        }

        [TearDown]
        public void TearDown()
        {
            context.Dispose();
            connection.Dispose();
        }

        [Test]
        public async Task StoreType_Create() 
        {
            StoreType storeType = new StoreType
            {
                Name = "Test"
            };

            await storeTypeReposiroty.CreateAsync(storeType);
            var actualResult = await context.SaveChangesAsync();

            actualResult.Should().Be(1);
        }

        [Test]
        public async Task StoreType_GetByIdAsync()
        {
            var actualResult = await storeTypeReposiroty.GetByIdAsync(1);

            actualResult.Should().NotBeNull();
        }
    }
}

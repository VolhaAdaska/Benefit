using Lab07.UnitTesting.DAL.Core.Context;
using NUnit.Framework;
using System.Data.Common;

namespace Lab07.UnitTesting.DAL.Repositories.Tests
{
    [TestFixture]
    public class StoreRepositoryTest
    {
        private DbConnection dbConnection;
        private GodelBenefitContext context;
        private StoreRepository storeRepository;


        [SetUp]
        public void SetUp()
        {
            dbConnection = Effort.DbConnectionFactory.CreateTransient();
            context = new GodelBenefitContext(dbConnection);

            storeRepository = new StoreRepository(context);
        }

        [Test]
        public void CreateAsync_When_Entity_Null_Should_Throw_ArgumentNullException()
        {
            Assert.That(() => storeRepository.CreateAsync(null), Throws.ArgumentNullException);
        }

        //[Test]
        //public async Task CreateAsync_When_Create_Should_Return_Store_Entity()
        //{
        //    Store store = new Store
        //    {
        //        Id = 1,
        //        Name = "SomeStore",
        //        Promocode = "SomePromocode",
        //        StoreTypeId = 1
        //    };

        //    await storeRepository.CreateAsync(store);
        //    context.SaveChanges();

        //}
    }
}
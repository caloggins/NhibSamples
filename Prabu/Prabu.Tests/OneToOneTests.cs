namespace Prabu.Tests
{
    using FizzWare.NBuilder;
    using FluentAssertions;
    using Library;
    using Library.Entities;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NHibernate;

    [TestClass]
    public class OneToOneTests
    {
        private const string ConnectionString = "Data Source=localhost;Initial Catalog=Prabu;Integrated Security=SSPI";

        private ISession session;

        [TestInitialize]
        public void Setup()
        {
            var sessionFactory = SessionFactoryFactory.CreateSessionFactory(ConnectionString);
            session = sessionFactory.OpenSession();
        }

        [TestMethod]
        public void SavingAUserEntity()
        {
            var userToBeSaved = Builder<User>.CreateNew()
                .With(user1 => user1.Id = 0)
                .Build();

            object save;

            using (var txn = session.BeginTransaction())
            {
                save = session.Save(userToBeSaved);
                txn.Commit();
            }

            var loadedUser = session.Get<User>(save);

            loadedUser.ShouldHave().AllPropertiesBut(user => user.Id).EqualTo(userToBeSaved);

            using (var txn = session.BeginTransaction())
            {
                session.Delete(loadedUser);
                txn.Commit();
            }
        }

        [TestMethod]
        public void SavingAUserWithADetailObject()
        {
            var userToBeSaved = Builder<User>.CreateNew()
                .With(user1 => user1.Id = 0)
                .Build();
            var detailToBeSaved = Builder<UserDetail>.CreateNew().Build();
            userToBeSaved.AddDetail(detailToBeSaved);

            object save;

            using (var txn = session.BeginTransaction())
            {
                save = session.Save(userToBeSaved);
                txn.Commit();
            }

            var loadedUser = session.Get<User>(save);

            loadedUser.ShouldHave().AllPropertiesBut(user => user.Id).EqualTo(userToBeSaved);
            loadedUser.Detail.ShouldHave().AllProperties().EqualTo(detailToBeSaved);

            using (var txn = session.BeginTransaction())
            {
                session.Delete(loadedUser);
                txn.Commit();
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            session.Dispose();
        }
    }
}
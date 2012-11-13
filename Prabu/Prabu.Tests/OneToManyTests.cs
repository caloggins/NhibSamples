namespace Prabu.Tests
{
    using FizzWare.NBuilder;
    using FluentAssertions;
    using Library;
    using Library.Entities;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NHibernate;

    [TestClass]
    public class OneToManyTests
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
        public void SavingTheGroup()
        {
            var accountToBeSaved = Builder<Account>.CreateNew().With(account => account.Id = 0).Build();

            object save;

            using (var txn = session.BeginTransaction())
            {
                save = session.Save(accountToBeSaved);
                txn.Commit();
            }

            var accountFromDatabase = session.Get<Account>(save);
            accountFromDatabase.ShouldHave().AllPropertiesBut(account => account.Id).EqualTo(accountToBeSaved);

            var customerToBeSaved = Builder<Customer>.CreateNew().With(customer => customer.Id = 0).Build();

            using (var txn = session.BeginTransaction())
            {
                save = session.Save(customerToBeSaved);
                txn.Commit();
            }

            var customerFromDatabase = session.Get<Customer>(save);
            customerFromDatabase.ShouldHave().AllPropertiesBut(customer => customer.Id).EqualTo(customerToBeSaved);

            var customerAccountToBeSaved = Builder<CustomerAccount>.CreateNew()
                .Build();
            accountToBeSaved.AddCustomerAccount(customerAccountToBeSaved);
            customerToBeSaved.AddCustomerAccount(customerAccountToBeSaved);


            using (var txn = session.BeginTransaction())
            {
                save = session.Save(customerAccountToBeSaved);
                txn.Commit();
            }

            var customerAccountFromDatabase = session.Get<CustomerAccount>(save);
            customerAccountFromDatabase.ShouldHave().AllProperties().EqualTo(customerAccountToBeSaved);

            using (var txn = session.BeginTransaction())
            {
                session.Delete(accountFromDatabase);
                session.Delete(customerFromDatabase);
                session.Delete(customerAccountFromDatabase);
                txn.Commit();
            }
        }
    }
}
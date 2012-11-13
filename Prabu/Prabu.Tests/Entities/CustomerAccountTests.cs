namespace Prabu.Tests.Entities
{
    using FizzWare.NBuilder;
    using FluentAssertions;
    using Library.Entities;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    // ReSharper disable InconsistentNaming
    [TestClass]
    public class CustomerAccountTests
    {
        [TestMethod]
        public void AddParent_WhenAnAccountIsAdded_ThePropertyShouldBeSet()
        {
            var sut = CustomerAccountFactory();
            var account = Builder<Account>.CreateNew().Build();

            sut.AddParent(account);

            sut.Account.Should().BeSameAs(account);
        }

        [TestMethod]
        public void AddParent_WhenAnAccountIsAdded_TheIdsShouldMatch()
        {
            var sut = CustomerAccountFactory();
            var account = Builder<Account>.CreateNew().Build();

            sut.AddParent(account);

            sut.AccountId.Should().Be(account.Id);
        }

        [TestMethod]
        public void AddParent_WhenACustomerIsAdded_ThePropertyShouldBeSet()
        {
            var sut = CustomerAccountFactory();
            var customer = Builder<Customer>.CreateNew().Build();

            sut.AddParent(customer);

            sut.Customer.Should().BeSameAs(customer);
        }

        [TestMethod]
        public void AddParent_WhenACustomerIsAdded_TheIdsShouldMatch()
        {
            var sut = CustomerAccountFactory();
            var customer = Builder<Customer>.CreateNew().Build();

            sut.AddParent(customer);

            sut.CustomerId.Should().Be(customer.Id);
        }

        private CustomerAccount CustomerAccountFactory()
        {
            return new CustomerAccount();
        }
    }
}
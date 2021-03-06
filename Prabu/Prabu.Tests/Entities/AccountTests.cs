﻿namespace Prabu.Tests.Entities
{
    using FizzWare.NBuilder;
    using FluentAssertions;
    using Library.Entities;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    // ReSharper disable InconsistentNaming
    [TestClass]
    public class AccountTests
    {
        [TestMethod]
        public void AddCustomerAccount_WhenACustomerAccountIsAdded_TheCustomerAccountShouldBeSet()
        {
            var sut = AccountFactory();
            var customerAccount = Builder<CustomerAccount>.CreateNew().Build();

            sut.AddCustomerAccount(customerAccount);

            sut.CustomerAccounts.Should().OnlyContain(account => account == customerAccount);
        }

        [TestMethod]
        public void AddCustomerAccount_WhenAdded_TheCustomerAccountShouldHaveAnAccount()
        {
            var sut = AccountFactory();
            var customerAccount = Builder<CustomerAccount>.CreateNew().Build();

            sut.AddCustomerAccount(customerAccount);

            customerAccount.Account.Should().Be(sut);
        }

        private Account AccountFactory()
        {
            return new Account();
        }
    }
}
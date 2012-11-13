namespace Prabu.Tests.Entities
{
    using FizzWare.NBuilder;
    using FluentAssertions;
    using Library.Entities;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class UserDetailTests
    {
        [TestMethod]
// ReSharper disable InconsistentNaming
        public void AddUser_WhenAUserIsAdded_TheIdsShouldMatch()
        {
            var sut = UserDetailFactory();
            var user = Builder<User>.CreateNew().Build();

            sut.AddUser(user);

            sut.UserId.Should().Be(user.Id);
        }

        private UserDetail UserDetailFactory()
        {
            var sut = new UserDetail();
            return sut;
        }

        [TestMethod]
        public void AddUser_WhenAUserIsAdded_TheUserShouldBeSet()
        {
            var sut = UserDetailFactory();
            var user = Builder<User>.CreateNew().Build();

            sut.AddUser(user);

            sut.ParentUser.Should().Be(user);
        }
    }
}
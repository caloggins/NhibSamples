namespace Prabu.Tests.Entities
{
    using FizzWare.NBuilder;
    using FluentAssertions;
    using Library.Entities;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class UserTests
    {
        public User UserFactory()
        {
            return new User();
        }

        [TestMethod]
        public void AddDetail_WhenADetailIsAdded_TheDetailShouldBeSet()
        {
            var sut = UserFactory();
            var testDetail = Builder<UserDetail>.CreateNew()
                .With(detail => detail.UserId = 0)
                .Build() ;

            sut.AddDetail(testDetail);

            sut.Detail.Should().Be(testDetail);
        }

        [TestMethod]
        public void AddDetail_WhenADetailIsAdded_TheDetailUserShouldBeSet()
        {
            var sut = UserFactory();
            sut.Id = 123L;
            var testDetail = Builder<UserDetail>.CreateNew()
                .With(detail => detail.UserId = 0)
                .Build();

            sut.AddDetail(testDetail);

            testDetail.ParentUser.Should().Be(sut);
        }
    }
}
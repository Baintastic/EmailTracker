using EmailTracker.Core.Models;
using EmailTracker.Repository.IRepositories;
using EmailTracker.Service.Services;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmailTracker.Service.Tests
{
    [TestFixture]
    public class EmailServiceTests
    {
        private IEmailRepository _emailRepository;
        private List<Email> _emails;

        [SetUp]
        public void SetUp()
        {
            _emailRepository = Substitute.For<IEmailRepository>();
            _emails = new List<Email>
            {
                new Email
                {
                    FromAddress = "info@companya.co",
                    ToAddress = "christ@gmail.com",
                    EmailSubject = "",
                    Body = "",
                    Cc = "",
                    Bcc = "",
                    IsArchived = true,
                },
                new Email
                {
                    FromAddress = "info@companyb.co",
                    ToAddress = "lola@gmail.com",
                    EmailSubject = "",
                    Body = "",
                    Cc = "",
                    Bcc = "",
                    IsArchived = true,
                },
            };
        }

        [Test]
        public async Task GetAllEmails_ShouldReturnAllEmails()
        {
            //Arrange
            var sut = CreateSUT();
            _emailRepository.GetAll().Returns(_emails);

            //Act
            var actual = await sut.GetAllEmails();

            //Assert
            Assert.AreEqual(_emails, actual);
        }

        [Test]
        public async Task GetLabelById_ShouldReturnLabel()
        {
            //Arrange
            var sut = CreateSUT();
            var emailId = 1;
            var email = new Email
            {
                FromAddress = "info@companyb.co",
                ToAddress = "lola@gmail.com",
                EmailSubject = "",
                Body = "",
                Cc = "",
                Bcc = "",
                IsArchived = true,
            };
            _emailRepository.GetById(emailId).Returns(email);

            //Act
            var actual = await sut.GetEmailById(emailId);

            //Assert
            Assert.AreEqual(email, actual);
        }

        [Test]
        public async Task FilterEmailsByLabelNameArchivedStatusOrFromEmailAddress_ShouldReturnFilteredEmails()
        {
            //Arrange
            var sut = CreateSUT();
            string labelName = "Important";
            bool? isArchived = null;
            string fromEmailAddress = null;
            _emailRepository.FilterEmailsByLabelNameArchivedStatusOrFromEmailAddress(labelName, isArchived, fromEmailAddress).Returns(_emails);

            //Act
            var actual = await sut.FilterEmailsByLabelNameArchivedStatusOrFromEmailAddress(labelName, isArchived, fromEmailAddress);

            //Assert
            Assert.AreEqual(_emails, actual);
        }

        private EmailService CreateSUT()
        {
            return new EmailService(_emailRepository);
        }
    }
}


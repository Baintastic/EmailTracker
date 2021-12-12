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

        [SetUp]
        public void SetUp()
        {
            _emailRepository = Substitute.For<IEmailRepository>();
        }

        [Test]
        public async Task GetAllEmails_ShouldReturnAllEmails()
        {
            //Arrange
            var service = GetEmailService();
            List<Email> emails = CreateFakeEmails();

            //Act
            _emailRepository.GetAll().Returns(emails);
            var actualResult = await service.GetAllEmails();

            //Assert
            Assert.AreEqual(emails, actualResult);
        }

       

        [Test]
        public async Task GetLabelById_ShouldReturnLabel()
        {
            //Arrange
            var service = new EmailService(_emailRepository); 
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

            //Act
            _emailRepository.GetById(emailId).Returns(email);
            var actualResult = await service.GetEmailById(emailId);

            //Assert
            Assert.AreEqual(email, actualResult);
        }


        [Test]
        public async Task FilterEmailsByLabelNameArchivedStatusOrFromEmailAddress_ShouldReturnAllEmails()
        {
            //Arrange
            var service = GetEmailService();
            string labelName = null;
            bool? isArchived = null;
            string fromEmailAddress = null;
            var emails = CreateFakeEmails();

            //Act
            _emailRepository.FilterEmailsByLabelNameArchivedStatusOrFromEmailAddress(labelName, isArchived, fromEmailAddress).Returns(emails);
            var actualResult = await service.FilterEmailsByLabelNameArchivedStatusOrFromEmailAddress(labelName, isArchived, fromEmailAddress);

            //Assert
            Assert.AreEqual(emails, actualResult);
        }

        private static List<Email> CreateFakeEmails()
        {
            return new List<Email>
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

        private EmailService GetEmailService()
        {
            return new EmailService(_emailRepository);
        }
    }
}


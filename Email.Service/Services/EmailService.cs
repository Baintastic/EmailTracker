using EmailTracker.Core.Models;
using EmailTracker.Repository.IRepositories;
using EmailTracker.Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailTracker.Service.Services
{
    public class EmailService : IEmailService
    {
        private readonly IEmailRepository emailRepository;
        public EmailService(IEmailRepository emailRepository)
        {
            this.emailRepository = emailRepository;
        }

        public Task SendEmail(Email email)
        {
            Task task = new Task(() => Console.WriteLine(""));

            var toEmailAddresses = GetEmailAddresses(email.ToAddress);
            var cCEmailAddresses = GetEmailAddresses(email.Cc);
            var bCCEmailAddresses = GetEmailAddresses(email.Bcc);
            var emailAddressesToSendTo = toEmailAddresses.Concat<string>(cCEmailAddresses).Concat<string>(bCCEmailAddresses).ToList();
            for (int i = 0; i < emailAddressesToSendTo.Count; i++)
            {
                task = emailRepository.Add(email);
            }
            return task;
        }

        public Task DeleteEmail(int emailId)
        {
            return emailRepository.Delete(emailId);
        }

        public Task UndeleteEmail(int emailId)
        {
            return emailRepository.UndeleteEmailById(emailId);
        }

        public Task<IEnumerable<Email>> GetAllEmails()
        {
            return emailRepository.GetAll();
        }

        public Task<Email> GetEmailById(int emailId)
        {
            return emailRepository.GetById(emailId);
        }

        public Task<IEnumerable<Email>> FilterEmailsByLabelNameArchivedStatusOrFromEmailAddress(string labelName, bool? isArchived, string fromAddress)
        {
            return emailRepository.FilterEmailsByLabelNameArchivedStatusOrFromEmailAddress(labelName, isArchived, fromAddress);
        }

        private List<string> GetEmailAddresses(string addressString)
        {
            //Email addresses are separated by semi colon.
            var emailAddresses = new List<string>();
            if (!string.IsNullOrEmpty(addressString))
            {
                emailAddresses = addressString.Split(";").ToList();
            }
            return emailAddresses;
        }
    }
}

using EmailTracker.Repository.IRepositories;
using EmailTracker.Service.IServices;
using System.Collections.Generic;
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

        public Task SendEmail(Core.Models.Email email)
        {
            return emailRepository.Add(email);
        }

        public Task DeleteEmail(int emailId)
        {
            return emailRepository.Delete(emailId);
        }

        public Task<IEnumerable<Core.Models.Email>> GetAllEmailsBySenderEmailAddress(string senderEmailAddress)
        {
            return emailRepository.GetAllEmailsByEmailAddress(senderEmailAddress);
        }

        public Task UndeleteEmail(int emailId)
        {
            return emailRepository.UndeleteEmailById(emailId);
        }

        public Task<IEnumerable<Core.Models.Email>> GetAllDeletedEmails()
        {
            return emailRepository.GetAllDeletedEmails();
        }

        public Task<IEnumerable<Core.Models.Email>> GetAllEmailsByLabel(string labelName)
        {
            return emailRepository.GetAllEmailsByLabelName(labelName);
        }
    }
}

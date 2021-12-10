﻿using EmailTracker.Repository.IRepositories;
using EmailTracker.Service.IServices;
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
    }
}
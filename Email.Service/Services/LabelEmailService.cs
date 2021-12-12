using EmailTracker.Core;
using EmailTracker.Repository.IRepositories;
using EmailTracker.Service.IServices;
using System.Threading.Tasks;

namespace EmailTracker.Service.Services
{
    public class LabelEmailService : ILabelEmailService
    {
        private readonly ILabelEmailRepository labelEmailRepository;
        public LabelEmailService(ILabelEmailRepository labelEmailRepository)
        {
            this.labelEmailRepository = labelEmailRepository;
        }

        public Task AddLabelToEmail(LabelEmail labelledEmail)
        {
            return labelEmailRepository.Add(labelledEmail);
        }

        public Task RemoveLabelFromEmail(int labelEmailId, int emailId)
        {
            return labelEmailRepository.Delete(labelEmailId, emailId);
        }
    }
}

using EmailTracker.Core.Models;
using EmailTracker.Repository.IRepositories;
using EmailTracker.Service.IServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmailTracker.Service.Services
{
    public class LabelService : ILabelService
    {
        private readonly ILabelRepository labelRepository;
        public LabelService(ILabelRepository labelRepository)
        {
            this.labelRepository = labelRepository;
        }

        public Task CreateLabel(Label label)
        {
            return labelRepository.Add(label);
        }

        public Task DeleteLabel(int labelId)
        {
            return labelRepository.Delete(labelId);
        }

        public Task<IEnumerable<Label>> GetAllLabels()
        {
            return labelRepository.GetAll();
        }

        public Task<Label> GetLabelById(int labelId)
        {
            return labelRepository.GetById(labelId);
        }
    }
}

using EmailTracker.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmailTracker.Service.IServices
{
    public interface ILabelService
    {
        Task CreateLabel(Label label);
        Task DeleteLabel(int labelId);
        Task<IEnumerable<Label>> GetAllLabels();
        Task<Label> GetLabelById(int labelId);
    }
}

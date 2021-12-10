using EmailTracker.Core.Models;
using System.Threading.Tasks;

namespace EmailTracker.Service.IServices
{
    public interface ILabelService
    {
        Task CreateLabel(LabelField label);
        Task DeleteLabel(int labelId);
    }
}

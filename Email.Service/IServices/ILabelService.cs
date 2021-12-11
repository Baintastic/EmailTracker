using EmailTracker.Core.Models;
using System.Threading.Tasks;

namespace EmailTracker.Service.IServices
{
    public interface ILabelService
    {
        Task CreateLabel(Label label);
        Task DeleteLabel(int labelId);
    }
}

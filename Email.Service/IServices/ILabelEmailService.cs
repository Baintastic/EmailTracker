using EmailTracker.Core;
using System.Threading.Tasks;

namespace EmailTracker.Service.IServices
{
    public interface ILabelEmailService
    {
        Task AddLabelToEmail(LabelEmail labelledEmail);

        Task RemoveLabelFromEmail(int labelEmailId);
    }
}

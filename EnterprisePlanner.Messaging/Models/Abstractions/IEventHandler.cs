using System.Threading.Tasks;

namespace EnterprisePlanner.Messaging.Models.Abstractions
{
    public interface IEventHandler
    {
        Task Handle();
    }
}

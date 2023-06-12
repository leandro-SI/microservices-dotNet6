using LeoShopping.Email.Messages;
using LeoShopping.Email.Model;

namespace LeoShopping.Email.Repository.Interfaces
{
    public interface IEmailRepository
    {
        Task LogEmail(UpdatePaymetResultMessage message);

    }
}

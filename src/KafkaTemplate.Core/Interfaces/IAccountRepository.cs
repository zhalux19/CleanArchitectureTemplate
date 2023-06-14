using KafkaTemplate.Core.Entities;

namespace KafkaTemplate.Core.Interfaces
{
    public interface IAccountRepository: IRepository<Account>
    {
        Task<Account> GetAccountByName(string name);
    }
}

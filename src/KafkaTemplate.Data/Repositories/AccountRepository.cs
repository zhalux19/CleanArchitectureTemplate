using AutoMapper;
using KafkaTemplate.Core.Entities;
using KafkaTemplate.Core.Interfaces;
using KafkaTemplate.Data.Config;
using KafkaTemplate.Data.DataModels;
using System.Linq.Expressions;

namespace KafkaTemplate.Data.Repositories
{
    public class AccountRepository : MongoRepository<AccountData, Account>, IAccountRepository
    {

        public AccountRepository(IMapper mapper, IMongoDbOptions options) : base(mapper, options)
        {

        }

        public async Task<Account> GetAccountByName(string name)
        {
            Expression<Func<Account, bool>> predicate = x => x.Name == name;
            return await base.FindFirstByPredicate(predicate);
        }

        //public async Task<Account> GetByName(string name)
        //{
        //    return await base.FindSingleOrDefault( x => x.Name == name );
        //}

        //public async Task<bool> DisableAdminById(string name) {
        //    var updateDefinition = Builders<AccountData>.Update.Set(x => x.IsAdmin, false);
        //    return await base.UpdateByPreidcate(x => x.Name == name, updateDefinition);
        //}

        //public Task<bool> ReplaceByPredicate(Expression<Func<Account, bool>> predicate, Account entity)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<List<Account>> FindWhere(Expression<Func<Account, bool>> predicate)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<Account> FindSingleOrDefault(Expression<Func<Account, bool>> predicate)
        //{
        //    throw new NotImplementedException();
        //}
    }
}

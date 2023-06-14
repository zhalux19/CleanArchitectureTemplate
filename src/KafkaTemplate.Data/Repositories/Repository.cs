using AutoMapper;
using KafkaTemplate.Data.Attributes;
using KafkaTemplate.Data.Config;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq.Expressions;

public abstract class MongoRepository<TData, TEntity>
{
    protected readonly IMongoCollection<TData> _collection;
    protected readonly IMapper _mapper;
    protected MongoRepository(IMapper mapper, IMongoDbOptions options)
    {
        _mapper = mapper;
        var database = new MongoClient(options.ConnectionString).GetDatabase(options.DatabaseName);
        _collection = database.GetCollection<TData>(GetCollectionName());
    }

    public virtual async Task<List<TEntity>> GetAll()
    {
        var data = await _collection.Find(_ => true).ToListAsync();
        return data.Select(x => MapToEntity(x)).ToList();
    }

    public virtual async Task<TEntity> GetById(string id)
    {
        ObjectId objectId = new(id);
        var filter = Builders<TData>.Filter.Eq("_id", objectId);
        var data = await _collection.Find(filter).FirstOrDefaultAsync();
        return MapToEntity(data);
    }

    public virtual async Task<TEntity> Create(TEntity entity)
    {
        var data = MapToData(entity);
        await _collection.InsertOneAsync(data);
        return MapToEntity(data);
    }

    //public virtual async Task<bool> ReplaceByPredicate(Expression<Func<TEntity, bool>> predicate, TEntity entity)
    //{
    //    var filter = Builders<TData>.Filter.Where(predicate);
    //    var data = MapToData(entity);
    //    var result = await _collection.ReplaceOneAsync(filter, data);
    //    return result.IsAcknowledged && result.ModifiedCount > 0;
    //}

    //public virtual async Task<bool> UpdateByPreidcate(Expression<Func<TEntity, bool>> predicate, UpdateDefinition<TEntity> updateDefinition)
    //{
    //    var filter = Builders<TData>.Filter.Where(predicate);
    //    var result = await _collection.UpdateOneAsync(filter, updateDefinition);
    //    return result.IsAcknowledged;
    //}

    //public virtual async Task<bool> Delete(string id)
    //{
    //    var filter = Builders<TData>.Filter.Eq("_id", id);
    //    var result = await _collection.DeleteOneAsync(filter);
    //    return result.IsAcknowledged && result.DeletedCount > 0;
    //}

    //public virtual async Task<List<TEntity>> FindWhere(Expression<Func<TEntity, bool>> predicate)
    //{
    //    var Data = await _collection.Find(predicate).ToListAsync();
    //    return Data.Select(x => MapToEntity(x)).ToList();
    //}

    public virtual async Task<TEntity> FindFirstByPredicate(Expression<Func<TEntity, bool>> predicate)
    {
        var translatedPredicate = TranslatePredicate(predicate);
        var data = await _collection.Find(translatedPredicate).FirstOrDefaultAsync();
        return MapToEntity(data);
    }

    private protected string GetCollectionName()
    {
        var entityType = typeof(TData);
        return ((CollectionAttribute)entityType.GetCustomAttributes(typeof(CollectionAttribute), true).FirstOrDefault()!).CollectionName;
    }

    protected TEntity MapToEntity(TData data)
    {
        return _mapper.Map<TEntity>(data);
    }
    protected TData MapToData(TEntity entity)
    {
        return _mapper.Map<TData>(entity);
    }

    protected Expression<Func<TData, bool>> TranslatePredicate(Expression<Func<TEntity, bool>> predicate)
    {
        var parameter = Expression.Parameter(typeof(TData), predicate.Parameters[0].Name);
        var body = new Visitor(parameter).Visit(predicate.Body);
        return Expression.Lambda<Func<TData, bool>>(body, parameter);
    }

    private class Visitor : ExpressionVisitor
    {
        private readonly ParameterExpression _parameter;

        public Visitor(ParameterExpression parameter)
        {
            _parameter = parameter;
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            return _parameter;
        }
    }
}
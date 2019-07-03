using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace MongoDBExcelOperator
{
    public class BasicService<T> where T : Entity
    {
        public IMongoCollection<T> MyCollection { get; }

        public BasicService(IMongoCollection<T> collection)
        {
            MyCollection = collection;
        }

        public async Task InsterOneAsync(T entity)
        {
            await MyCollection.InsertOneAsync(entity);
        }

        public async Task InsterManyAsync(IEnumerable<T> entities)
        {
            await MyCollection.InsertManyAsync(entities);
        }

        public async Task<T> FindOneByIdAsync(Guid id)
        {
            var filter = Builders<T>.Filter.Eq("_id", id);
            var result = await MyCollection.FindAsync<T>(filter);
            return result.FirstOrDefault();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace MongoDBExcelOperator
{
    public class CurrentShotNoService : BasicService<CurrentShotNo>
    {
        public CurrentShotNoService(IMongoCollection<CurrentShotNo> collection) : base(collection)
        {
            
        }

        public int GetCurrentShotNo()
        {
            var result = MyCollection.Find<CurrentShotNo>(Builders<CurrentShotNo>.Filter.Empty).ToList();
            if(result == null || result.Count == 0 )
            {
                return 0;
            }
            return result[0].No;
        }

        public void Upload(CurrentShotNo newShot)
        {
            MyCollection.DeleteMany(Builders<CurrentShotNo>.Filter.Empty);
            MyCollection.InsertOne(newShot);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace MongoDBExcelOperator
{
    public class TagPathService : BasicService<TagPath>
    {
        public TagPathService(IMongoCollection<TagPath> collection) : base(collection)
        {
        }

        public List<TagPath> DeriveAVersion(int versionNo)
        {
            FilterDefinitionBuilder<TagPath> builder = Builders<TagPath>.Filter;
            FilterDefinition<TagPath> filter = builder.Eq("VersionNo", versionNo);
            var result = MyCollection.Find<TagPath>(filter).ToList();
            return result;
        }

        public string GetFormatPath(string tagName, int versionNo)
        {
            FilterDefinitionBuilder<TagPath> builder = Builders<TagPath>.Filter;
            FilterDefinition<TagPath> filter1 = builder.Eq("VersionNo", versionNo);
            FilterDefinition<TagPath> filter2 = builder.Eq("TagName", tagName);
            var result = MyCollection.Find<TagPath>(filter1 & filter2).ToList();

            if(result.Count > 1)
            {
                throw new Exception("Same TagName in One VersionNo found! Sound the alarm！");
            }
            return result[0].TagFormatPath;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace MongoDBExcelOperator
{
    public class ShotNoVersionNoService : BasicService<ShotNoVersionNo>
    {
        public ShotNoVersionNoService(IMongoCollection<ShotNoVersionNo> collection) : base(collection)
        {
        }

        public int SearchBiggestVersionNo()
        {
            SortDefinitionBuilder<ShotNoVersionNo> builderSort = Builders<ShotNoVersionNo>.Sort;
            SortDefinition<ShotNoVersionNo> sort = builderSort.Descending("VersionNo");
            var result = MyCollection.Find<ShotNoVersionNo>(Builders<ShotNoVersionNo>.Filter.Empty).Sort(sort).ToList();

            return result[0].VersionNo;
        }

        /// <summary>
        /// 这里的逻辑是，如果有这一炮的版本号，直接返回
        /// 如果这一炮不存在版本号，找到比这一炮好小的最大炮号的版本号返回
        /// 比如搜索1056333，但只有小于等于1056300以及大于1056333的炮号存在ShotNoVersionNo数据库中，此时返回1056300的版本号
        /// </summary>
        /// <param name="shotNo"></param>
        /// <returns></returns>
        public int GetVersionNoByShotNo(int shotNo)
        {
            FilterDefinitionBuilder<ShotNoVersionNo> builder = Builders<ShotNoVersionNo>.Filter;
            FilterDefinition<ShotNoVersionNo> filter = builder.Eq("ShotNo", shotNo);
            var result = MyCollection.Find<ShotNoVersionNo>(filter).ToList();

            //找到了这一炮的版本号（可能存在多个），返回最大的那个
            if(result.Count >= 1)
            {
                int max = 1;
                foreach(var v in result)
                {
                    if (v.VersionNo > max) max = v.VersionNo;
                }
                return max;
            }

            //如果没有找到这一炮的版本号，找到比它小的所有炮
            builder = Builders<ShotNoVersionNo>.Filter;
            filter = builder.Lt("ShotNo", shotNo);
            result = MyCollection.Find<ShotNoVersionNo>(filter).ToList();

            var maxOne = new ShotNoVersionNo()
            {
                //因为必定存在-1炮，所以后面foreach肯定会有结果
                ShotNo = -2,
                VersionNo = 1
            };

            foreach(var r in result)
            {
                if(r.ShotNo > maxOne.ShotNo)
                {
                    maxOne = r;
                }
                else if(r.ShotNo == maxOne.ShotNo)
                {
                    if(r.VersionNo > maxOne.VersionNo)
                    {
                        maxOne = r;
                    }
                }
            }

            return maxOne.VersionNo;
        }
    }
}

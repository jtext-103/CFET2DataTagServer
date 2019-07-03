using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace MongoDBExcelOperator
{
    /// <summary>
    /// 给TagManagerThing操作MonogDB的接口
    /// </summary>
    public class MongoDBOperator
    {
        //指定之后只操作一个数据库，所以先全部给定以面后续还要传
        private string databaseHost;
        private string databaseName;

        private TagPathService myTagPathService;
        private ShotNoVersionNoService myShotNoVersionNoService;
        private CurrentShotNoService myCurrentShotNoService;

        private string tagPathCollectionName = "TagModel";
        private string shotNoVersionNoCollectionName = "ShotNoVersionNo";
        private string currentShotNoCollectionName = "CurrentShotNo";

        private MongoClient server;
        private IMongoDatabase dataBase;

        private IMongoCollection<TagPath> tagPathCollection;
        private IMongoCollection<ShotNoVersionNo> shotNoVersionNoCollection;
        private IMongoCollection<CurrentShotNo> currentShotNoCollection;

        public MongoDBOperator(string databaseHost, string databaseName)
        {
            this.databaseHost = databaseHost;
            this.databaseName = databaseName;

            server = new MongoClient(databaseHost);
            dataBase = server.GetDatabase(databaseName);

            tagPathCollection = dataBase.GetCollection<TagPath>(tagPathCollectionName);
            shotNoVersionNoCollection = dataBase.GetCollection<ShotNoVersionNo>(shotNoVersionNoCollectionName);
            currentShotNoCollection = dataBase.GetCollection<CurrentShotNo>(currentShotNoCollectionName);

            myTagPathService = new TagPathService(tagPathCollection);
            myShotNoVersionNoService = new ShotNoVersionNoService(shotNoVersionNoCollection);
            myCurrentShotNoService = new CurrentShotNoService(currentShotNoCollection);
        }

        public bool IsDatabaseExist()
        {
            if(tagPathCollection.Count(Builders<TagPath>.Filter.Empty) > 1)
            {
                return true;
            }
            return false;
        }

        public async void InsertTagPathMany(TagPathGroup many)
        {
            await myTagPathService.InsterManyAsync(many.Maps);
        }

        public async void InsertShotNoVersionNo(ShotNoVersionNo sv)
        {
            await myShotNoVersionNoService.InsterOneAsync(sv);
        }

        public int GetBiggestVersionNo()
        {
            return myShotNoVersionNoService.SearchBiggestVersionNo();
        }

        public TagPathGroup DeriveAVersion(int versionNo)
        {
            var tagPathGroup = new TagPathGroup();
            tagPathGroup.Maps = myTagPathService.DeriveAVersion(versionNo);
            return tagPathGroup;
        }

        public string GetFormatPath(string tagName, int shotNo)
        {
            int versionNo = myShotNoVersionNoService.GetVersionNoByShotNo(shotNo);
            return myTagPathService.GetFormatPath(tagName, versionNo);
        }

        public int GetVersionNoByShot(int shotNo)
        {
            return myShotNoVersionNoService.GetVersionNoByShotNo(shotNo);
        }

        public int GetCurrentShotNo()
        {
            return myCurrentShotNoService.GetCurrentShotNo();
        }

        public void UploadCurrentShotNo(CurrentShotNo newShot)
        {
            myCurrentShotNoService.Upload(newShot);
        }
    }
}

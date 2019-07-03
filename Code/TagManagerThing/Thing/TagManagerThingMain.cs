using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jtext103.CFET2.Core;
using Jtext103.CFET2.Core.Attributes;
using MongoDBExcelOperator;

namespace TagManager
{
    public partial class TagManagerThing : Thing
    {
        // 这个Thing的数据库在Init之后就不变了，一直存在这里
        private string databaseHost;
        private string databaseName;

        MongoDBOperator myMongoDBOperator;
        ExcelOperator myExcalOperator;

        //这两个都是临时的，方便操作
        ShotNoVersionNo shotNoVersionNo;
        TagPathGroup tagPathGroup;
        CurrentShotNo currentShotNo;

        private ManagerConfig myConfig;

        public override void TryInit(object initObj)
        {
            myConfig = new ManagerConfig((string)initObj);

            databaseHost = myConfig.DatabaseHost;
            databaseName = myConfig.DatabaseName;

            myMongoDBOperator = new MongoDBOperator(databaseHost, databaseName);
            myExcalOperator = new ExcelOperator();

            shotNoVersionNo = new ShotNoVersionNo();
            tagPathGroup = new TagPathGroup();
            currentShotNo = new CurrentShotNo();

            if (!myMongoDBOperator.IsDatabaseExist())
            {
                Console.WriteLine("The specified database does not exist! Please use the \"Import\" method to create the initial database from the configuration dir");
            }
        }
    }
}

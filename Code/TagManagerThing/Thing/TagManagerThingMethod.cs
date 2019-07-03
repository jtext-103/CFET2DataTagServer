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
        /// <summary>
        /// 用配置文件夹中的文件集合在Tag数据库中建立新的Version，之后最大版本号将+1
        /// 成功返回true，失败返回false
        /// </summary>
        /// <param name="configDirPath">配置文件夹完整路径</param>
        /// <returns></returns>
        [Cfet2Method]
        public bool Import(string configDirPath)
        {
            try
            {
                shotNoVersionNo = new ShotNoVersionNo();
                shotNoVersionNo.ShotNo = -1;
                if (myMongoDBOperator.IsDatabaseExist())
                {
                    shotNoVersionNo.VersionNo = BiggestVersionNo() + 1;
                }
                else
                {
                    shotNoVersionNo.VersionNo = 1;
                }


                tagPathGroup = myExcalOperator.GetNewTagPathGroup(configDirPath, shotNoVersionNo.VersionNo);

                myMongoDBOperator.InsertTagPathMany(tagPathGroup);
                myMongoDBOperator.InsertShotNoVersionNo(shotNoVersionNo);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 根据数据库中存在的某一版本号生成对应的配置文件组，默认最大版本号
        /// </summary>
        /// <param name="versionNo">导出的Tag数据库版本号，不给或1.0默认最大</param>
        /// <param name="configDirPath">导出路径，不给默认导出到桌面</param>
        [Cfet2Method]
        public bool Export(string configDirPath = null, int shotNo = -1)
        {
            int versionNo;
            if(shotNo <= 0)
            {
                versionNo = BiggestVersionNo();
            }
            versionNo = VersionNoByShotNo(shotNo);
            tagPathGroup = myMongoDBOperator.DeriveAVersion(versionNo);
            return myExcalOperator.ExportTagPathGroupToDir(tagPathGroup, configDirPath);
        }

        /// <summary>
        /// 在Tag数据库中加入一炮，版本号为最大，成功返回true，失败返回false
        /// </summary>
        /// <param name="shotNo">加入的炮号</param>
        /// <returns></returns>
        [Cfet2Method]
        public bool CreateNewShot(int shotNo)
        {
            try
            {
                shotNoVersionNo = new ShotNoVersionNo();
                shotNoVersionNo.ShotNo = shotNo;
                shotNoVersionNo.VersionNo = BiggestVersionNo();

                myMongoDBOperator.InsertShotNoVersionNo(shotNoVersionNo);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 更新最新炮号
        /// </summary>
        /// <param name="newShotNo">新炮号</param>
        /// <returns></returns>
        [Cfet2Method]
        public bool UploadCurrentShotNo(int newShotNo)
        {
            if(newShotNo < 0)
            {
                return false;
            }
            currentShotNo.No = newShotNo;
            try
            {
                myMongoDBOperator.UploadCurrentShotNo(currentShotNo);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

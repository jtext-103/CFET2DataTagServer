using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jtext103.CFET2.Core;
using Jtext103.CFET2.Core.Attributes;
using MongoDBExcelOperator;
using FormatPathTransfer;

namespace TagManager
{
    public partial class TagManagerThing : Thing
    {
        /// <summary>
        /// 指定的数据库是否存在，如果不存在考虑调用方法初始化
        /// </summary>
        /// <param name="databaseName">数据库名称</param>
        /// <returns></returns>
        [Cfet2Status]
        public bool IsDatabaseExist()
        {
            return myMongoDBOperator.IsDatabaseExist();
        }

        /// <summary>
        /// 通过炮号获取版本号
        /// </summary>
        /// <param name="shotNo"></param>
        /// <returns></returns>
        [Cfet2Status]
        public int VersionNoByShotNo(int shotNo)
        {
            return myMongoDBOperator.GetVersionNoByShot(shotNo);
        }

        /// <summary>
        /// 获取最大的Tag版本号
        /// </summary>
        /// <returns></returns>
        [Cfet2Status]
        public int BiggestVersionNo()
        {
            return myMongoDBOperator.GetBiggestVersionNo();
        }

        /// <summary>
        /// 根据Tag名查询解析后的完整通道路径
        /// </summary>
        /// <param name="tagName"></param>
        /// <returns></returns>
        [Cfet2Status]
        public string RealPath(string tagName, int shotNo = -1)
        {
            if(shotNo == 0)
            {
                shotNo = CurrentShotNo();
            }
            string formatPath = FormatPath(tagName, shotNo);
            return Transfer.TransferFormatPathToRealPath(formatPath, shotNo, Rule.ShotNoLastSubstitution);
        }

        /// <summary>
        /// 根据Tag名查询原始路径
        /// </summary>
        /// <param name="tagName"></param>
        /// <returns></returns>
        [Cfet2Status]
        public string FormatPath(string tagName, int shotNo = -1)
        {
            if (shotNo == 0)
            {
                shotNo = CurrentShotNo();
            }
            return myMongoDBOperator.GetFormatPath(tagName, shotNo);
        }

        /// <summary>
        /// 获取最新炮号
        /// </summary>
        /// <returns></returns>
        [Cfet2Status]
        public int CurrentShotNo()
        {
            return myMongoDBOperator.GetCurrentShotNo();
        }
    }
}

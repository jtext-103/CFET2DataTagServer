using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBExcelOperator
{
    /// <summary>
    /// 包括文件夹和文件
    /// </summary>
    public class TagPath : Entity
    {
        /// <summary>
        /// 文件或文件夹的相对路径（以“/root”开始），如果是文件则带.csv结尾
        /// 也就是说，只支持.csv文件格式
        /// 这里统一分隔符为“/”，具体用到操作时注意替换为操作系统对应分隔符
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 如果是一条Tag记录，给定此项，否则为null
        /// </summary>
        public string TagName { get; set; }

        public string TagFormatPath { get; set; }

        public int VersionNo { get; set; }

        public TagPath()
        {
            Path = null;
            TagName = null;
            VersionNo = 1;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBExcelOperator
{
    /// <summary>
    /// MongoDB数据库中TagPath Collection在本地的映射
    /// </summary>
    public class TagPathGroup
    {
        public List<TagPath> Maps { get; set; }

        public TagPathGroup()
        {
            Maps = new List<TagPath>();
        }
    }
}

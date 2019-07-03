using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBExcelOperator
{
    public class Entity
    {
        public Entity()
        {
            Id = Guid.NewGuid();
            CreateTime = DateTime.Now;
            ExtraInformation = new Dictionary<string, object>();
        }

        public Guid Id { get; set; }

        public DateTime CreateTime { get; set; }

        public Dictionary<string, object> ExtraInformation { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafkaTemplate.Data.Config
{
    public class MongoDbOptions: IMongoDbOptions
    {
        public const string ConfigurationSectionKey = "MongoDb";
        public string DatabaseName { get; set; }
        public string ConnectionString { get; set; }
    }
}

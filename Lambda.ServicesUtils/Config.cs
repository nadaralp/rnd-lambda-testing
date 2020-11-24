using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Lambda.ServicesUtils
{
    public class JsonConfig
    {
        [JsonPropertyName("instancesToModify")]
        public List<string> InstancesToModify { get; set; }
    }
}
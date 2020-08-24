using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SmartLifeNet.API;
using SmartLifeNet.Helpers.Extensions;
using Newtonsoft.Json;

namespace SmartLifeNet.Classes
{
    public class Device
    {
        public string name { get; set; }
        public string icon { get; set; }
        public string id { get; set; }
        public string dev_type { get; set; }
        public string ha_type { get; set; }
       
        public dynamic data { get; set; } = new ExpandoObject();


        public override string ToString() => $"{name} {id}";


        [JsonIgnore]
        public SmartLife context { get; set; }
    }
}

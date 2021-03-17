using Newtonsoft.Json;
using System.Collections.Generic;
using System.Dynamic;

namespace SmartLifeNet.Classes
{
   public class Device
   {
      public string name { get; set; }
      public string icon { get; set; }
      public string id { get; set; }
      public string dev_type { get; set; }
      public string ha_type { get; set; }

      public Data data { get; set; }
      public List<Device> channels { get; set; }

      public override string ToString() => $"{this.name} {this.id}";

      [JsonIgnore]
      public SmartLife context { get; set; }

      public Device()
      {
         this.channels = new List<Device>();
      }
   }

   public class Data
   {
      public bool online { get; set; }
      public bool state { get; set; }
   }
}
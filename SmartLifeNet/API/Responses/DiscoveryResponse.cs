using System.Dynamic;

namespace SmartLifeNet.API.Responses
{
   public class DiscoveryResponse
   {
      public DiscoveryResponsePayload payload { get; set; }

      public DiscoveryResponseHeader header { get; set; }
   }

   public class DiscoveryResponsePayload
   {
      public DiscoveryResponseDevice[] devices { get; set; }
      public dynamic[] scenes { get; set; }
   }

   public class DiscoveryResponseHeader
   {
      public string code { get; set; }
      public string payloadVersion { get; set; }
   }

   public class DiscoveryResponseDevice
   {
      public string name { get; set; }
      public string icon { get; set; }
      public string id { get; set; }
      public string dev_type { get; set; }
      public string ha_type { get; set; }

      public dynamic data { get; set; } = new ExpandoObject();
   }

   public class DiscoveryResponseScenes
   {

   }
}
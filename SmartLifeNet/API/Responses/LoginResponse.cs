using System.Dynamic;

namespace SmartLifeNet.API.Responses
{
   public class LoginResponse
   {
      public string header { get; set; }
      public string apikey { get; set; }
      public string sequence { get; set; }
      public dynamic config { get; set; } = new ExpandoObject();
   }
}
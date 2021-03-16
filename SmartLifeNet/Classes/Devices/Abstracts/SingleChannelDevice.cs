using System.Threading.Tasks;

namespace SmartLifeNet.Classes
{
   public abstract class SingleChannelDevice : Device
   {
      public abstract Task<string> GetState();


      public async Task SetState(int state)
      {
         await API.Rest.SetDeviceSkill(this.context.region, this.id, this.context.Credentials.access_token, state);
      }
   }
}
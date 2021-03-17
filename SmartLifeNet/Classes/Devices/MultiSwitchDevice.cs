using System.Linq;
using System.Threading.Tasks;

namespace SmartLifeNet.Classes
{
   public class MultiSwitchDevice : MultiChannelDevice
   {
      public async Task TurnOff(int? outlet = null)
      {
         if (outlet == null)
         {
            await this.SetAllChannels(0);
         }
         else
         {
            await this.SetChannel((int)outlet, 0);
         }
      }

      public async Task TurnOn(int? outlet = null)
      {
         if (outlet == null)
         {
            await this.SetAllChannels(1);
         }
         else
         {
            await this.SetChannel((int)outlet, 1);
         }
      }

      public async Task Toggle(int outlet)
      {
         if (await this.GetState(outlet) == "on")
         {
            await this.TurnOff(outlet);
         }
         else
         {
            await this.TurnOn(outlet);
         }
      }

      public async Task<string> GetState(int outlet)
      {
         if (outlet >= this.Channels.Count())
         {
            return null;
         }

         return await this.Channels[outlet].GetState();
      }
   }
}
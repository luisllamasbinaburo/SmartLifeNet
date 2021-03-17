using System.Threading.Tasks;

namespace SmartLifeNet.Classes
{
   public class SwitchDevice : SingleChannelDevice
   {
      public async Task TurnOff() => await this.SetState(0);
      public async Task TurnOn() => await this.SetState(1);
      public async Task Toggle()
      {
         if (await this.GetState() == "on")
         {
            await this.TurnOff();
         }
         else
         {
            await this.TurnOn();
         }
      }

      public async override Task<string> GetState() => (bool)this.data.state ? "on" : "off";
   }
}
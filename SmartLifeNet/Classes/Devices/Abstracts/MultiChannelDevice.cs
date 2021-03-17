using System.Threading.Tasks;

namespace SmartLifeNet.Classes
{
   public abstract class MultiChannelDevice : Device
   {
      public virtual SingleChannelDevice[] Channels { get; set; }

      public async Task SetAllChannels(int state)
      {
         foreach (var channel in this.Channels)
         {
            await channel.SetState(state);
         }
      }

      public async Task SetChannel(int outlet, int state)
      {
         await this.Channels[outlet].SetState(state);
      }
   }
}
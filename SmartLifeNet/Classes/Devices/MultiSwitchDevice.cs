using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLifeNet.Classes
{
    public class MultiSwitchDevice : MultiChannelDevice
    {
        public async Task TurnOff(int? outlet = null)
        {
            if (outlet == null) await SetAllChannels(0);
            else await SetChannel((int)outlet, 0);
        }

        public async Task TurnOn(int? outlet = null)
        {
            if (outlet == null) await SetAllChannels(1);
            else await SetChannel((int)outlet, 1);
        }

        public async Task Toggle(int outlet)
        {
            if (await GetState(outlet) == "on") await TurnOff(outlet);
            else await TurnOn(outlet);
        }

        public async Task<string> GetState(int outlet)
        {
            if (outlet >= Channels.Count()) return null;
            return await Channels[outlet].GetState();
        }
    }
}

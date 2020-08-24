using SmartLifeNet.Helpers.Extensions;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLifeNet.Classes
{
    public abstract class MultiChannelDevice : Device
    {
        public virtual SingleChannelDevice[] Channels { get; set; }

        public async Task SetAllChannels(int state)
        {
            foreach (var channel in Channels)
            {
                await channel.SetState(state);
            }
        }

        public async Task SetChannel(int outlet, int state)
        {
            await Channels[outlet].SetState(state);
        }
    }
}

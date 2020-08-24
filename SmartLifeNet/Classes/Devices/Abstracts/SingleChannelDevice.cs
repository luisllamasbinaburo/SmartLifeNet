using SmartLifeNet.Helpers.Extensions;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLifeNet.Classes
{
    public abstract class SingleChannelDevice : Device
    {
        public abstract Task<string> GetState();
        

        public async Task SetState(int state)
        {
            await API.Rest.SetDeviceSkill(context.region, id, context.Credentials.access_token, state);
        }
    }
}

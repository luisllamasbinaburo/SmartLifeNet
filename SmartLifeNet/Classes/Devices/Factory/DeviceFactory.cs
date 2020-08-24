using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartLifeNet.Classes
{
    internal static class DeviceFactory
    {
        internal static Device CreateDevice(SmartLife context, Device device)
        {
            var newDevice = CreateDeviceByDevType(device.dev_type);

            device.Adapt(newDevice);
            newDevice.context = context;

            return newDevice;
        }

        private static Device CreateDeviceByDevType(string dev_type)
        {
            Device newDevice = null;

            if (dev_type != null)
            {
                switch (dev_type)
                {
                    case "switch": newDevice = new SwitchDevice(); break;
                }
            }

            if (newDevice == null) newDevice = new Device();
            return newDevice;
        }


        internal static Device CreateMultiDevice(IGrouping<string, Device> group)
        {
            var newDevice = new MultiSwitchDevice();
            newDevice.id = group.Key;
            newDevice.Channels = group.Cast<SwitchDevice>().ToArray();

            return newDevice;
        }
    }
}

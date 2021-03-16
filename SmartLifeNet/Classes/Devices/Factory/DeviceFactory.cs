using Mapster;
using System.Collections.Generic;
using System.Linq;

namespace SmartLifeNet.Classes
{
   internal static class DeviceFactory
   {
      internal static Device CreateDevice(SmartLife context, Device device)
      {
         var newDevice = CreateDeviceByDevType(device.dev_type, device.channels.Count > 1);

         device.Adapt(newDevice);

         newDevice.context = context;

         return newDevice;
      }

      private static Device CreateDeviceByDevType(string dev_type, bool multiDevice)
      {
         Device newDevice = null;

         if (dev_type != null)
         {
            switch (dev_type)
            {
               case "switch": newDevice = new SwitchDevice(); break;
            }
         }

         if (newDevice == null)
         {
            if (multiDevice)
            {
               newDevice = new MultiSwitchDevice();
            }
            else
            {
               newDevice = new Device();
            }
         }

         return newDevice;
      }

      internal static Device CreateMultiDevice(IGrouping<string, Device> group)
      {
         var newDevice = new MultiSwitchDevice();
         newDevice.id = group.Key;

         try
         {
            newDevice = group.Cast<MultiSwitchDevice>().ToArray().First();
            for (int i = 0; i < newDevice.channels.Count; i++)
            {
               newDevice.channels[i] = CreateDevice(newDevice.context, newDevice.channels[i]);
            }
            newDevice.Channels = newDevice.channels.Select(c => c as SingleChannelDevice).ToArray();
         }
         catch (System.Exception ex)
         {
            newDevice.Channels = group.Cast<SwitchDevice>().ToArray();
         }

         return newDevice;
      }
   }
}
using Mapster;
using Newtonsoft.Json;
using SmartLifeNet.API.Responses;
using SmartLifeNet.Classes;
using SmartLifeNet.Helpers.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartLifeNet
{
   public class SmartLife
   {
      public string region { get; set; }
      public string email { get; set; }
      public string password { get; set; }
      public string at { get; set; }
      public string apikey { get; set; }


      [JsonIgnore]
      public Credentials Credentials { get; private set; }

      [JsonIgnore]
      public Device[] Devices { get; private set; }

      public SmartLife()
      {

      }

      public SmartLife(string email, string password, string region = "EU")
      {
         this.email = email;
         this.password = password;
         this.region = region;
      }

      public async Task<Credentials> Connect()
      {
         if (System.IO.File.Exists("credentials.json"))
         {
            this.RestoreCredenditalsFromFile();
         }

         if (this.Credentials.IsExpired)
         {
            await this.GetCredentials();
            this.StoreCredenditalsToFile();
         }

         return this.Credentials;
      }


      public async Task InitDevices()
      {
         if (System.IO.File.Exists("devices.json"))
         {
            this.RestoreDevicesFromFile();
         }
         else
         {
            await this.GetDevices();
            this.StoreDevicesToFile();
         }
      }


      public async Task<Credentials> GetCredentials()
      {
         var response = await API.Rest.GetCredentials(this.email, this.password, this.region);
         this.Credentials = JsonConvert.DeserializeObject<Credentials>(response);
         return this.Credentials;
      }

      public async Task GetDevices()
      {
         var json = await API.Rest.GetDevices(this.region, this.Credentials.access_token);
         var response = JsonConvert.DeserializeObject<DiscoveryResponse>(json);
         if (response.header.code != Constants.DiscoveryCode.SUCCESS)
         {
            return;
         }

         this.CreateDevices(response.payload.devices.Select(x => x.Adapt<Device>()).ToArray());
      }

      private void CreateDevices(Device[] devices)
      {
         var all = devices.Select(x => DeviceFactory.CreateDevice(this, x)).ToArray();

         var grouped = all.ToLookup(x => x.id.Split('_').First());
         this.Devices = grouped.Where(x => x.Count() == 1 && x.First() is not MultiSwitchDevice).Select(x => x.First())
                .Concat(
                    grouped.Where(x => x.Count() > 1 || x.First().channels.Count > 1).Select(x => DeviceFactory.CreateMultiDevice(x))
                    )
                .ToArray();

         this.deviceCache = all.ToDictionary(x => x.id);
      }

      private Dictionary<string, Device> deviceCache;


      public void StoreCredenditalsToFile(string filename = "credentials.json") => System.IO.File.WriteAllText(filename, this.Credentials.AsJson());
      public void RestoreCredenditalsFromFile(string filename = "credentials.json") => this.Credentials = System.IO.File.ReadAllText(filename).FromJson<Credentials>();

      public void StoreDevicesToFile(string filename = "devices.json") => System.IO.File.WriteAllText(filename, this.Devices.AsJson());
      public void RestoreDevicesFromFile(string filename = "devices.json") => this.CreateDevices(JsonConvert.DeserializeObject<Device[]>(System.IO.File.ReadAllText(filename)));

   }
}
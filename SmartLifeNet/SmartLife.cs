using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using SmartLifeNet.Classes;
using SmartLifeNet.Helpers.Extensions;
using SmartLifeNet.API.Responses;
using System.Linq;
using System.Collections.Generic;
using Mapster;

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


        public SmartLife(string email, string password, string region = "EU")
        {
            this.email = email;
            this.password = password;
            this.region = region;
        }

        public async Task<Credentials> Connect()
        {
            if (System.IO.File.Exists("credentials.json"))
                RestoreCredenditalsFromFile();

            if (Credentials.IsExpired)
            {
                await GetCredentials();
                StoreCredenditalsToFile();
            }

            return Credentials;
        }


        public async Task InitDevices()
        {
            if (System.IO.File.Exists("devices.json"))
            {
                RestoreDevicesFromFile();
            }
            else
            {
                await GetDevices();
                StoreDevicesToFile();
            }
        }


        public async Task<Credentials> GetCredentials()
        {
            var response = await API.Rest.GetCredentials(email, password, region);
            Credentials = JsonConvert.DeserializeObject<Credentials>(response);
            return Credentials;
        }

        public async Task GetDevices()
        {
            var json = await API.Rest.GetDevices(region, Credentials.access_token);
            var response = JsonConvert.DeserializeObject<DiscoveryResponse>(json);
            if (response.header.code != Constants.DiscoveryCode.SUCCESS) return;

            CreateDevices(response.payload.devices.Select(x => x.Adapt<Device>()).ToArray());
        }

        private void CreateDevices(Device[] devices)
        {
            var all = devices.Select(x => DeviceFactory.CreateDevice(this, x)).ToArray();

            var grouped = all.ToLookup(x => x.id.Split('_').First());
            Devices = grouped.Where(x => x.Count() == 1).Select(x => x.First())
                .Concat(
                    grouped.Where(x => x.Count() > 1).Select(x => DeviceFactory.CreateMultiDevice(x))
                    )
                .ToArray();

            deviceCache = all.ToDictionary(x => x.id);
        }

        private Dictionary<string, Device> deviceCache;


        public void StoreCredenditalsToFile(string filename = "credentials.json") => System.IO.File.WriteAllText(filename, Credentials.AsJson());
        public void RestoreCredenditalsFromFile(string filename = "credentials.json") => Credentials = System.IO.File.ReadAllText(filename).FromJson<Credentials>();

        public void StoreDevicesToFile(string filename = "devices.json") => System.IO.File.WriteAllText(filename, Devices.AsJson());
        public void RestoreDevicesFromFile(string filename = "devices.json") => CreateDevices(JsonConvert.DeserializeObject<Device[]>(System.IO.File.ReadAllText(filename)));

    }
}

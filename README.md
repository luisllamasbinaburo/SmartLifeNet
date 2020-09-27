# SmartLifeNet
SmartLifeNet is an API writen in .NET Standard to interact directly with SmartLifeNet API using your regular credentials.
Compatible with Windows, Linux and MAC.


### Key features
- Set on/off devices
- Compatible with Windows, Linux and MAC.


## Basic usage

```c#
var smart = new SmartLife(email, password);
await smart.Connect();
await smart.InitDevices();

var device = smart.Devices.FirstOrDefault(x => x is SmartLifeNet.Classes.SwitchDevice) as SmartLifeNet.Classes.SwitchDevice;
await device?.SetState(1);
```

### Get Credentials

Use Email, Password and Region to get SmartLife credentials, that includes required information to perform actions.
```c#
var smart = new SmartLife(email, password);
var credentials = await smart.GetCredentials();
```

Alternately, you can save credentials to avoid login.
```c#
smart.StoreCredenditalsToFile();
```

And later restore with,
```c#
smart.RestoreCredenditalsFromFile();
```

### Get Devices

Get Devices registered in you SmartLife account.
```c#
var smart = new SmartLife(email, password);
await smart.Connect();
await smart.InitDevices();
```

Devices are converted to one of the following classes.
- SwitchDevice
- MultiSwitchDevice

All of them are derived classes of generic `Device` class.

### Interact with devices

Each class provides there own methods to perform actions or retrieve measurement.
For example, `ThermostatDevice` provides
- TurnOn()
- TurnOff()

And, `MultiSwitchDevice` provides
- TurnOn()
- TurnOn(int channel)
- TurnOff()
- TurnOff(int channel)


## Todo
- [x] Get credentials
- [x] Get devices
- [x] Set on/off
- [x] Get measurements
- [ ] Add/test more devices
- [ ] Improve documentation/examples
- [ ] Improve tests

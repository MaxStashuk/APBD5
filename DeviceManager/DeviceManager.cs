using System.Text;
using DeviceManager.Devices;
using DeviceManager.Interfaces;
using DeviceManager.Utilities;

namespace DeviceManager;

public class DeviceManager
{
    private IRepository repository;
    private const int MaxCapacity = 15;
    private List<Device> _devices = new(capacity: MaxCapacity);

    public DeviceManager(IRepository repository)
    {
        this.repository = repository;
        _devices = this.repository.GetDevices().Take(MaxCapacity).ToList();
    }

    public void AddDevice(Device newDevice)
    {
        foreach (var storedDevice in _devices)
        {
            if (storedDevice.Id.Equals(newDevice.Id))
            {
                throw new ArgumentException($"Device with ID {storedDevice.Id} is already stored.", nameof(newDevice));
            }
        }

        if (_devices.Count >= MaxCapacity)
        {
            throw new Exception("Device storage is full.");
        }
        
        _devices.Add(newDevice);
        repository.saveDevices(_devices);
    }

    public void EditDevice(Device device)
    {
        var targetDeviceIndex = -1;
        for (var index = 0; index < _devices.Count; index++)
        {
            var storedDevice = _devices[index];
            if (storedDevice.Id.Equals(device.Id))
            {
                targetDeviceIndex = index;
                break;
            }
        }

        if (targetDeviceIndex == -1)
        {
            throw new ArgumentException($"Device with ID {device.Id} is not stored.", nameof(device));
        }

        _devices[targetDeviceIndex].EditDevice(device);
    }

    public void RemoveDeviceById(string deviceId)
    {
        Device? targetDevice = null;
        foreach (var storedDevice in _devices)
        {
            if (storedDevice.Id.Equals(deviceId))
            {
                targetDevice = storedDevice;
                break;
            }
        }

        if (targetDevice == null)
        {
            throw new ArgumentException($"Device with ID {deviceId} is not stored.", nameof(deviceId));
        }
        
        _devices.Remove(targetDevice);
    }

    public void TurnOnDevice(string id)
    {
        foreach (var storedDevice in _devices)
        {
            if (storedDevice.Id.Equals(id))
            {
                storedDevice.TurnOn();
                return;
            }
        }
        
        throw new ArgumentException($"Device with ID {id} is not stored.", nameof(id));
    }

    public void TurnOffDevice(string id)
    {
        foreach (var storedDevice in _devices)
        {
            if (storedDevice.Id.Equals(id))
            {
                storedDevice.TurnOff();
                return;
            }
        }
        
        throw new ArgumentException($"Device with ID {id} is not stored.", nameof(id));
    }

    public Device? GetDeviceById(string id)
    {
        foreach (var storedDevice in _devices)
        {
            if (storedDevice.Id.Equals(id))
            {
                return storedDevice;
            }
        }

        return null;
    }

    public void ShowAllDevices()
    {
        foreach (var storedDevices in _devices)
        {
            Console.WriteLine(storedDevices.ToString());
        }
    }
}
﻿namespace DeviceManager.Devices;

public abstract class Device
{
    public string Id { get; set; }
    public string Name { get; set; }
    public bool IsEnabled { get; set; }

    public Device(string id, string name, bool isEnabled)
    {
        Id = id;
        Name = name;
        IsEnabled = isEnabled;
    }

    public virtual void TurnOn()
    {
        IsEnabled = true;
    }

    public virtual void TurnOff()
    {
        IsEnabled = false;
    }

    public abstract String ToFile();
    
    public abstract void EditDevice(Device device);
}
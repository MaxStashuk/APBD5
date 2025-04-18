﻿using DeviceManager.Exceptions;

namespace DeviceManager.Devices;

public class PersonalComputer : Device
{
    public string? OperatingSystem { get; set; }
    
    public PersonalComputer(string id, string name, bool isEnabled, string? operatingSystem) : base(id, name, isEnabled)
    {
        if (!CheckId(id))
        {
            throw new ArgumentException("Invalid ID value. Required format: P-1", id);
        }
        
        OperatingSystem = operatingSystem;
    }
    
    public PersonalComputer() : base("", "", false){}

    public override void TurnOn()
    {
        if (OperatingSystem is null)
        {
            throw new EmptySystemException();
        }

        base.TurnOn();
    }

    public override string ToString()
    {
        string enabledStatus = IsEnabled ? "enabled" : "disabled";
        string osStatus = OperatingSystem is null ? "has not OS" : $"has {OperatingSystem}";
        return $"PC {Name} ({Id}) is {enabledStatus} and {osStatus}";
    }
    
    private bool CheckId(string id) => id.Contains("P-");

    public override String ToFile()
    {
        return ($"{this.Id},{this.Name}," +
                $"{this.IsEnabled},{this.OperatingSystem}");
    }
    
    public override void EditDevice(Device device)
    {
        if (device is not PersonalComputer)
        {
            throw new ArgumentException("Device is not Embedded");
        }
        if (device.Id != this.Id)
        {
            throw new ArgumentException("Trying to modify different device");
        }
        this.Id = device.Id;
        this.Name = device.Name;
    }
}
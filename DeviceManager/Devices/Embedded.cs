using System.Text.RegularExpressions;
using DeviceManager.Exceptions;

namespace DeviceManager.Devices;

class Embedded : Device
{
    public string NetworkName { get; set; }
    private string _ipAddress;
    private bool _isConnected = false;

    public string IpAddress
    {
        get => _ipAddress;
        set
        {
            Regex ipRegex = new Regex("^((25[0-5]|(2[0-4]|1\\d|[1-9]|)\\d)\\.?\\b){4}$");
            if (ipRegex.IsMatch(value))
            {
                _ipAddress = value;
            }

            throw new ArgumentException("Wrong IP address format.");
        }
    }
    
    public Embedded(string id, string name, bool isEnabled, string ipAddress, string networkName) : base(id, name, isEnabled)
    {
        if (CheckId(id))
        {
            throw new ArgumentException("Invalid ID value. Required format: E-1", id);
        }

        IpAddress = ipAddress;
        NetworkName = networkName;
    }

    public override void TurnOn()
    {
        Connect();
        base.TurnOn();
    }

    public override void TurnOff()
    {
        _isConnected = false;
        base.TurnOff();
    }

    public override string ToString()
    {
        string enabledStatus = IsEnabled ? "enabled" : "disabled";
        return $"Embedded device {Name} ({Id}) is {enabledStatus} and has IP address {IpAddress}";
    }

    private void Connect()
    {
        if (NetworkName.Contains("MD Ltd."))
        {
            _isConnected = true;
        }
        else
        {
            throw new ConnectionException();
        }
    }
    
    private bool CheckId(string id) => id.Contains("E-");

    public override String ToFile()
    {
        return ($"{this.Id},{this.Name}," +
                $"{this.IsEnabled},{this.IpAddress}," +
                $"{this.NetworkName}");
    }

    public override void EditDevice(Device device)
    {
        if (device is not Embedded)
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
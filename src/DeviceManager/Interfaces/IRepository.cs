using DeviceManager.Devices;

namespace DeviceManager.Interfaces;

public interface IRepository
{
    IEnumerable<Device> GetDevices();
    
    void saveDevices(IEnumerable<Device> devices);
}
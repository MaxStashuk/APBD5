using DeviceManager.Interfaces;
using DeviceManager.Utilities;

namespace DeviceManager.Factories;

public static class DeviceManagerFactory
{
    public static DeviceManager CreateDeviceManager(string filePath)
    {
        IRepository repository = new FileService(filePath);
        DeviceManager deviceManager = new DeviceManager(repository);
        return deviceManager;
    }
}
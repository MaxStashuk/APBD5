using System.Text;
using DeviceManager.Devices;
using DeviceManager.Interfaces;

namespace DeviceManager.Utilities;

public class FileService : IRepository
{
    private string _filePath;

    public FileService(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("The input device file could not be found.");
        }
        
        _filePath = filePath;
    }


    public IEnumerable<Device> GetDevices()
    {
        var devices = new List<Device>();

        var lines = File.ReadAllLines(_filePath);

        for (int i = 0; i < lines.Length; i++)
        {
            var line = lines[i];
            Device? parsed = ParseDevice(line, i);
            if(parsed != null)
                devices.Add(parsed);
        }
        return devices;
    }

    public void saveDevices(IEnumerable<Device> devices)
    {
        throw new NotImplementedException();
    }
    
    private Device? ParseDevice(string line, int lineNumber)
    {
        Device parsedDevice = null;
        try
        {
            if (line.StartsWith("P-"))
            {
                parsedDevice = DeviceParser.ParsePC(line, lineNumber);
            }
            else if (line.StartsWith("SW-"))
            {
                parsedDevice = DeviceParser.ParseSmartwatch(line, lineNumber);
            }
            else if (line.StartsWith("ED-"))
            {
                parsedDevice = DeviceParser.ParseEmbedded(line, lineNumber);
            }
            else
            {
                throw new ArgumentException($"Line {lineNumber} is corrupted.");
            }
        }
        catch (ArgumentException argEx)
        {
            Console.WriteLine(argEx.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Something went wrong during parsing this line: {line}. The exception message: {ex.Message}");
        }
        return parsedDevice;
    }
    
    public void SaveDevices(List<Device> devices)
    {
        StringBuilder devicesSb = new();

        foreach (var storedDevice in devices)
        {
            devicesSb.AppendLine(storedDevice.ToString());
        }
        
        File.WriteAllLines(_filePath, devicesSb.ToString().Split('\n'));
    }
    
}
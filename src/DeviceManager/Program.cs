﻿using DeviceManager.Devices;
using DeviceManager.Exceptions;
using DeviceManager.Factories;

namespace DeviceManager;

class Program
{
    public static void Main()
    {
        try
        {
            DeviceManager deviceManager = DeviceManagerFactory.CreateDeviceManager("input.txt");
            
            Console.WriteLine("Devices presented after file read.");
            deviceManager.ShowAllDevices();
            
            Console.WriteLine("Create new computer with correct data and add it to device store.");
            {
                PersonalComputer computer = new("P-2", "ThinkPad T440", false, null);
                deviceManager.AddDevice(computer);
            }
            
            Console.WriteLine("Let's try to enable this PC");
            try
            {
                deviceManager.TurnOnDevice("P-2");
            }
            catch (EmptySystemException ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            Console.WriteLine("Let's install OS for this PC");
            
            PersonalComputer editComputer = new("P-2", "ThinkPad T440", true, "Arch Linux");
            deviceManager.EditDevice(editComputer);
            
            Console.WriteLine("Let's try to enable this PC");
            deviceManager.TurnOnDevice("P-2");
            
            Console.WriteLine("Let's turn off this PC");
            deviceManager.TurnOffDevice("P-2");
            
            Console.WriteLine("Delete this PC");
            deviceManager.RemoveDeviceById("P-2");
            
            Console.WriteLine("Devices presented after all operations.");
            deviceManager.ShowAllDevices();
            deviceManager.SaveDevices();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
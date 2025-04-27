-- Seeding
INSERT INTO Device (Id, Name, IsEnabled) VALUES
    ('SW-02', 'Pulse Tracker', 1),
    ('PC-02', 'Office Desktop', 1),
    ('EM-02', 'Weather Station', 1);
GO

INSERT INTO Smartwatch (BatteryPercentage, DeviceId) VALUES
    (92, 'SW-02');
GO

INSERT INTO PersonalComputer (OperationSystem, DeviceId) VALUES
    ('Ubuntu 22.04', 'PC-02');
GO

INSERT INTO Embedded (IpAddress, NetworkName, DeviceId) VALUES
    ('10.0.0.5', 'WeatherNet', 'EM-02');
GO

USE DevicesDataBase
SELECT * FROM Device;

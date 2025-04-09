using DeviceManager.Devices;
using DeviceManager.Factories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

DeviceManager.DeviceManager deviceManager = DeviceManagerFactory.CreateDeviceManager("input.txt");

var devices = deviceManager.GetDevices();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/api/devices", () =>
{
    devices = deviceManager.GetDevices();
    return Results.Ok(devices);
});
app.MapGet("/api/devices/{id}", (string id) =>
{
    var device = deviceManager.GetDeviceById(id);
    return Results.Ok(device);
});

app.MapPost("/api/devices/embedded", (DeviceManager.Devices.Embedded device) =>
{
    if (deviceManager.GetDeviceById(device.Id) != null)
    {
        return Results.Problem(detail: "Already have device with this ID");
    }

    deviceManager.AddDevice(device);
    deviceManager.SaveDevices();
    devices = deviceManager.GetDevices();
    
    return Results.Ok();

});

app.MapPost("/api/devices/smartwatch", (DeviceManager.Devices.Smartwatch device) =>
{
    if (deviceManager.GetDeviceById(device.Id) != null)
    {
        return Results.Problem(detail: "Already have device with this ID");
    }

    deviceManager.AddDevice(device);
    deviceManager.SaveDevices();
    devices = deviceManager.GetDevices();
    
    return Results.Ok();

});

app.MapPost("/api/devices/pc", (DeviceManager.Devices.PersonalComputer device) =>
{
    if (deviceManager.GetDeviceById(device.Id) != null)
    {
        return Results.Problem(detail: "Already have device with this ID");
    }

    deviceManager.AddDevice(device);
    deviceManager.SaveDevices();
    devices = deviceManager.GetDevices();
    
    return Results.Ok();

});

app.MapPut("/api/devices/embedded", (Embedded device) =>
{   
    deviceManager.EditDevice(device);
    devices = deviceManager.GetDevices();
    return Results.Ok();
});

app.MapPut("/api/devices/smartwatch", (Smartwatch device) =>
{   
    deviceManager.EditDevice(device);
    devices = deviceManager.GetDevices();
    return Results.Ok();
});

app.MapPut("/api/devices/pc", (PersonalComputer device) =>
{   
    deviceManager.EditDevice(device);
    devices = deviceManager.GetDevices();
    return Results.Ok();
});

app.MapDelete("/api/devices/{id}", (string id) =>
{
    var device = deviceManager.GetDeviceById(id);
    if (device == null)
    {
        return Results.Problem(detail: "Device not found");
    }

    deviceManager.RemoveDeviceById(device.Id);
    return Results.Ok();
});

app.Run();

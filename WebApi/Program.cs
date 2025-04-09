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

app.MapPost("/api/devices/", (Device device) =>
{
    if (deviceManager.GetDeviceById(device.Id) != null)
    {
        deviceManager.AddDevice(device);
        devices = deviceManager.GetDevices();
    }
    else
    {
        return Results.Problem(detail:"Already have device with this ID");
    }
    return Results.Ok();
});

app.MapPut("/api/devices/", (Device device) =>
{
    deviceManager.EditDevice(device);
    devices = deviceManager.GetDevices();
    return Results.Ok();
});

app.Map

app.Run();

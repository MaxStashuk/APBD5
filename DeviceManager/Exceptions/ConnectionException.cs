namespace DeviceManager.Exceptions;

class ConnectionException : Exception
{
    public ConnectionException() : base("Wrong netowrk name.") { }
}
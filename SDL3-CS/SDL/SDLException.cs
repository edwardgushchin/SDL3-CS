namespace SDL3;

public class SDLException : Exception
{
    public SDLException() {}
    
    public SDLException(string message) : base(message) {}
    
    public SDLException(string message, Exception innerException) : base(message, innerException) {}
}
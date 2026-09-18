

namespace BuildBlocks.Exceptions;
public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message)
    {
    }

    public NotFoundException(string message, Exception innerException) : base($"Entity \"{message}\" ({innerException}) was not found.", innerException )
    {
    }
}

// Necessary usings.
using FluentResults;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.ResultVariations
{
    /// <summary>
    /// Null result class.
    /// </summary>
    /// <typeparam name="T">
    /// Generic for null result.
    /// </typeparam>
    public class NullResult<T> : Result<T>
    {
        public NullResult()
            : base()
        {
        }
    }
}

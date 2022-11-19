using System;
namespace SD.Models.Responses.Failures
{
    public interface IOperationFailure
    {
        string PropertyName { get; }

        string Description { get; }

        string Code { get; }
    }
}


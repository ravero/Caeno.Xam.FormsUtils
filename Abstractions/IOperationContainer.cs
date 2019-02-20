using System;

namespace FormsUtils.Abstractions
{
    /// <summary>
    /// An Interface for an operation container.
    /// </summary>
    public interface IOperationContainer
    {
        void StartProcessing();

        void StopProcessing();
    }
}
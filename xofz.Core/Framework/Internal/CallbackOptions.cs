namespace xofz.Framework.Internal
{
    using System;

    [Flags]
    internal enum CallbackOptions : uint
    {
        QueueToWorkerThread = 0x0,
        ExecuteOnlyOnce = 0x8, // if using this, set intervalInMs to 0 also
        ExecutionTakesALongTime = 0x10,
        ExecuteInTimerThread = 0x20,
        ExecuteInPersistentThread = 0x80,
        UseCurrentAccessToken = 0x100
    }
}

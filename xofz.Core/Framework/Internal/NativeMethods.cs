// ------------------------------------------------------------------------------------------------
// <copyright file="NativeMethods.cs" company="Care Controls">
//   Copyright (c) Care Controls Inc. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace xofz.Framework.Internal
{
    using System;
    using System.Runtime.InteropServices;

    internal static class NativeMethods
    {
        [DllImport("kernel32.dll")]
        public static extern bool CreateTimerQueueTimer(
            out IntPtr handle,
            IntPtr queueHandle,
            TimerCallback callback,
            IntPtr callbackParameter,
            uint msBeforeFirstCall,
            uint intervalInMs, // if zero, timer is signaled only once
            CallbackOptions callbackOptions);

        [DllImport("kernel32.dll")]
        public static extern bool DeleteTimerQueueTimer(
            IntPtr timerQueueHandle,
            IntPtr timerHandle,
            IntPtr completionEventHandle);
    }
}

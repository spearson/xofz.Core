namespace xofz.UI
{
    using System;

    public interface VncUi : Ui
    {
        event Action ConnectionLost;

        event Action<string> ErrorConnecting;

        void Connect(
            string hostName,
            string password,
            bool scaleToWindowSize,
            int port = 5900,
            bool viewOnly = false);

        void Disconnect();
    }
}

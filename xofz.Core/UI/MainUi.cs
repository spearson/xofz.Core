namespace xofz.UI
{
    using System;

    public interface MainUi : ShellUi
    {
        event Action ShutdownRequested;
    }
}

namespace xofz.Framework
{
    using System.Diagnostics;

    public class KeyboardLoader
    {
        public virtual void Load()
        {
            new Process
            {
                StartInfo = new ProcessStartInfo(
                    @"c:\windows\system32\osk.exe")
            }.Start();
        }
    }
}

namespace xofz.Framework.IO
{
    public class TcpIpIOBitterSettings
    {
        public virtual string IPAddressOrHostName { get; set; }

        public virtual int Port { get; set; }

        public virtual long ReadBufferLength { get; set; }

        public virtual int ReadTimeoutMilliseconds { get; set; }

        public virtual int WriteTimeoutMilliseconds { get; set; }

        public virtual int ConnectTimeoutMilliseconds { get; set; }
    }
}

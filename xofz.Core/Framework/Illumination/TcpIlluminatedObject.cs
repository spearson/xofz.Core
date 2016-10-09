namespace xofz.Framework.Illumination
{
    using System.Net;
    using System.Net.Sockets;
    using System.Threading.Tasks;

    public sealed class TcpIlluminatedObject : IlluminatedObject
    {
        public TcpIlluminatedObject(TcpListener listener, int port)
            : base(new object[] { listener, port })
        {
            this.listener = listener;
            this.port = port;
            this.setClient(new TcpClient());
        }

        public TcpClient Client => this.client;

        public async Task Connect()
        {
#pragma warning disable 4014
            this.client.ConnectAsync(
#pragma warning restore 4014
                IPAddress.Parse(((IPEndPoint)this.listener.LocalEndpoint).Address.ToString()),
                this.port); 
            await this.listener.AcceptTcpClientAsync();
        }

        public void Disconnect()
        {
            this.client.Close();
            this.setClient(new TcpClient());
        }

        private void setClient(TcpClient client)
        {
            this.client = client;
        }

        private TcpClient client;
        private readonly TcpListener listener;
        private readonly int port;
    }
}

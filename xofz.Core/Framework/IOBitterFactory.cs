namespace xofz.Framework
{
    using xofz.Framework.IO;
    using xofz.Framework.Materialization;

    public class IOBitterFactory
    {
        public IOBitterFactory(MethodWeb web)
        {
            this.web = web;
        }

        public virtual IOBitter NewFile(
            string filePath,
            string bitterName)
        {
            var fileBitter = new FileIOBitter(
                this.web);
            IOBitter bitter = fileBitter;
            bitter.Name = bitterName;
            fileBitter.Setup(filePath);

            return fileBitter;
        }

        public virtual IOBitter NewTcpIp(
            string host,
            int port,
            string bitterName)
        {
            var tcpBitter = new TcpIpIOBitter(
                    this.web);
            IOBitter bitter = tcpBitter;
            bitter.Name = bitterName;
            tcpBitter.Setup(host, port);

            return tcpBitter;
        }

        public virtual IOBitter NewMaterializedEnumerable(
            IOBitter bitter)
        {
            return new MaterializedEnumerableIOBitter(
                new LinkedListMaterializer(),
                bitter);
        }

        private readonly MethodWeb web;
    }
}

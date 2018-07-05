namespace xofz.Misc.Framework
{
    using xofz.Framework;
    using xofz.Framework.Materialization;
    using xofz.Misc.Framework.IO;

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

            return bitter;
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

            return bitter;
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

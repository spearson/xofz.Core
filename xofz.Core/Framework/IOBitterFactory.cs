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
            var bitter = new FileIOBitter(
                this.web);
            IOBitter toName = bitter;
            toName.Name = bitterName;
            bitter.Setup(filePath);

            return bitter;
        }

        public virtual IOBitter NewTcpIp(
            string host,
            int port,
            string bitterName)
        {
            var bitter = new TcpIpIOBitter(
                    this.web);
            IOBitter toName = bitter;
            toName.Name = bitterName;
            bitter.Setup(host, port);

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

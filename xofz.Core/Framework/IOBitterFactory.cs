namespace xofz.Framework
{
    using xofz.Framework.IO;

    // this class will serve a purpose when there 
    // is more than one IOBitter implementation
    public class IOBitterFactory
    {
        public IOBitterFactory(MethodWeb web)
        {
            this.web = web;
        }

        public virtual IOBitter NewFile()
        {
            return new FileIOBitter(this.web);
        }

        private readonly MethodWeb web;
    }
}

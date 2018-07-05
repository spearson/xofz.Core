namespace xofz.Misc.Framework.IO
{
    using System.Collections.Generic;

    public sealed class MaterializedEnumerableIOBitter : IOBitter
    {
        public MaterializedEnumerableIOBitter(
            Materializer materializer,
            IOBitter bitter)
        {
            this.materializer = materializer;
            this.bitter = bitter;
        }

        string IOBitter.Name { get; set; }

        IEnumerable<bool> IOBitter.Read()
        {
            return this.materializer.Materialize(
                this.bitter.Read());
        }

        void IOBitter.Write(
            IEnumerable<bool> bits, 
            out bool succeeded)
        {
            this.bitter.Write(
                this.materializer.Materialize(bits),
                out succeeded);
        }

        private readonly Materializer materializer;
        private readonly IOBitter bitter;
    }
}

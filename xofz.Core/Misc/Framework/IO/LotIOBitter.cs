namespace xofz.Misc.Framework.IO
{
    using System.Collections.Generic;

    public sealed class LotIOBitter 
        : IOBitter
    {
        public LotIOBitter(
            Lotter lotter,
            IOBitter bitter)
        {
            this.lotter = lotter;
            this.bitter = bitter;
        }

        public string Name { get; set; }

        IEnumerable<bool> IOBitter.Read()
        {
            return this.lotter.Materialize(
                this.bitter.Read());
        }

        void IOBitter.Write(
            IEnumerable<bool> bits, 
            out bool succeeded)
        {
            this.bitter.Write(
                this.lotter.Materialize(bits),
                out succeeded);
        }

        private readonly Lotter lotter;
        private readonly IOBitter bitter;
    }
}

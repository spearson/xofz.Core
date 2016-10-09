namespace xofz.Framework.Illumination
{
    public sealed class StringIlluminatedObject : IlluminatedObject
    {
        public StringIlluminatedObject(string s) : base(new object[] {s})
        {
            this.Value = s;
        }

        public string Value { get; }
    }
}

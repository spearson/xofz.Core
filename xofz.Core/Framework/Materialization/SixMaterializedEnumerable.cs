namespace xofz.Framework.Materialization
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public sealed class SixMaterializedEnumerable<T> : MaterializedEnumerable<T>
    {
        public SixMaterializedEnumerable(
            MaterializedEnumerable<T> one,
            MaterializedEnumerable<T> two,
            MaterializedEnumerable<T> three,
            MaterializedEnumerable<T> four,
            MaterializedEnumerable<T> five,
            MaterializedEnumerable<T> six)
        {
            this.one = one;
            this.two = two;
            this.three = three;
            this.four = four;
            this.five = five;
            this.six = six;
        }

        long MaterializedEnumerable<T>.Count => 6;

        public IEnumerator<T> GetEnumerator()
        {
            yield return this.one.GetEnumerator().Current;
            yield return this.two.GetEnumerator().Current;
            yield return this.three.GetEnumerator().Current;
            yield return this.four.GetEnumerator().Current;
            yield return this.five.GetEnumerator().Current;
            yield return this.six.GetEnumerator().Current;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public bool Contains(T item)
        {
            var array = new T[6];
            this.CopyTo(array);

            return new Collection<T>(array).Contains(item);
        }

        public void CopyTo(T[] array)
        {
            array[0] = this.one.GetEnumerator().Current;
            array[1] = this.two.GetEnumerator().Current;
            array[2] = this.three.GetEnumerator().Current;
            array[3] = this.four.GetEnumerator().Current;
            array[4] = this.five.GetEnumerator().Current;
            array[5] = this.six.GetEnumerator().Current;
        }

        private readonly MaterializedEnumerable<T> one, two, three, four, five, six;
    }
}

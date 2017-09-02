namespace xofz.Misc.Framework.Computation
{
    using System.Collections.Generic;
    using System.Linq;

    public class ShiftRegister
    {
        public ShiftRegister(int capacity)
        {
            this.capacity = capacity;
            this.shiftList = new LinkedList<bool>();
        }

        public bool this[int index] => this.currentArray[index];

        public int CurrentSize => this.shiftList.Count;

        public void ShiftLeft(bool input)
        {
            var linkedList = this.shiftList;
            linkedList.AddLast(input);

            if (linkedList.Count > this.capacity)
            {
                linkedList.RemoveFirst();
            }

            this.setCurrentArray(linkedList.ToArray());
        }

        public void ShiftRight(bool input)
        {
            var linkedList = this.shiftList;
            linkedList.AddFirst(input);

            if (linkedList.Count > this.capacity)
            {
                linkedList.RemoveLast();
            }

            this.setCurrentArray(linkedList.ToArray());
        }

        private void setCurrentArray(bool[] currentArray)
        {
            this.currentArray = currentArray;
        }

        private bool[] currentArray;
        private readonly int capacity;
        private readonly LinkedList<bool> shiftList;
    }
}

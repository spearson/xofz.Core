namespace xofz.Framework.Computation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class PrimeTester
    {
        public virtual bool RelativelyPrime(IEnumerable<long> numbers, bool onlyCheckLast)
        {
            LinkedList<long> orderedLinkedList;
            if (onlyCheckLast)
            {
                orderedLinkedList = new LinkedList<long>(numbers);
                var numberToCheck = orderedLinkedList.Last.Value;
                var squareRoot = (long)Math.Sqrt(numberToCheck);
                
                return new LinkedList<long>(orderedLinkedList.Where(number => number <= squareRoot))
                    .All(number => numberToCheck % number > 0);
            }

            orderedLinkedList = new LinkedList<long>(numbers);
            while (true)
            {
                var lowestNumber = orderedLinkedList.First.Value;
                orderedLinkedList.RemoveFirst();
                if (orderedLinkedList.Count == 0)
                {
                    return true;
                }

                if (orderedLinkedList.Any(number => number%lowestNumber == 0))
                {
                    return false;
                }
            }
        }

        public class Tester
        {
            public void TestSomeSets()
            {
                var primeTester = new PrimeTester();
                Console.WriteLine(primeTester.RelativelyPrime(new long[] { 3, 4, 5 }, false)); // should be true
                Console.WriteLine(primeTester.RelativelyPrime(new long[] { 3, 4, 25 }, false)); // should be true
                Console.WriteLine(primeTester.RelativelyPrime(new long[] { 3, 5, 25 }, false)); // should be false
                Console.WriteLine(primeTester.RelativelyPrime(new long[] { 3, 9, 25 }, false)); // should be false
                Console.WriteLine(primeTester.RelativelyPrime(new long[] { 7, 13, 17, 25, 75 }, false)); // should be false!
                Console.WriteLine(primeTester.RelativelyPrime(new long[] { 2, 7, 13, 23, 25, 27, 29, 31, 33 }, false)); // should be true! (i think)
            }
        }
    }
}

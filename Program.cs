using System;
using static InterviewPractice.BinarySearch;

namespace InterviewPractice
{
    class Program {
        private const int Count = 10;
        static void Main(string[] args) {

            Console.WriteLine("Part 1:");
            #region part1
            int[] search = new int[Count] {-1, 2, 5, 6, 10, 15, 18, 20, 3, 50};
            int[] array = new int[Count] { 2, 4, 6, 8, 10, 12, 14, 16, 18, 20 };
            int[] arrayRev = new int[Count] { 20, 18, 16, 14, 12, 10, 8, 6, 4, 2 };
            Console.Write("    Array: ");
            for(int i = 0; i < Count; i++) Console.Write("{0} ", array[i]);
            Console.WriteLine();
            for (int i = 0; i < 10; i++) Console.WriteLine("    Number: {0}\tPosition: {1}", search[i], Search(search[i], array) ?? -1);
            for (int i = 0; i < 10; i++) Console.WriteLine("    Number: {0}\tPosition: {1}", search[i], Search(search[i], arrayRev, false) ?? -1);
            #endregion
            Console.WriteLine("Part 2:");
            #region part2
            BidirectionalList list = new BidirectionalList();
            foreach(int number in array) {
                if(number%4 == 0)
                    list.AddToBegin(number);
                else
                    list.AddToEnd(number);
                Console.Write("    ");
                foreach(long value in list) {
                    Console.Write("{0} ", value);
                }
                Console.WriteLine();
            }

            foreach(int number in array) {
                list.Remove(number);
                Console.Write("    ");
                foreach (long value in list)
                {
                    Console.Write("{0} ", value);
                }
                Console.WriteLine();
            }
            #endregion
            Console.WriteLine("Part 3:");
            #region part3

            #endregion

            Console.ReadKey();
        }
    }
}

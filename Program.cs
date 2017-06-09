using System;
using static InterviewPractice.BinarySearch;

namespace InterviewPractice {
    internal class Program {
        private const int Count = 10;

        private static void Main(string[] args) {
            Console.WriteLine("Part 1:");

            #region part1

            var search = new int[Count] {-1, 2, 5, 6, 10, 15, 18, 20, 3, 50};
            var array = new int[Count] {2, 4, 6, 8, 10, 12, 14, 16, 18, 20};
            var arrayRev = new int[Count] {20, 18, 16, 14, 12, 10, 8, 6, 4, 2};
            Console.Write("    Array: ");
            for(var i = 0; i < Count; i++) Console.Write("{0} ", array[i]);
            Console.WriteLine();
            for(var i = 0; i < 10; i++)
                Console.WriteLine("    Number: {0}\tPosition: {1}", search[i], Search(search[i], array) ?? -1);
            for(var i = 0; i < 10; i++)
                Console.WriteLine("    Number: {0}\tPosition: {1}", search[i], Search(search[i], arrayRev, false) ?? -1);

            #endregion

            Console.WriteLine("Part 2:");

            #region part2

            var list = new BidirectionalList();
            foreach(var number in array) {
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

            foreach(var number in array) {
                list.Remove(number);
                Console.Write("    ");
                foreach(long value in list) {
                    Console.Write("{0} ", value);
                }
                Console.WriteLine();
            }

            #endregion

            Console.WriteLine("Part 3:");

            #region part3

            var list1 = new BidirectionalGenericList<MyInt>();
            var myIntArray = new MyInt[Count];
            for(var i = 0; i < Count; i++) {
                myIntArray[i] = new MyInt {Number = array[i]};
            }
            foreach(var number in myIntArray) {
                if(number.Number%4 == 0)
                    list1.AddToBegin(number);
                else
                    list1.AddToEnd(number);
                Console.Write("    ");
                foreach(var value in list1) {
                    Console.Write("{0} ", value.Number);
                }
                Console.WriteLine();
            }

            foreach(var number in myIntArray) {
                list1.Remove(number);
                Console.Write("    ");
                foreach(var value in list1) {
                    Console.Write("{0} ", value.Number);
                }
                Console.WriteLine();
            }

            #endregion

            Console.ReadKey();
        }
    }

    public class MyInt {
        public int Number;
    }
}

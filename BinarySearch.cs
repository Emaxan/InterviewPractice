namespace InterviewPractice {
    public static class BinarySearch {
        /// <summary>
        ///     Binary search in sorted array.
        /// </summary>
        /// <param name="array">Sorted array of type int[]</param>
        /// <param name="element">Target element.</param>
        /// <param name="increase">True if array sorted by increasing, false if sorted by decreasing. Default true.</param>
        /// <returns>Index of target element or null if there are no such element in array.</returns>
        public static int? Search(int element, int[] array, bool increase = true) {
            if(increase) {
                if((array.Length < 0) || (element < array[0]) || (element > array[array.Length - 1])) return null;
            }
            else if((array.Length < 0) || (element > array[0]) || (element < array[array.Length - 1])) return null;

            int first = 0, last = array.Length;

            while(last > first) {
                int middle = first + (last - first)/2;

                if(increase)
                    if(array[middle] >= element) last = middle;
                    else first = middle + 1;
                else if(array[middle] <= element) last = middle;
                else first = middle + 1;
            }

            if(array[last] == element) return last;
            return null;
        }
    }
}

namespace InterviewPractice {
    public static class BinarySearch {
        /// <summary>
        /// Бинарный поиск в отсортированном массиве.
        /// </summary>
        /// <param name="array">Отсортированный массив типа int[]</param>
        /// <param name="element">Искомый элемент.</param>
        /// <param name="increase">True если массив отсортирован по возрастанию, false в противном случае. По умолчанию true.</param>
        /// <returns>Возвращает индекс искомого элемента либо null, если элемент не найден.</returns>
        public static int? Search(int element, int[] array, bool increase = true) {
            if(increase) {
                if((array.Length < 0) || (element < array[0]) || (element > array[array.Length - 1])) return null;
            }
            else {
                if((array.Length < 0) || (element > array[0]) || (element < array[array.Length - 1])) return null;
            }

            int first = 0, last = array.Length;

            while(last > first) {
                int middle = first + (last - first)/2;

                if(increase)
                    if (array[middle] >= element) last = middle;
                    else first = middle + 1;
                else
                    if (array[middle] <= element) last = middle;
                    else first = middle + 1;
            }

            if(array[last] == element) return last;
            return null;
        }
    }
}
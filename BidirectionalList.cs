using System;
using System.Collections;

namespace InterviewPractice {
    public class BidirectionalList: IEnumerable {
        private const int Next = 2, Prev = 1, Value = 0;
        private long?[][] _array;
        private int? _startIndex;

        /// <summary>
        ///     Initialize new list.
        /// </summary>
        /// <param name="count">Initial count of elements to locate memory. Default 0. When below 0 - reset to 0.</param>
        public BidirectionalList(int count = 0) {
            Clear();
            if(count <= 0) return;
            Resize(count);
        }

        /// <summary>
        ///     Count of elements in list.
        /// </summary>
        public int Count{ get; private set; }

        /// <summary>
        ///     Count of elements in memory.
        /// </summary>
        private int MemoryCount{ get; set; }

        /// <summary>
        ///     Get enumerator for using list in foreach.
        /// </summary>
        /// <returns>Enumerator of list.</returns>
        public IEnumerator GetEnumerator() {
            if(!_startIndex.HasValue) yield break;
            int index = _startIndex.Value;
            while(_array[Value][index].HasValue) {
                long number = _array[Value][index].Value;
                if(_array[Next][index].HasValue) {
                    index = (int) _array[Next][index];
                    yield return number;
                }
                else {
                    yield return number;
                    yield break;
                }
            }
        }

        /// <summary>
        ///     Add an item to the top of the list.
        /// </summary>
        /// <param name="number">Added item.</param>
        public int AddToBegin(long number) {
            if(Count == MemoryCount) Resize(MemoryCount*2);

            int? emptyElement = EmptyElement();
            if(!emptyElement.HasValue) throw new Exception("Can't found empty element.");

            _array[Value][emptyElement.Value] = number;
            if(_startIndex.HasValue) {
                _array[Prev][_startIndex.Value] = emptyElement;
                _array[Next][emptyElement.Value] = _startIndex;
            }
            _startIndex = emptyElement;
            Count++;
            return emptyElement.Value;
        }

        /// <summary>
        ///     Add an item to the end of the list.
        /// </summary>
        /// <param name="number"> Added item.</param>
        public int AddToEnd(long number) {
            if(Count == MemoryCount) Resize(MemoryCount*2);

            var emptyElement = EmptyElement();
            if(!emptyElement.HasValue) throw new Exception("Can't found empty element.");

            var lastElement = LastElement();
            if(lastElement.HasValue) {
                _array[Next][lastElement.Value] = emptyElement;
                _array[Prev][emptyElement.Value] = lastElement;
            }
            else _startIndex = emptyElement;

            _array[Value][(int) emptyElement] = number;
            Count++;
            return emptyElement.Value;
        }

        /// <summary>
        ///     Remove first item from the top of the list that equals to parameter.
        /// </summary>
        /// <param name="number">Removing item.</param>
        /// <returns>True, if item found and deleted, false in other ways.</returns>
        public bool Remove(long number) {
            if(!_startIndex.HasValue) return false;
            int index = _startIndex.Value;
            while((_array[Next][index] != null) && (_array[Value][index] != number))
                index = (int) _array[Next][index];
            if(_array[Value][index] != number) return false;
            if(Count == 1) {
                Clear();
                return true;
            }
            if(_array[Prev][index].HasValue)
                _array[Next][(int) _array[Prev][index]] = _array[Next][index];
            if(_array[Next][index].HasValue)
                _array[Prev][(int) _array[Next][index]] = _array[Prev][index];
            _array[Value][index] = null;
            Count--;
            return true;
        }

        /// <summary>
        ///     Clear the list.
        /// </summary>
        public void Clear() {
            _startIndex = null;
            Count = 0;
            MemoryCount = 0;
            _array = new long?[3][];
            for(var i = 0; i < 3; i++)
                _array[i] = new long?[0];
        }

        /// <summary>
        ///     Change count of allocated memory.
        /// </summary>
        /// <param name="newSize">New count of element to allocate memory.</param>
        private void Resize(int newSize) {
            var oldCount = MemoryCount;
            if(newSize == 0) newSize = 1;
            for(var i = 0; i < 3; i++) {
                Array.Resize(ref _array[i], newSize);
                for(var j = oldCount; j < newSize; j++)
                    _array[i][j] = null;
            }
            MemoryCount = newSize;
        }

        /// <summary>
        ///     Search first empty element in memory.
        /// </summary>
        /// <returns>Index of first empty element in memory or null if it no such elements.</returns>
        private int? EmptyElement() {
            for(var i = 0; i < MemoryCount; i++) {
                if(_array[Value][i] == null) return i;
            }
            return null;
        }

        /// <summary>
        ///     Search last element.
        /// </summary>
        /// <returns>Index of last element or null if list is empty.</returns>
        private int? LastElement() {
            if(_startIndex == null) return null;
            var index = (int) _startIndex;
            while(_array[Next][index] != null)
                index = (int) _array[Next][index];
            return index;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;

namespace InterviewPractice {
    public class BidirectionalGenericList<T>: IEnumerable<T> where T: class {
        private int?[] _next, _prev;
        private int? _startIndex;
        private T[] _value;

        /// <summary>
        ///     Initialize new list.
        /// </summary>
        /// <param name="count">Initial count of elements to locate memory. Default 0. When below 0 - reset to 0.</param>
        public BidirectionalGenericList(int count = 0) {
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
        public IEnumerator<T> GetEnumerator() {
            if(!_startIndex.HasValue) yield break;
            int index = _startIndex.Value;
            while(_value[index] != null) {
                T number = _value[index];
                if(_next[index] != null) {
                    index = (int) _next[index];
                    yield return number;
                }
                else {
                    yield return number;
                    yield break;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }

        /// <summary>
        ///     Add an item to the top of the list.
        /// </summary>
        /// <param name="number">Added item.</param>
        public int AddToBegin(T number) {
            if(Count == MemoryCount) Resize(MemoryCount*2);

            int? emptyElement = EmptyElement();
            if(!emptyElement.HasValue) throw new Exception("Can't found empty element.");

            _value[emptyElement.Value] = number;
            if(_startIndex.HasValue) {
                _prev[_startIndex.Value] = emptyElement;
                _next[emptyElement.Value] = _startIndex;
            }
            _startIndex = emptyElement;
            Count++;
            return emptyElement.Value;
        }

        /// <summary>
        ///     Add an item to the end of the list.
        /// </summary>
        /// <param name="number"> Added item.</param>
        public int AddToEnd(T number) {
            if(Count == MemoryCount) Resize(MemoryCount*2);

            int? emptyElement = EmptyElement();
            if(!emptyElement.HasValue) throw new Exception("Can't found empty element.");

            int? lastElement = LastElement();
            if(lastElement.HasValue) {
                _next[lastElement.Value] = emptyElement;
                _prev[emptyElement.Value] = lastElement;
            }
            else _startIndex = emptyElement;

            _value[(int) emptyElement] = number;
            Count++;
            return emptyElement.Value;
        }

        /// <summary>
        ///     Remove first item from the top of the list that equals to parameter.
        /// </summary>
        /// <param name="number">Removing item.</param>
        /// <returns>True, if item found and deleted, false in other ways.</returns>
        public bool Remove(T number) {
            if(!_startIndex.HasValue) return false;
            int index = _startIndex.Value;
            while((_next[index] != null) && (_value[index] != number))
                index = (int) _next[index];
            if(_value[index] != number) return false;
            if(Count == 1) {
                Clear();
                return true;
            }
            if(_prev[index].HasValue)
                _next[(int) _prev[index]] = _next[index];
            if(_next[index].HasValue)
                _prev[(int) _next[index]] = _prev[index];
            _value[index] = null;
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
            _value = new T[0];
            _next = new int?[0];
            _prev = new int?[0];
        }

        /// <summary>
        ///     Change count of allocated memory.
        /// </summary>
        /// <param name="newSize">New count of element to allocate memory.</param>
        private void Resize(int newSize) {
            int oldCount = MemoryCount;
            if(newSize == 0) newSize = 1;
            Array.Resize(ref _value, newSize);
            for(int j = oldCount; j < newSize; j++)
                _value[j] = null;
            Array.Resize(ref _next, newSize);
            for(int j = oldCount; j < newSize; j++)
                _next[j] = null;
            Array.Resize(ref _prev, newSize);
            for(int j = oldCount; j < newSize; j++)
                _prev[j] = null;
            MemoryCount = newSize;
        }

        /// <summary>
        ///     Search first empty element in memory.
        /// </summary>
        /// <returns>Index of first empty element in memory or null if it no such elements.</returns>
        private int? EmptyElement() {
            for(int i = 0; i < MemoryCount; i++)
                if(_value[i] == null) return i;
            return null;
        }

        /// <summary>
        ///     Search last element.
        /// </summary>
        /// <returns>Index of last element or null if list is empty.</returns>
        private int? LastElement() {
            if(_startIndex == null) return null;
            int index = (int) _startIndex;
            while(_next[index] != null)
                index = (int) _next[index];
            return index;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.NeuralNetworkTools
{
    public static class EnumeratorConverter
    {
        public class Increase : IEnumerator
        {
            private int _counter;
            private int _length;
            public Increase(int length)
            {
                _length = length;
                Reset();
            }
            public object Current => _counter;
            public bool MoveNext()
            {
                _counter++;
                return _counter < _length;
            }
            public void Reset()
            {
                _counter = -1;
            }
        }
        public class Decrease : IEnumerator
        {
            private int _counter;
            private int _length;
            public Decrease(int length)
            {
                _length = length;
                Reset();
            }
            public object Current => _counter;
            public bool MoveNext()
            {
                _counter--;
                return _counter >= 0;
            }
            public void Reset()
            {
                _counter = _length;
            }
        }
    }
}
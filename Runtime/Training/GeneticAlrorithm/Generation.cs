using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.NeuralNetworkTools
{
    public class Generation : IEnumerator
    {
        private NeuralNetwork[] _neuralNetworks;
        private int _currentIndex, _population;
        private NeuralNetwork _currentNeuralNetwork;

        public int Population => _population;
        public int CurrentIndex => _currentIndex;

        public NeuralNetwork this[int index]
        {
            get { return _neuralNetworks[index]; }
            set { _neuralNetworks[index] = value; }
        }

        public Generation(NeuralNetwork neuralNetwork, int population)
        {
            Init(neuralNetwork, population);
        }
        public void Init(NeuralNetwork neuralNetwork, int population)
        {
            _population = population;
            _neuralNetworks = new NeuralNetwork[population];
            for (int i = 0; i < _population; i++)
                _neuralNetworks[i] = neuralNetwork.Copy();
            Reset();
        }

        public object Current => _currentNeuralNetwork;
        public bool MoveNext()
        {
            _currentIndex++;
            if (_currentIndex >= _population)
                return false;
            _currentNeuralNetwork = _neuralNetworks[_currentIndex];
            return true;
        }

        public void Reset()
        {
            _currentIndex = -1;
            _currentNeuralNetwork = null;
        }
    }
}

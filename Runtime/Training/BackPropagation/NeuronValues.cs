using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.NeuralNetworkTools
{
    public class NeuronValues
    {
        private double[][] _results;
        private int[] _topology;
        private int _layerCount;

        public int LayerCount => _layerCount;
        public double[] this[int layer]
        {
            get { return _results[layer]; }
            set { _results[layer] = value; }
        }
        public double this[int layer, int neuron]
        {
            get { return _results[layer][neuron]; }
            set { _results[layer][neuron] = value; }
        }

        public NeuronValues(int[] topology)
        {
            _topology = topology;
            _layerCount = topology.Length - 1;
            _results = new double[_layerCount][];
            for (int i = 0; i < _layerCount; i++)
                _results[i] = new double[topology[i + 1]];
        }
    }
}
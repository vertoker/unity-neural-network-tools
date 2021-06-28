using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game.NeuralNetworkTools
{
    public delegate void CorrectNeuralNetwork(double error);
    public class BackPropagation
    {
        private NeuralNetwork _neuralNetwork;
        private NeuronValues _results, _deltas;
        private IEnumerator increase, degrease;

        public BackPropagation(NeuralNetwork neuralNetwork)
        {
            _neuralNetwork = neuralNetwork;
            increase = new EnumeratorConverter.Increase(neuralNetwork.LayersLength);
            degrease = new EnumeratorConverter.Decrease(neuralNetwork.LayersLength);
            _results = new NeuronValues(neuralNetwork.Topology);
            _deltas = new NeuronValues(neuralNetwork.Topology);
        }

        public double[] Process(double[] inputs)
        {
            return _neuralNetwork.Process(inputs, out _results);
        }

        public void Correct(double error)//Закончить алгоритм
        {
            degrease.Reset();
            if (degrease.MoveNext())
                StartDeltaFill((int)degrease.Current);
            while (degrease.MoveNext())
                DeltaFill((int)degrease.Current);
        }

        public void StartDeltaFill(int index)
        {
            for (int i = 0; i < _deltas.LayerCount; i++)
            {
                //_deltas[index][i] = 
            }
        }
        public void DeltaFill(int index)
        {

        }
    }
}
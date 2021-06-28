/// Author: Samuel Arzt
/// Date: March 2017
/// (I modified this script)

using UnityEngine;
using System;

namespace Game.NeuralNetworkTools
{
    [Serializable]
    public class NeuralLayer
    {
        [SerializeField] private int _inputCount;
        [SerializeField] private int _outputCount;
        [SerializeField] private double[,] _weights;

        private ActivationCalculate _activation;
        private ActivationType _activationType;

        public int InputCount => _inputCount;
        public int OutputCount => _outputCount;
        public int WeightsCount => _inputCount * _outputCount;
        public double[,] Weights
        {
            get { return _weights; }
            set { _weights = value; }
        }

        public double this[int pastIndex, int nextIndex]
        {
            get { return _weights[pastIndex, nextIndex]; }
            set { _weights[pastIndex, nextIndex] = value; }
        }
        public double this[int index]
        {
            get
            {
                TotalCounter(index, out int pastIndex, out int nextIndex);
                return _weights[pastIndex, nextIndex];
            }
            set
            {
                TotalCounter(index, out int pastIndex, out int nextIndex);
                _weights[pastIndex, nextIndex] = value;
            }
        }
        public void TotalCounter(int index, out int pastIndex, out int nextIndex)
        {
            pastIndex = index / _inputCount;
            nextIndex = index - pastIndex * _inputCount;
        }

        public NeuralLayer(int inputCount, int outputCount, ActivationType activationType = ActivationType.Sigmoid)
        {
            _activationType = activationType;
            _activation = ActivationConverter.Convert[activationType];
            _inputCount = inputCount;
            _outputCount = outputCount;
            _weights = new double[inputCount, outputCount];
        }
        public NeuralLayer(int inputCount, int outputCount, double[,] weights, ActivationType activationType = ActivationType.Sigmoid)
        {
            _activationType = activationType;
            _activation = ActivationConverter.Convert[activationType];
            _inputCount = inputCount;
            _outputCount = outputCount;
            _weights = weights;
        }

        public double[] Process(double[] inputs)
        {
            double[] outputs = new double[_outputCount];
            for (int i = 0; i < _inputCount; i++)
                for (int j = 0; j < _outputCount; j++)
                    outputs[j] += inputs[i] * _weights[i, j];
            for (int i = 0; i < outputs.Length; i++)
                outputs[i] = _activation.Invoke(outputs[i]);
            return outputs;
        }

        /// <summary>
        /// This method set random weights in current NL
        /// </summary>
        public void SetRandomWeights(double length, double offset, System.Random random)
        {
            for (int i = 0; i < _inputCount; i++)
                for (int j = 0; j < _outputCount; j++)
                    _weights[i, j] += random.NextDouble() * length + offset;
        }

        public NeuralLayer Copy()
        {
            return new NeuralLayer(_inputCount, _outputCount, _weights, _activationType);
        }
    }
    public delegate double WeightModification(double value);
}
/// Author: Samuel Arzt
/// Date: March 2017
/// (I modified this script)

using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System;
using System.Collections;

namespace Game.NeuralNetworkTools
{
    [Serializable]
    public class NeuralNetwork
    {
        private int[] _topology;
        private NeuralLayer[] _layers;
        private ActivationType _functionActivation;

        public int[] Topology => _topology;
        public int TopologyLength => _topology.Length;
        public int LayersLength => _layers.Length;
        public ActivationType FunctionActivation => _functionActivation;

        public NeuralLayer this[int index]
        {
            get { return _layers[index]; }
            set { _layers[index] = value; }
        }

        public NeuralNetwork(int[] topology, ActivationType functionActivation = ActivationType.Sigmoid)
        {
            _functionActivation = functionActivation;
            _topology = topology;
            int length = topology.Length - 1;
            _layers = new NeuralLayer[length];

            for (int i = 0; i < length; i++)
                _layers[i] = new NeuralLayer(topology[i], topology[i + 1], functionActivation);
        }
        public NeuralNetwork(NeuralLayer[] layers, ActivationType functionActivation = ActivationType.Sigmoid)
        {
            _functionActivation = functionActivation;
            _layers = layers;
            int length = _layers.Length;
            _topology = new int[length + 1];

            for (int i = 0; i < length; i++)
                _topology[i] = _layers[i].InputCount;
            _topology[length] = _layers[length - 1].OutputCount;
        }
        public NeuralNetwork(int[] topology, NeuralLayer[] layers, ActivationType functionActivation = ActivationType.Sigmoid)
        {
            _functionActivation = functionActivation;
            _topology = topology;
            _layers = layers;
        }

        /// <summary>
        /// Main method of NN
        /// </summary>
        public double[] Process(double[] inputs)
        {
            foreach (NeuralLayer layer in _layers)
                inputs = layer.Process(inputs);
            return inputs;
        }
        /// <summary>
        /// Main method of NN (with results in every step)
        /// </summary>
        public double[] Process(double[] inputs, out NeuronValues results)
        {
            results = new NeuronValues(_topology);
            for (int i = 0; i < _layers.Length; i++)
            {
                inputs = _layers[i].Process(inputs);
                results[i] = inputs;
            }
            return inputs;
        }

        /// <summary>
        /// This method set random weights in all NN
        /// </summary>
        public void FillRandomWeights(double min, double max, System.Random random)
        {
            if (_layers == null)
                return;
            double length = Math.Abs(max - min);
            foreach (NeuralLayer layer in _layers)
                layer.SetRandomWeights(length, min, random);
        }

        public void GetRandomLayerID(System.Random random, out int randomLayer)
        {
            randomLayer = random.Next(0, _layers.Length);
        }
        public void GetRandomWeightID(System.Random random, out int randomLayer, out int inputWeight, out int outputWeight)
        {
            randomLayer = random.Next(0, _layers.Length);
            inputWeight = random.Next(0, _layers[randomLayer].InputCount);
            outputWeight = random.Next(0, _layers[randomLayer].OutputCount);
        }

        /// <summary>
        /// This method return deep copy (need for training)
        /// </summary>
        public NeuralNetwork Copy()
        {
            NeuralNetwork newNet = new NeuralNetwork(_topology, _functionActivation);
            for (int i = 0; i < newNet.LayersLength; i++)
                newNet[i] = _layers[i].Copy();
            return newNet;
        }

        //This section about file management
        //P.S: This class can easy save and load with JsonUtility
        public void Save(params string[] paths)
        {
            Save(Path.Combine(paths));
        }
        public void Save(string filePath)
        {
            string data = SaveUtility.ToString(this);
            File.WriteAllText(filePath, data);
        }
        public static NeuralNetwork Load(string filePath)
        {
            string data = File.ReadAllText(filePath);
            return SaveUtility.FromString(data);
        }
        public static bool Has(string filePath)
        {
            try
            {
                string data = File.ReadAllText(filePath);
                SaveUtility.FromString(data);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
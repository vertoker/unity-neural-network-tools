/// Author: Samuel Arzt
/// Date: March 2017
/// (I modified this script)

using System;
using UnityEngine;

namespace Game.NeuralNetworkTools
{
    public enum MutationType
    {
        SingleWeight = 0,
        SingleLayer = 1,
        RandomizeNN = 2
    }
    public class GeneticAlgorithm
    {
        private double _min, _max;
        private double _length, _offset;
        private System.Random _randomizer;

        public System.Random GetRandomizer => _randomizer;

        public GeneticAlgorithm(double min = -1.0d, double max = 1.0d)
        {
            _max = max;
            _offset = _min = min;
            _length = Math.Abs(max - min);
            InitRandom();
        }
        public void InitRandom()
        {
            _randomizer = new System.Random(UnityEngine.Random.Range(int.MinValue, int.MaxValue));
        }

        public void CorrectingWeights(Generation generation, MutationType type = MutationType.SingleWeight)
        {
            generation.Reset();
            switch (type)
            {
                case MutationType.SingleWeight:
                    SingleWeight(generation);
                    break;
                case MutationType.SingleLayer:
                    SingleLayer(generation);
                    break;
                case MutationType.RandomizeNN:
                    RandomizeNN(generation);
                    break;
            }
        }

        private void SingleWeight(Generation generation)
        {
            while (generation.MoveNext())
            {
                InitRandom();
                NeuralNetwork neuralNetwork = (NeuralNetwork)generation.Current;
                neuralNetwork.GetRandomWeightID(_randomizer, out int randomLayer, out int inputWeight, out int outputWeight);
                neuralNetwork[randomLayer][inputWeight, outputWeight] = _randomizer.NextDouble() * _length + _offset;
            }
        }
        private void SingleLayer(Generation generation)
        {
            while (generation.MoveNext())
            {
                InitRandom();
                NeuralNetwork neuralNetwork = (NeuralNetwork)generation.Current;
                neuralNetwork.GetRandomLayerID(_randomizer, out int randomLayer);
                neuralNetwork[randomLayer].SetRandomWeights(_length, _offset, _randomizer);
            }
        }
        private void RandomizeNN(Generation generation)
        {
            while (generation.MoveNext())
            {
                InitRandom();
                NeuralNetwork neuralNetwork = (NeuralNetwork)generation.Current;
                neuralNetwork.FillRandomWeights(_min, _max, _randomizer);
            }
        }
    }
}
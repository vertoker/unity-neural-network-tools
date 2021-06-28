using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.NeuralNetworkTools
{
    public class ErrorComputation
    {
        private TrainingSet[] _sets;
        private ErrorCalculate _calculate;
        private int _setsCount;

        public ErrorComputation(TrainingSet[] sets, ErrorType errorType = ErrorType.MSE)
        {
            _sets = sets;
            _setsCount = sets.Length;
            _calculate = ErrorConverter.Convert[errorType];
        }

        public double[] ErrorCalculate(Generation generation)
        {
            generation.Reset();
            double[] errors = new double[generation.Population];
            while (generation.MoveNext())
                errors[generation.CurrentIndex] = ErrorCalculate((NeuralNetwork)generation.Current);
            return errors;
        }

        public double ErrorCalculate(NeuralNetwork neuralNetwork)
        {
            double sum = 0;
            for (int i = 0; i < _setsCount; i++)
            {
                double[] output = neuralNetwork.Process(_sets[i].InputLayer);
                sum += _sets[i].ErrorCalculate(output, _calculate);
            }
            return sum / _setsCount;
        }

        public void ErrorCalculate(BackPropagation backPropagation)
        {
            for (int i = 0; i < _setsCount; i++)
            {
                double[] output = backPropagation.Process(_sets[i].InputLayer);
                double error = _sets[i].ErrorCalculate(output, _calculate);
                backPropagation.Correct(error);
            }
        }
    }
}

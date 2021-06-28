using System.Collections.Generic;
using System;

namespace Game.NeuralNetworkTools
{
    public enum ActivationType
    {
        Binary = 0,
        Linear = 1,
        Sigmoid = 2,
        Hyperbolic = 3,
        ReLu = 4
    }
    public delegate double ActivationCalculate(double value);
    public static class ActivationConverter
    {
        private static Dictionary<ActivationType, ActivationCalculate> _convert = new Dictionary<ActivationType, ActivationCalculate>()
        {
            { ActivationType.Binary, Binary },
            { ActivationType.Linear, Linear },
            { ActivationType.Sigmoid, Sigmoid },
            { ActivationType.Hyperbolic, Hyperbolic },
            { ActivationType.ReLu, ReLu }
        };
        public static Dictionary<ActivationType, ActivationCalculate> Convert => _convert;

        public static double Binary(double value)
        {
            if (value < 0)
                return 0;
            return 1;
        }
        public static double Linear(double value)
        {
            if (value > 0.5)
                return 1;
            if (value < -0.5)
                return 0;
            return value + 0.5;
        }
        public static double Sigmoid(double value)
        {
            return 1.0 / (1.0 + Math.Exp(-value));
        }
        public static double Hyperbolic(double value)
        {
            return Math.Tanh(value);
        }
        public static double ReLu(double value)
        {
            if (value < 0)
                return 0;
            return value;
        }
    }
}
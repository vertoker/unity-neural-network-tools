using System.Collections.Generic;
using System;

namespace Game.NeuralNetworkTools
{
    public enum ErrorType
    {
        MSE = 0,
        RootMSE = 1,
        Arctan = 2
    }
    public delegate double ErrorCalculate(double[] source, double[] output);
    public static class ErrorConverter
    {
        private static Dictionary<ErrorType, ErrorCalculate> _convert = new Dictionary<ErrorType, ErrorCalculate>()
        {
            { ErrorType.MSE, new ErrorCalculate(MeanSquaredError) },
            { ErrorType.RootMSE, new ErrorCalculate(RootMeanSquaredError) },
            { ErrorType.Arctan, new ErrorCalculate(Arctan) }
        };
        public static Dictionary<ErrorType, ErrorCalculate> Convert => _convert;
        private static double MeanSquaredError(double[] source, double[] output)
        {
            int length = source.Length;
            double error = 0;
            for (int i = 0; i < length; i++)
                error += Math.Pow(source[i] - output[i], 2);
            return error / length;
        }
        private static double RootMeanSquaredError(double[] source, double[] output)
        {
            return Math.Sqrt(MeanSquaredError(source, output));
        }
        private static double Arctan(double[] source, double[] output)
        {
            int length = source.Length;
            double error = 0;
            for (int i = 0; i < length; i++)
                error += Math.Pow(Math.Atan(source[i] - output[i]), 2);
            return error / length;
        }
    }

}

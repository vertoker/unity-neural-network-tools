using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.NeuralNetworkTools
{
    public static class MathNN
    {
        public static double Min(out int index, params double[] values)
        {
            index = 0;
            double result = values[index];
            for (int i = 1; i < values.Length; i++)
            {
                if (values[i] < result)
                {
                    result = values[i];
                    index = i;
                }
            }
            return result;
        }
        public static double Max(out int index, params double[] values)
        {
            index = 0;
            double result = values[index];
            for (int i = 1; i < values.Length; i++)
            {
                if (values[i] > result)
                {
                    result = values[i];
                    index = i;
                }
            }
            return result;
        }
    }

}
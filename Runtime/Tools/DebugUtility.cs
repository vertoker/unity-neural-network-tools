using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.NeuralNetworkTools
{
    public static class DebugUtility
    {
        public static void Log(double[] array)
        {
            Debug.Log(string.Join(SaveUtility.ARRAY_DIVIDE_STRING, array));
        }
        public static void Log(NeuralLayer layer)
        {
            Debug.Log(SaveUtility.FromArray(layer));
        }
        public static void Log(NeuralNetwork network)
        {
            Debug.Log(SaveUtility.ToString(network));
        }
        public static void Log(object message)
        {
            Debug.Log(message);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Game.NeuralNetworkTools
{
    public static class SaveUtility
    {
        public static char ARRAY_DIVIDE { get; private set; } = ';';
        public static char OBJECT_DIVIDE { get; private set; } = '\n';
        public static string ARRAY_DIVIDE_STRING { get; private set; } = ARRAY_DIVIDE.ToString();
        public static string OBJECT_DIVIDE_STRING { get; private set; } = OBJECT_DIVIDE.ToString();

        public static string ToString(NeuralNetwork neuralNetwork)
        {
            string length = neuralNetwork.TopologyLength.ToString();
            string func = ((int)neuralNetwork.FunctionActivation).ToString();
            string topology = string.Join(ARRAY_DIVIDE_STRING, neuralNetwork.Topology);
            string[] weights = new string[neuralNetwork.LayersLength];
            for (int i = 0; i < neuralNetwork.LayersLength; i++)
                weights[i] = FromArray(neuralNetwork[i]);
            string weightsAll = string.Join(OBJECT_DIVIDE_STRING, weights);
            return string.Join(OBJECT_DIVIDE_STRING, length, func, topology, weightsAll);
        }
        public static NeuralNetwork FromString(string data)
        {
            string[] objects = data.Split(OBJECT_DIVIDE);
            ActivationType functionActivation = (ActivationType)int.Parse(objects[1]);
            int[] topology = ToArray(objects[2], int.Parse(objects[0]));

            NeuralNetwork neuralNetwork = new NeuralNetwork(topology, functionActivation);

            int length = objects.Length, counter = 0;
            for (int i = 3; i < length; i++)
            {
                double[,] weights = ToArray(objects[i], topology[counter], topology[counter + 1]);
                neuralNetwork[counter].Weights = weights;
                counter++;
            }
            return neuralNetwork;
        }

        private static int[] ToArray(string data, int length)
        {
            int counter = 0, indexBuilder = 0;
            int[] array = new int[length];
            StringBuilder builder = new StringBuilder(10);
            foreach (char ch in data)
            {
                if (ch == ARRAY_DIVIDE)
                {
                    array[counter] = int.Parse(builder.ToString());
                    builder.Clear(); counter++; indexBuilder = 0;
                }
                else
                {
                    builder[indexBuilder] = ch;
                    indexBuilder++;
                }
            }
            return array;
        }

        public static string FromArray(NeuralLayer layer)
        {
            int inputCount = layer.InputCount;
            int outputCount = layer.OutputCount;
            double[,] array = layer.Weights;
            string[] data = new string[inputCount * outputCount];
            int counter = 0;
            for (int i = 0; i < inputCount; i++)
            {
                for (int j = 0; j < outputCount; j++)
                {
                    data[counter] = array[i, j].ToString();
                    counter++;
                }
            }
            return string.Join(ARRAY_DIVIDE_STRING, data);
        }
        public static double[,] ToArray(string data, int inputCount, int outputCount)
        {
            string[] nums = data.Split(ARRAY_DIVIDE);
            double[,] array = new double[inputCount, outputCount];
            int i = 0, j = 0, length = inputCount * outputCount;
            for (int index = 0; index < length; index++)
            {
                array[i, j] = double.Parse(nums[index]);
                j++;
                if (j == outputCount)
                {
                    j = 0;
                    i++;
                }
            }
            return array;
        }
    }
}
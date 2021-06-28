using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine;

namespace Game.NeuralNetworkTools
{
    public static class UnificationConverter
    {
        #region Image
        public static double[] ImageRGBA2Layer(Texture2D texture, ref int width, ref int height)
        {
            width = texture.width; height = texture.height;
            double[] layer = new double[width * height * 4];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    int index = (i * height + j) * 4;
                    Color color = texture.GetPixel(i, j);
                    layer[index] = color.r;
                    layer[index + 1] = color.g;
                    layer[index + 2] = color.b;
                    layer[index + 3] = color.a;
                }
            }
            return layer;
        }
        public static Texture2D Layer2ImageRGBA(double[] layer, int width, int height)
        {
            Texture2D texture = new Texture2D(width, height);
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    int index = (i * height + j) * 4;
                    float r = (float)layer[index];
                    float g = (float)layer[index + 1];
                    float b = (float)layer[index + 2];
                    float a = (float)layer[index + 3];
                    texture.SetPixel(i, j, new Color(r, g, b, a));
                }
            }
            return texture;
        }
        public static double[] ImageRGB2Layer(Texture2D texture, ref int width, ref int height)
        {
            width = texture.width; height = texture.height;
            double[] layer = new double[width * height * 3];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    int index = (i * height + j) * 3;
                    Color color = texture.GetPixel(i, j);
                    layer[index] = color.r;
                    layer[index + 1] = color.g;
                    layer[index + 2] = color.b;
                }
            }
            return layer;
        }
        public static Texture2D Layer2ImageRGB(double[] layer, int width, int height)
        {
            Texture2D texture = new Texture2D(width, height);
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    int index = (i * height + j) * 3;
                    float r = (float)layer[index];
                    float g = (float)layer[index + 1];
                    float b = (float)layer[index + 2];
                    float a = (float)layer[index + 3];
                    texture.SetPixel(i, j, new Color(r, g, b, a));
                }
            }
            return texture;
        }
        #endregion

        #region Audio
        public static double[] Audio2Layer(AudioClip clip, ref int samples, ref int channels, ref int frequency)
        {
            samples = clip.samples;
            channels = clip.channels;
            frequency = clip.frequency;

            int length = samples * channels;
            float[] data = new float[length];
            double[] layer = new double[length];

            clip.GetData(data, 0);
            for (int i = 0; i < length; i++)
                layer[i] = data[i];
            return layer;
        }
        public static AudioClip Layer2Audio(double[] layer, int samples, int channels, int frequency)
        {
            int length = samples * channels;
            float[] data = new float[length];
            AudioClip clip = AudioClip.Create("sample", samples, channels, frequency, true);
            for (int i = 0; i < length; i++)
                data[i] = (float)layer[i];
            clip.SetData(data, 0);
            return clip;
        }
        #endregion
    }
}
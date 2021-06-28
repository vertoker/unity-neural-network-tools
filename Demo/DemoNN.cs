using System.Collections;
using System.Collections.Generic;
using Game.NeuralNetworkTools;
using UnityEngine;
using System;

public class DemoNN : MonoBehaviour
{
    public TrainingSet[] sets;
    private NeuralNetwork neuralNetwork;
    private GeneticAlgorithm geneticAlgorithm;
    private ErrorComputation computation;

    private int[] topology = new int[] { 5, 25, 25, 25, 2 };
    private int populationInGeneration = 20;
    private int generationCounter = 0;

    public void Start()
    {
        computation = new ErrorComputation(sets, ErrorType.MSE);
        neuralNetwork = new NeuralNetwork(topology);
        geneticAlgorithm = new GeneticAlgorithm(-5d, 5d);
        neuralNetwork.FillRandomWeights(-5d, 5d, geneticAlgorithm.GetRandomizer);
        StartCoroutine(GenerationCoroutine());
    }

    IEnumerator GenerationCoroutine()
    {
        while (generationCounter < 1)
        {
            generationCounter++;
            yield return null;
            print("Current generation is " + generationCounter);
            Generation generation = new Generation(neuralNetwork, populationInGeneration);
            geneticAlgorithm.CorrectingWeights(generation, MutationType.SingleLayer);
            double[] errors = computation.ErrorCalculate(generation);
            double minError = MathNN.Min(out int index, errors);
            neuralNetwork = generation[index];
            neuralNetwork.Save(Application.dataPath, "data.txt");
            DebugUtility.Log(errors);
            print("Error equals " + minError);
        }
    }
}

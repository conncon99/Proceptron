using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerceptronManager : MonoBehaviour
{
    //Perceptron details
    public Perceptron perceptron;
    [Range(-1000,1000)]
    public int greaterThanValue;
    double weight1, weight2;
    [Range(0,1)]
    public double adjustmentValue = 0.05;

    //Training Data
    public int numInputs = 100;
    double[,] trainingInputs;
    double[] trainingOutputs;

    //Testing Data
    public int testSize = 25;
    double[,] testingInputs;
    double[] testingOutputs;

    double trainingAccuracy = 0;
    double testingAccuracy = 0;

    [SerializeField]
    Text trainingAccuracyText, testingAccuracyText;



    // Start is called before the first frame update
    void Start()
    {
        //Set up Perceptron
        trainingInputs = new double[2, numInputs];
        trainingOutputs = new double[numInputs];
        weight1 = 0.5;
        weight2 = 0.5;
        
        createRandomInputsAndOutputs(trainingInputs, trainingOutputs);
        perceptron = new Perceptron(trainingInputs, trainingOutputs, adjustmentValue,greaterThanValue, weight1, weight2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void createRandomInputsAndOutputs(double[,] inputs, double[] outputs)
    {
        for (int i = 0; i < outputs.Length; i++)
        {
            for (int j = 0; j < trainingInputs.GetLength(0); j++)
            {
                inputs[j, i] = Random.Range(-1000, 1000);
            }
            outputs[i] = inputs[0, i] + inputs[1, i] > greaterThanValue ? 1 : 0; 
        }
    }

    double getTestingAccuracy()
    {
        double correctTests = 0;
        for (int i = 0; i < testSize; i++)
        {
            correctTests += perceptron.calculateOutput(testingInputs[0, i], testingInputs[1, i]) == testingOutputs[i] ? 1 : 0;
            //Debug.Log("Correct Tests: " + correctTests);
        }
        return (correctTests / testSize) * 100; ;
    }

    public void train()
    {
        trainingAccuracy = perceptron.train();
        trainingAccuracyText.text = $"Training Accuracy: {trainingAccuracy:N2}%";
    }

    public void test()
    {
        testingInputs = new double[2, testSize];
        testingOutputs = new double[testSize];

        createRandomInputsAndOutputs(testingInputs, testingOutputs);
        testingAccuracy = getTestingAccuracy();
        testingAccuracyText.text = $"Testing Accuracy: {testingAccuracy:N2}%";
    }

}
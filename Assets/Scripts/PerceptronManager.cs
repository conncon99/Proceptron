using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerceptronManager : MonoBehaviour
{
    //Perceptron details
    public Perceptron perceptron;
    public int greaterThanValue;
    public double adjustmentValue;

    //Training Data
    public int numInputs;
    double[,] trainingInputs;
    double[] trainingOutputs;

    //Testing Data
    public int testSize;
    double[,] testingInputs;
    double[] testingOutputs;

    double trainingAccuracy = 0;
    double testingAccuracy = 0;

    [SerializeField]
    Text trainingAccuracyText, testingAccuracyText, invalidInputsText, testingText;
    [SerializeField]
    InputField AdjustmentValueInput, TrainingDataInput, TestingDataInput, BoundaryInput, ownValue, ownValueTwo;
    [SerializeField]
    Button TestingButton, TryingButton;

    private void Start()
    {
        TestingButton.gameObject.SetActive(false);
        invalidInputsText.gameObject.SetActive(false);
        TryingButton.gameObject.SetActive(false);
    }

    public void setUpPerceptron()
    {
        invalidInputsText.gameObject.SetActive(false);
        try
        {
            adjustmentValue = double.Parse(AdjustmentValueInput.text);
            greaterThanValue = int.Parse(BoundaryInput.text);
            numInputs = int.Parse(TrainingDataInput.text);
            testSize = int.Parse(TestingDataInput.text);
        }
        catch
        {
            invalidInputsText.gameObject.SetActive(true);
            return;
        }


        trainingInputs = new double[2, numInputs];
        trainingOutputs = new double[numInputs];

        createRandomInputsAndOutputs(trainingInputs, trainingOutputs, true);
        if(perceptron == null)
        {
            perceptron = new Perceptron(trainingInputs, trainingOutputs, adjustmentValue, greaterThanValue);
        }
        else
        {
            perceptron.setInputData(trainingInputs);
            perceptron.setCorrectOutput(trainingOutputs);
            perceptron.setAdjustmentValue(adjustmentValue);
            perceptron.setGreaterThanValue(greaterThanValue);
        }
        train();
        TestingButton.gameObject.SetActive(true);
        TryingButton.gameObject.SetActive(true);
    }

    void createRandomInputsAndOutputs(double[,] inputs, double[] outputs, bool isTraining)
    {
        int valueRange = isTraining ? 50 : 1000;

        for (int i = 0; i < outputs.Length; i++)
        {
            for (int j = 0; j < trainingInputs.GetLength(0); j++)
            {
                
                inputs[j, i] = Random.Range(greaterThanValue - valueRange, greaterThanValue + valueRange);
            }
            outputs[i] = inputs[0, i] + inputs[1, i] > greaterThanValue ? 1 : 0; 
        }
    }

    double getTestingAccuracy()
    {
        if (perceptron == null) return -1;
        double correctTests = 0;
        for (int i = 0; i < testSize; i++)
        {
            correctTests += perceptron.calculateOutput(perceptron.getOneDimensionInputs(testingInputs, i), out double calculation) == testingOutputs[i] ? 1 : 0;
            //Debug.Log("Correct Tests: " + correctTests);
        }
        return (correctTests / testSize) * 100; ;
    }

    public void train()
    {
        if (perceptron == null) return;
        trainingAccuracy = perceptron.train();
        Debug.Log("Training Accuracy: " + trainingAccuracy);
        trainingAccuracyText.text = $"Training Accuracy: {trainingAccuracy:N2}%";
    }

    public void test()
    {
        if (perceptron == null) return;
        testingInputs = new double[2, testSize];
        testingOutputs = new double[testSize];

        createRandomInputsAndOutputs(testingInputs, testingOutputs, false);
        testingAccuracy = getTestingAccuracy();
        testingAccuracyText.text = $"Testing Accuracy: {testingAccuracy:N2}%";
    }

    public void tryIt()
    {
        string tryingOutput = "";
        if (perceptron == null) return;
        double[] inputs = new double[2];
        try
        {
            inputs[0] = double.Parse(ownValue.text);
            inputs[1] = double.Parse(ownValueTwo.text);
        }
        catch
        {
            invalidInputsText.gameObject.SetActive(true);
            return;
        }

        int perceptronOutput = perceptron.calculateOutput(inputs, out double calculation);
        int correctValue = inputs[0] + inputs[1] > greaterThanValue ? 1 : 0;

        string greaterOrLess = perceptronOutput == 1 ? "greater" : "not greater than"; 
        string correctString = perceptronOutput == correctValue ? "correctly" : "incorrectly";
        tryingOutput = $"The perceptron {correctString} predicted that {inputs[0] + inputs[1]} is {greaterOrLess} than {greaterThanValue}";
        testingText.text = tryingOutput;
    }

}

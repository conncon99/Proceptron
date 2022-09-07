using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perceptron
{
    //Perceptron
    //2 Inputs, 1 output
    //The purpose of this perceptron will be to output 1 if input1 + input2 is > a value greaterThanValue, otherwise 0

    double[,] inputData;
    double[] weights, correctOutput;
    double adjustmentValue, trainingAccuracy, greaterThanValue;

    public Perceptron(double[,] inputData, double[] correctOutput, double adjustmentValue, double greaterThanValue)
    {
        this.inputData = inputData;
        this.correctOutput = correctOutput;
        this.greaterThanValue = greaterThanValue;
        weights = new double[2];

        for (int i = 0; i < weights.Length; i++)
        {
            weights[i] = Random.Range(0.0f, 1.0f);
        }

        this.adjustmentValue = adjustmentValue;
    }

    public void setAdjustmentValue(double adjVal)
    {
        adjustmentValue = adjVal;
    }

    public void setGreaterThanValue(double greaterThanVal)
    {
        greaterThanValue = greaterThanVal;
    }

    public void setInputData(double[,] input)
    {
        inputData = input;
    }

    public void setCorrectOutput(double[] corOutput)
    {
        correctOutput = corOutput;
    }

    public double train()
    {
        trainingAccuracy = 0;
        double correctTrainingValues = 0;
        for (int i = 0; i < correctOutput.Length; i++)
        {
            double output = calculateOutput(getOneDimensionInputs(inputData, i), out double calculation);
            if(output != correctOutput[i])
            {
                double error = correctOutput[i] - output;
                //Debug.Log("Error: " + error);
                for (int j = 0; j < weights.Length; j++)
                {
                    weights[j] += error * (inputData[j, i]) * adjustmentValue;
                }
            }
            else
            {
                correctTrainingValues++;
                //Debug.Log("Correct Training: " + correctTrainingValues);
                //Debug.Log("i: " + correctTrainingValues);
            }
            trainingAccuracy = (correctTrainingValues / (i + 1)) * 100;
            //Debug.Log("Training Accuracy: " + trainingAccuracy);
            //Debug.Log($"Weight1: {weights[0]}");
            //Debug.Log($"Weight2: {weights[1]}");
        }
        //Debug.Log("Training Accuracy: " + trainingAccuracy);
        return trainingAccuracy;
    }

    public int calculateOutput(double[] inputs, out double calculation)
    {
        //double initalSum = input1 + input2;
        double initialSum = 0;
        calculation = 0;
        for (int i = 0; i < weights.Length; i++)
        {
            calculation += inputs[i] * weights[i];
            initialSum += inputs[i];
        }
        //Activation function
        int returnValue = calculation > 1 ? 1 : 0;
        int correctValue = initialSum > greaterThanValue ? 1 : 0;
        if (returnValue != correctValue)
        {
            //Debug.Log($"Sum {initialSum}, guessed {returnValue}, should have been {correctValue}");
        }
        return returnValue;
    }

    public double[] getOneDimensionInputs(double[,] inputs, int indegreaterThanValue)
    {
        double[] newInputs = new double[inputs.GetLength(0)];
        for (int i = 0; i < inputs.GetLength(0); i++)
        {
            newInputs[i] = inputs[i, indegreaterThanValue];
        }
        return newInputs;
    }
}

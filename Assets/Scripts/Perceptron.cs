using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perceptron
{
    //Perceptron
    //2 Inputs, 1 output
    //The purpose of this perceptron will be to output 1 if input1 + input2 is > a value x, otherwise 0


    double weight1;
    double weight2;
    double[,] inputData;
    double[] correctOutput;
    double x;
    double adjustmentValue;
    double trainingAccuracy;

    public Perceptron(double[,] inputData, double[] correctOutput, double adjustmentValue, double x, double weight1, double weight2)
    {
        this.inputData = inputData;
        this.correctOutput = correctOutput;
        this.x = x;
        this.weight1 = weight1;
        this.weight2 = weight2;
        this.adjustmentValue = adjustmentValue;
    }

    void setAdjustmentValue(double adjVal)
    {
        adjustmentValue = adjVal;
    }

    public double train()
    {
        trainingAccuracy = 0;
        double correctTrainingValues = 0;
        for (int i = 0; i < correctOutput.Length; i++)
        {
            if(calculateOutput(inputData[0, i], inputData[1, i]) > correctOutput[i])
            {
                weight1 -= adjustmentValue;
                weight2 -= adjustmentValue;
            }
            else if(calculateOutput(inputData[0, i], inputData[1, i]) < correctOutput[i])
            {
                weight1 += adjustmentValue;
                weight2 += adjustmentValue;
            }
            else
            {
                correctTrainingValues++;
                //Debug.Log("Correct Training: " + correctTrainingValues);
                //Debug.Log("i: " + correctTrainingValues);
            }
            trainingAccuracy = (correctTrainingValues / (i + 1)) * 100;
            //Debug.Log("Training Accuracy: " + trainingAccuracy);
        }
        Debug.Log($"Weight1: {weight1}");
        Debug.Log($"Weight2: {weight2}");
        return trainingAccuracy;
    }

    public int calculateOutput(double input1, double input2)
    {
        double initalSum = input1 + input2;
        //Normalize inputs to be between 0 and 1
        /*input1 /= 100;
        input2 /= 100;*/

        double calculation = input1 * weight1 + input2 * weight2;
        //Activation function
        int returnValue = calculation >= 1 ? 1 : 0;
        int correctValue = initalSum >= x ? 1 : 0;
        if (returnValue != correctValue)
        {
            Debug.Log($"Sum {initalSum}, guessed {returnValue}, should have been {correctValue}");
        }
        return returnValue;
    }


}

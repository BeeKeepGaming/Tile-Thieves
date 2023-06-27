/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class NeuralNetworkPlayer : MonoBehaviour
{
    public static NeuralNetworkPlayer instance;
    private NeuralNetwork neuralNetwork;
    [SerializeField] float[] gameBoard;

    [SerializeField] private string trainingDataFileName = "training_data.txt";

    private void Awake()
    {
        instance = this;

        int inputSize = 9; // Size of the input layer
        int hiddenSize = 18; // Size of the hidden layer
        int outputSize = 9; // Size of the output layer

        neuralNetwork = new NeuralNetwork(inputSize, hiddenSize, outputSize);

        // Load the training data
        neuralNetwork.LoadTrainingData(trainingDataFileName);
    }


    public void GetAIMove()
    {
        //Get the current board state
        gameBoard = SpawnBoard.instance.GetBoardState();

        // Make a move based on the output of the neural network
        int move = GetBestMove(gameBoard);

        // Send info to create AI dot on board
        PlaceDot.instance.PlaceAIDot(move);
    }

    private int GetBestMove(float[] boardState)
    {
        // Choose the move with the highest output value from the neural network
        float[] outputs = neuralNetwork.FeedForward(boardState);

        // Find the index of the highest output value for a valid move
        int maxIndex = -1;
        float maxValue = -Mathf.Infinity;

        for (int i = 0; i < outputs.Length; i++)
        {
            if (boardState[i] == 0 && outputs[i] > maxValue)
            {
                maxIndex = i;
                maxValue = outputs[i];
            }
        }

        if (maxIndex == -1)
        {
            Debug.LogError("No valid move found! The AI player cannot make a move.");
        }

        return maxIndex;
    }
}
*/
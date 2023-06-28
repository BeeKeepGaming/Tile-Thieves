/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuralNetworkTrainer : MonoBehaviour
{
    private NeuralNetwork neuralNetwork;

    [SerializeField] int trainingSessions;
    // 
    private void Start()
    {
        int inputSize = 9; // Size of the input layer
        int hiddenSize = 18; // Size of the hidden layer (input + output sizes of game)
        int outputSize = 9; // Size of the output layer

        neuralNetwork = new NeuralNetwork(inputSize, hiddenSize, outputSize);

        // Train the neural network
        TrainNeuralNetwork(trainingSessions);

        // Save the training data
        neuralNetwork.SaveTrainingData("training_data.txt");
    }

    private void TrainNeuralNetwork(int iterations)
    {
        for (int i = 0; i < iterations; i++)
        {
            // Set board state to empty
            float[] boardState = new float[9];

            // Randomly choose which player starts in the training
            int chooseStart = Random.Range(0,2);

            if(chooseStart == 0)
            {
                PlayTrainingGameAIStart(boardState);
            } 
            else
            {
                PlayTrainingGamePlayerStart(boardState);
            }
        }
    }

    void PlayTrainingGameAIStart(float[] boardState)
    {
        int aiMove = -2; // Initialize AIMove variable

        while (!GameWon(boardState, 1) && !GameWon(boardState, -1) && !BoardFull(boardState))
        {
            // Let the AI make a move and update the board state with AI's move
            aiMove = GetBestMove(boardState);
            boardState[aiMove] = 1f;

            if (GameWon(boardState, 1) || BoardFull(boardState))
            {
                break;
            }

            // Simulate opponent's move (random selection) and update the board state with opponent's move
            int opponentMove = GetRandomValidMove(boardState);
            boardState[opponentMove] = -1f;

            if (GameWon(boardState, -1) || BoardFull(boardState))
            {
                break;
            }
        }

        // Assign target outputs for the AI's move
        float[] targetOutputs = new float[boardState.Length];
        targetOutputs[aiMove] = 1f;

        // Train the player with the current board state and target outputs
        neuralNetwork.Train(boardState, targetOutputs);
    }

    void PlayTrainingGamePlayerStart(float[] boardState)
    {
        int aiMove = -1; // Initialize AIMove variable

        while (!GameWon(boardState, 1) && !GameWon(boardState, -1) && !BoardFull(boardState))
        {
            // Simulate opponent's move (random selection) and update the board state with opponent's move
            int opponentMove = GetRandomValidMove(boardState);
            boardState[opponentMove] = -1f;

            if (GameWon(boardState, -1) || BoardFull(boardState))
            {
                break;
            }

            // Let the AI make a move and update the board state with AI's move
            aiMove = GetBestMove(boardState);
            boardState[aiMove] = 1f;

            if (GameWon(boardState, 1) || BoardFull(boardState))
            {
                break;
            }
        }

        // Assign target outputs for the player's move
        float[] targetOutputs = new float[boardState.Length];
        targetOutputs[aiMove] = 1f;

        // Train the player with the current board state and target outputs
        neuralNetwork.Train(boardState, targetOutputs);
    }

    private int GetRandomValidMove(float[] boardState) // For player during training
    {
        int randomIndex;
        do
        {
            randomIndex = Random.Range(0, 9);
        }
        while (boardState[randomIndex] != 0f);

        return randomIndex;
    }

    public int GetBestMove(float[] boardState)
    {
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

    public bool GameWon(float[] _boardState, int playerValue)
    {
        return (_boardState[0] == playerValue && _boardState[1] == playerValue && _boardState[2] == playerValue) ||
               (_boardState[3] == playerValue && _boardState[4] == playerValue && _boardState[5] == playerValue) ||
               (_boardState[6] == playerValue && _boardState[7] == playerValue && _boardState[8] == playerValue) ||
               (_boardState[0] == playerValue && _boardState[3] == playerValue && _boardState[6] == playerValue) ||
               (_boardState[1] == playerValue && _boardState[4] == playerValue && _boardState[7] == playerValue) ||
               (_boardState[2] == playerValue && _boardState[5] == playerValue && _boardState[8] == playerValue) ||
               (_boardState[0] == playerValue && _boardState[4] == playerValue && _boardState[8] == playerValue) ||
               (_boardState[6] == playerValue && _boardState[4] == playerValue && _boardState[2] == playerValue);
    }

    public bool BoardFull(float[] _boardState)
    {
        for (int i = 0; i < 9; i++)
        {
            if (_boardState[i] == 0f)
            {
                return false;
            }
        }
        return true;
    }
}
*/
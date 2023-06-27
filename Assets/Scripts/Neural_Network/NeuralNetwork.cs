using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class NeuralNetwork
{
    private int inputSize; // Size of the input layer
    private int hiddenSize; // Size of the hidden layer (input + output sizes of game)
    private int outputSize; // Size of the output layer
    private float[,] weightsInputHidden;
    private float[,] weightsHiddenOutput;
    private float[] biasesHidden;
    private float[] biasesOutput;
    private float learningRate = 0.01f;

    #region LEARNING RATE INFO
    /* LEARNING RATE */
    /*
        The value of the learning rate is an important hyperparameter that can significantly impact the 
        training process of a neural network. It determines the step size at which the weights and biases 
        are updated during training.

        The appropriate value for the learning rate depends on the specific problem, data, and network 
        architecture. Generally, a learning rate in the range of 0.01 to 0.1 is commonly used as a starting 
        point. 

        Specifically, the learning rate is a configurable hyperparameter used in the training of neural 
        networks that has a small positive value, often in the range between 0.0 and 1.0.

        This means that a learning rate of 0.1, a traditionally common default value, would mean that 
        weights in the network are updated 0.1 * (estimated weight error) or 10% of the estimated weight 
        error each time the weights are updated.

        LINK: https://machinelearningmastery.com/learning-rate-for-deep-learning-neural-networks/
    */
    #endregion
    #region WEIGHTS INFO
    /* WEIGHTS */
    /*
        In a neural network, weights are used to determine the strength and influence of the connections 
        between neurons. The values in these weight matrices control the flow of information and play a 
        crucial role in the network's ability to learn and make accurate predictions. By adjusting these 
        weights during the training process, the neural network can optimize its performance and improve 
        its ability to solve the given problem.

        weightsInputHidden: This variable represents the weights between the input layer and the hidden 
        layer of the neural network. It is a two-dimensional array where each element represents the 
        weight between a specific input neuron and a specific hidden neuron. The first dimension of the 
        array corresponds to the number of neurons in the input layer, and the second dimension corresponds 
        to the number of neurons in the hidden layer.

        weightsHiddenOutput: This variable represents the weights between the hidden layer and the output 
        layer of the neural network. Similar to weightsInputHidden, it is also a two-dimensional array where 
        each element represents the weight between a specific hidden neuron and a specific output neuron. 
        The first dimension of the array corresponds to the number of neurons in the hidden layer, and the 
        second dimension corresponds to the number of neurons in the output layer.

        Random Initialization: One common method is to initialize the weights randomly. You can assign 
        random values from a uniform or normal distribution to the weight matrices. This approach helps 
        introduce randomness and avoids symmetry issues during the training process. The random values 
        should be small to prevent the network from starting with overly large or small weights that could 
        lead to instability.
    */ 
    #endregion
    #region BIASES INFO
    /* BIASES */
    /*
        Biases provide flexibility to neural networks by allowing them to learn and model complex 
        relationships even when the inputs are zero. They act as constant terms that affect the output of 
        each neuron, regardless of the input values. By adjusting the biases during training, the neural 
        network can learn to shift the activation function and make the necessary adjustments to better 
        fit the training data.

        biasesHidden: This variable represents the biases for the hidden layer of the neural network. 
        Biases are additional parameters in a neural network that allow for shifting the activation of 
        each neuron in the layer. The biasesHidden array stores the bias values for each neuron in the 
        hidden layer. The size of this array is equal to the number of neurons in the hidden layer.

        biasesOutput: This variable represents the biases for the output layer of the neural network. 
        Similar to biasesHidden, the biasesOutput array stores the bias values for each neuron in the 
        output layer. The size of this array is equal to the number of neurons in the output layer.

        Small random values: You can generate random values from a uniform distribution within a small range, 
        such as [-0.5, 0.5]. This keeps the initial biases close to zero and avoids initializing them with 
        large values that could lead to unstable behavior in the early stages of training.
    */
    #endregion

    public NeuralNetwork(int _inputSize, int _hiddenSize, int _outputSize)
    {
        inputSize = _inputSize;
        hiddenSize = _hiddenSize;
        outputSize = _outputSize;

        weightsInputHidden = new float[inputSize, hiddenSize];
        weightsHiddenOutput = new float[hiddenSize, outputSize];
        biasesHidden = new float[hiddenSize];
        biasesOutput = new float[outputSize];

        InitializeWeightsAndBiases();
    }

    private void InitializeWeightsAndBiases()
    {
        // Initialize weights with random values
        for (int i = 0; i < inputSize; i++)
        {
            for (int j = 0; j < hiddenSize; j++)
            {
                weightsInputHidden[i, j] = Random.Range(-1f, 1f);
            }
        }

        for (int i = 0; i < hiddenSize; i++)
        {
            for (int j = 0; j < outputSize; j++)
            {
                weightsHiddenOutput[i, j] = Random.Range(-1f, 1f);
            }
        }

        // Initialize biases with random values
        for (int i = 0; i < hiddenSize; i++)
        {
            biasesHidden[i] = Random.Range(-0.5f, 0.5f);
        }

        for (int i = 0; i < outputSize; i++)
        {
            biasesOutput[i] = Random.Range(-0.5f, 0.5f);
        }
    }

    #region FEEDFORWARD INFO
    /*
        The FeedForward method in the NeuralNetwork class performs the forward pass of a neural network. 
        It takes an array of input values and calculates the activations of the hidden layer and the output 
        layer. The hidden layer activations are computed by applying the sigmoid activation function to the 
        weighted sum of the input values multiplied by the corresponding weights and added to the biases. 
        The output layer activations are computed in a similar way, using the activations of the hidden 
        layer as inputs. The method returns an array of output layer activations, representing the network's 
        predictions based on the given input values.
    */
    #endregion
    public float[] FeedForward(float[] inputs)
    {
        float[] hiddenLayer = new float[hiddenSize];
        float[] outputLayer = new float[outputSize];

        // Calculate values of hidden layer
        for (int i = 0; i < hiddenSize; i++)
        {
            float weightedSum = 0f;
            for (int j = 0; j < inputSize; j++)
            {
                weightedSum += inputs[j] * weightsInputHidden[j, i];
            }
            hiddenLayer[i] = Sigmoid(weightedSum + biasesHidden[i]);
        }

        // Calculate values of output layer
        for (int i = 0; i < outputSize; i++)
        {
            float weightedSum = 0f;
            for (int j = 0; j < hiddenSize; j++)
            {
                weightedSum += hiddenLayer[j] * weightsHiddenOutput[j, i];
            }
            outputLayer[i] = Sigmoid(weightedSum + biasesOutput[i]);
        }

        return outputLayer;
    }

    /* Activation Method */
    private float Sigmoid(float x)
    {
        return 1f / (1f + Mathf.Exp(-x));
    }

    #region TRAIN INFO
    /*
        The Train method in the NeuralNetwork class is responsible for training the neural network using 
        backpropagation. It takes an array of input values and an array of target output values, which 
        represent the desired network response for the given input. The method performs a forward pass to 
        calculate the activations of the hidden layer and the output layer. It then computes the errors 
        between the predicted outputs and the target outputs. These errors are used to calculate the errors 
        in the hidden layer. The method then updates the weights and biases of the network based on the 
        errors and the learning rate. The weights are adjusted in proportion to the error and the corresponding 
        activations of the layers involved. The biases are updated directly by adding the product of the 
        learning rate and the error. By repeatedly calling the Train method with different training examples, 
        the network learns to adjust its weights and biases to improve its predictions over time.
    */
    #endregion
    public void Train(float[] inputs, float[] targetOutputs) // Backpropagation
    {
        float[] hiddenLayer = new float[hiddenSize];
        float[] outputLayer = new float[outputSize];

        // Calculate values of hidden layer
        for (int i = 0; i < hiddenSize; i++)
        {
            float weightedSum = 0f;
            for (int j = 0; j < inputSize; j++)
            {
                weightedSum += inputs[j] * weightsInputHidden[j, i];
            }
            hiddenLayer[i] = Sigmoid(weightedSum + biasesHidden[i]);
        }

        // Calculate values of output layer
        for (int i = 0; i < outputSize; i++)
        {
            float weightedSum = 0f;
            for (int j = 0; j < hiddenSize; j++)
            {
                weightedSum += hiddenLayer[j] * weightsHiddenOutput[j, i];
            }
            outputLayer[i] = Sigmoid(weightedSum + biasesOutput[i]);
        }

        // Backpropagation
        float[] outputErrors = new float[outputSize];
        for (int i = 0; i < outputSize; i++)
        {
            outputErrors[i] = targetOutputs[i] - outputLayer[i];
        }

        float[] hiddenErrors = new float[hiddenSize];
        for (int i = 0; i < hiddenSize; i++)
        {
            float error = 0f;
            for (int j = 0; j < outputSize; j++)
            {
                error += outputErrors[j] * weightsHiddenOutput[i, j];
            }
            hiddenErrors[i] = error;
        }

        // Update weights and biases
        for (int i = 0; i < hiddenSize; i++)
        {
            for (int j = 0; j < outputSize; j++)
            {
                weightsHiddenOutput[i, j] += learningRate * outputErrors[j] * hiddenLayer[i];
            }
        }

        for (int i = 0; i < inputSize; i++)
        {
            for (int j = 0; j < hiddenSize; j++)
            {
                weightsInputHidden[i, j] += learningRate * hiddenErrors[j] * inputs[i];
            }
        }

        for (int i = 0; i < outputSize; i++)
        {
            biasesOutput[i] += learningRate * outputErrors[i];
        }

        for (int i = 0; i < hiddenSize; i++)
        {
            biasesHidden[i] += learningRate * hiddenErrors[i];
        }
    }

    public void SaveTrainingData(string fileName)
    {
        string folderPath = "Assets/TrainingData";
        string filePath = Path.Combine(folderPath, fileName);

        // Create the directory if it doesn't exist
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        using (StreamWriter writer = new StreamWriter(filePath))
        {
            // Write the weightsInputHidden
            for (int i = 0; i < weightsInputHidden.GetLength(0); i++)
            {
                for (int j = 0; j < weightsInputHidden.GetLength(1); j++)
                {
                    writer.Write(weightsInputHidden[i, j] + " / ");
                }
                writer.WriteLine();
            }

            // Write the weightsHiddenOutput
            for (int i = 0; i < weightsHiddenOutput.GetLength(0); i++)
            {
                for (int j = 0; j < weightsHiddenOutput.GetLength(1); j++)
                {
                    writer.Write(weightsHiddenOutput[i, j] + " | ");
                }
                writer.WriteLine();
            }

            // Write the biasesHidden
            for (int i = 0; i < biasesHidden.Length; i++)
            {
                writer.Write(biasesHidden[i] + " / ");
            }
            writer.WriteLine();

            // Write the biasesOutput
            for (int i = 0; i < biasesOutput.Length; i++)
            {
                writer.Write(biasesOutput[i] + " | ");
            }
            writer.WriteLine();
        }

        Debug.Log("Training data saved to: " + filePath);
    }

    public void LoadTrainingData(string fileName)
    {
        string folderPath = "Assets/TrainingData";
        string filePath = Path.Combine(folderPath, fileName);

        if (File.Exists(filePath))
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                // Read the weightsInputHidden
                for (int i = 0; i < weightsInputHidden.GetLength(0); i++)
                {
                    string[] weights = reader.ReadLine().Split(" / ");
                    for (int j = 0; j < weightsInputHidden.GetLength(1); j++)
                    {
                        float weight = float.Parse(weights[j]);
                        weightsInputHidden[i, j] = weight;
                    }
                }

                // Read the weightsHiddenOutput
                for (int i = 0; i < weightsHiddenOutput.GetLength(0); i++)
                {
                    string[] weights = reader.ReadLine().Split(" | ");
                    for (int j = 0; j < weightsHiddenOutput.GetLength(1); j++)
                    {
                        float weight = float.Parse(weights[j]);
                        weightsHiddenOutput[i, j] = weight;
                    }
                }

                // Read the biasesHidden
                string[] biasHiddenValues = reader.ReadLine().Split(" / ");
                for (int i = 0; i < biasesHidden.Length; i++)
                {
                    float bias = float.Parse(biasHiddenValues[i]);
                    biasesHidden[i] = bias;
                }

                // Read the biasesOutput
                string[] biasOutputValues = reader.ReadLine().Split(" | ");
                for (int i = 0; i < biasesOutput.Length; i++)
                {
                    float bias = float.Parse(biasOutputValues[i]);
                    biasesOutput[i] = bias;
                }
            }

            Debug.Log("Training data loaded from: " + filePath);
        }
        else
        {
            Debug.LogError("Training data file not found: " + filePath);
        }
    }
}

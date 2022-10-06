using System.Collections.Generic;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace RocketAI.AI
{
    /// <summary>
    /// Implementation of a feedforward neural network, for use with the rockets.
    /// </summary>
    public class NeuralNetwork
    {
        /// </summary>
        /// Tracks the neural networks.
        /// <summary>
        internal static Dictionary<int, NeuralNetwork> s_networks = new();

        /// <summary>
        /// The "id" (index) of the brain, should also align to the "id" of the rocket it is attached.
        /// </summary>
        internal int Id;

        /// <summary>
        /// How many layers of neurons (3+). Do not do 2 or 1.
        /// 2 => input connected to output.
        /// 1 => input is output, and feed forward will crash.
        /// Initializing network to the right size.
        /// INPUT: xaccel, yaccel, xspeed, yspeed, angle, alt etc.
        /// OUTPUT: angle, thrust
        /// </summary>
        public static int[] Layers { get; set; } = new int[3] { 7, 7, 2 };

        /// <summary>
        /// The neurons.
        /// [layer][neuron]
        /// </summary>
        internal double[][] Neurons;

        /// <summary>
        /// NN Biases. Either improves or lowers the chance of this neuron fully firing.
        /// [layer][neuron]
        /// </summary>
        internal double[][] Biases;

        /// <summary>
        /// NN weights. Reduces or amplifies the output for the relationship between neurons in each layer
        /// [layer][neuron][neuron]
        /// </summary>
        internal double[][][] Weights;

        /// <summary>
        /// Indicator for how fit this NN is for the purpose.
        /// </summary>
        internal float Fitness = 0;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id">Unique ID of the neuron.</param>
        /// <param name="layerDefinition">Defines size of the layers.</param>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Init*() set the fields.
        internal NeuralNetwork(int id, int[] layerDefinition, bool addToList = true)
#pragma warning restore CS8618
        {
            // (1) INPUT (2) HIDDEN (3) OUTPUT. Less than 3 would be INPUT->OUTPUT, not a useful neural network
            if (layerDefinition.Length < 2) throw new ArgumentException(nameof(layerDefinition)+" length <2 makes no sense.");

            Id = id; // used to reference this network

            // copy layerDefinition to Layers; although for rockets this must not change.     
            Layers = new int[layerDefinition.Length];

            for (int layer = 0; layer < layerDefinition.Length; layer++)
            {
                Layers[layer] = layerDefinition[layer];
            }

            // if layerDefinition is [2,3,2] then...
            // 
            // Neurons :      (o) (o)    <-2  INPUT
            //              (o) (o) (o)  <-3
            //                (o) (o)    <-2  OUTPUT
            //

            InitialiseNeurons();
            InitialiseBiases();
            InitialiseWeights();

            // track all the neurons we created
            if (addToList)
            {
                if (!s_networks.ContainsKey(Id)) s_networks.Add(Id, this); else s_networks[Id] = this;
            }
        }

        /// <summary>
        /// Create empty storage array for the neurons in the network.
        /// </summary>
        private void InitialiseNeurons()
        {
            List<double[]> neuronsList = new();

            // if layerDefinition is [2,3,2] ..   float[]
            // Neurons :      (o) (o)    <-2  ... [ 0, 0 ]
            //              (o) (o) (o)  <-3  ... [ 0, 0, 0 ]
            //                (o) (o)    <-2  ... [ 0, 0 ]
            //

            for (int layer = 0; layer < Layers.Length; layer++)
            {
                neuronsList.Add(new double[Layers[layer]]);
            }

            Neurons = neuronsList.ToArray();
        }

        /// <summary>
        /// Generate a random number between -0.5...+0.5.
        /// </summary>
        /// <returns></returns>
        private static float RandomFloatBetweenMinusHalfToPlusHalf()
        {
            return (float)(RandomNumberGenerator.GetInt32(0, 1000) - 500) / 1000;
        }

        /// <summary>
        /// initializes and populates biases.
        /// </summary>
        private void InitialiseBiases()
        {
            List<double[]> biasList = new();

            // for each layer of neurons, we have to set biases.
            for (int layer = 0; layer < Layers.Length; layer++)
            {
                double[] bias = new double[Layers[layer]];

                for (int biasLayer = 0; biasLayer < Layers[layer]; biasLayer++)
                {
                    bias[biasLayer] = 0; // there is debate over 0 vs. random -  RandomFloatBetweenMinusHalfToPlusHalf();
                }

                biasList.Add(bias);
            }

            Biases = biasList.ToArray();
        }

        /// <summary>
        /// initializes random array for the weights being held in the network.
        /// </summary>
        private void InitialiseWeights()
        {
            List<double[][]> weightsList = new(); // used to construct weights, as dynamic arrays aren't supported

            for (int layer = 1; layer < Layers.Length; layer++)
            {
                List<double[]> layerWeightsList = new();

                int neuronsInPreviousLayer = Layers[layer - 1];

                for (int neuronIndexInLayer = 0; neuronIndexInLayer < Neurons[layer].Length; neuronIndexInLayer++)
                {
                    double[] neuronWeights = new double[neuronsInPreviousLayer];

                    for (int neuronIndexInPreviousLayer = 0; neuronIndexInPreviousLayer < neuronsInPreviousLayer; neuronIndexInPreviousLayer++)
                    {
                        neuronWeights[neuronIndexInPreviousLayer] = RandomFloatBetweenMinusHalfToPlusHalf();
                    }

                    layerWeightsList.Add(neuronWeights);
                }

                weightsList.Add(layerWeightsList.ToArray());
            }

            Weights = weightsList.ToArray();
        }

        /// <summary>
        /// Feed forward, inputs >==> outputs.
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns></returns>
        internal double[] FeedForward(double[] inputs)
        {
            // put the INPUT values into layer 0 neurons
            for (int i = 0; i < inputs.Length; i++)
            {
                Neurons[0][i] = inputs[i];
            }

            // we start on layer 1 as we are computing values from prior layers (layer 0 is inputs)
            for (int layer = 1; layer < Layers.Length; layer++)
            {
                for (int neuronIndexForLayer = 0; neuronIndexForLayer < Neurons[layer].Length; neuronIndexForLayer++)
                {
                    // sum of outputs from the previous layer
                    double value = 0F;

                    for (int neuronIndexInPreviousLayer = 0; neuronIndexInPreviousLayer < Neurons[layer - 1].Length; neuronIndexInPreviousLayer++)
                    {
                        // remember: the "weight" amplifies or reduces, so we take the output of the prior neuron and "amplify/reduce" it's output here
                        value += Weights[layer - 1][neuronIndexForLayer][neuronIndexInPreviousLayer] * Neurons[layer - 1][neuronIndexInPreviousLayer];
                    }

                    // any neuron fires or not based on the input. The point of a bias is to move the activation up or down.
                    // e.g. the value could be 0.3, adding a bias of 0.5 takes it to 0.8. You might think why not just use the weights to achieve this
                    // but remember weights are individual per prior layer neurons, the bias affects the SUM() of them.

                    // Activate is TANH         1_       ___
                    // (hyperbolic tangent)     0_      /
                    //                         -1_  ___/
                    //                                | | |
                    //                     -infinity -2 0 2..infinity
                    //                               
                    // i.e. TANH flatters any value to between -1 and +1.
                    Neurons[layer][neuronIndexForLayer] = Math.Tanh(value + Biases[layer][neuronIndexForLayer]);
                }
            }

            return Neurons[^1]; // final* layer contains OUTPUT
        }

        /// <summary>
        /// A simple mutation function for any genetic implementations, ensuring it DOES mutate.
        /// </summary>
        /// <param name="pctChance"></param>
        /// <param name="val"></param>
        internal void Mutate(int pctChance, float val)
        {
            bool mutated = false;

            while (!mutated) // ensure SOMETHING changes, otherwise we'll get two identical rockets.
            {
                for (int layerIndex = 0; layerIndex < Biases.Length; layerIndex++)
                {
                    for (int neuronIndex = 0; neuronIndex < Biases[layerIndex].Length; neuronIndex++)
                    {
                        if (RandomNumberGenerator.GetInt32(0, 100) <= pctChance)
                        {
                            mutated = true;
                            Biases[layerIndex][neuronIndex] += (float)(RandomNumberGenerator.GetInt32((int)(-val * 10000), (int)(val * 10000))) / 20000;
                        }
                    }
                }

                for (int layerIndex = 0; layerIndex < Weights.Length; layerIndex++)
                {
                    for (int neuronIndexForLayer = 0; neuronIndexForLayer < Weights[layerIndex].Length; neuronIndexForLayer++)
                    {
                        for (int neuronIndexInPreviousLayer = 0; neuronIndexInPreviousLayer < Weights[layerIndex][neuronIndexForLayer].Length; neuronIndexInPreviousLayer++)
                        {
                            if (RandomNumberGenerator.GetInt32(0, 100) <= pctChance)
                            {
                                mutated = true;
                                Weights[layerIndex][neuronIndexForLayer][neuronIndexInPreviousLayer] += (float)(RandomNumberGenerator.GetInt32((int)(-val * 10000), (int)(val * 10000))) / 20000;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Sorts the network so fitter AI networks appear at the bottom.
        /// </summary>
        internal static void SortNetworkByFitness()
        {
            s_networks = s_networks.OrderBy(x => x.Value.Fitness).ToDictionary(x => x.Key, x => x.Value);
        }

        /// <summary>
        /// Copies from one NN to another
        /// </summary>
        /// <param name="neuralNetworkToCloneFrom"></param>
        /// <param name="neuralNetworkCloneTo"></param>
        internal static void CopyFromTo(NeuralNetwork neuralNetworkToCloneFrom, NeuralNetwork neuralNetworkCloneTo)
        {
            for (int layerIndex = 0; layerIndex < neuralNetworkToCloneFrom.Biases.Length; layerIndex++)
            {
                for (int neuronIndex = 0; neuronIndex < neuralNetworkToCloneFrom.Biases[layerIndex].Length; neuronIndex++)
                {
                    neuralNetworkCloneTo.Biases[layerIndex][neuronIndex] = neuralNetworkToCloneFrom.Biases[layerIndex][neuronIndex];
                }
            }

            for (int layerIndex = 0; layerIndex < neuralNetworkToCloneFrom.Weights.Length; layerIndex++)
            {
                for (int neuronIndexInLayer = 0; neuronIndexInLayer < neuralNetworkToCloneFrom.Weights[layerIndex].Length; neuronIndexInLayer++)
                {
                    for (int neuronIndexInPreviousLayer = 0; neuronIndexInPreviousLayer < neuralNetworkToCloneFrom.Weights[layerIndex][neuronIndexInLayer].Length; neuronIndexInPreviousLayer++)
                    {
                        neuralNetworkCloneTo.Weights[layerIndex][neuronIndexInLayer][neuronIndexInPreviousLayer] = neuralNetworkToCloneFrom.Weights[layerIndex][neuronIndexInLayer][neuronIndexInPreviousLayer];
                    }
                }
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuralNetwork
{
    readonly int [] _net_dimensions;  // amount of neurons in each layer
    List<List<List<float>>> weights;  // weigth-values for each connection from one layer to the next

    const float E = 2.718281828f;
    //int[] biases;


    public NeuralNetwork(int[] dimensions)
    {
        _net_dimensions = dimensions;
        weights = new List<List<List<float>>>();
        InitializeWeights();
        // OutputWeights();
        List<float> inp = new List<float>();
        inp.Add(1f);
        inp.Add(0.4f);
        inp.Add(0f);
        FeedForward(inp);
    }


    void InitializeWeights()
    {
        // initialize random weights - range(-1 <=> 1)
        for (int layer = 0; layer < _net_dimensions.Length-1; layer++)
        {
            List<List<float>> current_layer = new List<List<float>>();
            for (int neuron = 0; neuron < _net_dimensions[layer]; neuron++)
            {
                List<float> current_neuron = new List<float>();
                for (int next_neuron = 0; next_neuron < _net_dimensions[layer + 1]; next_neuron++)
                {
                    current_neuron.Add(Random.Range(-1f,1f));
                }
                current_layer.Add(current_neuron);
            }
            weights.Add(current_layer);
        }
    }


    List<float> ListDot(List<float> input_activations, List<List<float>> weight_layer)
    {
        List<float> dot_product = new List<float>();
        for (int to_neuron_n = 0; to_neuron_n < weight_layer[0].Count; to_neuron_n++)
        {
            dot_product.Add(0);
            for (int from_neuron_n = 0; from_neuron_n < input_activations.Count; from_neuron_n++)
            {
                dot_product[to_neuron_n] += input_activations[from_neuron_n] * weight_layer[from_neuron_n][to_neuron_n];
            }
        }
        return dot_product;
    }


    List<float> ActivationFunction(List<float> layer)
    {
        for (int neuron=0; neuron < layer.Count; neuron++)
        {
            layer[neuron] = 1 / (1 + Mathf.Pow(E, -layer[neuron]));
        }
        return layer;
    }


    public List<float> FeedForward(List<float> input)
    {
        List<float> layer_input = input;

        for (int layer=0; layer < weights.Count; layer++)
        {
            layer_input = ListDot(layer_input, weights[layer]);
            layer_input = ActivationFunction(layer_input);
        }
        return layer_input;
    }




    void OutputWeights()
    {
        for (int y=0; y<weights.Count; y++)
        {
            for (int x = 0; x < weights[y].Count; x++)
            {
                Debug.Log(weights[y][x][0]);
            }
            Debug.Log("NEW LAYER");
        }
    }

}

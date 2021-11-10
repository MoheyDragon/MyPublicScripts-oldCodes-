using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class card : MonoBehaviour
{
    public Card_System cardObject;
    [HideInInspector]
    public Sprite img;
    [HideInInspector]
    public int inputNumber,outputNumber;
    [HideInInspector]
    public bool[] connected;
    GameObject[] inputPrefabs,outputPrefabs;
    [HideInInspector]
    public int[] inputs, outputs;
    TextMeshPro cardName;
    Vector3 mOffset;float mZcoord;new Camera camera;

    void Start()
    {
        camera = Camera.main;
        inputPrefabs = cardObject.inputPrefabs; outputPrefabs = cardObject.outputPrefabs;
        inputNumber = inputPrefabs.Count();
        outputNumber = outputPrefabs.Count();
        inputs = new int[inputNumber];
        outputs = new int[outputNumber];
        connected = new bool[outputNumber];
        int counter = 0;
        foreach (Transform spawner in transform.Find("Inputs").transform)
        {
            if (spawner.tag=="input"&&counter<inputNumber)
            {
                Instantiate(inputPrefabs[counter], spawner.transform);
                inputs[counter] = inputPrefabs[counter].GetComponent<input>().type;
                counter++;
            }
        }
        counter = 0;
        foreach (Transform spawner in transform.Find("Outputs").transform)
        {
            if (spawner.tag == "output" && counter < outputNumber)
            {
                outputPrefabs[counter].GetComponent<Output>().id = counter;
                connected[counter] = false;
                Instantiate(outputPrefabs[counter], spawner.transform);
                outputs[counter] = outputPrefabs[counter].GetComponent<Output>().type;
                counter++;
            }
        }
        foreach (Transform child in transform)
        {
            if (cardName == null)
            {
                if (child.tag == "Text")
                {
                    cardName = child.gameObject.GetComponent<TextMeshPro>();
                    cardName.text = cardObject.name;
                }
            }
        }
        img = cardObject.img;
    }
    public bool CheckConnection()
    {
        foreach (bool i in connected)
        {
            if (i==true)
            {
                return true;
            }
        }
        return false;
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeManager : MonoBehaviour
{
    [SerializeField]
    Transform[] Flees;
    [HideInInspector]
    public Vector3[] FleeTargets;
    private void Start()
    {
        FleeTargets = new Vector3[Flees.Length];
        foreach (Transform target in Flees)
        {
            FleeTargets[target.GetSiblingIndex()] = Flees[target.GetSiblingIndex()].position;
        }
    }
}

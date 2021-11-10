using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSwitcher : MonoBehaviour
{
    public AK.Wwise.Switch floorType;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player")
        {
            floorType.SetValue(other.gameObject);
        }
    }
}

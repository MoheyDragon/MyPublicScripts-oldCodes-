using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RammingFront : MonoBehaviour
{
    public void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Front")
        {
            Debug.Log(transform.parent.name + " Has Hit " + col.transform.parent + " and his health is " + col.GetComponentInParent<ShipMain>().Health);
            col.GetComponentInParent<ShipMain>().Damage(transform.GetComponentInParent<ShipMain>().RammingFrontPower);
            GetComponentInParent<ShipMain>().Damage(col.GetComponentInParent<ShipMain>().RammingFrontPower);
        }
        if (col.tag == "Side")
        {
            Debug.Log(transform.parent.name + " Has Hit " + col.transform.parent + " and his health is " + col.GetComponentInParent<ShipMain>().Health);
            col.GetComponentInParent<ShipMain>().Damage(transform.GetComponentInParent<ShipMain>().RammingFrontPower);
            GetComponentInParent<ShipMain>().Damage(transform.GetComponentInParent<ShipMain>().RammingFrontPower / 10);
        }

        if (col.tag == "Back")
        {
            Debug.Log(transform.parent.name + " Has Hit " + col.transform.parent + " and his health is " + col.GetComponentInParent<ShipMain>().Health);
            col.GetComponentInParent<ShipMain>().Damage(transform.GetComponentInParent<ShipMain>().RammingFrontPower*2);
            GetComponentInParent<ShipMain>().Damage(transform.GetComponentInParent<ShipMain>().RammingSidePower / 10);
        }
    }
}

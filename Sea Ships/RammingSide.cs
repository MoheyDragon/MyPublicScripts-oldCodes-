using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RammingSide : MonoBehaviour
{
    public void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Side")
        {
            Debug.Log(transform.parent.name + " Has Hit " + col.transform.parent + " and his health is " + col.GetComponentInParent<ShipMain>().Health);
            col.GetComponentInParent<ShipMain>().Damage(transform.GetComponentInParent<ShipMain>().RammingSidePower);
            GetComponentInParent<ShipMain>().Damage(transform.GetComponentInParent<ShipMain>().RammingSidePower);
        }
        if (col.tag=="Back")
        {
            Debug.Log(transform.parent.name + " Has Hit " + col.transform.parent + " and his health is " + col.GetComponentInParent<ShipMain>().Health);
            col.GetComponentInParent<ShipMain>().Damage(transform.GetComponentInParent<ShipMain>().RammingSidePower);
            GetComponentInParent<ShipMain>().Damage(col.GetComponentInParent<ShipMain>().RammingSidePower);
        }
    }
}

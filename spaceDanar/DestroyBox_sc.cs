using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBox_sc : MonoBehaviour {

	void OnTriggerExit2D (Collider2D other)
    {
        Destroy(other.gameObject);
    }
}

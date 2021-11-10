using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMangerTrainingLevel : MonoBehaviour
{
    Camera cam;public GameObject Tries;
    // Start is called before the first frame update
    void Awake()
    {
        cam = Camera.main;
        cam.GetComponent<Camera_Controller>().FollowPlayer();
        CharecterController.Level = 6;
    }
    private void Start()
    {
        Tries.SetActive(false);
    }
}

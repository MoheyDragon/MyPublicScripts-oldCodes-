using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Island : MonoBehaviour
{
    CharacterController controller;
    int value,islandsindex;
    GameObject picture;
    MeshRenderer mat;
    Vector3 MoveDirection;
    const int spare= -700;
    Main father;
    const int speedValue = 50;
    private void Awake()
    {
        father = GameObject.FindGameObjectWithTag("ship").GetComponent<Main>();
        picture = transform.GetChild(0).GetChild(0).gameObject;
        mat = picture.GetComponent<MeshRenderer>();
        picture.SetActive(false);
        controller = GetComponent<CharacterController>();
    }
    void Update()
    {
        if (father.isMoving)
        {
            if (father.speed==0)
                MoveDirection = new Vector3(0, 0, -0.5f* speedValue);
            else
            MoveDirection = new Vector3(0, 0, -father.speed*speedValue);
            controller.Move(MoveDirection * Time.deltaTime);
            if (transform.position.z < spare)
                transform.position = father.startPoint.position;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ship")
        {
            picture.SetActive(true);
            father.checkforend();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ship"))
        {
                mat.material = other.GetComponent<Main>().ChangePicture();
                picture.SetActive(false);
        }
    }
}

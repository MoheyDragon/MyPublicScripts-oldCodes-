using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Camera_Controller : MonoBehaviour
{
    Transform target;
    Vector3 offset;
    float pitch = 2.31f;
    float CurrentYaw = 95.14f; float CurrentHorzintalYaw = 0f; float MaxYaw;
    float ZoomSpeed = 4f;
    float minZoom = 2;
    float maxZoom = 6;
    public static float CurrentZoom = 8f;
    private void Start()
    {
        offset = new Vector3(1.54f, -1.02f, 0.97f);
        MaxYaw = -60f;
    }
    void Update()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        CurrentZoom -= Input.GetAxis("Mouse ScrollWheel") * ZoomSpeed;
            CurrentZoom = Mathf.Clamp(CurrentZoom, minZoom, maxZoom);
            CurrentYaw += Input.GetAxis("Mouse X") * target.GetComponent<CharecterController>().RotateSpeed * Time.deltaTime;
            CurrentHorzintalYaw -= Input.GetAxis("Mouse Y") * target.GetComponent<CharecterController>().RotateSpeed * Time.deltaTime;
            CurrentHorzintalYaw = Mathf.Clamp(CurrentHorzintalYaw, MaxYaw, 60f);
                target.Rotate(0, Input.GetAxis("Mouse X") * target.GetComponent<CharecterController>().RotateSpeed * Time.deltaTime, 0);
        
    }
    void LateUpdate()
    {
            if (target != null)
            {
                transform.position = target.position - offset * CurrentZoom;
                transform.LookAt(target.position + Vector3.up * pitch);
                transform.RotateAround(target.position, Vector3.up, CurrentYaw);
                transform.RotateAround(target.position, transform.right, CurrentHorzintalYaw);
            }
    }
}

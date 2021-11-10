using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    public Transform target, Front;
    public Vector3 offset;
    public float pitch = 2f;
    private float CurrentYaw = 0f;
    public float YawSpeed = 100f;
    public float ZoomSpeed = 4f;
    public float minZoom = 5f;
    public float maxZoom = 15f;
    private float CurrentZoom = 10f;



    void Update()
    {
        CurrentZoom -= Input.GetAxis("Mouse ScrollWheel") * ZoomSpeed;
        CurrentZoom = Mathf.Clamp(CurrentZoom, minZoom, maxZoom);
        CurrentYaw += Input.GetAxis("Mouse X") * YawSpeed * Time.deltaTime;
        if (CurrentZoom<=7&& offset.y<-0.25)
        {
            offset.y = offset.y +0.1f;
            
        }
        else if ( CurrentZoom>7&&offset.y> -0.8f)
        {
            offset.y -=0.1f;
        }
        

    }
    void LateUpdate()
    {
        if (CurrentZoom > 2)
        {
            transform.position = target.position - offset * CurrentZoom;
            transform.LookAt(target.position + Vector3.up * pitch);
            transform.RotateAround(target.position, Vector3.up, CurrentYaw);
        }
        if (CurrentZoom <= 2)
        {
            transform.position = Front.position - offset * CurrentZoom;
            transform.LookAt(Front.position + Vector3.up * pitch);
            transform.RotateAround(Front.position, Vector3.up, CurrentYaw);
        }
        

    }
}
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
    public static GameObject CameraSkip;
    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Level3")
        {
            this.GetComponent<Animator>().SetBool("CutScene", true);
        }
        offset = new Vector3(1.54f, -1.02f, 0.97f);
        try
        {
            CameraSkip = GameObject.FindGameObjectWithTag("CameraSkip");
        }
        catch (System.Exception)
        {
            CameraSkip = null;
            throw;
        }
        if (SceneManager.GetActiveScene().name == "TrainingLevel") MaxYaw = -25f;
        else MaxYaw = -60f;
    }
    void Update()
    {
        if (!CharecterController.cutscene&&!Menu.Pause)
        {
            CurrentZoom -= Input.GetAxis("Mouse ScrollWheel") * ZoomSpeed;
            CurrentZoom = Mathf.Clamp(CurrentZoom, minZoom, maxZoom);
            CurrentYaw += Input.GetAxis("Mouse X") * target.GetComponent<CharecterController>().RotateSpeed * Time.deltaTime;
            CurrentHorzintalYaw -= Input.GetAxis("Mouse Y") * target.GetComponent<CharecterController>().RotateSpeed * Time.deltaTime;
            CurrentHorzintalYaw = Mathf.Clamp(CurrentHorzintalYaw, MaxYaw, 60f);
            if (CharecterController.ControlMethode == 2)
                target.Rotate(0, Input.GetAxis("Mouse X") * target.GetComponent<CharecterController>().RotateSpeed * Time.deltaTime, 0);
        }
        if (SceneManager.GetActiveScene().name == "Level3" && Input.GetKeyDown(KeyCode.Space))
        {
            if (!Menu.Pause)
            {
                if (!SceneManger3.SkipLock)
                {
                    this.GetComponent<Animator>().SetBool("CutScene", false);
                    FollowPlayer();
                    CameraSkip.SetActive(false);
                }
            }
        }
    }
    void LateUpdate()
    {
        if (!CharecterController.cutscene)
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
    public void FollowPlayer()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        CharecterController.cutscene = false;
        if (SceneManager.GetActiveScene().name == "Level3")
        {
            this.GetComponent<Animator>().SetBool("CutScene", false);
            CameraSkip.SetActive(false);
            SceneManger3.SkipLock = true;
        }
        target.GetComponent<CharecterController>().StartCoroutine("Fix");
    }
    public void Control2()
    {
        try
        {
            target.forward = transform.forward;
            var rot = target.rotation;
            rot.x = rot.z = 0;
            target.rotation = rot;
        }
        catch (System.Exception)
        {
            return;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragger : MonoBehaviour
{
    private Vector3 _dragOffset;
    private Camera _cam;
    bool IsSelected,IsInPlace;
    float _speed = 1000;
    int value;
    Vector3 offset=new Vector3(-80,-80,0);
    void Awake()
    {
        _cam = Camera.main;
    }
    private void Start()
    {
        IsInPlace = false;
        value = transform.GetSiblingIndex();
    }

    void OnMouseDown()
    {
        if (!IsInPlace)
                _dragOffset = transform.position - GetMousePos();
    }
    void OnMouseUp()
    {
        if (!IsInPlace)
        {
                Collider[] hitColliders = Physics.OverlapSphere(GetMousePos(), 0.4f);
                foreach (var hitCollider in hitColliders)
                {
                    if (hitCollider.CompareTag("slot"))
                    {
                        Debug.Log(hitCollider.transform.GetSiblingIndex());
                        if (hitCollider.transform.GetSiblingIndex() == value)
                        {
                            IsInPlace = true;
                            Destroy(gameObject.GetComponent<BoxCollider2D>());
                            transform.position = hitCollider.transform.position+offset;
                            PuzzelCutter.PiecesInPlace++;
                        if (PuzzelCutter.PiecesInPlace==9)
                            Debug.Log("Win");
                        }
                    }
                }
        }


    }

    void OnMouseDrag()
    {
        if (!IsInPlace)
        transform.position = Vector3.MoveTowards(transform.position, GetMousePos() + _dragOffset, _speed * Time.deltaTime);
    }
    Vector3 GetMousePos()
    {
        var mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        return mousePos;
    }
}

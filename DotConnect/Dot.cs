using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Dot : MonoBehaviour
{
    int type;
    Vector3 startPos;
    Vector3 endPos;
    LineRenderer lr;Material mat; Color color;
    [HideInInspector]
    public int id;
    [HideInInspector]
    public bool checker=true;
    levelManager manager;
    Camera cam;
    public static Dot hit;
    public static bool ClickUpWrong,IsAlt;
    // Start is called before the first frame update
    void Start()
    {
        color = GetComponent<SpriteRenderer>().color;
        mat = new Material(Shader.Find("Sprites/Default"));
        cam = Camera.main;
        startPos = new Vector3(transform.position.x, transform.position.y,0);
        lr = gameObject.AddComponent<LineRenderer>();
        lr.material = mat;
        lr.startColor = color;lr.endColor = color;
        lr.startWidth = 0.23f; lr.endWidth = 0.23f;
        lr.numCapVertices = 10;lr.numCornerVertices = 10;
        lr.positionCount = 2;
        lr.SetPosition(0, startPos);
        lr.enabled = false;
        lr.sortingOrder= 1;
        id = transform.GetSiblingIndex();
        ClickUpWrong = false;
        type = transform.parent.GetSiblingIndex();
        manager = transform.parent.GetComponentInParent<levelManager>();
        IsAlt = false;
    }
    private void OnMouseUp()
    {
        if (hit.checker)
             hit.lr.enabled = false;
    }
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (hit.type == type)
            {
                if (Mathf.Abs(hit.id - id) == 1)
                {
                    IsAlt = false;
                    if (id > hit.id && !manager.Checkers[type].DotsConnected[hit.id])
                    {
                        CheckIn(hit.id,type,false);
                        
                    }
                    else if (id < hit.id && !manager.Checkers[type].DotsConnected[id])
                    {
                        lr.SetPosition(1, hit.startPos);
                        hit.lr.enabled = false;
                        hit = transform.parent.GetChild(id).GetComponent<Dot>();
                        CheckIn(id, type,true);
                    }
                    else
                        return;
                }
                else if (hit.id == id&&IsAlt)
                {
                    if (!manager.Checkers[type].DotsConnected[id])
                    {
                        lr.SetPosition(1, hit.startPos);
                        hit.lr.enabled = false;
                        hit = transform.parent.GetChild(id).GetComponent<Dot>();
                        CheckIn(id, type, true);
                    }
                    IsAlt = false;
                }
                else if (!IsAlt)
                {
                    if ((id == 0 && hit.id == manager.Checkers[type].DotsCount - 1) || (id == manager.Checkers[type].DotsCount - 1 && hit.id == 0) && !manager.Checkers[type].DotsConnected[manager.Checkers[type].DotsCount - 1])
                    {
                        IsAlt = false;
                        if (id != 0)
                        {
                            lr.SetPosition(1, hit.startPos);
                            hit.lr.enabled = false;
                            hit = transform.parent.GetChild(id).GetComponent<Dot>();
                            CheckIn(manager.Checkers[type].DotsCount - 1, type, true);
                        }
                        else
                            CheckIn(manager.Checkers[type].DotsCount - 1, type, false);
                    }
                }
                else
                    return;
            }

        }
    }
    private void OnMouseDown()
    {
        if ((!manager.Checkers[type].firstMove && (id != manager.Checkers[type].currentId&& id != manager.Checkers[type].altCurrentId)) || manager.Checkers[type].Finished)
        {
            IsAlt = false;
            checker = false;
            hit = null;
            return;
        }
        if (id==manager.Checkers[type].altCurrentId)
        {
            if (manager.Checkers[type].altCurrentId==0)
                hit = transform.parent.GetChild(manager.Checkers[type].DotsCount-1).GetComponent<Dot>();
            else
                hit = transform.parent.GetChild(manager.Checkers[type].altCurrentId - 1).GetComponent<Dot>();
            IsAlt = true;
            hit.lr.SetPosition(0, startPos);
            hit.checker = true;
            hit.lr.enabled = true;
        }
        else
        {
            IsAlt = false;
            hit = this;
            hit.checker = true;
            hit.lr.enabled = true;
        }
    }
    private void OnMouseDrag()
    {
        if (hit.checker)
        {
            endPos= cam.ScreenToWorldPoint(Input.mousePosition);
            hit.lr.SetPosition(1, endPos);
        }
    }
    void CheckIn(int _id,int type,bool IsAlt)
    {
        manager.NextCheckIn(_id,type,IsAlt);
        if (IsAlt)
        {
            lr.enabled = true;
            hit.checker = false;
        }
        else
        {
            hit.lr.SetPosition(1, startPos);
            hit.lr.enabled = true;
            hit.checker = false;
        }
    }
}

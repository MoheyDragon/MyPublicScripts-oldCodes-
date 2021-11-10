using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    int Woods;
    public GameObject Inven;
    public Text building, woodAmount;
    Transform CamChild;
    RaycastHit hit;
    [SerializeField]
    Transform tentPlaceHolder;
    public GameObject tentprefab;
    bool isbuilding = false;
    CharecterController player;
    // Start is called before the first frame update
    private void Start()
    {
        player = gameObject.GetComponent<CharecterController>();
        CamChild = Camera.main.transform.GetChild(0);
    }
    public void Collect(int type,int amount)
    {
        player.collect.Post(gameObject);
        if (type==0)
            Woods += amount;
        woodAmount.text = (Woods).ToString();
        if (Woods>=25)
        {
            building.color = Color.green;
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Inven.SetActive(!Inven.activeSelf);
            if (Inven.activeSelf)
            {
                Time.timeScale = 0;
            }
            else
                Time.timeScale = 1;
        }
        if (isbuilding)
        {
            Time.timeScale = 1;
            Camera_Controller.CurrentZoom = 2;
            if (Physics.Raycast(CamChild.position, CamChild.forward, out hit, 20))
            {
                tentPlaceHolder.position = new Vector3(Mathf.RoundToInt(hit.point.x), Mathf.RoundToInt(hit.point.y), Mathf.RoundToInt(hit.point.z));
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                player.build.Post(gameObject);
                Instantiate(tentprefab, tentPlaceHolder.position, Quaternion.identity);
                Woods -= 25;
                if (Woods < 25)
                {
                    building.color = Color.red;
                }
                tentPlaceHolder.gameObject.SetActive(false);
                woodAmount.text = Woods.ToString();
                isbuilding = false;
            }
        }
    }
    public void Build()
    {
        if (Woods<25)
            return;
            Inven.SetActive(false);
        tentPlaceHolder.gameObject.SetActive(true);
        isbuilding = true;
    }
}

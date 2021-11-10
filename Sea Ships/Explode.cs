using UnityEngine;
public class Explode : MonoBehaviour
{
    public int Power;
    public int Chance;

    // Update is called once per frame
    void Update()
    {
        transform.localScale += new Vector3(1,1,1);
        Destroy(gameObject, 0.5f);
    }
     void OnTriggerEnter(Collider col)
    {
        
        if (col.gameObject.tag == "Player"|| col.gameObject.tag == "Enemy")
        {
            int R = Random.Range(0, 99);
            col.GetComponent<ShipMain>().Damage(Power);
            if (R>=0&&R<Chance&&Manger.instance.CurrentLevel>=7&& col.gameObject.tag == "Player")
            {
                col.GetComponent<SantaMaria>().CrewFell(1, transform);
            }
        }
    }
}

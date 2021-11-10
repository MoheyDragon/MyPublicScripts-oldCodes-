using UnityEngine;
using UnityEngine.UI;
public class PlayerShip : ShipMain
{
    public static bool RamLock = false;
    protected Vector3 MoveDirection = Vector3.zero;
    public static int CrewNumber = 120;
    public Text CrewNumberText;
    public Image HealthBar;
    public GameObject CrewFellAnimation;
    public override void Start()
    {
        base.Start();
    }
    public override void Awake()
    {
        base.Awake();
        CrewNumberText.text = CrewNumber.ToString();
    }
    public override void Update()
    {
        base.Update();
        if (Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, Input.GetAxis("Horizontal") * RotateSpeed * 50 * Time.deltaTime, 0);
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            WavesParticle.loop = false;
            WavesHolder.DetachChildren();
        }
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            WaveInstantiate();
        }

        if (Input.GetKey(KeyCode.W))
        {
            if (RamLock == false)
            {
                Anime.SetBool("IsSailing", true);
                MoveDirection = new Vector3(0, 0, MoveSpeed);
                MoveDirection = transform.TransformDirection(MoveDirection);
                rb.AddForce(MoveDirection,ForceMode.VelocityChange);
            }
        }

        if (Input.GetKeyUp(KeyCode.W))
            Anime.SetBool("IsSailing", false);
        HealthBar.fillAmount = (float)Health/ (float)MaxHealth;
        CrewNumberText.text = CrewNumber.ToString();
    }
}

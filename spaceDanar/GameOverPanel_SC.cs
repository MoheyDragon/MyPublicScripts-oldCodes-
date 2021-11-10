using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverPanel_SC : MonoBehaviour {

    void Update()
    {

        if (Input.GetButton("Jump") )
        {
            SceneManager.LoadScene("SampleScene");
            Score_SC.TotalScore = 0;
        }
        if (Input.GetButton("Cancel"))
        {
            Application.Quit();
        }
    }
}

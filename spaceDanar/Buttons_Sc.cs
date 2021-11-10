using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons_Sc : MonoBehaviour {
   
  
    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
        Score_SC.TotalScore = 0;
    }
   
 
   

}

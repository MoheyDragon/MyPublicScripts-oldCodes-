using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextScoreScript : MonoBehaviour {
    public static TextScoreScript current;
    void Start()
    {
        current = this;
    }
	public void UpdateScore()
    {
        GetComponent<Text>().text = Score_SC.TotalScore.ToString();
    }
}

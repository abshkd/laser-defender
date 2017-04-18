using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {

    private Text text;
    private int score = 0;


    private void Start()
    {
        text = GetComponent<Text>();
        Reset();
    }

    public void Score( int points)
    {
        score += points;
        text.text = score.ToString();

    }

    private void Reset()
    {
        score = 0;
        text.text = score.ToString();
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Text text = GetComponent<Text>();
        text.text = ScoreKeeper.score.ToString();
        ScoreKeeper.Reset();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

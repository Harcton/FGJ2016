using UnityEngine;
using System.Collections;

public class CounterFlip : MonoBehaviour {

    public int score = 0;
	
	// Update is called once per frame
	void Update ()
    {
        gameObject.GetComponent<TextMesh>().text = "Rotator Rank: " + score;
	}
}

using UnityEngine;
using System.Collections;

public class SpriteDepth : MonoBehaviour {

    SpriteRenderer spriteRenderer;
	// Use this for initialization
	void Start ()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        spriteRenderer.sortingLayerID = (int)gameObject.transform.position.z * 10;
	}
}

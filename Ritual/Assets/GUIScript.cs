using UnityEngine;
using System.Collections;

public class GUIScript : MonoBehaviour
{
    public GameObject RedButton;
    public GameObject YellowButton;
    public GameObject BlueButton;
    public GameObject GreenButton;

    public GameObject CameraObject;

    /* STATES:
     * 0 = RedButton
     * 1 = YellowButton
     * 2 = BlueButton
     * 3 = GreenButton
     */
    public int state;

    private GameObject redButton;
    private GameObject yellowButton;
    private GameObject blueButton;
    private GameObject greenButton;


	// Use this for initialization
	void Start ()
    {
        redButton = (GameObject)Instantiate(RedButton, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
        yellowButton = (GameObject)Instantiate(YellowButton, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
        blueButton = (GameObject)Instantiate(BlueButton, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
        greenButton = (GameObject)Instantiate(GreenButton, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);

        redButton.transform.rotation = CameraObject.transform.rotation;
        yellowButton.transform.rotation = CameraObject.transform.rotation;
        greenButton.transform.rotation = CameraObject.transform.rotation;
        blueButton.transform.rotation = CameraObject.transform.rotation;
	}
	
	// Update is called once per frame
	void LateUpdate ()
    {
        redButton.transform.position = CameraObject.transform.position + new Vector3(3.0f, -2.0f, 5.0f);
        yellowButton.transform.position = redButton.transform.position + new Vector3(0.4f, -0.3f, -0.3f);
        blueButton.transform.position = redButton.transform.position + new Vector3(-0.4f, -0.3f, -0.3f);
        greenButton.transform.position = redButton.transform.position + new Vector3(0.0f, -0.6f, -0.6f);
	}
}

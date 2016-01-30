using UnityEngine;
using System.Collections;

public class PhysicsScript : MonoBehaviour
{
    void OnTriggerStay (Collider col)
    {
        if (col.gameObject.name == "Tree")
        {

        }
    }
}

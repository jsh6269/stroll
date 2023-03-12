using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMove : MonoBehaviour
{
    public GameObject Player;
    Transform p;

    // Start is called before the first frame update
    void Start()
    {
        p = Player.transform;    
    }

    void LateUpdate()
    {
        float xx = p.position.x > -20 ? p.position.x : -20;
        xx = xx < 162 ? xx : 162;
        Vector3 temp = new Vector3(xx, p.position.y+3, transform.position.z);
        transform.position = temp;

    }
}

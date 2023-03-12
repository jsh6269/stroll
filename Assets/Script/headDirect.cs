using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class headDirect : MonoBehaviour
{
    public GameObject player;
    private SpriteRenderer sRenderer;

    // Start is called before the first frame update
    void Start()
    {
        sRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.transform.position.x < 37)
        {
            sRenderer.flipX = true;
        }
        else if (player.transform.position.x > 43)
        {
            sRenderer.flipX = false;
        }
    }
}

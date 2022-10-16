using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    private Transform playerToChase;
    public bool isFlipped = false;
    // Start is called before the first frame update
    void Start()
    {
        playerToChase = FindObjectOfType<PlayerController>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerToChase.position.x < transform.position.x && isFlipped)
        {
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (playerToChase.position.x > transform.position.x && !isFlipped)
        {
            transform.Rotate(0f, -180f, 0f);            
            isFlipped = true;
        }
    }
    
}

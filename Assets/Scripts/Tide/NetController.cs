using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetController : MonoBehaviour
{
    Rigidbody2D rb;
    bool hasCollided = false;
    Rigidbody2D colliderRB;
    
    public TideController tide;
    public float maxSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(hasCollided)
        {
            rb.velocity = colliderRB.velocity;
        } else
        {
            rb.velocity = new Vector2(tide.MovingRight ? maxSpeed : 0 - maxSpeed, 0);
        }
        
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!hasCollided)
        {
            hasCollided = true;
            colliderRB = collision.gameObject.GetComponent<Rigidbody2D>();
        }
        
    }


}

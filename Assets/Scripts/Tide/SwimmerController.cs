using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimmerController : MonoBehaviour
{
    public float speed;
    public bool facingRight = false;
    bool movingRight;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        movingRight = Random.Range(0, 2) == 0;
        GetComponent<SpriteRenderer>().flipX = facingRight ? !movingRight : movingRight;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(movingRight ? speed : -speed, 0f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        movingRight = !movingRight;
        GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
    }
}

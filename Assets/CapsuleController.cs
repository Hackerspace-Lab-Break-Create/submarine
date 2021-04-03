using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleController : MonoBehaviour
{
    Rigidbody2D rb;
    public float maxSpeed;

    private bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        var verticalMove = Input.GetAxis("Vertical");
        var horizontalMove = Input.GetAxis("Horizontal");

        if((verticalMove != 0 || horizontalMove != 0) && canMove)
        {
            rb.velocity = new Vector2(maxSpeed * horizontalMove, maxSpeed * verticalMove);
        }
    }

    internal void UnblockInput()
    {
        canMove = true;
    }

    internal void BlockInput()
    {
        canMove = false;
    }
}

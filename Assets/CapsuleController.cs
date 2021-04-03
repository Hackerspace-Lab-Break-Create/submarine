using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleController : MonoBehaviour
{
    Rigidbody2D rb;
    public float maxSpeed;

    private bool canMove = true;
    private HashSet<GameObject> collidedNets = new HashSet<GameObject>();
    private float[] speeds = { 10f, 6.66f, 3.33f, 0f };

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
        var speed = speeds[collidedNets.Count];
       

        if((verticalMove != 0 || horizontalMove != 0) && canMove)
        {
            rb.velocity = new Vector2(speed * horizontalMove, speed * verticalMove);
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


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collidedNets.Count < 3 && collision.gameObject.tag == "net")
        {
            Debug.Log("hit a net");
            collidedNets.Add(collision.gameObject);
        }
    }
}

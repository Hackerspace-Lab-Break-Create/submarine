using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmergeController : MonoBehaviour
{

    public GameObject submarineGO;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = submarineGO.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var isSubmerging = rb.gravityScale == 1;
        rb.velocity = Vector2.zero;

        if (isSubmerging)
        {
            rb.AddForce(new Vector2(0, -10f), ForceMode2D.Impulse);
        } else
        {
            rb.AddForce(new Vector2(0, 10f), ForceMode2D.Impulse);
        }
        submarineGO.GetComponent<CapsuleController>().BlockInput();


    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var isSubmerging = rb.gravityScale == 1;

        if (isSubmerging)
        {
            rb.gravityScale = 0;
        }
        else
        {
            rb.gravityScale = 1;
        }

        submarineGO.GetComponent<CapsuleController>().UnblockInput();
    }

}

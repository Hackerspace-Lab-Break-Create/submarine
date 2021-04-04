using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmergeController : MonoBehaviour
{

    public GameObject submarineGO;

    Rigidbody2D rb;

    private AudioSource _audiosource;

    // Start is called before the first frame update
    void Start()
    {
        rb = submarineGO.GetComponent<Rigidbody2D>();
        _audiosource = gameObject.GetComponent<AudioSource>();
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
        submarineGO.GetComponent<PlayerController>().BlockInput();


    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var isSubmerging = rb.gravityScale == 1;

        if (isSubmerging)
        {
            _audiosource.Stop();
            rb.gravityScale = 0;
        }
        else
        {
            _audiosource.Play();
            rb.gravityScale = 1;
        }

        submarineGO.GetComponent<PlayerController>().UnblockInput();
    }

}

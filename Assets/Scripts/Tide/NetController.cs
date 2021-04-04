using UnityEngine;

public class NetController : MonoBehaviour
{
    Rigidbody2D rb;
    bool hasCollided = false;
    Rigidbody2D colliderRB;
    private TideController _tideController;

    public float maxSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();


        _tideController = GameObject.FindGameObjectWithTag("Tide")            
            .GetComponent<TideController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(hasCollided)
        {
            rb.velocity = colliderRB.velocity;
        } else
        {
            rb.velocity = new Vector2(_tideController.MovingRight ? maxSpeed : 0 - maxSpeed, 0);
        }
        
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.IsTouchingLayers(LayerMask.NameToLayer("Land")))
        {
            return;
        }

        try
        {
            if (!hasCollided)
            {
                hasCollided = true;
                colliderRB = collision.gameObject.GetComponent<Rigidbody2D>();

                var playerController = collision.gameObject.GetComponent<PlayerController>();

                var clips = playerController.GetSoundClips();
                var audioSource = playerController.GetAudioSource();

                audioSource.PlayOneShot(clips[6]);
            }
        }
        catch (System.Exception)
        {
            Debug.Log("Error");
        }
    }
}

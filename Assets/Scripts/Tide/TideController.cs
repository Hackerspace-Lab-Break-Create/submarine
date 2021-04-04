using UnityEngine;

public class TideController : MonoBehaviour
{
    public float waitTime;
    float timer = 0.0f;
    public bool MovingRight { get; set; }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > waitTime)
        {
            MovingRight = !MovingRight;
            timer -= waitTime;
        }
    }

    
}

using UnityEngine;

public class TideController : MonoBehaviour
{
    public float waitTime;
    float timer = 0.0f;
    public bool MovingRight { get; set; }

    public GameObject netObj;
    public MeshCollider background;

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
            Instantiate(netObj, GetRandomLocation(), new Quaternion());
        }
    }

    public Vector3 GetRandomLocation()
    {
        var x = Random.Range(background.bounds.min.x, background.bounds.max.x);
        var y = Random.Range(background.bounds.min.y, background.bounds.max.y);

        return new Vector3(x, y, 0f);
    }
}

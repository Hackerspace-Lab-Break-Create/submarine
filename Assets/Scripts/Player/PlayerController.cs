using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform WaterReference;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private Collider2D GetPlayerWaterCollider2D()
    {
        return null;
    }

    private Collider2D GetWaterCollider2D()
    {
        return Physics2D.OverlapBox(WaterReference.position, new Vector2(), 0.0F);
    }
}

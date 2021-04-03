using Assets.Scripts.Player;
using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject WaterReference;
    public BoxCollider2D PlayerWaterCollider;

    public float HMoveSpeed = 5.0F;
    public float VMoveSpeed = 0.0F;

    private PlayerLocation _playerLocation;

    private Rigidbody2D _playerRigidBody;


    // Start is called before the first frame update
    void Start()
    {
        _playerRigidBody = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_playerLocation == PlayerLocation.ABOVEWATER)
        {
            _playerRigidBody.gravityScale = -1;
        }

        var LHMove = Input.GetAxis("Horizontal");
        var LVMove = Input.GetAxis("Vertical");


        if (_playerLocation == PlayerLocation.UNDERWATER)
        {
            gameObject.GetComponent<Rigidbody2D>()
                .velocity = new Vector2(HMoveSpeed * LHMove, VMoveSpeed * LVMove);
        }
    }

    private Collider2D GetPlayerWaterCollider2D()
    {
        var spriteRender = gameObject.GetComponent<SpriteRenderer>();

        return Physics2D.OverlapBox(spriteRender.transform.position, spriteRender.bounds.size, 0.0F);
    }

    private Collider2D GetWaterCollider2D()
    {
        var spriteRender = WaterReference.GetComponent<SpriteRenderer>();

        return Physics2D.OverlapBox(spriteRender.transform.position, spriteRender.bounds.size, 0.0F);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Water")
        {
            _playerLocation = PlayerLocation.ABOVEWATER;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Water")
        {
            _playerLocation = PlayerLocation.UNDERWATER;
        }
    }
}

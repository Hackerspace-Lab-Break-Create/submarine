using Assets.Scripts.Player;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //Editor Refs
    public float HMoveSpeed = 10.0F;
    public float VMoveSpeed = 10.0F;

    //Components
    private Rigidbody2D _playerRigidBody;
    private PlayerInventory _playerInventory;


    private HashSet<GameObject> collidedNets = new HashSet<GameObject>();
    private bool hasInput = true;

    // Start is called before the first frame update
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0051:Remove unused private members", Justification = "Called By engine")]
    public void Start()
    {
        _playerRigidBody = gameObject.GetComponent<Rigidbody2D>();
        _playerInventory = gameObject.GetComponent<PlayerInventory>();
    }

    // Update is called once per frame
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0051:Remove unused private members", Justification = "Called By engine")]
    public void Update()
    {
        
    }

    internal void UnblockInput()
    {
        hasInput = true;
    }

    internal void BlockInput()
    {
        hasInput = false;
    }

    #region Unity Events
    public void OnCollisionEnter2D(Collision2D collision)
    {
        var gameObject = collision.gameObject;

        if (gameObject.layer == LayerMask.NameToLayer("Collectables"))
        {
            var collectable = gameObject.GetComponent<ICollectable>();
            collectable.OnCollect(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collidedNets.Count < 3 && collision.gameObject.tag == "net")
        {
            collidedNets.Add(collision.gameObject);
        }
    }

    public void OnMove(InputValue context)
    {
        Debug.Log("Move");

        var value = context.Get<Vector2>();


        var verticalMove = value.y;//Input.GetAxis("Vertical");
        var horizontalMove = value.x;//Input.GetAxis("Horizontal");

        if (hasInput)
        {
            _playerRigidBody.velocity = Vector2.zero;
        }

        if ((verticalMove != 0 || horizontalMove != 0) && hasInput)
        {
            _playerRigidBody.velocity = new Vector2(GetSpeed(HMoveSpeed, collidedNets.Count) * horizontalMove, GetSpeed(VMoveSpeed, collidedNets.Count) * verticalMove);
        }
        
       
    }

    public void OnGrab(InputValue context)
    {
        Debug.Log("LEts grab!");
    }

    #endregion


    public PlayerInventory GetInventory()
    {
        return _playerInventory;
    }

    public float GetSpeed(float baseSpeed, int collisions)
    {
        if(collisions == 3)
        {
            return 0f;
        }

        if(collisions == 0)
        {
            return baseSpeed;
        }

        return baseSpeed * ((3 - collisions) * (1f / 3f));

    }
   
}

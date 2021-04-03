using Assets.Scripts.Player;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //Editor Refs
    public float HMoveSpeed = 5.0F;
    public float VMoveSpeed = 5.0F;

    //Components
    private Rigidbody2D _playerRigidBody;
    private PlayerInventory _playerInventory;


    private PlayerLocation _playerLocation;
    private bool canMove = true;

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
        _playerLocation = _playerRigidBody.gravityScale == 1 ? PlayerLocation.ABOVEWATER : PlayerLocation.UNDERWATER;
    }

    internal void UnblockInput()
    {
        canMove = true;
    }

    internal void BlockInput()
    {
        canMove = false;
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

    public void OnMove(InputValue context)
    {
        Debug.Log("Move");

        var value = context.Get<Vector2>();


        var verticalMove = value.y;//Input.GetAxis("Vertical");
        var horizontalMove = value.x;//Input.GetAxis("Horizontal");

        if ((verticalMove != 0 || horizontalMove != 0) && canMove)
        {
            _playerRigidBody.velocity = new Vector2(HMoveSpeed * horizontalMove, VMoveSpeed * verticalMove);
        }
        else
        {
            _playerRigidBody.velocity = Vector2.zero;
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
}

using Assets.Scripts;
using Assets.Scripts.Player;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //Editor Refs
    public float HMoveSpeed = 10.0F;
    public float VMoveSpeed = 10.0F;
    public GameObject UIText;

    //Components
    private Rigidbody2D _playerRigidBody;
    private PlayerInventory _playerInventory;

    private HashSet<GameObject> collidedNets = new HashSet<GameObject>();
    private bool hasInput = true;
    private int direction;

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

    public void OnMove(InputValue input)
    {
        var value = input.Get<Vector2>();

        var verticalMove = value.y;
        var horizontalMove = value.x;

        if (hasInput)
        {
            _playerRigidBody.velocity = Vector2.zero;
        }

        if ((verticalMove != 0 || horizontalMove != 0) && hasInput)
        {
            _playerRigidBody.velocity = new Vector2(GetSpeed(HMoveSpeed, collidedNets.Count) * horizontalMove, GetSpeed(VMoveSpeed, collidedNets.Count) * verticalMove);

            if (horizontalMove != 0)
            {
                direction = (int)horizontalMove;
                var component = gameObject.GetComponent<SpriteRenderer>();

                if (direction != 0)
                {
                    component.flipX = direction == 1 ? true : false;
                }

            }
        }
    }

    public void OnGrab(InputValue input)
    {
        var bottles = GameState.Trash;
        var closest = default(GameObject);
        var closestDistance = default(float);

        foreach (var bottle in bottles)
        {
            if (closest == null)
            {
                closest = bottle;
                continue;
            }

            var LclosestDistance = Vector2.Distance(transform.position, closest.transform.position);
            var currentDistance = Vector2.Distance(transform.position, bottle.transform.position);

            if (currentDistance < LclosestDistance)
            {
                closest = bottle;
                closestDistance = currentDistance;
            }
        }

        Debug.DrawLine(transform.position, closest.transform.position, Color.red, 1.0F, false);

        if (closestDistance > 5.0F)
        {
            var obj = Instantiate(UIText);
            var text = obj.transform.Find("Canvas/Text").gameObject
            .GetComponent<TMPro.TextMeshProUGUI>();

            text.text = "Too far";

            return;
        }

        var differenceX = closest.transform.position.x - transform.position.x;
        var differenceY = closest.transform.position.y - transform.position.y;

        if ((direction == -1 && differenceX > 0) || (direction == 1 && differenceX < 0))
        {
            var obj = Instantiate(UIText);
            var text = obj.transform.Find("Canvas/Text").gameObject
            .GetComponent<TMPro.TextMeshProUGUI>();

            text.text = "Wrong side";

            return;
        }

        closest.GetComponent<Rigidbody2D>()
            .AddForce(
            new Vector2(differenceX < 0 ? Mathf.Lerp(0F, 5.0F, differenceX) : Mathf.Lerp(0F, -5.0F, differenceX),
            differenceY < 0 ? Mathf.Lerp(0F, 5.0F, differenceY) : Mathf.Lerp(0F, -5.0F, differenceY)
            ), ForceMode2D.Impulse);

        GameState.Trash.Remove(closest);
        StartCoroutine(DestroyTrash(closest));
    }

    private IEnumerator<WaitForSeconds> DestroyTrash(GameObject trash)
    {
        yield return new WaitForSeconds(0.5F);

        Destroy(trash);
    }

    public void OnRepair(InputValue input)
    {
        if (collidedNets.Count <= 0)
        {
            return;
        }

        var usedRepairKit = _playerInventory.UseRepairKit();

        if (usedRepairKit)
        {
            var netToRemove = collidedNets.Last();
            collidedNets.Remove(netToRemove);

            GameState.Net.Remove(netToRemove);
            Destroy(netToRemove);
        }
    }

    public void OnDebug(InputValue input)
    {
        GameState.Phase = GameState.GamePhase.PLAYING;
    }

    #endregion

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

    public PlayerInventory GetInventory()
    {
        return _playerInventory;
    }

   public int GetNetCount()
    {
        return collidedNets.Count;
    }
}

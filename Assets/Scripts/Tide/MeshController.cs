using Assets.Scripts;
using UnityEngine;

public class MeshController : MonoBehaviour
{
    // Start is called before the first frame update
    public void Awake()
    {
        GameState.SpawnMesh = gameObject.GetComponent<MeshCollider>();
    }
}

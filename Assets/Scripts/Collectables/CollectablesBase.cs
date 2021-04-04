using UnityEngine;

public abstract class CollectablesBase : MonoBehaviour, ICollectable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    virtual public bool OnCollect(PlayerController player)
    {
        return true;
    }
}

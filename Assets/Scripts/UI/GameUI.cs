using Assets.Scripts.Player;
using UnityEngine;

public class GameUI : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        _playerInventory = GameObject.FindGameObjectWithTag("Player")
            .GetComponent<Assets.Scripts.Player.PlayerInventory>();

        _knifesCountUI = gameObject.transform.Find("InventoryPanel/KnifeCountUI").gameObject
            .GetComponent<TMPro.TextMeshProUGUI>();
    }

    private TMPro.TextMeshProUGUI _knifesCountUI;
    private PlayerInventory _playerInventory;

    // Update is called once per frame
    void Update()
    {
        _knifesCountUI.text = _playerInventory.GetKnifeCount().ToString();
    }
}

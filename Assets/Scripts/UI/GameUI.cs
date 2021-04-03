using Assets.Scripts;
using Assets.Scripts.Player;
using UnityEngine;

public class GameUI : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        _playerInventory = GameObject.FindGameObjectWithTag("Player")
            .GetComponent<Assets.Scripts.Player.PlayerInventory>();

        _repairKitCountUI = gameObject.transform.Find("InventoryPanel/RepairKit/RepairKitCountUI").gameObject
            .GetComponent<TMPro.TextMeshProUGUI>();

        _trashCountUI = gameObject.transform.Find("InventoryPanel/MissingTrash/TrashCountUI").gameObject
            .GetComponent<TMPro.TextMeshProUGUI>();

        _scoreCountUI = gameObject.transform.Find("ScorePanel/Score/ScoreCountUI").gameObject
            .GetComponent<TMPro.TextMeshProUGUI>();
    }

    private TMPro.TextMeshProUGUI _repairKitCountUI;
    private TMPro.TextMeshProUGUI _trashCountUI;
    private TMPro.TextMeshProUGUI _scoreCountUI;
    private PlayerInventory _playerInventory;

    // Update is called once per frame
    void Update()
    {
        _repairKitCountUI.text = _playerInventory.GetRepairKitCount().ToString();

        _trashCountUI.text = GameState.Trash.Count.ToString();

        _scoreCountUI.text = GameState.Points.ToString();

    }
}

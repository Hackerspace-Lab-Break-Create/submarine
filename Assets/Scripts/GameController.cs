using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{
    public GameObject GameOverPrefab;
    public GameObject NetPrefab;
    public List<GameObject> TrashPrefabs;

    public MeshCollider GameAreaMesh;
    public int InitialNets = 10;
    public int InitialTrash = 5;

    private PlayerController _playerController;
    private float netTime = 0.0F;
    private float partialNetTime = 0.0F;

    private float trashTime = 0.0F;
    private float partialTrashTime = 0.0F;


    // Start is called before the first frame update
    public void Start()
    {
        _playerController = GameObject
            .FindGameObjectWithTag("Player")
            .GetComponent<PlayerController>();
    }

    // Update is called once per frame
    public void Update()
    {
        #region Transições

        if (GameState.OldPhase == GameState.GamePhase.STARTMENU && GameState.Phase == GameState.GamePhase.PLAYING)
        {
            SpawnNets(true);

            SpawnTrash(true);

            _playerController.gameObject.GetComponent<PlayerInput>().ActivateInput(); //Player
            gameObject.GetComponent<PlayerInput>().DeactivateInput(); //UI

            GameState.OldPhase = GameState.GamePhase.PLAYING;
        }
        else if (GameState.OldPhase == GameState.GamePhase.PLAYING && GameState.Phase == GameState.GamePhase.GAMEOVER)
        {
            var obj = Instantiate(GameOverPrefab);
            var text = obj.transform.Find("Canvas/Text").gameObject
            .GetComponent<TMPro.TextMeshProUGUI>();

            text.text = GameState.GameOverMessage;

            _playerController.gameObject.GetComponent<PlayerInput>().DeactivateInput(); //Player
            gameObject.GetComponent<PlayerInput>().ActivateInput(); //UI

            GameState.OldPhase = GameState.GamePhase.GAMEOVER;
        }
        else if (GameState.OldPhase == GameState.GamePhase.GAMEOVER && GameState.Phase == GameState.GamePhase.STARTMENU)
        {
            Debug.Log("Going to menu;");

            //Volta para menu
        }
        #endregion

        #region GameFlow
        if (GameState.Phase == GameState.GamePhase.PLAYING)
        {
            ValidateGameOver();

            SpawnNets();

            SpawnTrash();
        }
        else if (GameState.Phase == GameState.GamePhase.GAMEOVER)
        {

        }
        else if (GameState.Phase == GameState.GamePhase.STARTMENU)
        {
            
        }

        #endregion
    }

    public void OnReturnToMenuGameover(InputValue input)
    {
        GameState.Phase = GameState.GamePhase.STARTMENU;
    }

    private void SpawnTrash(bool initialSpawn = false)
    {
        if (initialSpawn)
        {
            for (int i = 0; i < InitialTrash; i++)
            {
                var trash = Instantiate(GetRandomTrash(), GetRandomLocation(), new Quaternion());
                GameState.Trash.Add(trash);
            }

            return;
        }

        trashTime += Time.deltaTime;
        if (trashTime > 30.0F)
        {
            partialTrashTime += Time.deltaTime;
            if (partialTrashTime > 20.0F)
            {
                partialTrashTime -= partialTrashTime;
                var trash = Instantiate(GetRandomTrash(), GetRandomLocation(), new Quaternion());
                GameState.Trash.Add(trash);
            }
        }
    }

    private void SpawnNets(bool initialSpawn = false)
    {
        if (initialSpawn)
        {
            for (int i = 0; i < InitialNets; i++)
            {
                var net = Instantiate(NetPrefab, GetRandomLocation(), new Quaternion());
                GameState.Net.Add(net);
            }

            return;
        }

        netTime += Time.deltaTime;
        if (netTime > 30.0F)
        {
            partialNetTime += Time.deltaTime;
            if (partialNetTime > 15.0F)
            {
                partialNetTime -= partialNetTime;
                var net = Instantiate(NetPrefab, GetRandomLocation(), new Quaternion());
                GameState.Net.Add(net);
            }
        }
    }

    private GameObject GetRandomTrash()
    {
        var index = Random.Range(0, TrashPrefabs.Count);

        return TrashPrefabs[index];
    }

    private Vector3 GetRandomLocation()
    {
        var x = Random.Range(GameAreaMesh.bounds.min.x, GameAreaMesh.bounds.max.x);
        var y = Random.Range(GameAreaMesh.bounds.min.y, GameAreaMesh.bounds.max.y);

        return new Vector3(x, y, 0f);
    }

    private void ValidateGameOver()
    {
        if (_playerController.GetNetCount() == 3 && _playerController.GetInventory().GetRepairKitCount() == 0)
        {
            GameState.GameOverMessage = "Submarine got stuck with nets!";
            GameState.Phase = GameState.GamePhase.GAMEOVER;
        }
    }
}

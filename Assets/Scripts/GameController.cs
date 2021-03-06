using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private static GameController _instance;

    public GameObject GameOverPrefab;
    public GameObject NetPrefab;
    public List<GameObject> TrashPrefabs;
    public List<AudioClip> Clips;


    public int InitialNets = 10;
    public int InitialTrash = 5;

    private AudioSource _audioSource;

    private float netTime = 0.0F;
    private float partialNetTime = 0.0F;

    private float trashTime = 0.0F;
    private float partialTrashTime = 0.0F;

    private System.Func<PlayerController> _playerController;
    private System.Func<MeshCollider> _gameAreaMesh;
    private System.Func<MainBG> _mainController;

    public void Awake()
    {
        DontDestroyOnLoad(this);

        if (_instance == null)
        {
            _instance = this;
        } else
        {
            DestroyObject(gameObject);
        }

    }

    // Start is called before the first frame update
    public void Start()
    {
        _playerController = () => { return GameState.PlayerController; };
        _mainController = () => { return GameState.MainController; };
        _gameAreaMesh = () => { return GameState.SpawnMesh; };
        _audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void Update()
    {
        #region Transições

        if (GameState.OldPhase == GameState.GamePhase.STARTMENU && GameState.Phase == GameState.GamePhase.PLAYING)
        {
            GameState.ResetState();

            if (GameState.canPlay)
            {
                SpawnNets(true);

                SpawnTrash(true);

                _playerController().gameObject.GetComponent<PlayerInput>().ActivateInput(); //Player
                gameObject.GetComponent<PlayerInput>().DeactivateInput(); //UI

                GameState.OldPhase = GameState.GamePhase.PLAYING;
            }
        }
        else if (GameState.OldPhase == GameState.GamePhase.PLAYING && GameState.Phase == GameState.GamePhase.GAMEOVER)
        {
            _playerController().gameObject.GetComponent<PlayerInput>().DeactivateInput(); //Player
            gameObject.GetComponent<PlayerInput>().ActivateInput(); //UI

            GameState.OldPhase = GameState.GamePhase.GAMEOVER;

            GameState.canPlay = false;

            GameState.MainController.ShowLoseScreen();

            _audioSource.PlayOneShot(Clips[0]);
        }
        else if ((GameState.OldPhase == GameState.GamePhase.GAMEOVER || GameState.OldPhase == GameState.GamePhase.WIN) && GameState.Phase == GameState.GamePhase.STARTMENU)
        {
            SceneManager.LoadScene("MainMenu");
            GameState.OldPhase = GameState.GamePhase.STARTMENU;
        }
        else if (GameState.OldPhase == GameState.GamePhase.PLAYING && GameState.Phase == GameState.GamePhase.WIN)
        {
            _playerController().gameObject.GetComponent<PlayerInput>().DeactivateInput(); //Player
            gameObject.GetComponent<PlayerInput>().ActivateInput(); //UI

            GameState.MainController.ShowWinScreen();
            _audioSource.PlayOneShot(Clips[1]);
            
            GameState.OldPhase = GameState.GamePhase.WIN;
            GameState.canPlay = false;
        }
        else if (GameState.OldPhase == GameState.GamePhase.PLAYING && GameState.Phase == GameState.GamePhase.PAUSED)
        {
            GameState.canPlay = false;
            GameState.OldPhase = GameState.GamePhase.PAUSED;
        }
        else if (GameState.OldPhase == GameState.GamePhase.PAUSED && GameState.Phase == GameState.GamePhase.PLAYING)
        {
            GameState.canPlay = true;
            GameState.OldPhase = GameState.GamePhase.PLAYING;
        }
        #endregion

        #region GameFlow
        if (GameState.Phase == GameState.GamePhase.PLAYING)
        {
            if (GameState.canPlay)
            {
                ValidateGameOver();

                SpawnNets();

                SpawnTrash();

                CheckTrash();
            }
        }
        else if (GameState.Phase == GameState.GamePhase.GAMEOVER)
        {

        }
        else if (GameState.Phase == GameState.GamePhase.STARTMENU)
        {
            
        }

        #endregion
    }
    private void ValidateGameOver()
    {
        if (_playerController().GetNetCount() == 3 && _playerController().GetInventory().GetRepairKitCount() == 0)
        {
            GameState.GameOverMessage = "Submarine got stuck with nets!";
            GameState.Phase = GameState.GamePhase.GAMEOVER;
        }
    }

    private void CheckTrash()
    {
        if (GameState.Trash.Count == 0)
        {
            GameState.Phase = GameState.GamePhase.WIN;
        }
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
    public void OnReturntoMenuGameover(InputValue input)
    {
        GameState.Phase = GameState.GamePhase.STARTMENU;
    }

    private GameObject GetRandomTrash()
    {
        var index = Random.Range(0, TrashPrefabs.Count);

        return TrashPrefabs[index];
    }

    private Vector3 GetRandomLocation()
    {
        var x = Random.Range(_gameAreaMesh().bounds.min.x, _gameAreaMesh().bounds.max.x);
        var y = Random.Range(_gameAreaMesh().bounds.min.y, _gameAreaMesh().bounds.max.y);

        return new Vector3(x, y, 0f);
    }

}

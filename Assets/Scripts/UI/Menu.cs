using Assets.Scripts;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.UI.Button;

public class Menu : MonoBehaviour
{
    public Button _startButton { get; private set; }

    void Start()
    {
    }

    void Update()
    {
        
    }

    public void OnStartGame()
    {
        SceneManager.LoadScene("Main");
        GameState.Phase = GameState.GamePhase.PLAYING;
    }

    public void OnShowBindings()
    {
        gameObject.transform.Find("Menu").gameObject.SetActive(false);
        gameObject.transform.Find("Bindings").gameObject.SetActive(true);
    }

    public void OnExitBindings()
    {
        gameObject.transform.Find("Menu").gameObject.SetActive(true);
        gameObject.transform.Find("Bindings").gameObject.SetActive(false);
    }

    public void OnExitGame()
    {
        Application.Quit();
    }
}

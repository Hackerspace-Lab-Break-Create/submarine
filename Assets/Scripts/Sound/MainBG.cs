using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MainBG : MonoBehaviour
{
    public GameObject WinScreen;
    public GameObject LoseScreen;

    private void Awake()
    {
        GameState.MainController = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (new List<GameState.GamePhase>() {
            GameState.GamePhase.GAMEOVER,
            GameState.GamePhase.WIN
        }.Any((item) => { return GameState.Phase == item; }))
        {
            gameObject.GetComponent<AudioSource>().Stop();
        }
    }

    public void ShowWinScreen()
    {
        WinScreen.SetActive(true);
    }

    public void ShowLoseScreen()
    {
        LoseScreen.SetActive(true);
    }
}

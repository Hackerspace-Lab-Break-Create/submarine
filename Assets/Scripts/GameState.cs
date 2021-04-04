using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    internal static class GameState
    {
        public static int Points = 0;

        public static GamePhase Phase = GamePhase.STARTMENU;
        public static GamePhase OldPhase = GamePhase.STARTMENU;

        public static List<GameObject> Trash = new List<GameObject>();
        public static List<GameObject> Net = new List<GameObject>();

        public static string GameOverMessage;

        public static PlayerController PlayerController { get; internal set; }
        public static MeshCollider SpawnMesh { get; internal set; }
        public static MainBG MainController { get; internal set; }

        public static bool canPlay = false;

        public enum GamePhase
        {
            PLAYING,
            GAMEOVER,
            STARTMENU,
            PAUSED,
            WIN
        }

        internal static void ResetState()
        {
            Trash = new List<GameObject>();
            Net = new List<GameObject>();
            Points = 0;
        }
    }
}

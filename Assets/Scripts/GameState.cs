using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    internal static class GameState
    {
        public static int Points;

        public static GamePhase Phase = GameState.GamePhase.STARTMENU;
        public static GamePhase OldPhase = GameState.GamePhase.STARTMENU;

        public static List<GameObject> Trash = new List<GameObject>();
        public static List<GameObject> Net = new List<GameObject>();

        public static string GameOverMessage;

        public static PlayerController PlayerController { get; internal set; }
        public static MeshCollider SpawnMesh { get; internal set; }

        public enum GamePhase
        {
            PLAYING,
            GAMEOVER,
            STARTMENU,
            PAUSED
        }

    }
}

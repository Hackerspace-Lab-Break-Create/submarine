using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerInventory : MonoBehaviour
    {
        public int KnifesCollectionCount = 1;

        public PlayerInventory()
        {
            _knifes = 0;
        }

        private int _knifes;

        public void AddKnife()
        {
            _knifes += KnifesCollectionCount;
        }

        public int GetKnifeCount()
        {
            return _knifes;
        }
    }
}

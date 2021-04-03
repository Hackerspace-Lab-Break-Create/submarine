using System;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerInventory : MonoBehaviour
    {
        public int RepairKitCollectionCount = 1;

        public PlayerInventory()
        {
            _repairKits = 0;
        }

        private int _repairKits;

        public bool AddKnife()
        {
            if (_repairKits == 3)
            {
                return false;
            }

            _repairKits += RepairKitCollectionCount;

            return true;
        }

        public int GetRepairKitCount()
        {
            return _repairKits;
        }

        public bool UseRepairKit()
        {
            if (_repairKits == 0)
            {
                return false;
            }

            _repairKits--;

            return true;
        }
    }
}

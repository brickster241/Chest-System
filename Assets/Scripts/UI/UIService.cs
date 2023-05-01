using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Generics;
using Services.Chest;
using ChestSlot;

namespace Services.UI {
    public class UIService : GenericMonoSingleton<UIService>
    {
        public void SpawnChest() {
            GameObject Chest = ChestService.Instance.FetchChestFromPool();
            if (Chest != null) {
                Transform chestSlotTransform = SlotService.Instance.GetChestSlot();
                Chest.transform.SetParent(chestSlotTransform, false);
                Chest.SetActive(true);
            } else {
                Debug.Log("SLOTS ARE FULL.");
            }
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Generics;
using Scriptables;
using Services.Chest;
using Services.Events;
using ChestSlot;

namespace Services.UI {
    public class UIService : GenericMonoSingleton<UIService>
    {
        public void SpawnChest() {
            (GameObject, ChestScriptableObject) chestValues = ChestService.Instance.FetchChestFromPool();
            GameObject Chest = chestValues.Item1;
            ChestScriptableObject chestConfig = chestValues.Item2;
            if (Chest != null) {
                Transform chestSlotTransform = SlotService.Instance.GetChestSlot();
                Chest.transform.SetParent(chestSlotTransform, false);
                Chest.SetActive(true);
                EventService.Instance.InvokeChestSpawnedEvent(chestConfig.CHEST_COINS_RANGE, chestConfig.CHEST_GEMS_RANGE, chestConfig.CHEST_TYPE);
            } else {
                EventService.Instance.InvokeSlotFullEvent();
            }
        }
    }

}

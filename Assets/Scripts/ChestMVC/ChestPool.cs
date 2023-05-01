using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MVC.Chest {
    public class ChestPool {
        private ChestView[] chestPool;

        public ChestPool(int poolCount, ChestView chestPrefab, Transform parentTF) {
            chestPool = new ChestView[poolCount];
            for (int i = 0; i < poolCount; i++) {
                ChestView chestView = GameObject.Instantiate<ChestView>(chestPrefab, parentTF);
                ChestModel chestModel = new ChestModel();
                ChestController chestController = new ChestController(chestModel, chestView);
                chestModel.SetChestController(chestController);
                chestView.SetChestController(chestController);
                chestView.gameObject.SetActive(false);
                chestPool[i] = chestView;
            }
        }

        public ChestView GetChestItem() {
            for (int i = 0; i < chestPool.Length; i++) {
                if (!chestPool[i].gameObject.activeInHierarchy)
                    return chestPool[i]; 
            }
            return null;
        }

        public void ReturnChestItem(ChestView chestView) {
            chestView.gameObject.SetActive(false);
        }
    }

}

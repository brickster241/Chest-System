using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scriptables;
using Generics;
using MVC.Chest;

namespace Services.Chest {

    public class ChestService : GenericMonoSingleton<ChestService>
    {
        [SerializeField] ChestScriptableObjectList chestConfigs;
        [SerializeField] ChestView ChestPrefab;
        [SerializeField] Transform ChestsParentTF;
        private ChestPool chestPool;

        private void Start() {
            chestPool = new ChestPool(4, ChestPrefab, ChestsParentTF);
        }

        public GameObject FetchChestFromPool() {
            ChestView chestObject = chestPool.GetChestItem();
            if (chestObject != null) {
                ChestScriptableObject chestConfig = FetchRandomChestConfiguration();
                chestObject.GetChestController().GetChestModel().SetChestConfiguration(chestConfig);
                chestObject.GetChestController().SetViewAttributes();
                return chestObject.gameObject;
            }
            return null;
        }

        public ChestScriptableObject FetchRandomChestConfiguration() {
            int index = Random.Range(0, chestConfigs.chestScriptableObjects.Length);
            return chestConfigs.chestScriptableObjects[index];
        }

        public void ReturnChestToPool(ChestView chestView) {
            chestPool.ReturnChestItem(chestView);
        }
    }
}

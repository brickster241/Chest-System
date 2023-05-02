using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Generics;
using Scriptables;
using Services.Chest;
using Services.Events;
using ChestSlot;
using UnityEngine.UI;
using TMPro;

namespace Services.UI {
    public class UIService : GenericMonoSingleton<UIService>
    {
        public int COIN_COUNT {get; private set;}
        public int GEM_COUNT {get; private set;}
        [SerializeField] TextMeshProUGUI COIN_TEXT;
        [SerializeField] TextMeshProUGUI GEM_TEXT;
        private int EXPLORE_COST = 50;

        private void Start() {
            COIN_COUNT = 100;
            GEM_COUNT = 50;
        }

        private void OnEnable() {
            EventService.Instance.onCollectCoinGem += UpdateCoinAndGems;
        }
        
        public void SpawnChest() {
            if (COIN_COUNT < EXPLORE_COST) {
                EventService.Instance.InvokeNotEnoughCoinsEvent();
                return;
            }
            (GameObject, ChestScriptableObject) chestValues = ChestService.Instance.FetchChestFromPool();
            GameObject Chest = chestValues.Item1;
            ChestScriptableObject chestConfig = chestValues.Item2;
            if (Chest != null) {
                Transform chestSlotTransform = SlotService.Instance.GetChestSlot();
                Chest.transform.SetParent(chestSlotTransform, false);
                Chest.SetActive(true);
                UpdateCoinAndGems(-EXPLORE_COST, 0);
                EventService.Instance.InvokeChestSpawnedEvent(chestConfig.CHEST_COINS_RANGE, chestConfig.CHEST_GEMS_RANGE, chestConfig.CHEST_TYPE);
            } else {
                EventService.Instance.InvokeSlotFullEvent();
            }
        }

        private void UpdateCoinAndGems(int COINS, int GEMS) {
            COIN_COUNT += COINS;
            GEM_COUNT += GEMS;
            COIN_TEXT.text = COIN_COUNT.ToString();
            GEM_TEXT.text = GEM_COUNT.ToString();
        }

        private void OnDisable() {
            EventService.Instance.onCollectCoinGem -= UpdateCoinAndGems;
        }
    }

}

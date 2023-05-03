using UnityEngine;
using Generics;
using Scriptables;
using TMPro;

namespace Services {

    /*
        UIService MonoSingleton class. Handles Menu UI (Coin Count, Gem Coint, Spawn Button)
    */
    public class UIService : GenericMonoSingleton<UIService>
    {
        public int COIN_COUNT {get; private set;}
        public int GEM_COUNT {get; private set;}
        [SerializeField] TextMeshProUGUI COIN_TEXT;
        [SerializeField] TextMeshProUGUI GEM_TEXT;
        [SerializeField] int EXPLORE_COST = 50;

        /*
            Sets Value of Initial COINS & GEMS.
        */
        private void Start() {
            COIN_COUNT = 100;
            GEM_COUNT = 50;
        }

        /*
            Subscribes to the onCollectCoinGem Event.
        */
        private void OnEnable() {
            EventService.Instance.onCollectCoinGem += UpdateCoinAndGems;
        }
        
        /*
            Method Gets Executed whenever Spawn Button is Clicked.
            Fetches Chest Configuration & Sets Chest Slot.
            Initiates NotEnoughCoins, ChestSpawned, SlotFull event based on Conditions.
        */
        public void SpawnChest() {
            AudioService.Instance.PlayAudio(SoundType.BUTTON_CLICK);
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

        /*
            UpdateCoinAndGems Method. Updates the COIN & GEM Count.
        */
        private void UpdateCoinAndGems(int COINS, int GEMS) {
            COIN_COUNT += COINS;
            GEM_COUNT += GEMS;
            COIN_TEXT.text = COIN_COUNT.ToString();
            GEM_TEXT.text = GEM_COUNT.ToString();
        }

        /*
            Unsubscribes to the onCollectCoinGem Event.
        */
        private void OnDisable() {
            EventService.Instance.onCollectCoinGem -= UpdateCoinAndGems;
        }
    }

}

using System;
using UnityEngine;
using Generics;
using Chest.MVC;
using Chest.StateMachine;

namespace Services {

    /*
        EventService MonoSingleton Class. Used to Handle All Events which happen in Game.
    */
    public class EventService : GenericMonoSingleton<EventService>
    {
        public event Action onSlotFull;
        public event Action<Vector2Int, Vector2Int, ChestType> onChestSpawned;
        public event Action<int, int, int, ChestState, ChestType, GameObject> onChestClicked;
        public event Action<int, int> onCollectCoinGem;
        public event Action onNotEnoughCoins;

        /*
            Invokes the Slot is Full Event. Gets Executed when Slots are Full.
        */
        public void InvokeSlotFullEvent() {
            onSlotFull?.Invoke();
        }

        /*
            Invokes the Chest Spawned Event. Gets Executed whenever New Chest is Spawned.
        */
        public void InvokeChestSpawnedEvent(Vector2Int COIN_RANGE, Vector2Int GEM_RANGE, ChestType chestType) {
            onChestSpawned?.Invoke(COIN_RANGE, GEM_RANGE, chestType);
        }
        
        /*
            Invokes the Chest Clicked Event. Gets Executed whenever Chest is Clicked.
        */
        public void InvokeChestClickedEvent(int COINS, int GEMS, int GEMS_TO_UNLOCK, ChestState chestState, ChestType chestType, GameObject gameObject) {
            onChestClicked?.Invoke(COINS, GEMS, GEMS_TO_UNLOCK, chestState, chestType, gameObject);
        }

        /*
            Invokes the Collect Coin & Gem Event. Gets Executed whenever Total Coins & Gems change.
        */
        public void InvokeCollectCoinGemEvent(int COINS, int GEMS) {
            onCollectCoinGem?.Invoke(COINS, GEMS);
        }

        /*
            Invokes Not Enough Coins event. Gets Executed whenever Coins / Gems are not Enough for Exploring / Unlocking Chests.
        */
        public void InvokeNotEnoughCoinsEvent() {
            onNotEnoughCoins?.Invoke();
        }
    }
}

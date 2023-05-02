using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Generics;
using Scriptables;

namespace Services.Events {
    public class EventService : GenericMonoSingleton<EventService>
    {
        public event Action onSlotFull;
        public event Action<Vector2Int, Vector2Int, ChestType> onChestSpawned;
        public event Action<int, int, int, ChestState, ChestType, GameObject> onChestClicked;

        public void InvokeSlotFullEvent() {
            onSlotFull?.Invoke();
        }

        public void InvokeChestSpawnedEvent(Vector2Int COIN_RANGE, Vector2Int GEM_RANGE, ChestType chestType) {
            onChestSpawned?.Invoke(COIN_RANGE, GEM_RANGE, chestType);
        }

        public void InvokeChestClickedEvent(int COINS, int GEMS, int GEMS_TO_UNLOCK, ChestState chestState, ChestType chestType, GameObject gameObject) {
            onChestClicked?.Invoke(COINS, GEMS, GEMS_TO_UNLOCK, chestState, chestType, gameObject);
        }
    }
}
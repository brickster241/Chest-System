using UnityEngine;
using Scriptables;

namespace Chest.MVC {

    public enum ChestType {
        COMMON,
        MINI,
        RARE,
        LEGENDARY
    }

    public class ChestModel
    {
        private ChestController chestController;
        public int CHEST_COINS {get; private set;}
        public int CHEST_GEMS {get; private set;}
        private float MAX_UNLOCK_TIME;
        public float UNLOCK_TIME {get; private set;}
        private float MAX_GEMS_TO_UNLOCK;
        public float GEMS_TO_UNLOCK {get; private set;}
        public Sprite CHEST_SPRITE {get; private set;}
        public ChestType CHEST_TYPE {get; private set;}

        public void SetChestConfiguration(ChestScriptableObject chestScriptableObject) {
            UNLOCK_TIME = chestScriptableObject.MAX_UNLOCK_TIME;
            MAX_UNLOCK_TIME = chestScriptableObject.MAX_UNLOCK_TIME;
            GEMS_TO_UNLOCK = chestScriptableObject.MAX_GEMS_TO_UNLOCK;
            MAX_GEMS_TO_UNLOCK = chestScriptableObject.MAX_GEMS_TO_UNLOCK;
            CHEST_SPRITE = chestScriptableObject.CHEST_SPRITE;
            CHEST_TYPE = chestScriptableObject.CHEST_TYPE;
            CHEST_COINS = Random.Range(chestScriptableObject.CHEST_COINS_RANGE.x, chestScriptableObject.CHEST_COINS_RANGE.y);
            CHEST_GEMS = Random.Range(chestScriptableObject.CHEST_GEMS_RANGE.x, chestScriptableObject.CHEST_GEMS_RANGE.y);
        }

        public ChestController GetChestController() {
            return chestController;
        }

        public void SetChestController(ChestController _chestController) {
            chestController = _chestController;
        }

        public void UpdateUnlockTime(float deltaTime) {
            UNLOCK_TIME = Mathf.Max(UNLOCK_TIME - deltaTime, 0);
            GEMS_TO_UNLOCK = (int)Mathf.Ceil((UNLOCK_TIME / MAX_UNLOCK_TIME) * MAX_GEMS_TO_UNLOCK);
        }

    }

}

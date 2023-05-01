using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scriptables;

namespace MVC.Chest {
    public class ChestModel
    {
        private ChestController chestController;
        public int CHEST_COINS {get; private set;}
        public int CHEST_GEMS {get; private set;}
        public int UNLOCK_TIME {get; private set;}
        public int GEMS_TO_UNLOCK {get; private set;}
        public Sprite CHEST_SPRITE {get; private set;}
        public ChestType CHEST_TYPE {get; private set;}

        public void SetChestConfiguration(ChestScriptableObject chestScriptableObject) {
            Debug.Log("CHEST SCRIPTABLE OBJECT : " + chestScriptableObject);
            UNLOCK_TIME = chestScriptableObject.UNLOCK_TIME;
            GEMS_TO_UNLOCK = chestScriptableObject.MAX_GEMS_TO_UNLOCK;
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

    }

}

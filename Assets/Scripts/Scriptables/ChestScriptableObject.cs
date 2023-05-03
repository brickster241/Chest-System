using UnityEngine;
using Chest.MVC;

namespace Scriptables {

    /*
        ChestScriptableObject class. (Used to create ScriptableObjects)
        Used to Create Chest Configurations.
    */

    [CreateAssetMenu(fileName = "ChestScriptableObject", menuName = "Scriptable-Objects/ChestScriptableObject")]
    public class ChestScriptableObject : ScriptableObject {
        public Vector2Int CHEST_COINS_RANGE;
        public Vector2Int CHEST_GEMS_RANGE;
        public int MAX_UNLOCK_TIME;
        public int MAX_GEMS_TO_UNLOCK;
        public Sprite CHEST_SPRITE;
        public ChestType CHEST_TYPE;
    }
}

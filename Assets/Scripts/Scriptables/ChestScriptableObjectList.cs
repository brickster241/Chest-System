using UnityEngine;

namespace Scriptables {

    /*
        ChestScriptableObjectList class. Used to create Nested ScriptableObject for Chest Configurations.
    */

    [CreateAssetMenu(fileName = "ChestScriptableObjectList", menuName = "Scriptable-Objects/ChestScriptableObject-List")]
    public class ChestScriptableObjectList : ScriptableObject {
        public ChestScriptableObject[] chestScriptableObjects;
    }
}
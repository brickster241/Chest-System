using UnityEngine;

namespace Scriptables {
    [CreateAssetMenu(fileName = "ChestScriptableObjectList", menuName = "Scriptable-Objects/ChestScriptableObject-List")]
    public class ChestScriptableObjectList : ScriptableObject {
        public ChestScriptableObject[] chestScriptableObjects;
    }
}
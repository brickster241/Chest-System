using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scriptables {
    [CreateAssetMenu(fileName = "ChestScriptableObjectList", menuName = "Scriptable-Objects/ChestScriptableObject-List")]
    public class ChestScriptableObjectList : ScriptableObject {
        public ChestScriptableObject[] chestScriptableObjects;
    }
}
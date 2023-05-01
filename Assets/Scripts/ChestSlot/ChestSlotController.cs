using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace ChestSlot {
    public enum ChestSlotType {
        EMPTY,
        FILLED
    }

    public class ChestSlotController : MonoBehaviour
    {
        public ChestSlotType CHEST_SLOT_STATUS;
        public GameObject EMPTY_TEXT;

        private void Update() {
            if (CHEST_SLOT_STATUS == ChestSlotType.EMPTY && !EMPTY_TEXT.activeInHierarchy) {
                EMPTY_TEXT.SetActive(true);
            } else if (CHEST_SLOT_STATUS == ChestSlotType.FILLED && EMPTY_TEXT.activeInHierarchy) {
                EMPTY_TEXT.SetActive(false);
            }
        }
        
    }

}

using UnityEngine;
using Generics;
using ChestSlot;

namespace Services {
    public class SlotService : GenericMonoSingleton<SlotService>
    {
        [SerializeField] ChestSlotController[] chestSlots;

        public Transform GetChestSlot() {
            for (int i = 0; i < chestSlots.Length; i++) {
                if (chestSlots[i].CHEST_SLOT_STATUS == ChestSlotType.EMPTY) {
                    chestSlots[i].CHEST_SLOT_STATUS = ChestSlotType.FILLED;
                    return chestSlots[i].transform;
                }
            }
            return null;
        }

        public void ResetChestSlot(Transform chestSlotTransform) {
            for (int i = 0; i < chestSlots.Length; i++) {
                if (chestSlots[i].gameObject == chestSlotTransform.gameObject) {
                    chestSlots[i].CHEST_SLOT_STATUS = ChestSlotType.EMPTY;
                    break;
                }
            }
        }
    }

}

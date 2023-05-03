using UnityEngine;
using Generics;
using ChestSlot;

namespace Services {

    /*
        SlotService MonoSingleton class. Handles Chest-Slots & Gets / Resets Chest Slot.
    */
    public class SlotService : GenericMonoSingleton<SlotService>
    {
        [SerializeField] ChestSlotController[] chestSlots;

        /*
            Returns First Available Chest Slot's Transform based on CHEST_SLOT_STATUS.
            Transform is Returned so that Chest GameObject gets spawned with Slot as parent.
        */
        public Transform GetChestSlot() {
            for (int i = 0; i < chestSlots.Length; i++) {
                if (chestSlots[i].CHEST_SLOT_STATUS == ChestSlotType.EMPTY) {
                    chestSlots[i].CHEST_SLOT_STATUS = ChestSlotType.FILLED;
                    return chestSlots[i].transform;
                }
            }
            return null;
        }

        /*
            Resets the ChestSlot Status to EMPTY.
        */
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

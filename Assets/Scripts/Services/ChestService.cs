using UnityEngine;
using Scriptables;
using Generics;
using Chest.MVC;
using Chest.StateMachine;

namespace Services {

    public class ChestService : GenericMonoSingleton<ChestService>
    {
        [SerializeField] ChestScriptableObjectList chestConfigs;
        [SerializeField] ChestView ChestPrefab;
        [SerializeField] Transform ChestsParentTF;
        private ChestPool chestPool;

        private void Start() {
            chestPool = new ChestPool(4, ChestPrefab, ChestsParentTF);
        }

        public (GameObject, ChestScriptableObject) FetchChestFromPool() {
            ChestView chestObject = chestPool.GetChestItem();
            if (chestObject != null) {
                ChestScriptableObject chestConfig = FetchRandomChestConfiguration();
                chestObject.GetChestController().GetChestModel().SetChestConfiguration(chestConfig);
                chestObject.GetChestController().SetViewAttributes();
                chestObject.GetChestController().GetChestSM().ResetSM();
                return (chestObject.gameObject, chestConfig);
            }
            return (null, null);
        }

        public ChestScriptableObject FetchRandomChestConfiguration() {
            int index = Random.Range(0, chestConfigs.chestScriptableObjects.Length);
            return chestConfigs.chestScriptableObjects[index];
        }

        public void ReturnChestToPool(ChestView chestView) {
            chestPool.ReturnChestItem(chestView);
        }

        public void TriggerPopUp(ChestController chestController) {
            ChestSM chestSM = chestController.GetChestSM();
            int COINS = chestController.GetChestModel().CHEST_COINS;
            int GEMS = chestController.GetChestModel().CHEST_GEMS;
            int GEMS_TO_UNLOCK = (int)chestController.GetChestModel().GEMS_TO_UNLOCK;
            ChestState CHEST_STATE = chestSM.currentChestStateEnum;
            ChestType chestType = chestController.GetChestModel().CHEST_TYPE;
            if (CHEST_STATE == ChestState.OPEN) {
                AudioService.Instance.PlayAudio(SoundType.CHEST_OPENED);
                ReturnChestToPool(chestController.GetChestView());
                SlotService.Instance.ResetChestSlot(chestController.GetChestView().transform.parent);
            }
            EventService.Instance.InvokeChestClickedEvent(COINS, GEMS, GEMS_TO_UNLOCK, CHEST_STATE, chestType, chestController.GetChestView().gameObject);
        }

        public void UnlockChest(GameObject ChestGameObject) {
            ChestController chestController = ChestGameObject.GetComponent<ChestView>().GetChestController();
            chestController.GetChestModel().UpdateUnlockTime(chestController.GetChestModel().UNLOCK_TIME);
        }

        public void DequeueChestFromWaitingQueue() {
            ChestQueueService.Instance.DequeueChest();
        }
    }
}

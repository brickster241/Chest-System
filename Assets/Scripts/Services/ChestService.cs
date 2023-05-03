using UnityEngine;
using Scriptables;
using Generics;
using Chest.MVC;
using Chest.StateMachine;

namespace Services {

    /*
        ChestService MonoSingleton Class. Handles Creation of Chest GameObjects.
        Communicates with Other Services.
    */
    public class ChestService : GenericMonoSingleton<ChestService>
    {
        [SerializeField] ChestScriptableObjectList chestConfigs;
        [SerializeField] ChestView ChestPrefab;
        [SerializeField] Transform ChestsParentTF;
        [SerializeField] int MAX_QUEUE_COUNT;
        private ChestPool chestPool;
        private ChestQueueService chestQueueService;

        private void Start() {
            chestPool = new ChestPool(4, ChestPrefab, ChestsParentTF);
            chestQueueService = new ChestQueueService(MAX_QUEUE_COUNT);
        }

        /*
            Fetches Chest Configuration & GameObject associated from the ChestPool.
            Also Sets View Attributes, Model Configuration & Resets State Machine.
        */
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

        /*
            Returns ChestScriptableObject from ChestScriptableObjectList which contains different ChestModel Configurations.
        */
        public ChestScriptableObject FetchRandomChestConfiguration() {
            int index = Random.Range(0, chestConfigs.chestScriptableObjects.Length);
            return chestConfigs.chestScriptableObjects[index];
        }

        /*
            Returns Chest GameObject to Pool. Disables the Chest GameObject.
        */
        public void ReturnChestToPool(ChestView chestView) {
            chestPool.ReturnChestItem(chestView);
        }

        /*
            Triggers PopUp & Uses Different Services to Handle Audio, Events, ChestSlots.
        */
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

        /*
            Unlocks Chest Instantly. Sets the UNLOCK TIME to 0. Executed when UNLOCK NOW button is Clicked.
        */
        public void UnlockChest(GameObject ChestGameObject) {
            ChestController chestController = ChestGameObject.GetComponent<ChestView>().GetChestController();
            chestController.GetChestModel().UpdateUnlockTime(chestController.GetChestModel().UNLOCK_TIME);
        }

        /*
            Dequeues Chest From Waiting Queue : Which starts the Timer.
        */
        public void DequeueChestFromWaitingQueue() {
            chestQueueService.DequeueChest();
        }

        /*
            Returns boolean specifying is it Possible to Add Chest to Waiting Queue.
        */
        public bool isChestQueueingPosssible() {
            return chestQueueService.isChestQueueingPosssible();
        }

        /*
            Adds Chest GameObject to Waiting Queue.
            Also Starts Unlocking the First Chest if no chest is currently Unlocking.
        */
        public void AddInQueue(GameObject chestObject) {
            chestQueueService.AddInQueue(chestObject);
        }

    }
}

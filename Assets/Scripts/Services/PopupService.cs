using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Generics;
using Chest.MVC;
using Chest.StateMachine;

namespace Services {

    /*
        PopupService MonoSingleton Class. Handles All the logic & functionality of PopupUI.
    */
    public class PopupService : GenericMonoSingleton<PopupService>
    {
        [SerializeField] GameObject PopupUI;
        [SerializeField] Button OkButton;
        [SerializeField] Button UnlockButton;
        [SerializeField] Button QueueButton;
        [SerializeField] Button CancelButton;
        [SerializeField] TextMeshProUGUI unlockButtonText;
        [SerializeField] TextMeshProUGUI detailText;
        [SerializeField] TextMeshProUGUI chestText;
        [SerializeField] TextMeshProUGUI chestTitleText;

        /*
            Subscribing to onSlotsFull, OnChestSpawned, onChestClicked, onNotEnoughCoins Event.
        */
        private void OnEnable() {
            EventService.Instance.onSlotFull += OnSlotsFull;
            EventService.Instance.onChestSpawned += OnChestSpawnedSuccesful;
            EventService.Instance.onChestClicked += OnChestButtonClicked;
            EventService.Instance.onNotEnoughCoins += DisplayNotEnoughResources;
        }

        /*
            onQueueButtonClick Method. Gets Executed when Queue Button is Clicked.
        */
        public void OnQueueButtonClick(GameObject ChestGameObject) {
            ClearPopUp();
            if (ChestService.Instance.isChestQueueingPosssible()) {
                ChestService.Instance.AddInQueue(ChestGameObject);
            } else {
                PopupUI.SetActive(true);
                detailText.text = "QUEUE IS FULL. TRY AGAIN LATER.";
                OkButton.gameObject.SetActive(true);
                detailText.gameObject.SetActive(true);
            }
        }

        /*
            onUnlockButtonClick Method. Gets Executed when Unlock Button is Clicked.    
        */
        public void OnUnlockButtonClick(GameObject ChestGameObject, int GEMS_TO_UNLOCK) {
            AudioService.Instance.PlayAudio(SoundType.BUTTON_CLICK);
            if (UIService.Instance.GEM_COUNT >= GEMS_TO_UNLOCK) {
                ChestService.Instance.UnlockChest(ChestGameObject);
                EventService.Instance.InvokeCollectCoinGemEvent(0, -GEMS_TO_UNLOCK);
                ClearPopUp();
            } else {
                ClearPopUp();
                DisplayNotEnoughResources();
            }
        }

        /*
            Clears the PopupUI & Disables all Gameobjects.
        */
        public void ClearPopUp() {
            AudioService.Instance.PlayAudio(SoundType.BUTTON_CLICK);
            PopupUI.SetActive(false);
            QueueButton.onClick.RemoveAllListeners();
            QueueButton.gameObject.SetActive(false);
            UnlockButton.onClick.RemoveAllListeners();
            UnlockButton.gameObject.SetActive(false);
            CancelButton.gameObject.SetActive(false);
            OkButton.gameObject.SetActive(false);
            detailText.gameObject.SetActive(false);
            chestText.gameObject.SetActive(false);
        }

        /*
            Displays Slots are Full Popup.
        */
        public void OnSlotsFull() {
            PopupUI.SetActive(true);
            OkButton.gameObject.SetActive(true);
            detailText.text = "ALL SLOTS ARE FULL. TRY AGAIN LATER.";
            detailText.gameObject.SetActive(true);
        }

        /*
            Displays Not Enough Coins / Gems Popup.
        */
        public void DisplayNotEnoughResources() {
            PopupUI.SetActive(true);
            OkButton.gameObject.SetActive(true);
            detailText.text = "NOT ENOUGH COINS / GEMS. TRY AGAIN LATER.";
            detailText.gameObject.SetActive(true);
        }

        /*
            OnChestButtonClicked Method. Gets Executed whenever Chest is Clicked.
            COINS, GEMS, GEMS_TO_UNLOCK, CHEST_STATE & ChestType along with GameObject are Added to Handle All States.
        */
        public void OnChestButtonClicked(int COINS, int GEMS, int GEMS_TO_UNLOCK, ChestState CHEST_STATE, ChestType chestType, GameObject chestObject) {
            AudioService.Instance.PlayAudio(SoundType.BUTTON_CLICK);
            if (CHEST_STATE == ChestState.LOCKED) {
                ChestLockedStatePopUp(chestObject);
            } else if (CHEST_STATE == ChestState.UNLOCKING) {
                ChestUnlockingStatePopUp(GEMS_TO_UNLOCK, chestObject);
            } else if (CHEST_STATE == ChestState.OPEN) {
                ChestOpenStatePopUp(COINS, GEMS, chestType);
            }
        }

        /*
            Displays Popup UI when Chest is Clicked in LOCKED State.
            Enables Queue & Cancel Buttons.
        */
        private void ChestLockedStatePopUp(GameObject chestObject) {
            PopupUI.SetActive(true);
            detailText.text = "CHEST IS LOCKED. QUEUE UNLOCKING ?";
            detailText.gameObject.SetActive(true);
            QueueButton.onClick.AddListener(() => { OnQueueButtonClick(chestObject);});
            QueueButton.gameObject.SetActive(true);
            CancelButton.gameObject.SetActive(true);
        }

        /*
            Displays Popup UI when Chest is Clicked in UNLOCKING State.
            Enables Unlock Now & Cancel Buttons.
        */
        private void ChestUnlockingStatePopUp(int GEMS_TO_UNLOCK, GameObject chestObject) {
            PopupUI.SetActive(true);
            detailText.text = "UNLOCK CHEST FOR " + GEMS_TO_UNLOCK + " GEMS ?";
            detailText.gameObject.SetActive(true);
            unlockButtonText.text = GEMS_TO_UNLOCK.ToString();
            UnlockButton.onClick.AddListener(() => { OnUnlockButtonClick(chestObject, GEMS_TO_UNLOCK);});
            UnlockButton.gameObject.SetActive(true);
            CancelButton.gameObject.SetActive(true);
        }

        /*
            Displays Popup UI when Chest is Clicked in OPEN State.
            Displays COINS, GEMS obtained.
        */
        private void ChestOpenStatePopUp(int COINS, int GEMS, ChestType chestType) {
            chestTitleText.text = GetChestTypeText(chestType) + " CHEST OPENED !!";
            chestText.text = "COINS FOUND : " + COINS + "\nGEMS FOUND  :   " + GEMS;
            chestText.text = chestText.text.Replace("\\n", "\n");
            PopupUI.SetActive(true);
            chestText.gameObject.SetActive(true);
            OkButton.gameObject.SetActive(true);
            EventService.Instance.InvokeCollectCoinGemEvent(COINS, GEMS);
        }

        /*
            Gets the ChestType Text by taking ChestType as Input.
        */
        private string GetChestTypeText(ChestType chestType) {
            string ChestTypeText = "";
            if (chestType == ChestType.COMMON) {
                ChestTypeText = "COMMON";
            } else if (chestType == ChestType.MINI) {
                ChestTypeText = "MINI";
            } else if (chestType == ChestType.RARE) {
                ChestTypeText = "RARE";
            } else if (chestType == ChestType.LEGENDARY) {
                ChestTypeText = "LEGENDARY";
            }
            return ChestTypeText;
        }

        /*
            Displays Popup when Chest is Spawned Successfully.
            Displays Chest Type & Coin , gem Range.
        */
        public void OnChestSpawnedSuccesful(Vector2Int COIN_RANGE, Vector2Int GEM_RANGE, ChestType chestType) {
            string ChestTypeText = GetChestTypeText(chestType);
            chestTitleText.text = ChestTypeText + " CHEST FOUND !!";
            chestText.text = "COINS RANGE : " + COIN_RANGE.x + " - " + COIN_RANGE.y + "\nGEMS RANGE  :   " + GEM_RANGE.x + " - " + GEM_RANGE.y;
            chestText.text = chestText.text.Replace("\\n", "\n");
            PopupUI.SetActive(true);
            chestText.gameObject.SetActive(true);
            OkButton.gameObject.SetActive(true);
        }

        /*
            Unsubscribing to onSlotsFull, OnChestSpawned, onChestClicked, onNotEnoughCoins Event.
        */
        private void OnDisable() {
            EventService.Instance.onSlotFull -= OnSlotsFull;
            EventService.Instance.onChestSpawned -= OnChestSpawnedSuccesful;
            EventService.Instance.onChestClicked -= OnChestButtonClicked;
            EventService.Instance.onNotEnoughCoins -= DisplayNotEnoughResources;
        
        }
    }

}

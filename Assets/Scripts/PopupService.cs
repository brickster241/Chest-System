using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Scriptables;
using TMPro;
using Services.Events;
using Generics;
using Services.Chest;

public class PopupService : GenericMonoSingleton<PopupService>
{
    [SerializeField] GameObject PopupUI;
    [SerializeField] Button OkButton;
    [SerializeField] Button UnlockButton;
    [SerializeField] Button CancelButton;
    [SerializeField] TextMeshProUGUI detailText;
    [SerializeField] TextMeshProUGUI chestText;
    [SerializeField] TextMeshProUGUI chestTitleText;

    private void OnEnable() {
        EventService.Instance.onSlotFull += OnSlotsFull;
        EventService.Instance.onChestSpawned += OnChestSpawnedSuccesful;
        EventService.Instance.onChestClicked += OnChestButtonClicked;
    }

    public void OnUnlockButtonClick(GameObject ChestGameObject) {
        ChestService.Instance.StartUnlockingChest(ChestGameObject);
        ClearPopUp();
    }

    public void ClearPopUp() {
        PopupUI.SetActive(false);
        UnlockButton.onClick.RemoveAllListeners();
        UnlockButton.gameObject.SetActive(false);
        CancelButton.gameObject.SetActive(false);
        OkButton.gameObject.SetActive(false);
        detailText.gameObject.SetActive(false);
        chestText.gameObject.SetActive(false);
    }

    public void OnSlotsFull() {
        PopupUI.SetActive(true);
        OkButton.gameObject.SetActive(true);
        detailText.text = "ALL SLOTS ARE FULL. TRY AGAIN LATER.";
        detailText.gameObject.SetActive(true);
    }

    public void OnChestButtonClicked(int COINS, int GEMS, int GEMS_TO_UNLOCK, ChestState CHEST_STATE, ChestType chestType, GameObject gameObject) {
        Debug.Log("CURRENT CHEST STATE : " + CHEST_STATE);
        if (CHEST_STATE == ChestState.LOCKED) {
            ChestLockedStatePopUp(gameObject);
        } else if (CHEST_STATE == ChestState.UNLOCKING) {
            ChestUnlockingStatePopUp();
        } else if (CHEST_STATE == ChestState.OPEN) {
            ChestOpenStatePopUp(COINS, GEMS, chestType);
        }
    }

    private void ChestLockedStatePopUp(GameObject gameObject) {
        PopupUI.SetActive(true);
        detailText.text = "CHEST IS LOCKED. START UNLOCKING ?";
        detailText.gameObject.SetActive(true);
        UnlockButton.onClick.AddListener(() => { OnUnlockButtonClick(gameObject);});
        UnlockButton.gameObject.SetActive(true);
        CancelButton.gameObject.SetActive(true);
    }

    private void ChestUnlockingStatePopUp() {

    }

    private void ChestOpenStatePopUp(int COINS, int GEMS, ChestType chestType) {
        chestTitleText.text = GetChestTypeText(chestType) + " CHEST OPENED !!";
        chestText.text = "COINS FOUND : " + COINS + "\nGEMS FOUND  :   " + GEMS;
        chestText.text = chestText.text.Replace("\\n", "\n");
        PopupUI.SetActive(true);
        chestText.gameObject.SetActive(true);
        OkButton.gameObject.SetActive(true);
    }

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

    public void OnChestSpawnedSuccesful(Vector2Int COIN_RANGE, Vector2Int GEM_RANGE, ChestType chestType) {
        string ChestTypeText = GetChestTypeText(chestType);
        chestTitleText.text = ChestTypeText + " CHEST FOUND !!";
        chestText.text = "COINS RANGE : " + COIN_RANGE.x + " - " + COIN_RANGE.y + "\nGEMS RANGE  :   " + GEM_RANGE.x + " - " + GEM_RANGE.y;
        chestText.text = chestText.text.Replace("\\n", "\n");
        PopupUI.SetActive(true);
        chestText.gameObject.SetActive(true);
        OkButton.gameObject.SetActive(true);
    }

    private void OnDisable() {
        EventService.Instance.onSlotFull -= OnSlotsFull;
        EventService.Instance.onChestSpawned -= OnChestSpawnedSuccesful;
        EventService.Instance.onChestClicked -= OnChestButtonClicked;
    }
}

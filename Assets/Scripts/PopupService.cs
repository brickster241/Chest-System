using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Scriptables;
using TMPro;
using Services.Events;

public class PopupService : MonoBehaviour
{
    [SerializeField] GameObject PopupUI;
    [SerializeField] GameObject OkButton;
    [SerializeField] GameObject UnlockButton;
    [SerializeField] GameObject CancelButton;
    [SerializeField] TextMeshProUGUI slotsFullText;
    [SerializeField] TextMeshProUGUI chestSpawnedText;

    private void OnEnable() {
        EventService.Instance.onSlotFull += OnSlotsFull;
        EventService.Instance.onChestSpawned += OnChestSpawnedSuccesful;
    }

    public void OnOkButtonClick() {
        PopupUI.SetActive(false);
        UnlockButton.SetActive(false);
        CancelButton.SetActive(false);
        OkButton.SetActive(false);
        slotsFullText.gameObject.SetActive(false);
        chestSpawnedText.gameObject.SetActive(false);
    }

    public void OnSlotsFull() {
        PopupUI.SetActive(true);
        OkButton.SetActive(true);
        slotsFullText.gameObject.SetActive(true);
    }

    public void OnChestSpawnedSuccesful(Vector2Int COIN_RANGE, Vector2Int GEM_RANGE, ChestType chestType) {
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
        chestSpawnedText.text = ChestTypeText + " CHEST FOUND !!! \n\nCOINS RANGE : " + COIN_RANGE.x + " - " + COIN_RANGE.y + "\nGEMS RANGE   : " + GEM_RANGE.x + " - " + GEM_RANGE.y;
        chestSpawnedText.text = chestSpawnedText.text.Replace("\\n", "\n");
        PopupUI.SetActive(true);
        chestSpawnedText.gameObject.SetActive(true);
        OkButton.SetActive(true);
    }

    private void OnDisable() {
        EventService.Instance.onSlotFull -= OnSlotsFull;
        EventService.Instance.onChestSpawned -= OnChestSpawnedSuccesful;
    }
}

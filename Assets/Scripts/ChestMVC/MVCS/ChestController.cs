using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scriptables;
using Services.Chest;

namespace MVC.Chest {
    public class ChestController
    {
        private ChestModel chestModel;
        private ChestView chestView;
        private ChestSM chestSM;
        
        public ChestController(ChestModel _chestModel, ChestView _chestView) {
            chestModel = _chestModel;
            chestView = _chestView;
            chestSM = new ChestSM(this);
        }

        public ChestModel GetChestModel() {
            return chestModel;
        }

        public ChestView GetChestView() {
            return chestView;
        }

        public ChestSM GetChestSM() {
            return chestSM;
        }

        public void OnChestButtonClicked() {
            ChestService.Instance.TriggerPopUp(this);
        }

        public void SetViewAttributes() {
            // SET SPRITE & TEXT & TIMER
            SetChestSprite(chestModel.CHEST_SPRITE);
            SetChestTypeText(chestModel.CHEST_TYPE);
        }

        public void SetChestSprite(Sprite chestSprite) {
            chestView.ChestSprite.sprite = chestSprite;
        }

        public void SetChestTypeText(ChestType chestType) {
            if (chestType == ChestType.COMMON) {
                chestView.Chest_Type.text = "COMMON";
            } else if (chestType == ChestType.MINI) {
                chestView.Chest_Type.text = "MINI";
            } else if (chestType == ChestType.RARE) {
                chestView.Chest_Type.text = "RARE";
            } else {
                chestView.Chest_Type.text = "LEGENDARY";
            }
        }

        public void DequeueChest() {
            ChestService.Instance.DequeueChestFromWaitingQueue();
        }

    }

}

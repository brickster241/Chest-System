using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace MVC.Chest {
    public class ChestView : MonoBehaviour
    {
        private ChestController chestController;
        public Image ChestSprite;
        public TextMeshProUGUI Chest_Type;
        public TextMeshProUGUI Timer_Text;

        public ChestController GetChestController() {
            return chestController;
        }

        public void SetChestController(ChestController _chestController) {
            chestController = _chestController;
        }

        public void OnChestClicked() {
            chestController.OnChestButtonClicked();
        }
        
    }

}

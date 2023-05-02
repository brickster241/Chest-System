using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MVC.Chest;

public class ChestOpenState : ChestBaseState
{
    public ChestOpenState(ChestSM _chestSM) : base(_chestSM) {}

    public override void OnStateEnter()
    {
        base.OnStateEnter();
        SetTimerText();
    }

    private void SetTimerText() {
        ChestController chestController = chestSM.GetChestController();
        chestController.GetChestView().Timer_Text.text = "OPEN";
    }

    public override void OnChestButtonClicked()
    {
        base.OnChestButtonClicked();
    }
}

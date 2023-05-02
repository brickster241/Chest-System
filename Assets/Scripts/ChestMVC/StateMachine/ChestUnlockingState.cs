using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MVC.Chest;

public class ChestUnlockingState : ChestBaseState
{
    public ChestUnlockingState(ChestSM _chestSM) : base(_chestSM) {}

    public override void OnStateEnter()
    {
        base.OnStateEnter();
        chestSM.GetChestController().GetChestView().StartCoroutine(StartTimer());
    }


    private IEnumerator StartTimer() {
        ChestModel chestModel = chestSM.GetChestController().GetChestModel();
        ChestView chestView = chestSM.GetChestController().GetChestView();
        while (chestModel.UNLOCK_TIME > 0) {
            chestModel.UpdateUnlockTime(Time.deltaTime);
            chestView.Timer_Text.text = GetTimeText(chestModel.UNLOCK_TIME);
            yield return new WaitForEndOfFrame();
        }
        chestSM.SwitchState(ChestState.OPEN);
        chestSM.GetChestController().DequeueChest();
    }

    private string GetTimeText(float UNLOCK_TIME) {
        int minutes = (int)UNLOCK_TIME / 60;
        int seconds = (int)UNLOCK_TIME % 60;
        string finalText = "";
        if (minutes < 10) {
            finalText = "0" + minutes + ":";
        } else {
            finalText = minutes + ":";
        }
        if (seconds < 10) {
            finalText += "0" + seconds;
        } else {
            finalText += seconds;
        }
        return finalText;
    }

    public override void OnStateUpdate()
    {
        base.OnStateUpdate();
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Generics;
using MVC.Chest;

public class ChestQueueService : GenericMonoSingleton<ChestQueueService>
{
    [SerializeField] int MAX_QUEUE_COUNT;
    private ChestController currentChest = null;
    private Queue<ChestController> ChestsInUnlockingQueue;

    private void Start() {
        ChestsInUnlockingQueue = new Queue<ChestController>();  
    }

    public bool isChestQueueingPosssible() {
        return ChestsInUnlockingQueue.Count < MAX_QUEUE_COUNT;
    }

    public void AddInQueue(GameObject chestObject) {
        ChestController chestController = chestObject.GetComponent<ChestView>().GetChestController();
        chestController.GetChestSM().SwitchState(ChestState.QUEUED);
        ChestsInUnlockingQueue.Enqueue(chestController);
        if (currentChest == null) {
            DequeueChest();
        }
    }

    public void DequeueChest() {
        if (ChestsInUnlockingQueue.Count > 0) {
            ChestController chestController = ChestsInUnlockingQueue.Dequeue();
            currentChest = chestController;
            chestController.GetChestSM().SwitchState(ChestState.UNLOCKING);
        } else {
            currentChest = null;
        }
        
    }
}

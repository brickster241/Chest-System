using System.Collections.Generic;
using UnityEngine;
using Chest.MVC;
using Chest.StateMachine;

namespace Services {

    /*
        ChestQueueService Class. Handles All the Queue Operations.
        Enqueues & Dequeues Chest to switch to UNLOCKING State.
    */
    public class ChestQueueService
    {
        private int MAX_QUEUE_COUNT;
        private ChestController currentChest = null;
        private Queue<ChestController> ChestsInUnlockingQueue;

        public ChestQueueService(int _MAX_QUEUE_COUNT) {
            MAX_QUEUE_COUNT = _MAX_QUEUE_COUNT;
            ChestsInUnlockingQueue = new Queue<ChestController>();
        }

        /*
            Returns boolean specifying is it Possible to Add Chest to Waiting Queue.
        */
        public bool isChestQueueingPosssible() {
            return ChestsInUnlockingQueue.Count < MAX_QUEUE_COUNT;
        }

        /*
            Adds Chest GameObject to Waiting Queue.
            Also Starts Unlocking the First Chest if no chest is currently Unlocking.
        */
        public void AddInQueue(GameObject chestObject) {
            ChestController chestController = chestObject.GetComponent<ChestView>().GetChestController();
            chestController.GetChestSM().SwitchState(ChestState.QUEUED);
            ChestsInUnlockingQueue.Enqueue(chestController);
            if (currentChest == null) {
                DequeueChest();
            }
        }

        /*
            Performs Dequeue Operation on the Waiting Queue, and switches the state to UNLOCKING.
        */
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

}

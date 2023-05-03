using Chest.MVC;

namespace Chest.StateMachine {
    /*
        ChestQueuedState class. Handles Functionality of when Chest is in QUEUED state.
        Chest switches to this state after LOCKED state, when there is space in Waiting Queue.
    */
    public class ChestQueuedState : ChestBaseState
    {
        public ChestQueuedState(ChestSM _chestSM) : base(_chestSM) {}

        /*
            Method gets called when Chest enters QUEUED state.
        */
        public override void OnStateEnter()
        {
            base.OnStateEnter();
            SetTimerText();
        }

        /*
            Sets the Timer Text to QUEUED.
        */
        private void SetTimerText() {
            ChestController chestController = chestSM.GetChestController();
            chestController.GetChestView().Timer_Text.text = "QUEUED";
        }
    }
}

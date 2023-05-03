using Chest.MVC;

namespace Chest.StateMachine {
    /*
        ChestLockedState class. Handles Functionality of when Chest is in LOCKED state.
        Is used when New Chest is Spawned.
    */
    public class ChestLockedState : ChestBaseState
    {
        public ChestLockedState(ChestSM _chestSM) : base(_chestSM) {}

        /*
            Method gets called when Chest enters LOCKED state.
        */
        public override void OnStateEnter()
        {
            base.OnStateEnter();
            SetTimerText();
        }

        /*
            Sets the Timer Text to LOCKED.
        */
        private void SetTimerText() {
            ChestController chestController = chestSM.GetChestController();
            chestController.GetChestView().Timer_Text.text = "LOCKED";
        }
    }

}

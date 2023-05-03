using Chest.MVC;

namespace Chest.StateMachine {

    /*
        ChestOpenState class. Handles Functionality of when Chest is in OPEN state.
        Chest switches to this state after UNLOCKING state.
    */
    public class ChestOpenState : ChestBaseState
    {
        public ChestOpenState(ChestSM _chestSM) : base(_chestSM) {}

        /*
            Method gets called when Chest enters OPEN state.
        */
        public override void OnStateEnter()
        {
            base.OnStateEnter();
            SetTimerText();
        }

        /*
            Sets the Timer Text to OPEN.
        */
        
        private void SetTimerText() {
            ChestController chestController = chestSM.GetChestController();
            chestController.GetChestView().Timer_Text.text = "OPEN";
        }
    }

}

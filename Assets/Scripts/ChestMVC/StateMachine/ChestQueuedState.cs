using Chest.MVC;

namespace Chest.StateMachine {
    public class ChestQueuedState : ChestBaseState
    {
        public ChestQueuedState(ChestSM _chestSM) : base(_chestSM) {}

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            SetTimerText();
        }

        private void SetTimerText() {
            ChestController chestController = chestSM.GetChestController();
            chestController.GetChestView().Timer_Text.text = "QUEUED";
        }

        public override void OnChestButtonClicked()
        {
            base.OnChestButtonClicked();
        }
    }
}

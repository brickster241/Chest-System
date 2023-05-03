using Chest.MVC;

namespace Chest.StateMachine {
    public class ChestLockedState : ChestBaseState
    {
        public ChestLockedState(ChestSM _chestSM) : base(_chestSM) {}

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            SetTimerText();
        }

        private void SetTimerText() {
            ChestController chestController = chestSM.GetChestController();
            chestController.GetChestView().Timer_Text.text = "LOCKED";
        }

        public override void OnChestButtonClicked()
        {
            base.OnChestButtonClicked();
        }
    }

}

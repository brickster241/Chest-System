using System;

namespace Chest.StateMachine {

    public class ChestBaseState
    {
        protected ChestSM chestSM;

        public ChestBaseState(ChestSM _chestSM) {
            chestSM = _chestSM;
        }

        public virtual void OnStateEnter() {}

        public virtual void OnStateExit() {}

        public virtual void OnStateUpdate() {}

        public virtual void OnChestButtonClicked() {}
    }

}

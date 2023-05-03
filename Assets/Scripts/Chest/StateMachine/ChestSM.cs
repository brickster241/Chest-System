using Chest.MVC;

namespace Chest.StateMachine {

    public enum ChestState {
        LOCKED,
        QUEUED,
        UNLOCKING,
        OPEN
    }

    public class ChestSM
    {
        public ChestBaseState currentChestState = null;
        public ChestState currentChestStateEnum = ChestState.LOCKED;
        private ChestLockedState lockedState;
        private ChestQueuedState queuedState;
        private ChestUnlockingState unlockingState;
        private ChestOpenState openState;
        private ChestController chestController;

        public ChestSM(ChestController _chestController) {
            chestController = _chestController;
            lockedState = new ChestLockedState(this);
            queuedState = new ChestQueuedState(this);
            unlockingState = new ChestUnlockingState(this);
            openState = new ChestOpenState(this);
            ResetSM();
        }

        public void ResetSM() {
            SwitchState(ChestState.LOCKED);
        }

        public ChestController GetChestController() {
            return chestController;
        }

        public void SwitchState(ChestState chestState) {
            ChestBaseState newState = GetChestBaseStateFromEnum(chestState);
            if (currentChestState == newState) {
                return;
            }
            if (currentChestState != null)
                currentChestState.OnStateExit();
            currentChestState = newState;
            currentChestStateEnum = chestState;
            currentChestState.OnStateEnter();
        }

        public ChestBaseState GetChestBaseStateFromEnum(ChestState chestState) {
            if (chestState == ChestState.LOCKED) {
                return lockedState;
            } else if (chestState == ChestState.QUEUED) {
                return queuedState;
            } else if (chestState == ChestState.UNLOCKING) {
                return unlockingState;
            } else if (chestState == ChestState.OPEN) {
                return openState;
            } else {
                return null;
            }
        }
    }


}

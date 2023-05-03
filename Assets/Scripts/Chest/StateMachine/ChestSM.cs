using Chest.MVC;

namespace Chest.StateMachine {
    /*
        Enum for Chest State.
    */
    public enum ChestState {
        LOCKED,
        QUEUED,
        UNLOCKING,
        OPEN
    }

    /*
        ChestSM class. Acts as a State Machine for Chest. Contains Reference to all different States & ChestController.
    */
    public class ChestSM
    {
        public ChestBaseState currentChestState = null;
        public ChestState currentChestStateEnum = ChestState.LOCKED;
        private ChestLockedState lockedState;
        private ChestQueuedState queuedState;
        private ChestUnlockingState unlockingState;
        private ChestOpenState openState;
        private ChestController chestController;

        /*
            Constructor to Initialize all ChestStates & set reference to ChestController.
        */
        public ChestSM(ChestController _chestController) {
            chestController = _chestController;
            lockedState = new ChestLockedState(this);
            queuedState = new ChestQueuedState(this);
            unlockingState = new ChestUnlockingState(this);
            openState = new ChestOpenState(this);
            ResetSM();
        }

        /*
            Resets the State to LOCKED. Gets called everytime when Chest is Spawned.
        */
        public void ResetSM() {
            SwitchState(ChestState.LOCKED);
        }

        /*
            Returns Reference to the ChestController connected with the StateMachine.
        */
        public ChestController GetChestController() {
            return chestController;
        }

        /*
            Switches the currentState of Chest.
            Also checks if we are trying to switch again into the currentState.
        */
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

        /*
            Returns reference to the ChestBaseState object with respect to the Enum Value.
        */
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

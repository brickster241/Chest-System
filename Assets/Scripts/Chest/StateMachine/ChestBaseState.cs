using System;

namespace Chest.StateMachine {

    /*
        ChestBaseState class. All Chest State classes inherit this class.
        Contains reference to the State Machine.
    */
    public class ChestBaseState
    {
        protected ChestSM chestSM;

        /*
            Constructor to set a refrence to Chest State Machine.
        */
        public ChestBaseState(ChestSM _chestSM) {
            chestSM = _chestSM;
        }

        /*
            Method gets called when Chest enters the state.
        */
        public virtual void OnStateEnter() {}

        /*
            Method gets called when Chest exits the state.
        */
        public virtual void OnStateExit() {}

        /*
            Method gets called every frame while Chest is in the state.
        */
        public virtual void OnStateUpdate() {}
    }

}

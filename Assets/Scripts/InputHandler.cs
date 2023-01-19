using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TH {
    public class InputHandler : MonoBehaviour {

        [Header("Inputs Controller")]
        public PongInputs pongInputs;

        [Header("Live Players Inputs For debug purpose")]
        public bool raquette_left_up_input;
        public bool raquette_left_down_input;
        public bool raquette_right_up_input;
        public bool raquette_right_down_input;
        public bool restart_input;
        public bool pause_input;

        private void OnEnable() {
            if (pongInputs == null) {
                pongInputs= new PongInputs();
            }
            pongInputs.Enable();
        }

        private void OnDisable() {
            pongInputs.Disable();
        }

        public void TickInput() {
            raquette_left_up_input = pongInputs.RaquetteLeft.Up.IsInProgress();
            raquette_left_down_input = pongInputs.RaquetteLeft.Down.IsInProgress();
            raquette_right_up_input = pongInputs.RaquetteRight.Up.IsInProgress();
            raquette_right_down_input = pongInputs.RaquetteRight.Down.IsInProgress();

            pongInputs.Actions.Restart.performed += i => restart_input = true;
            pongInputs.Actions.Pause.performed += i => pause_input = true;
        }
    }
}

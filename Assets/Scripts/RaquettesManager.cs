using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TH {
    public class RaquettesManager : MonoBehaviour {
        InputHandler inputHandler;
        public RaquetteLocomotion raquetteLeftLocomotion { get; private set; }
        public RaquetteLocomotion raquetteRightLocomotion { get; private set; }

        private void Awake() {
            inputHandler = GetComponent<InputHandler>();

            RaquetteLocomotion[] raquettes = GetComponentsInChildren<RaquetteLocomotion>();
            foreach (RaquetteLocomotion raquette in raquettes) {
                if (raquette.isLeft) {
                    raquetteLeftLocomotion = raquette;
                } else {
                    raquetteRightLocomotion = raquette;
                }
            }
        }

        private void Update() {
            float delta = Time.deltaTime;

            inputHandler.TickInput();

            raquetteLeftLocomotion.HandleMovement(delta);
            raquetteRightLocomotion.HandleMovement(delta);
        }

        private void FixedUpdate() {
        }

        public void Reset() {
            raquetteLeftLocomotion.Reset();
            raquetteRightLocomotion.Reset();
        }
    }
}

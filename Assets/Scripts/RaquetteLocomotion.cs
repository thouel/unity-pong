using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TH {
    public partial class RaquetteLocomotion: MonoBehaviour {
        InputHandler inputHandler;
        public new Rigidbody2D rigidbody;

        public float movementSpeed = 2.0f;

        public bool isLeft;

        private Vector3 origin;

        private void Awake() {
            inputHandler = GetComponentInParent<InputHandler>();
            rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Start() {
            origin = transform.position;
        }

        public void Reset() {
            transform.position = origin;
        }

        public void HandleMovement(float delta) {
            bool up = isLeft ? inputHandler.raquette_left_up_input : inputHandler.raquette_right_up_input;
            bool down = isLeft ? inputHandler.raquette_left_down_input : inputHandler.raquette_right_down_input;

            // Do not move
            if (up && down) {
                return;
            }

            if (up) {
                Vector3 pos = transform.position + transform.up * movementSpeed * delta;
                if (pos.y > 3.5f) pos.y = 3.65f;
                transform.position = pos;
            }
            if (down) {
                Vector3 pos = transform.position - transform.up * movementSpeed * delta;
                if (pos.y < -3.5f) pos.y = -3.65f;
                transform.position = pos;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TH {
    public class PaletLocomotion : MonoBehaviour {

        [Header("Palet Speed")]
        public float initialSpeed = 10.0f;
        public float currentSpeed;
        public float movementSpeedIncrement = 0.5f;

        public new Rigidbody2D rigidbody;

        private Vector3 origin;
        private Vector2 velocityBeforePause;

        private void Awake() {
            rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Start() {
            currentSpeed = initialSpeed;
            origin = transform.position;
        }

        public void Reset() {
            transform.position = origin;
            currentSpeed = initialSpeed;
        }

        public void LaunchPalet() {
            int x = Random.Range(-1, 1);
            if (x == 0 ) { x++; }
            rigidbody.velocity = new Vector2(x, 0) * currentSpeed;
        }

        public void Pause() {
            velocityBeforePause = rigidbody.velocity;
            rigidbody.velocity = new Vector2(0, 0);
        }

        public void Unpause() {
            rigidbody.velocity = velocityBeforePause;
        }

        public void MoveAfterCollision(Vector3 raquettePosition, float raquetteHeight, bool isLeftRaquette) {
            float y = HitFactor(transform.position, raquettePosition, raquetteHeight);

            // Calculate direction, make length=1 via .normalized
            Vector2 dir = new Vector2((isLeftRaquette ? 1 : -1), y).normalized;

            // Increase the movement of the palet
            IncreaseMovementSpeed();

            // Set Velocity with dir * speed
            rigidbody.velocity = dir * currentSpeed;
        }
        private float HitFactor(Vector2 paletPosition, Vector2 raquettePosition, float raquetteHeight) {
            return (paletPosition.y - raquettePosition.y) / raquetteHeight;
        }

        private void IncreaseMovementSpeed() {
            currentSpeed += movementSpeedIncrement;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TH {
    public class PaletCollider : MonoBehaviour {
        PaletLocomotion paletLocomotion;
        GameManager gameManager;

        private void Awake() {
            paletLocomotion = GetComponent<PaletLocomotion>();
            gameManager = GetComponent<GameManager>();
        }

        private void OnCollisionEnter2D(Collision2D other) {
            
            // The palet hit the left raquette
            if (other.gameObject.CompareTag("R_L")) {
                
                paletLocomotion.MoveAfterCollision(other.transform.position, other.collider.bounds.size.y, true);

            } else if (other.gameObject.CompareTag("R_R")) {
                // The palet hit the right raquette
                paletLocomotion.MoveAfterCollision(other.transform.position, other.collider.bounds.size.y, false);

            } else if (other.gameObject.CompareTag("Goal_Left")) {
                
                // The palet touched the left goal, then score for right team
                gameManager.ScoreGoal(false);

            } else if (other.gameObject.CompareTag("Goal_Right")) {
                
                // The palet touched the right goal, then score for left team
                gameManager.ScoreGoal(true);
            }
        }
    }
}

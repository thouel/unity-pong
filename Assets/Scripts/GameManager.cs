using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TH {
    public class GameManager : MonoBehaviour {
        PaletLocomotion paletLocomotion;
        RaquettesManager raquettesManager;
        InputHandler inputHandler;

        [Header("UI Handling")]
        public Text leftTeamScoreUI;
        public Text rightTeamScoreUI;
        public SpriteRenderer leftGoalUI;
        public SpriteRenderer rightGoalUI;
        public GameObject pauseUI;
        private Coroutine blinkRoutine;
        public float blinkDuration;

        [Header("Score")]
        public int leftTeamScore;
        public int rightTeamScore;

        private bool pauseFlag;


        private void Awake() {
            paletLocomotion = GetComponent<PaletLocomotion>();
            raquettesManager = FindObjectOfType<RaquettesManager>();
            inputHandler = FindObjectOfType<InputHandler>();
        }

        private void Start() {
            leftTeamScore = 0;
            rightTeamScore = 0;
            pauseFlag = false;
        }

        private void Update() {
            HandlePause(); 
            HandleRestart();
        }

        private void FixedUpdate() {
        }
        private void LateUpdate() {
            inputHandler.raquette_left_up_input = false;
            inputHandler.raquette_left_down_input = false;
            inputHandler.raquette_right_up_input = false;
            inputHandler.raquette_right_down_input = false;
            inputHandler.restart_input = false;
            inputHandler.pause_input = false;
        }

        private void HandleRestart() {
            
            if (!inputHandler.restart_input || pauseFlag) {
                return;
            }

            // Raz scores
            leftTeamScore = 0;
            rightTeamScore = 0;
            leftTeamScoreUI.text = 0.ToString();
            rightTeamScoreUI.text = 0.ToString();

            // Raz flags
            pauseFlag = false;

            // Raz Raquettes
            raquettesManager.Reset();

            // Raz Palet
            paletLocomotion.Reset();

            // Launch Palet
            paletLocomotion.LaunchPalet();
        }

        private void HandlePause() {
            if (!inputHandler.pause_input) {
                return;
            }

            if (!pauseFlag) {
                // Pause the game
                // Nothing can move
                pauseUI.SetActive(true);
                paletLocomotion.Pause();
                pauseFlag = true;
            } else {
                // Already in pause, then unpause the game
                pauseUI.SetActive(false);
                paletLocomotion.Unpause();
                pauseFlag = false;
            }
        }

        // Restart

        public void ScoreGoal(bool forLeftTeam) {
            if (forLeftTeam) {
                leftTeamScore += 1;
                leftTeamScoreUI.text = leftTeamScore.ToString();
            } else {
                rightTeamScore += 1;
                rightTeamScoreUI.text = rightTeamScore.ToString();
            }
            Blink(forLeftTeam);
        }

        public void Blink(bool forLeftTeam) {
            // If the flashRoutine is not null, then it is currently running.
            if (blinkRoutine != null) {
                // In this case, we should stop it first.
                // Multiple FlashRoutines the same time would cause bugs.
                StopCoroutine(blinkRoutine);
            }

            // Start the Coroutine, and store the reference for it.
            blinkRoutine  = StartCoroutine(BlinkRoutine(forLeftTeam));
        }

        private IEnumerator BlinkRoutine(bool forLeftTeam) {
            SpriteRenderer theOneToBlink = (forLeftTeam ? rightGoalUI : leftGoalUI);

            Color theColorToKeep = theOneToBlink.color;

            // Swap to the flashMaterial.
            theOneToBlink.color = Color.white;

            // Pause the execution of this function for "duration" seconds.
            yield return new WaitForSeconds(blinkDuration);

            // After the pause, swap back to the original material.
            theOneToBlink.color = theColorToKeep;

            // Set the routine to null, signaling that it's finished.
            blinkRoutine = null;
        }
    }
}

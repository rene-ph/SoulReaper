using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Assets.Scripts
{
    public class GameManagerTutorial : MonoBehaviour
    {


        public GameObject MovementGO;
        public GameObject FinishStepGO;
        public GameObject MovementBoostGO;
        public GameObject CollectGO;
        public GameObject ArrowsGO;
        public GameObject TutorialFinishedGO;

        bool isFinishMovement = false;
        bool isFinishSpeedBoost = true;
        bool isFinishCollectables = true;
        bool isFinishArrow = true;

        Reaper reaper;
        public int souls;
        public int arrows;

        List<string> movementsDone =  new List<string>(); 

        void Start()
        {
            reaper = FindObjectOfType<Reaper>();
        }

    
        void Update()
        {
            if (isFinishMovement == false)
            {
                MovementTutorial();
            }
            else if (isFinishSpeedBoost == false)
            {
                MovementSpeedTutorial();
            }    
            else if(isFinishCollectables == false)
            {
               CollectablesTutorial();
            }
            else if(isFinishArrow == false)
            {
                ArrowsTutorial();
            }
        
        
        }

        private void ArrowsTutorial()
        {
            if (arrows == 0)
            {
                StartCoroutine(ShowFinishStep(ArrowsGO, TutorialFinishedGO));
                StartCoroutine(StopBoosted(0.6f));
                isFinishArrow = true;
            }
        }

        private IEnumerator StopBoosted(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            StopReaper();
            isFinishArrow = true;
        }

        private void CollectablesTutorial()
        {

            if(souls == 0)
            {
                StartCoroutine(ShowFinishStep(CollectGO, ArrowsGO));
                StopReaper();
                isFinishCollectables = true;
                isFinishArrow = false;
            }
        }

        private void MovementSpeedTutorial()
        {
            foreach (Touch touch in Input.touches)
            {
                switch (touch.phase)
                {
                    case TouchPhase.Ended:

                       if(touch.tapCount >= 2)
                        {
                            StartCoroutine(ShowFinishStep(MovementBoostGO, CollectGO));
                            StartCoroutine(StopBoostedReaper(0.6f));

                        }
                        break;
                }
            }
        }


        /// <summary>
        /// Corutine to stop the reaper that has movement boost. 
        /// </summary>
        /// <param name="seconds"></param>
        /// <returns></returns>
        private IEnumerator StopBoostedReaper(float seconds)
        {
          yield return new WaitForSeconds(seconds);
          StopReaper();
          isFinishSpeedBoost = true;
          isFinishCollectables = false;
          reaper.setPosition(Reaper.StatePosition.NONE);
          reaper.speedMovement = 3;
        }


        /// <summary>
        /// Tutorial that will finish when the user does all the movements
        /// </summary>
        private void MovementTutorial()
        {
            foreach (Touch touch in Input.touches)
            {
                switch (touch.phase)
                {
                    case TouchPhase.Ended:

                        if (!movementsDone.Contains(reaper.getPosition().ToString()))
                        {
                            movementsDone.Add(reaper.getPosition().ToString());

                            if (movementsDone.Count == 4)
                            {
                                StopReaper();
                                StartCoroutine(ShowFinishStep(MovementGO, MovementBoostGO));
                                isFinishMovement = true;
                                isFinishSpeedBoost = false;
                            }
                        }
                        break;
                }
            }
        }


        /// <summary>
        /// Sets the interval between the finish of the current tutorial to the next one
        /// </summary>
        /// <param name="currentGO">Current Tutorial active</param>
        /// <param name="nextGO">Next Tutorial to be active</param>
        /// <returns></returns>
        IEnumerator ShowFinishStep(GameObject currentGO, GameObject nextGO)
        {
            currentGO.SetActive(false);

            FinishStepGO.SetActive(true);
            yield return new WaitForSeconds(2f);
            FinishStepGO.SetActive(false);

            ShowNextTutorial(nextGO);
        }


        private void ShowNextTutorial(GameObject tutorialGO)
        {
            tutorialGO.SetActive(true);
        }

        /// <summary>
        /// Stops the movement of the character and aligns to the center
        /// </summary>
        private void StopReaper()
        {
            reaper.speedMovement = 0;
            reaper.transform.position = new Vector2(0f, 0f);
        }


        public void setSouls(int amount)
        {
            souls -= amount;
        }


        public void setArrows(int amount)
        {
            arrows -= amount;
        }
    }

}

using UnityEngine;
using System.Collections;
using System;

namespace Assets.Scripts
{
    public class Soul : MonoBehaviour
    {

        #region Variables

        GameManager gameManager;
        GameManagerTutorial gameManagerTuto;
        AudioSource soulAudio;

        public int points;
        public bool Tutorial;

        #endregion


        #region UnityEvents

  

        void Start()
        {
            SetSoul(); 
        }


        /// <summary>
        /// Destroy the current object and sets the points if the reaper has touch it. 
        /// It only recolects souls when the shield is not enable 
        /// </summary>
        /// <param name="collision">The object that makes the collision</param>
        void OnCollisionEnter2D(Collision2D collision)
        {
            ReaperCollision(collision);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Interacts between the soul and the reaper and his shield
        /// </summary>
        /// <param name="collision"></param>
        private void ReaperCollision(Collision2D collision)
        {
            foreach (Transform child in collision.gameObject.transform)
            {
                switch (child.tag)
                {
                    case "Shield":
                        if (child.gameObject.activeInHierarchy == false && collision.gameObject.tag == "Reaper")
                        {
                            if(Tutorial)
                            {
                                Destroy(this.gameObject);
                                gameManagerTuto.setSouls(1);

                            }
                            else
                            {
                                soulAudio.PlayOneShot(soulAudio.clip);
                                Destroy(this.gameObject);
                                gameManager.setPoints(points);
                            }
                          
                        }
                        break;
                }

            }
        }

        private void SetSoul()
        {
            if (Tutorial)
            {
                gameManagerTuto = FindObjectOfType<GameManagerTutorial>();
            }
            else
            {
                gameManager = FindObjectOfType<GameManager>();
                soulAudio = gameManager.GetComponent<AudioSource>();
             
            }
        }



        #endregion
    }
}

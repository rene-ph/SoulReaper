using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

namespace Assets.Scripts
{
    public class Arrow : MonoBehaviour
    {
        #region Variables

        public int arrowSpeed;
        public bool Tutorial;

        Soul soul;
        GameManager gameManager;
        GameManagerTutorial gameManagerTuto;

		Reaper reaper;

		#endregion

		#region UnityEvents

		void Awake()
		{
			reaper = FindObjectOfType<Reaper>();
		}

		void Start()
        {
            SetArrow();
		}


        void Update()
        {
            this.transform.Translate(Vector3.down * Time.deltaTime * arrowSpeed);
        }

        void OnCollisionEnter2D(Collision2D collision)
        {

            DestroyArrowInsideLimit(collision);
            ReaperCollision(collision);

        }


        #endregion

        #region Methods

        /// <summary>
        /// Destroy all the arrows that are outside of the map
        /// </summary>
        /// <param name="collision"></param>
        private void DestroyArrowInsideLimit(Collision2D collision)
        {
            if (collision.gameObject.tag == "LimitDown" ||
                collision.gameObject.tag == "LimitLeft" ||
                collision.gameObject.tag == "LimitRight")
            {
                Destroy(this.gameObject);
            }

        }

        private void SetArrow()
        {
            if (Tutorial)
            {
                gameManagerTuto = FindObjectOfType<GameManagerTutorial>();
            }
            else
            {
                soul = FindObjectOfType<Soul>();
                gameManager = FindObjectOfType<GameManager>();
            }

        }

        /// <summary>
        /// Interaction between the collision of the arrow and the reaper and his shield
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
                            Destroy(this.gameObject);
                            reaper.DownLife();
							reaper.SoundHit();
							reaper.AnimationHit();
						}
                        else
                        {
                            Destroy(this.gameObject);

                            if(Tutorial)
                            {
                                gameManagerTuto.setArrows(1);
                            }
                        }
                        break;
                }
            }

        }



		#endregion
	}
}

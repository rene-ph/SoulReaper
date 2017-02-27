using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

namespace Assets.Scripts
{
    public class Reaper : MonoBehaviour
    {
        #region Variables 

        public float speedMovement;
        public GameObject reaperShield;
        public GameObject topArrow;
        public GameObject downArrow;
        public GameObject leftArrow;
        public GameObject rightArrow;

        private Camera mainCamera;
        SpriteRenderer reaperSprite;
		AudioSource hitSound;
		GameManager gameManager;

		private Vector2 touchPosition;

        Vector2 bottomLeftWorldCoordinates;
        Vector2 topRightWorldCoordinates;

        private double angle;

        int tapCount;

        float offsetTop;
        float offsetDown;
        float offsetRight;
        float offsetLeft;
       


        /// <summary>
        /// The different ways to control the character
        /// </summary>
        public enum StatePosition
        {
            NONE,
            TOP,
            DOWN,
            LEFT,
            RIGHT
        };

        StatePosition statePosition = StatePosition.NONE;

        #endregion

        #region UnityEvents

        void Start()
        {
			gameManager = FindObjectOfType<GameManager>();
			reaperSprite = this.GetComponent<SpriteRenderer>();
            mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
			hitSound = gameManager.GetComponents<AudioSource>()[1];
		}

        void Update ()
        {
            UpdateMovement();
            WorldBorders();
            SpeedUp();

			DebugPCControls();
        }


		/// <summary>
		/// Controls to test in PC
		/// </summary>
		private void DebugPCControls()
		{
			switch(Input.inputString) {
				case "w":
					statePosition = StatePosition.TOP;
					break;
				case "s":
					statePosition = StatePosition.DOWN;
					break;
				case "d":
					statePosition = StatePosition.RIGHT;
					break;
				case "a":
					statePosition = StatePosition.LEFT;
					break;
				case "q":
					StartCoroutine(SpeedAndInvincible());
					break;
			}

		}

		/// <summary>
		/// Defines the bordes on the map. 
		/// </summary>
		private void WorldBorders()
        {
            bottomLeftWorldCoordinates = mainCamera.ViewportToWorldPoint(Vector2.zero);
            topRightWorldCoordinates = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));

            offsetTop  = -0.8f;
            offsetDown = 0.8f;
            offsetLeft = 0.8f;
            offsetRight = -0.8f;
        }


        #endregion

        #region Methods


        public void goReaperTop()
        {
            statePosition = StatePosition.TOP;
            StartCoroutine(ActiveArrowControls(topArrow));
        }

        public void goReaperDown()
        {
            statePosition = StatePosition.DOWN;
            StartCoroutine(ActiveArrowControls(downArrow));
        }

        public void goReaperLeft()
        {
            statePosition = StatePosition.LEFT;
            StartCoroutine(ActiveArrowControls(leftArrow));
        }

        public void goReaperRight()
        {
            statePosition = StatePosition.RIGHT;
            StartCoroutine(ActiveArrowControls(rightArrow));
        }

        private IEnumerator ActiveArrowControls(GameObject Arrow)
        {
            Arrow.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            Arrow.SetActive(false);
        }

        private void SpeedUp()
        {
            foreach (Touch touch in Input.touches)
            {
                switch (touch.phase)
                {
                    case TouchPhase.Ended:
                        tapCount = touch.tapCount;
                        if (tapCount >= 2)
                        {
                            StartCoroutine(SpeedAndInvincible());
                        }
                        break;
                }
            }
        }



   
		public void SpeedUpButton() 
		{
			StartCoroutine(SpeedAndInvincible());
		}

        IEnumerator SpeedAndInvincible()
        {
            speedMovement = 6;
            Invisible();
            yield return new WaitForSeconds(0.5f);
            speedMovement = 3;
            NoInvisible();
        }

        private void NoInvisible()
        {
            reaperShield.SetActive(false);
            reaperSprite.color = Color.white;
        }

        /// <summary>
        /// Makes the reaper invisible for a couple of seconds and it changes his color
        /// </summary>
        private void Invisible()
        {
            reaperShield.SetActive(true);
            reaperSprite.color = Color.red;
        }



        /// <summary>
        /// Moves the character according to the position that is set. 
        /// </summary>
        private void UpdateMovement()
        {
            switch(statePosition)
            {
                case StatePosition.TOP:
                    this.transform.Translate(Vector3.up * Time.deltaTime * speedMovement);
                    this.transform.position = new Vector3(this.transform.position.x, Mathf.Clamp(this.transform.position.y, bottomLeftWorldCoordinates.y, topRightWorldCoordinates.y + offsetTop));
                    break;
                case StatePosition.DOWN:
                    this.transform.Translate(Vector3.down * Time.deltaTime * speedMovement, Space.World);
                    this.transform.position = new Vector3(this.transform.position.x, Mathf.Clamp(this.transform.position.y, bottomLeftWorldCoordinates.y + offsetDown, topRightWorldCoordinates.y ));
                    break;
                case StatePosition.RIGHT:
                    this.transform.Translate(Vector3.right * Time.deltaTime * speedMovement, Space.World);
                    this.transform.position = new Vector3(Mathf.Clamp(this.transform.position.x, bottomLeftWorldCoordinates.x, topRightWorldCoordinates.x + offsetRight), this.transform.position.y);
                    break;
                case StatePosition.LEFT:
                    this.transform.Translate(Vector3.left * Time.deltaTime * speedMovement, Space.World);
                    this.transform.position = new Vector3(Mathf.Clamp(this.transform.position.x, bottomLeftWorldCoordinates.x + offsetLeft, topRightWorldCoordinates.x), this.transform.position.y);
                    break;
            }
        }


        public StatePosition getPosition()
        {
            return statePosition;
        }

        public void setPosition ( StatePosition newState)
        {
            statePosition = newState;
        }

		public void AnimationHit()
		{
			hitSound.PlayOneShot(hitSound.clip);
		}

		public void SoundHit()
		{
			this.GetComponent<Animator>().SetTrigger("isHit");
		}

		public void DownLife()
		{
			gameManager.life -= 1;
			gameManager.lifeGO.GetComponent<Text>().text = gameManager.life.ToString();
		}




		#endregion
	}
}

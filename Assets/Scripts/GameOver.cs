using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

namespace Assets.Scripts
{
    public class GameOver : MonoBehaviour
    {
        #region Variables 

        public GameObject pointsGO;
        GameManager gameManager;

        Text textPoints;

        #endregion

        #region UnityEvents

        void Start()
        {
            gameManager = FindObjectOfType<GameManager>();
            textPoints = pointsGO.GetComponent<Text>();
        }


        void Update()
        {
            FinalScore();
        }

        #endregion

        #region Methods

        private void FinalScore()
        {
            textPoints.text = gameManager.getPoints().ToString();
        }

        #endregion
    }
}

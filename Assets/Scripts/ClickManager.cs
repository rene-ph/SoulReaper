using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

namespace Assets.Scripts
{
    public class ClickManager : MonoBehaviour
    {
        #region Variables 

        GameManager gameManager;
        Reaper reaper;

        int initialLife = 3;
        int initialPoints = 0;


        #endregion

        #region UnityEvents

        void Start()
        {
            gameManager = FindObjectOfType<GameManager>();
           
        }

        //This is because the reaper is destroyed every time and crash the script
        void Update ()
        {
            reaper = FindObjectOfType<Reaper>();
        }

        #endregion


        #region Methods

        #region WorldScene

        public void GoToWorld1()
        {
            SceneManager.LoadScene("World_1", LoadSceneMode.Single); 

        }

        public void GoToWorld2()
        {
            SceneManager.LoadScene("World_2", LoadSceneMode.Single);
        }


        public void GoToWorld3()
        {
            SceneManager.LoadScene("World_3", LoadSceneMode.Single);

        }


        public void GoToWorld4()
        {
            SceneManager.LoadScene("World_4", LoadSceneMode.Single);
        }

        public void GoToWorld5()
        {
            SceneManager.LoadScene("World_5", LoadSceneMode.Single);
        }
        #endregion


        #region Victory/Lose scene

        public void NextLevel()
        {
            gameManager.ChangeLevel();
            gameManager.StartGame();

        }


        public void TryAgain()
        {
            gameManager.life = initialLife;
            gameManager.points = initialPoints;
            gameManager.ResetLevel();
            gameManager.gamelevel = GameManager.GameLevel.Easy;
            gameManager.StartGame();
        }

        #endregion

        #region MenuScene

        public void NewGame()
        {
            SceneManager.LoadScene("World", LoadSceneMode.Single);
        }


        public void BackToMenu()
        {
            SceneManager.LoadScene("Menu", LoadSceneMode.Single); 
        }

        public void GoToScores()
        {
            SceneManager.LoadScene("Scores", LoadSceneMode.Single);
        }

        public void GoToTutorial()
        {
            SceneManager.LoadScene("Tutorial", LoadSceneMode.Single);
        }


        public void Exit()
        {
            Application.Quit();
        }

        #endregion

        #endregion

        #region ControlMovement

        public void goTop()
        {
            reaper.goReaperTop();
        }

        public void goDown()
        {
            reaper.goReaperDown();
        }

        public void goLeft()
        {
            reaper.goReaperLeft();
        }

        public void goRight()
        {
            reaper.goReaperRight();
        }

		public void boost() 
		{
			reaper.SpeedUpButton();
		}
        #endregion

    }
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        #region Variables

        [Header("World Settings")]

        [Tooltip("Player's current point")]
        public int points;
        [Tooltip("Player's current life")]
        public int life;
        [Tooltip("Max arrow rate in the current level")]
        public int rateArrow;
		[Tooltip("Current level")]
		public int currentLevel;
		[Tooltip("Sets the current GameWorld")]
        public GameWorlds currentWorld ;
        [Tooltip("Current GameLevel")]
        public GameLevel gamelevel;

        [Header("GameObjects of the world")]

        public GameObject pointsGO;
        public GameObject gameStartGO;
        public GameObject gameOverGO;
        public GameObject victoryGO;
        public GameObject loseGO;
		public GameObject currentLevelGO;
		public GameObject lifeGO;

		[Header("Spawns position")]

		public GameObject reaperSpawn;
        public GameObject soulSpawn;
        public GameObject arrowSpawnTop;
        public GameObject arrowSpawnLeft;
        public GameObject arrowSpawnRight;
        public GameObject arrowSpawnDown;
		public GameObject infernoWheelSpawn;
		public GameObject demonBatSpawn;
		public GameObject shurikenSpawn;

		GameObject reaperInstance;
        GameObject soulInstance;
        GameObject arrowInstanceTop;
        GameObject arrowInstanceLeft;
        GameObject arrowInstanceRight;


        System.Random random = new System.Random();

		List<GenerateMatrix.Point> lstCoordinates = new List<GenerateMatrix.Point>();
        int maxLevelsPerWorld = 10;
        int souls;
		int rowSouls;
		int columSouls;

		Text textPoints;

        /// <summary>
        /// Different states of the game
        /// </summary>
        enum GameState
        {
            Start,
            Victory,
            Lose
        }

        /// <summary>
        /// Different levels of the game
        /// </summary>
        public enum GameLevel
        {
            Easy,
            Normal,
            Hard
        }

        public enum GameWorlds
        {
            Bonus, 
            World_1,
            World_2,
            World_3,
            World_4,
            World_5
        };


        GameState currentGame = GameState.Start;

        #endregion

        #region UnityEvents

        void Start()
        {
            textPoints = pointsGO.GetComponent<Text>();
			currentLevelGO.GetComponent<Text>().text = currentLevel.ToString();
			StartGame();

		}

        void Update()
        {
            UpdateScore();
            IsGameOver();

		}
		void FixedUpdate() 
		{ 
			Screen.SetResolution(650, 1100, false);
	    }

		#endregion

		#region Methods

		/// <summary>
		/// Starts a new game in the same world. 
		/// </summary>
		public void StartGame()
        {
            NewGameLevel();
            GenerateCharacter();
            GenerateSoulMatrix();
			GenerateObstacles();      
   
        }


		/// <summary>
		/// Generates the obstacles according to the current world. 
		/// </summary>
		private void GenerateObstacles()
		{
			switch(currentWorld) 
			{
				case GameWorlds.World_1:
					GenerateArrows();
					break;
				case GameWorlds.World_2:
					GenerateArrows();
					GenerateInfernoWheels();
					break;
				case GameWorlds.World_3:
					GenerateDemonBat();
					GenerateInfernoWheels();
					break;
				case GameWorlds.World_4:
					GenerateArrows();
					GenerateShuriken();
					break;
				case GameWorlds.World_5:
					GenerateArrows();
					GenerateInfernoWheels();
					GenerateDemonBat();
					GenerateShuriken();
					break;
			}
		}

		private void GenerateShuriken()
		{
			switch (gamelevel)
			{
				case GameLevel.Easy:
					Instantiate(Resources.Load("Shuriken"), shurikenSpawn.transform, false);
					break;
				case GameLevel.Normal:
					var ShurikenNormal1 = Instantiate(Resources.Load("Shuriken"), shurikenSpawn.transform, false) as GameObject;
					ShurikenNormal1.GetComponent<Shuriken>().speed = 5;

					var ShurikenNormal2 = Instantiate(Resources.Load("Shuriken"), shurikenSpawn.transform, false) as GameObject;
					ShurikenNormal2.GetComponent<Shuriken>().speed = 5;

					break;
				case GameLevel.Hard:
					var ShurikenHard1 = Instantiate(Resources.Load("Shuriken"), shurikenSpawn.transform, false) as GameObject;
					ShurikenHard1.GetComponent<Shuriken>().speed = 10;

					var ShurikenHard2 = Instantiate(Resources.Load("Shuriken"), shurikenSpawn.transform, false) as GameObject;
					ShurikenHard2.GetComponent<Shuriken>().speed = 10;

					var ShurikenHard3 = Instantiate(Resources.Load("Shuriken"), shurikenSpawn.transform, false) as GameObject;
					ShurikenHard3.GetComponent<Shuriken>().speed = 10;
					break;
			}
		}

		private void GenerateDemonBat()
		{
			switch (gamelevel)
			{
				case GameLevel.Easy:
					Instantiate(Resources.Load("DemonBat"), demonBatSpawn.transform, false);
					break;
				case GameLevel.Normal:
					Instantiate(Resources.Load("DemonBat"), demonBatSpawn.transform, false);

					var DeamonBat = Instantiate(Resources.Load("DemonBat"), demonBatSpawn.transform, false) as GameObject;
					DeamonBat.GetComponent<DeamonBat>().defaultMovement = Scripts.DeamonBat.StatePosition.RIGHT;

					break;
				case GameLevel.Hard:
					Instantiate(Resources.Load("DemonBat"), demonBatSpawn.transform, false);

					var DeamonBat2 = Instantiate(Resources.Load("DemonBat"), demonBatSpawn.transform, false) as GameObject;
					DeamonBat2.GetComponent<DeamonBat>().defaultMovement = Scripts.DeamonBat.StatePosition.RIGHT;

					break;
			}
		}

		private void GenerateInfernoWheels()
		{
			switch (gamelevel)
			{
				case GameLevel.Easy:
				    Instantiate(Resources.Load("InfernoWheel"), infernoWheelSpawn.transform, false); 
					break;
				case GameLevel.Normal:
					Instantiate(Resources.Load("InfernoWheel"), infernoWheelSpawn.transform, false);

					var Inferno = Instantiate(Resources.Load("InfernoWheel"), infernoWheelSpawn.transform, false) as GameObject;
					Inferno.GetComponent<InfernoWheel>().InfernoWheels(3f, 3f, 4f, 4f, 0.1f);

					var Inferno2 = Instantiate(Resources.Load("InfernoWheel"), infernoWheelSpawn.transform, false) as GameObject;
					Inferno2.GetComponent<InfernoWheel>().InfernoWheels(3f, 1f, 5f, 1f, 1f);


					break;
				case GameLevel.Hard:
					Instantiate(Resources.Load("InfernoWheel"), infernoWheelSpawn.transform, false);

					var InfernoHard = Instantiate(Resources.Load("InfernoWheel"), infernoWheelSpawn.transform, false) as GameObject;
					InfernoHard.GetComponent<InfernoWheel>().InfernoWheels(3f, 3f, 4f, 4f, 0.1f);

					var InfernoHard2 = Instantiate(Resources.Load("InfernoWheel"), infernoWheelSpawn.transform, false) as GameObject;
				    InfernoHard2.GetComponent<InfernoWheel>().InfernoWheels(3f, 1f, 5f, 1f, 1f);

					var InfernoHard3 = Instantiate(Resources.Load("InfernoWheel"), infernoWheelSpawn.transform, false) as GameObject;
					InfernoHard3.GetComponent<InfernoWheel>().InfernoWheels(3f, 5f, 0f, 3f, 0.8f);

					var InfernoHard4 = Instantiate(Resources.Load("InfernoWheel"), infernoWheelSpawn.transform, false) as GameObject;
					InfernoHard4.GetComponent<InfernoWheel>().InfernoWheels(-3f, 5f, 0f, 3f, 0.8f);

					break;
			}
		}

		/// <summary>
		/// If the player reaches the max leves per world then it has access to the new world. 
		/// </summary>
		private void NextWorld()
        {

            if (maxLevelsPerWorld <= currentLevel)
            {
                GenerateNewWorld();
            }

        }

		/// <summary>
		/// Loads the new world when the game is over 
		/// </summary>
        private void GenerateNewWorld()
        {
            int newGameWorld = 0;

            newGameWorld = currentWorld.GetHashCode() + 1;
            GameWorlds newWorld = (GameWorlds)newGameWorld;
            currentWorld = newWorld;

            switch (newWorld)
            {
                case GameWorlds.World_2:
                    SceneManager.LoadScene("World_2", LoadSceneMode.Single);
                    break;
                case GameWorlds.World_3:
                    SceneManager.LoadScene("World_3", LoadSceneMode.Single);
                    break;
                case GameWorlds.World_4:
                    SceneManager.LoadScene("World_4", LoadSceneMode.Single);
                    break;
                case GameWorlds.World_5:
                    SceneManager.LoadScene("World_5", LoadSceneMode.Single);
                    break;
            }
            
        }

        private void GenerateCharacter()
        {
            reaperInstance = Instantiate(Resources.Load("Reaper"), reaperSpawn.transform, false) as GameObject;
            reaperInstance.transform.position = new Vector2 (reaperSpawn.transform.position.x, reaperSpawn.transform.position.y);
        }

		/// <summary>
		/// Creates a new draw of souls to be render randomly
		/// It switches between levels and creates the respective soul for the level
		/// </summary>

		private void GenerateSoulMatrix()
		{
			float gapBewteenX = 0;
			float gapBetweenY = 0;
			float spriteWidth = 0;
			float spriteHeight = 0;
			float spritePixelPerUnit = 0;

			lstCoordinates = GenerateMatrix.GenerateRandomShape();

			for (int row = 1; row <= rowSouls; row++)
			{
			    
				if (row > 1)
				{
				    gapBewteenX = 0;
					gapBetweenY += (spriteHeight / spritePixelPerUnit) * 1.5f;
			    }

			    for (int column = 1; column <= columSouls; column++)
				{

					soulInstance = GenerateSoul(lstCoordinates, row, column);

					soulInstance.transform.position = new Vector2(soulSpawn.transform.position.x + gapBewteenX, soulSpawn.transform.position.y + gapBetweenY);

					spriteHeight = soulInstance.GetComponent<SpriteRenderer>().sprite.rect.height;
					spriteWidth = soulInstance.GetComponent<SpriteRenderer>().sprite.rect.width;
					spritePixelPerUnit = soulInstance.GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;

					if(soulInstance.tag == "EmptySoul") 
					{
						Destroy(soulInstance);
					}

					gapBewteenX += (spriteWidth / spritePixelPerUnit) * 1.5f;
				}

			}
		}


		/// <summary>
		/// Creates the different soul depending if its renderable or not
		/// </summary>
		/// <param name="lstCoordinates"> List that contains the coordinats to draw</param>
		/// <param name="row">Row's number to be tested if it is inside the list of coordinates</param>
		/// <param name="column">Column's number to be tested if it is inside the list of coordinates</param>
		/// <returns></returns>
		private GameObject GenerateSoul(List<GenerateMatrix.Point> lstCoordinates, int row, int column)
		{

			if (lstCoordinates.Contains(new GenerateMatrix.Point(row, column)))
			{
				lstCoordinates.Remove(new GenerateMatrix.Point(row, column));
				return CreateNewSoul();
			}
			else
			{
				return CreateEmptySoul();
			}
		}

		/// <summary>
		/// Creates an empty soul to ocuppy the space in the grid that is not collectable
		/// </summary>
		/// <returns></returns>
		private GameObject CreateEmptySoul()
		{
			return Instantiate(Resources.Load("EmptySoul"), soulSpawn.transform, false) as GameObject;
		}

		/// <summary>
		/// Creates a new soul according to the game level
		/// </summary>
		/// <returns></returns>
		private GameObject CreateNewSoul()
        {
            switch (currentWorld)
            {
                case GameWorlds.World_1:
                    return Instantiate(Resources.Load("Soul"), soulSpawn.transform, false) as GameObject;
                case GameWorlds.World_2:
                    return Instantiate(Resources.Load("SoulB"), soulSpawn.transform, false) as GameObject;
                case GameWorlds.World_3:
                    return Instantiate(Resources.Load("SoulC"), soulSpawn.transform, false) as GameObject;
				case GameWorlds.World_4:
					return Instantiate(Resources.Load("SoulC"), soulSpawn.transform, false) as GameObject;
				case GameWorlds.World_5:
					return Instantiate(Resources.Load("SoulC"), soulSpawn.transform, false) as GameObject;
			}
            return null;
        }




        /// <summary>
        /// Generates the arrows according to the game level
        /// </summary>

        void GenerateArrows()
        {
            switch(gamelevel)
            {
                case GameLevel.Easy:
                    InvokeRepeating("CreateNewArrowTop", 0.5f, rateArrow);
                    break;
                case GameLevel.Normal:
                    InvokeRepeating("CreateNewArrowTop", 0.5f, rateArrow);
                    InvokeRepeating("CreateNewArrowLeft", 1f, rateArrow);
                    break;
                case GameLevel.Hard:
                    InvokeRepeating("CreateNewArrowTop", 0.5f, rateArrow);
                    InvokeRepeating("CreateNewArrowLeft", 0.5f, rateArrow);
                    InvokeRepeating("CreateNewArrowRight", 0.8f, rateArrow);
                    break;
            }

        }

        void CreateNewArrowTop()
        {
            arrowInstanceTop = Instantiate(Resources.Load("Arrow"), arrowSpawnTop.transform, false) as GameObject;
            arrowInstanceTop.transform.position = new Vector2(arrowSpawnTop.transform.position.x + (random.Next(-2, 2)) , arrowSpawnTop.transform.position.y);
        }

        void CreateNewArrowLeft()
        {
            arrowInstanceLeft = Instantiate(Resources.Load("ArrowLeft"), arrowSpawnLeft.transform, false) as GameObject;
            arrowInstanceLeft.transform.position = new Vector2(arrowSpawnLeft.transform.position.x, arrowSpawnLeft.transform.position.y + (random.Next(-2, 2)));
        }

        void CreateNewArrowRight()
        {
            arrowInstanceRight = Instantiate(Resources.Load("ArrowRight"), arrowSpawnRight.transform, false) as GameObject;
            arrowInstanceRight.transform.position = new Vector2(arrowSpawnRight.transform.position.x, arrowSpawnRight.transform.position.y + (random.Next(-2, 2)));
        }


        /// <summary>
        /// Resets the game to a new level.
        /// Sets the souls and lifes to new ones, sets the difficult and shows the main screen to play
        /// </summary>
        public void NewGameLevel()
        {
            currentGame = GameState.Start;
            SetSoulsAndLife();
            SetDifficult();
            ShowStartScreen();
        }

    
        /// <summary>
        /// Sets the life text, and generates the number of the rows and columns randomsly.
        /// </summary>
        private void SetSoulsAndLife()
        {
            lifeGO.GetComponent<Text>().text = life.ToString();
            rowSouls = 6;
			columSouls = 6;
        }


        /// <summary>
        /// Sets the difficult of the game 
        /// </summary>
        private void SetDifficult()
        {
            int level = Convert.ToInt32(currentLevelGO.GetComponent<Text>().text);
		

            if (level >= 1 && level <= 3)
            {
                gamelevel = GameLevel.Easy;
            }
            else if (level >= 4 && level <= 6)
            {
                gamelevel = GameLevel.Normal;
            }
            else if (level >= 7 && level <= 10)
            {
                gamelevel = GameLevel.Hard;
			
			}
        }

        /// <summary>
        /// Shows the main Screen where the user plays
        /// </summary>
        private void ShowStartScreen()
        {
            gameStartGO.SetActive(true);
            gameOverGO.SetActive(false);
            victoryGO.SetActive(false);
            loseGO.SetActive(false);
        }

        /// <summary>
        /// Eliminates the arrows that are in the scene and incrementes the currentLevel by one
        /// </summary>
        public void ChangeLevel()
        {
			DeleteObjects();
			NewLevel();
        }

        /// <summary>
        /// Resets the level to 1
        /// </summary>
        public void ResetLevel()
        {
            currentLevel = 1;
            currentLevelGO.GetComponent<Text>().text = currentLevel.ToString();
        }

        /// <summary>
        /// Increse the next level to 1
        /// </summary>
        private void NewLevel()
        {
            currentLevelGO.GetComponent<Text>().text = (++currentLevel).ToString();
        }

        /// <summary>
        /// Shows the screen when the player lose. 
        /// </summary>
        private void ShowLoseScreen()
        {
            gameStartGO.SetActive(false);
            gameOverGO.SetActive(true);
            loseGO.SetActive(true);
        }

        /// <summary>
        /// Shows the screen when the player wins
        /// </summary>
        private void ShowVictoryScreen()
        {
            gameStartGO.SetActive(false);
            gameOverGO.SetActive(true);
            victoryGO.SetActive(true);
        }


		/// <summary>
		/// Deletes the garbage objects when the leves is completed
		/// </summary>
		/// <param name="gameobjectSpawn">Gameobject to delete the things</param>
		private void DeleteObjectsInsideSpawn(GameObject gameobjectSpawn) 
		{
			foreach (Transform child in gameobjectSpawn.transform)
			{
				Destroy(child.gameObject);
			}
		}

        /// <summary>
        /// Stops all the Invokes that generate the arrrows
        /// </summary>
        private void StopArrows()
        {
            CancelInvoke("CreateNewArrowTop");
            CancelInvoke("CreateNewArrowLeft");
            CancelInvoke("CreateNewArrowRight");
        }

        /// <summary>
        /// Check if there is a wining or lossing condition.
        /// It validates if its a new world, then it changes to another scene, if not just it shows the same scene. 
        /// </summary>
        private void IsGameOver()
        {
            souls = GameObject.FindGameObjectsWithTag("Soul").Length;

            if (souls == 0 && life > 0)
            {
                VictoryConditions();
            }
            else if (life <= 0)
            {
                LoseConditions();
            }
        }


        /// <summary>
        /// Executes the lossing steps.
        /// </summary>
        private void LoseConditions()
        {
            currentGame = GameState.Lose;
            Destroy(reaperInstance);
            StopArrows();
			DeleteObjects();
			ShowLoseScreen();
            SetHighScore();
        }


        /// <summary>
        /// Executes the victory steps and shows the according screen if there is in the same world. 
        /// </summary>
        private void VictoryConditions()
        {
            if (currentLevel >= maxLevelsPerWorld)
            {
                StopArrows();
				DeleteObjects();
				NextWorld();
				UnlockWorld();
            }
            else
            {
                currentGame = GameState.Victory;
                Destroy(reaperInstance);
                StopArrows();
				DeleteObjects();
				ShowVictoryScreen();
            }
        }


		/// <summary>
		/// Unlocks the new world to access
		/// </summary>
		private void UnlockWorld()
		{
		  switch(currentWorld) 
		  {
				case GameWorlds.World_2:
					PlayerPrefs.SetInt("World2", 1);
					break;
				case GameWorlds.World_3:
					PlayerPrefs.SetInt("World3", 1);
					break;
				case GameWorlds.World_4:
					PlayerPrefs.SetInt("World4", 1);
					break;
				case GameWorlds.World_5:
					PlayerPrefs.SetInt("World5", 1);
					break;
			}
		}


		/// <summary>
		/// Deletes all the objects when its a new level or if its gameover
		/// </summary>
		private void DeleteObjects()
		{
			switch(currentWorld) {

				case GameWorlds.World_1:
					DeleteObjectsInsideSpawn(arrowSpawnTop);
					DeleteObjectsInsideSpawn(arrowSpawnLeft);
					DeleteObjectsInsideSpawn(arrowSpawnRight);
					DeleteObjectsInsideSpawn(arrowSpawnDown);
					DeleteObjectsInsideSpawn(soulSpawn);
					break;
				case GameWorlds.World_2:
					DeleteObjectsInsideSpawn(infernoWheelSpawn);
					DeleteObjectsInsideSpawn(soulSpawn);
					break;
				case GameWorlds.World_3:
					DeleteObjectsInsideSpawn(infernoWheelSpawn);
					DeleteObjectsInsideSpawn(demonBatSpawn);
					DeleteObjectsInsideSpawn(soulSpawn);
					break;
				case GameWorlds.World_4:
					DeleteObjectsInsideSpawn(arrowSpawnTop);
					DeleteObjectsInsideSpawn(arrowSpawnLeft);
					DeleteObjectsInsideSpawn(arrowSpawnRight);
					DeleteObjectsInsideSpawn(arrowSpawnDown);
					DeleteObjectsInsideSpawn(shurikenSpawn);
					DeleteObjectsInsideSpawn(soulSpawn);
					break;
				case GameWorlds.World_5:
					DeleteObjectsInsideSpawn(arrowSpawnTop);
					DeleteObjectsInsideSpawn(arrowSpawnLeft);
					DeleteObjectsInsideSpawn(arrowSpawnRight);
					DeleteObjectsInsideSpawn(arrowSpawnDown);
					DeleteObjectsInsideSpawn(shurikenSpawn);
					DeleteObjectsInsideSpawn(infernoWheelSpawn);
					DeleteObjectsInsideSpawn(demonBatSpawn);
					DeleteObjectsInsideSpawn(soulSpawn);
					break;
			}
		}

		/// <summary>
		/// Sets the PlayerPrefs to save the highscore and max level 
		/// </summary>
		private void SetHighScore()
        {
			if (PlayerPrefs.HasKey("highScore") == false || points > PlayerPrefs.GetInt("highScore", points))
			{
				PlayerPrefs.SetInt("maxLevel", Convert.ToInt32(currentLevelGO.GetComponent<Text>().text));
				PlayerPrefs.SetString("maxWorld", currentWorld.ToString());
				PlayerPrefs.SetInt("highScore", points);
				PlayerPrefs.Save();
			}
		}

        /// <summary>
        /// Updates the text of the score
        /// </summary>
        private void UpdateScore()
        {
            textPoints.text = getPoints().ToString();
        }

        /// <summary>
        /// Sets the player's points
        /// </summary>
        /// <param name="amount"></param>
        public void setPoints(int amount)
        {
            points += amount;
        }

        /// <summary>
        /// Gets the player's  points
        /// </summary>
        /// <returns></returns>

        public int getPoints()
        {
            return points;
        }

        #endregion
    }
}

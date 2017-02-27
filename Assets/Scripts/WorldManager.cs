using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
	public class WorldManager : MonoBehaviour
	{
		[Header("World Objects")]
		public GameObject World2;
		public GameObject World3;
		public GameObject World4;
		public GameObject World5;

		void Awake() 
	    {
	      SetGameKeys();
	    }

		void FixedUpdate()
		{
			Screen.SetResolution(650, 1100, false);
		}


		private void SetGameKeys()
		{
			if(PlayerPrefs.HasKey("World2") == false) 
			{
				PlayerPrefs.SetInt("World2", 0);
				PlayerPrefs.SetInt("World3", 0);
				PlayerPrefs.SetInt("World4", 0);
				PlayerPrefs.SetInt("World5", 0);
				PlayerPrefs.Save();
			} 
			else 
			{
			  GetKeys();
			}

		}


		private void GetKeys()
		{
			if(PlayerPrefs.GetInt("World2") == 1) 
			{
				EnableWorld(World2);
			}

			if (PlayerPrefs.GetInt("World3") == 1)
			{
				EnableWorld(World3);
			}

			if (PlayerPrefs.GetInt("World4") == 1)
			{
				EnableWorld(World4);
			}

			if (PlayerPrefs.GetInt("World5") == 1)
			{
				EnableWorld(World5);
			}

		}

		private void EnableWorld(GameObject world) 
		{
			world.GetComponent<Button>().interactable = true;
		}
	}
}

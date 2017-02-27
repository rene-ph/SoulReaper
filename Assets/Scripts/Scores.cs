using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class Scores : MonoBehaviour {

    public GameObject scoreGO;
    public GameObject levelGO;
	public GameObject worldGO;

    void Start ()
    {
        if(PlayerPrefs.HasKey("highScore") == true)
        {
            scoreGO.GetComponent<Text>().text = PlayerPrefs.GetInt("highScore").ToString();
            levelGO.GetComponent<Text>().text = PlayerPrefs.GetInt("maxLevel").ToString();
			worldGO.GetComponent<Text>().text = GetWorldName(PlayerPrefs.GetString("maxWorld").ToString());
		}
       
    }

	void FixedUpdate()
	{
		Screen.SetResolution(650, 1100, false);
	}

	private string GetWorldName(string worldName)
	{
		switch(worldName)
		{
			case "World_1":
				return "World 1";

			case "World_2":
				return "World 2";

			case "World_3":
				return "World 3";

			case "World_4":
				return "World 4";

			case "World_5":
				return "World 5";
		}

		return "";
	}
}

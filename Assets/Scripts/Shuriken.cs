using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
namespace Assets.Scripts
{
	public class Shuriken : MonoBehaviour {


	public int speed;
	public StatePosition currentPosition;

	float degree = 57.29f;
	Reaper reaper;

	public enum StatePosition
	{
		TOP,
		DOWN,
		LEFT,
		RIGHT
	}

	#region UnityEvents

	void Awake() 
	{
        reaper = FindObjectOfType<Reaper>();
	}

	void Update ()
	{
		Movement();	
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		switch (collision.gameObject.tag)
		{
			case "LimitLeft":
				currentPosition = StatePosition.RIGHT;
				transform.localEulerAngles = new Vector3(0f, 0f, (UnityEngine.Random.rotation.z * degree));
				break;
			case "LimitRight":
				currentPosition = StatePosition.LEFT;
				transform.localEulerAngles = new Vector3(0f, 0f, (UnityEngine.Random.rotation.z * degree));
				break;
			case "LimitDown":
				currentPosition = StatePosition.TOP;
				transform.localEulerAngles = new Vector3(0f, 0f, (UnityEngine.Random.rotation.z * degree));
				break;
			case "LimitTop":
				currentPosition = StatePosition.DOWN;
				transform.localEulerAngles = new Vector3(0f, 0f, (UnityEngine.Random.rotation.z * degree));
				break;
			case "Reaper":
				ReaperCollision(collision);
				break;
		}
	}

	private void ReaperCollision(Collision2D collision)
	{
		foreach (Transform child in collision.gameObject.transform)
		{
			switch (child.tag)
			{
				case "Shield":
					if (child.gameObject.activeInHierarchy == false && collision.gameObject.tag == "Reaper")
					{
						reaper.DownLife();
					    reaper.SoundHit();
						reaper.AnimationHit();
					}
					break;
			}
		}
	}


	#endregion


	private void Movement()
	{
	   switch(currentPosition) 
	   {
			case StatePosition.LEFT:
				transform.Translate(Vector2.left * Time.deltaTime * speed);
				break;
			case StatePosition.RIGHT:
				transform.Translate(Vector2.right * Time.deltaTime * speed);
				break;
			case StatePosition.TOP:
				transform.Translate(Vector2.up * Time.deltaTime * speed);
				break;
			case StatePosition.DOWN:
				transform.Translate(Vector2.down * Time.deltaTime * speed);
				break;
		}
	}
  }

}

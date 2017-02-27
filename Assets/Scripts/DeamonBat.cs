using UnityEngine;
using System.Collections;
using System;
namespace Assets.Scripts
{
	public class DeamonBat : MonoBehaviour {

    #region Variables

	public int limitRightX;
	public int limitLeftX;
	public int speed;
	public GameObject laserSpawn;
	public StatePosition defaultMovement;


	public enum StatePosition
	{
	  TOP,
	  DOWN,
	  LEFT,
	  RIGHT
	}

	#endregion

	#region UnityEvents

	void Start()
	{
		StartCoroutine("ShootLaser");
	}

	void Update()
	{
		Movement();
	}


	void OnCollisionEnter2D(Collision2D collision)
	{
		switch (collision.gameObject.tag)
		{
			case "LimitLeft":
				defaultMovement = StatePosition.RIGHT;
				break;
			case "LimitRight":
				defaultMovement = StatePosition.LEFT;
				break;
		}
	}

	#endregion

	#region Methods

	IEnumerator ShootLaser()
	{
	  while(true) {
			yield return new WaitForSeconds(1f);
			laserSpawn.SetActive(true);
			yield return new WaitForSeconds(1f);
			laserSpawn.SetActive(false);
		}
	}


	private void Movement()
	{
	   switch(defaultMovement) 
	   {

			case StatePosition.RIGHT:
				this.transform.Translate(Vector3.right * Time.deltaTime * speed);
				break;
			case StatePosition.LEFT:
				this.transform.Translate(Vector3.left * Time.deltaTime * speed);
				break;
	   }
	}

	#endregion
   }
}

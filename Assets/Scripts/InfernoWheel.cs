using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

namespace Assets.Scripts
{
    public class InfernoWheel : MonoBehaviour {


    #region Variables

        Reaper reaper;


        public float amplitudeX;
        public float amplitudeY;
        public float omegaX;
        public float omegaY;
        public float speed;

        float x;
        float y;
        float time;

    #endregion

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
        ReaperCollision(collision);
    }


     #endregion

	#region Constructor 

	public void InfernoWheels(float _amplitudeX, float _amplitudeY, float _omegaX, float _omegaY, float _speed)
	{
		this.amplitudeX = _amplitudeX;
		this.amplitudeY = _amplitudeY;
		this.omegaX = _omegaX;
		this.omegaY = _omegaY;
		this.speed = _speed;
	}

	#endregion

    #region Methods

    private void Movement()
    {
        time += Time.deltaTime;
        x = amplitudeX * Mathf.Cos(omegaX * time ) * speed;
        y = (amplitudeY * Mathf.Sin(omegaY * time )) * speed;
        transform.position = new Vector2(x, y);
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
	}
}

using UnityEngine;
using System.Collections;
namespace Assets.Scripts
{
	public class JoystickMovement : MonoBehaviour
	{
		Reaper reaper;
  

	   void Update() 
	   {
		   reaper = FindObjectOfType<Reaper>();
	   }

		void OnCollisionEnter2D(Collision2D collision)
		{
			switch(collision.gameObject.tag) 
			{
				case "Top":
					reaper.goReaperTop();
					return;
				case "Right":
					reaper.goReaperRight();
					return;
				case "Down":
					reaper.goReaperDown();
					return;
				case "Left":
					reaper.goReaperLeft();
					return;
			}
		}
	}
}


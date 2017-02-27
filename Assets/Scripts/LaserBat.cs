using UnityEngine;
using UnityEngine.UI;
using System.Collections;


namespace Assets.Scripts
{
  public class LaserBat : MonoBehaviour
  {
		#region Variables

		Reaper reaper;

		#endregion

		#region UnityEvents

		void Awake()
		{
			reaper = FindObjectOfType<Reaper>();
		}

		void OnCollisionEnter2D(Collision2D collision) 
		{

			if(collision.transform.tag == "Reaper") 
			{
				ReaperCollision(collision);
			}

		}

		#endregion

		#region Methods

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

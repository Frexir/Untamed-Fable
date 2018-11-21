using UnityEngine;
using System.Collections;

public class HowToPlayMenu : MonoBehaviour {

	public string mainMenu;

	public void MainMenu ()
	{

		Application.LoadLevel (mainMenu);
	}
}

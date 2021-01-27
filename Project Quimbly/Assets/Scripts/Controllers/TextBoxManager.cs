using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class TextBoxManager : MonoBehaviour
{

	public GameObject TextBox;
	public GameObject tocontinue;

	public Text thetext;
	
	// Use this for initialization

	public TextAsset textfile;
	public string[] textlines;

	public int currentline;
	public int endatline;

	
	void Start()
	{
		
		if (textfile != null)
		{
			textlines = (textfile.text.Split('\n'));
		}
		
		if (endatline == 0)
		{
			endatline = textlines.Length - 1;
		}
	
	}
	void Update()
	{
		thetext.text = textlines[currentline];

		if(Input.GetKeyDown (KeyCode.Return) || Input.GetKeyDown(KeyCode.A))
		{
			currentline += 1;
	
		}

		if (currentline > endatline)
		{
			TextBox.SetActive(false);
			tocontinue.SetActive(true);
		}

	}

}

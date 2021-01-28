using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using System;

public class TextBoxManager : MonoBehaviour
{

	public GameObject TextBox;
	public float speed = 0.1f;
	public TMP_Text thetext;
	
	// Use this for initialization

	public TextAsset textfile;
	public string[] textlines;
	private string TypedLine = "";
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
		StartCoroutine(ShowText());
	}

	IEnumerator ShowText()
	{

		for (int i = 0; i < textlines[currentline].Length; i++)
		{
			if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.A))
			{
				i = 0;
			}
			else if (TypedLine != textlines[currentline]) 
			{
				TypedLine = textlines[currentline].Substring(0, i);
				yield return new WaitForSeconds(speed);
			}

			
				
		}
		
	}
		void Update()
		{
		thetext.text = TypedLine;

		if(Input.GetKeyDown (KeyCode.Return) || Input.GetKeyDown(KeyCode.A))
		{
			if (textlines[currentline] == TypedLine)
			{
				
				TypedLine = "";
				currentline = currentline + 1;
			}
			else
			{
				TypedLine = textlines[currentline];
			}

		}

		if (currentline > endatline)
		{
			TextBox.SetActive(false);
		}

	}

}

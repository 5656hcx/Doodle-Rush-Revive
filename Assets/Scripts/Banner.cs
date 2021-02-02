using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Banner : MonoBehaviour
{
	public TextMeshProUGUI Title;
	public TextMeshProUGUI Version;
	private string originText;
	private static string email = "5656hcx@gmail.com";

	void Start()
	{
		originText = Title.text;
	}

	public void ChangeDisplay(bool display_email)
	{
		if (display_email)
		{
			Title.text = email;
			Version.gameObject.SetActive(false);
		}
		else
		{
			Title.text = originText;
			Version.gameObject.SetActive(true);
		}
	}
}

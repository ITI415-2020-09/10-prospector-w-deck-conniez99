﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Card : MonoBehaviour
{

	public string suit;
	public int rank;
	public Color color = Color.black;
	public string colS = "Black";  // or "Red"

	public List<GameObject> decoGOs = new List<GameObject>();
	public List<GameObject> pipGOs = new List<GameObject>();

	public GameObject back;  // back of card;
	public CardDefinition def;  // from DeckXML.xml		
	public SpriteRenderer[] spriteRenderers;

	// Use this for initialization
	void Start()
	{
		SetSortOrder(0);
	}

	public void PopulateSpriteRenderers()
	{
		if (spriteRenderers == null || spriteRenderers.Length == 0)
		{
			spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
		}
	}

	// Sets the sortingLayerName on all SpriteRenderer Components
	public void SetSortingLayerName(string tSLN)
	{
		PopulateSpriteRenderers();

		foreach (SpriteRenderer tSR in spriteRenderers)
		{
			tSR.sortingLayerName = tSLN;
		}
	}

	// Sets the sortingOrder of all SpriteRenderer Components
	public void SetSortOrder(int sOrd)
	{
		PopulateSpriteRenderers();
		foreach (SpriteRenderer tSR in spriteRenderers)
		{
			if (tSR.gameObject == this.gameObject)
			{
				tSR.sortingOrder = sOrd;
				continue;
			}

			switch (tSR.gameObject.name)
			{
				case "back": // if the name is "back"
					tSR.sortingOrder = sOrd + 2;
					break;
				case "face":
				default:
					tSR.sortingOrder = sOrd + 1;
					break;
			}
		}
	}

	public bool faceUp
	{
		get
		{
			return (!back.activeSelf);
		}

		set
		{
			back.SetActive(!value);
		}
	}

	// Virtual methods can be overridden by subclass methods with the same name
	virtual public void OnMouseUpAsButton()
	{
		print(name);
	}
}


	[System.Serializable]
	public class Decorator
	{
		public string type;         // For card pips, tyhpe = "pip"
		public Vector3 loc;         // location of sprite on the card
		public bool flip = false;   //whether to flip vertically
		public float scale = 1.0f;
	}

	[System.Serializable]
	public class CardDefinition
	{
		public string face; //sprite to use for face cart
		public int rank;    // value from 1-13 (Ace-King)
		public List<Decorator>
						pips = new List<Decorator>();  // Pips Used
	}

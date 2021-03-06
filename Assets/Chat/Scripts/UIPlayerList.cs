﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate void PlayerListChange(PlayerChat player);
public class UIPlayerList : MonoBehaviour {

	public LocalPlayerList myList;
	public GameObject textPrefab;
	// Use this for initialization
	void Start () {
		myList.addPlayer += addPlayer;
		myList.removePlayer += removePlayer;
		//
		if(myList.players.Count>0)
		{
			for(int x=0;x<myList.players.Count;x++)
			{
				addPlayer( myList.players[x] );
			}
		}
	} 
	
	// Update is called once per frame
	void Update () {
		
	}

	public void addPlayer( PlayerChat player )
	{
		//create game object
		GameObject gameObject = Instantiate(textPrefab, Vector3.zero , Quaternion.identity);
		UIPlayerNameHandler handler = gameObject.GetComponent<UIPlayerNameHandler>();
		handler.setInit(player);
		gameObject.transform.parent = this.transform;
	}
	public void removePlayer(PlayerChat player)
	{
		//PlayerChat[] players = this.gameObject.GetComponentInChildren<PlayerChat>();
	}
}

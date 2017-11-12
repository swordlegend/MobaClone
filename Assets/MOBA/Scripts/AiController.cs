﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class AiController : NetworkBehaviour {

	public float targetRange = 1f;

	TargetFinder targetFinder;
	CombatHandler combatHandler;
	public MovementHandler movementHandler;
	
	public bool isBusy = false;

	public float delayCounter = 0;
	public float checkDelay = 1f;
	// Use this for initialization
	void Start () {
		if( !isServer )
		{
			return;
		}
		combatHandler = this.GetComponent<CombatHandler>();
		combatHandler.targetChanged += targetChanged;
		targetFinder = this.GetComponent<TargetFinder>();
		movementHandler = this.GetComponent<MovementHandler>();
		
	}
	void targetChanged()
	{
		delayCounter = checkDelay;
	}
	// Update is called once per frame
	void Update () 
	{
		if( !isServer )
		{
			return;
		}
		delayCounter += Time.deltaTime;
		//I'll establish a queue system so AI actions are not run every frame and can be spread out a bit
		// For now I'll have it check every second
		if(delayCounter > checkDelay)
		{
			delayCounter = 0;
			doChecks();	
		}
	}
//if isbusy - wait until attack is done or timer is done?
	
	public void doChecks()
	{
		if(combatHandler.target != null)
		{
			//is target is dead, get new target
			if( !combatHandler.target.isAlive() )
			{
				combatHandler.target = null;
				targetFinder.getNewTarget();
				return;
			}
			if( !isBusy )
			{
				Transform other  = combatHandler.target.transform;
				Vector3 offset = other.position - transform.position;
            	float sqrLen = offset.sqrMagnitude;
				if( sqrLen < targetRange * targetRange)
				{
					RaycastHit2D[] hits = Physics2D.LinecastAll(transform.position, other.position, combatHandler.otherTeamLayerMask.value);

					//RaycastHit2D[] hits = Physics2D.RaycastAll (Camera.main.ScreenToWorldPoint(mousePos), Vector2.zero);
					//Debug.Log(transform.position+" "+other.position);
					bool hitObject = false;
					for( int x=0;x<hits.Length;x++)
					{
						if (hits[x].transform == other)
						{
							hitObject = true;
						}
					}
					if (hitObject)
					{
						 //RaycastHit2D
						 // attack target
						 //Debug.Log("attack target.");
						 //isBusy = true;
						 //use power
						 if(movementHandler!=null)
						 	movementHandler.stopMoving();
					}
					else
					{
						//get new target?
						Debug.Log("target is blocked.");
					}
				}
				else
				{
					combatHandler.target = null;
					targetFinder.getNewTarget();
				}
			}
			//is currently doing action
			//is target in range
			//is target in view
			
			//combatHandler.
			//if( targetRange)
		}
		//or become dead
		/*if(combatHandler.target == null)
		{
			targetFinder.getNewTarget();
		}*/


		//if target moves out of targetRange
	}
	
}
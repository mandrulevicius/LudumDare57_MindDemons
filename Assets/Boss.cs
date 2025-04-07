using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public AudioClip bossBattleSound;
    private GameObject SinsConatiner;
    private GameObject SinsPlaces;
    private GameObject player;
    private InteractionEvents eventer;

    private void Awake()
    {
        eventer = GameObject.FindWithTag("Events").GetComponent<InteractionEvents>();
    }

    private void OnDestroy()
    {
        eventer.changeBossBattle();
        eventer.PlayGenericClip();
    }

    public void StartBattle()
    {
        var sinsContainer = GameObject.FindWithTag("SinsContainer");
        var sinsPlaces = GameObject.FindWithTag("SinPlaces");
        var player = GameObject.FindWithTag("Player");
        var BattleWalls = GameObject.FindWithTag("BossWalls");
        var bossEntity = gameObject.GetComponent<Entity>();
        bossEntity.movementTarget = player; 
        bossEntity.inCombat = true;
        eventer.audioClip = bossBattleSound;
        eventer.PlayNewAudio();
        eventer.changeBossBattle();    
        for (int i = 0; i < BattleWalls.transform.childCount; i++)
        {
            BattleWalls.transform.GetChild(i).gameObject.active=true;
        }   
        var sins = new List<GameObject>();
        for (int i = 0; i < sinsContainer.transform.childCount; i++)
        {
            sins.Add(sinsContainer.transform.GetChild(i).gameObject);
        }

        var places = new List<GameObject>();
        for (int i = 0; i < sinsPlaces.transform.childCount; i++)
        {
            places.Add(sinsPlaces.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < Mathf.Min(sins.Count, places.Count); i++)
        {
            sins[i].transform.position = places[i].transform.position;
            var entity = sins[i].GetComponent<Entity>();
            if (entity != null)
            {
                entity.isShooting = true;
                entity.inCombat = true;
                entity.movementTarget = player; // Set target for movement
            }
        }
    }
}
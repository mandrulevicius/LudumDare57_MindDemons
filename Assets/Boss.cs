using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Boss : MonoBehaviour
{
    AudioClip bossBattleSound;
    private GameObject SinsConatiner;
    private GameObject SinsPlaces;
    private GameObject player;


    public void StartBattle()
    {
        var sinsContainer = GameObject.FindWithTag("SinsContainer");
        var sinsPlaces = GameObject.FindWithTag("SinPlaces");
        var player = GameObject.FindWithTag("Player");

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
            // Move the entire sin GameObject, including all its children
            sins[i].transform.SetParent(places[i].transform); 
            sins[i].transform.localPosition = Vector3.zero; 

            // Ensure relevant Entity component is updated for combat
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
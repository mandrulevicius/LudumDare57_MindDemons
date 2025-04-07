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

        var sins = sinsContainer.GetComponentsInChildren<Transform>(true)
            .Where(t => t != sinsContainer.transform) // Avoid root container itself
            .Select(t => t.gameObject)
            .ToArray();

        var places = sinsPlaces.GetComponentsInChildren<Transform>(true)
            .Where(t => t != sinsPlaces.transform) // Avoid root container itself
            .Select(t => t.gameObject)
            .ToArray();

        for (int i = 0; i < Mathf.Min(sins.Length, places.Length); i++)
        {
            // Move the entire sin GameObject, including all its children
            sins[i].transform
                .SetParent(places[i].transform, worldPositionStays: false); // Keep local position to target
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
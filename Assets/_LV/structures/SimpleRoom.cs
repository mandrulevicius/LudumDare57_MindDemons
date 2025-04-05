using UnityEngine;
using System.Collections.Generic;

public class SimpleRoom : MonoBehaviour
{
    // =========================== TRIGGERS ===========================
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        
        GameObject[] enemiesInTrigger = FindEnemiesInTrigger();
        foreach (GameObject enemy in enemiesInTrigger)
        {
            enemy.GetComponent<Entity>().movementTarget = other.gameObject;
        }
    }

    private GameObject[] FindEnemiesInTrigger()
    {
        // Get all colliders in the trigger
        List<Collider2D> collidersList = new List<Collider2D>();
        ContactFilter2D filter = new ContactFilter2D().NoFilter();
        GetComponent<Collider2D>().Overlap(filter, collidersList);
        Collider2D[] colliders = collidersList.ToArray();

        // Filter for enemies
        return System.Array.FindAll(
            System.Array.ConvertAll(colliders, col => col.gameObject),
            go => go.layer == LayerMask.NameToLayer("Enemy")
        );
    }
    
}

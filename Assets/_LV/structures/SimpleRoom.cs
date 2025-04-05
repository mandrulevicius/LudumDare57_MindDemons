using UnityEngine;
using System.Collections.Generic;

public class SimpleRoom : MonoBehaviour
{
    // =========================== TRIGGERS ===========================
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log($"{gameObject.name} triggered by PLAYER");

            // Find all enemies in the trigger area
            GameObject[] enemiesInTrigger = FindEnemiesInTrigger();
            Debug.Log($"Found {enemiesInTrigger.Length} enemies in trigger");

            // Do something with the enemies...
            foreach (GameObject enemy in enemiesInTrigger)
            {
                Debug.Log($"Enemy in trigger: {enemy.name}");
                enemy.GetComponent<Entity>().movementTarget = other.gameObject;
            }
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

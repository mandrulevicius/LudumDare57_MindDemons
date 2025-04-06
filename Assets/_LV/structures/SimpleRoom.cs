using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class SimpleRoom : MonoBehaviour
{
    // =========================== TRIGGERS ===========================
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (!other.gameObject.CompareTag("Player")) return;
        var eventer = GameObject.FindWithTag("Events").GetComponent<InteractionEvents>();
        GameObject[] enemiesInTrigger = FindOthersInTrigger("Enemy");
        GameObject[] sinsInTrigger = FindOthersInTrigger("Sin");
        GameObject[] npcInTrigger = FindOthersInTrigger("Npc");
        foreach (GameObject enemy in enemiesInTrigger)
        {
            enemy.GetComponent<Entity>().movementTarget = other.gameObject;
        }  
        foreach (GameObject sin in sinsInTrigger)
        {
            eventer.npc = sin;
            eventer.onnpcChanged();
        }
        foreach (GameObject npc in npcInTrigger)
        {
            eventer.npc = npc;
            eventer.onnpcChanged();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(!other.gameObject.CompareTag("Player")) return;
        var eventer = GameObject.FindWithTag("Events").GetComponent<InteractionEvents>();
        eventer.npc=null;
        eventer.onnpcChanged();
    }
    private GameObject[] FindOthersInTrigger( string layer)
    {
        // Get all colliders in the trigger
        List<Collider2D> collidersList = new List<Collider2D>();
        ContactFilter2D filter = new ContactFilter2D().NoFilter();
        GetComponent<Collider2D>().Overlap(filter, collidersList);
        Collider2D[] colliders = collidersList.ToArray();

        // Filter for enemies
        return System.Array.FindAll(
            System.Array.ConvertAll(colliders, col => col.gameObject),
            go => go.layer == LayerMask.NameToLayer(layer)
        );
    }
    
}

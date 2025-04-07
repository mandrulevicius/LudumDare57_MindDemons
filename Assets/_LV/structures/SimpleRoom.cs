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
        GameObject[] boss = FindOthersInTrigger("Boss");
        
        var once = 0;
        foreach (GameObject enemy in enemiesInTrigger)
        {
            enemy.GetComponent<Entity>().movementTarget = other.gameObject;
            if(enemy.GetComponent<EvilThought>().audioClip && once==0)
            eventer.audioClip = enemy.GetComponent<EvilThought>().audioClip;
            if(enemy.GetComponent<EvilThought>().EndOfBattleAudioClip && once==0)
            eventer.endOfBattleAudio = enemy.GetComponent<EvilThought>().EndOfBattleAudioClip;
            eventer.PlayNewAudio();
            once++;
        }
    once = 0;
        foreach (GameObject sin in sinsInTrigger)
        {
            eventer.npc = sin;
            eventer.onnpcChanged();
            if(sin.GetComponent<Sin>().audioClip)
            {
                eventer.audioClip = sin.GetComponent<Sin>().audioClip;
                eventer.PlayNewAudio();
            }

        }
        foreach (GameObject npc in npcInTrigger)
        {
            eventer.npc = npc;
            eventer.onnpcChanged();
        }
        foreach (GameObject npc in boss)
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

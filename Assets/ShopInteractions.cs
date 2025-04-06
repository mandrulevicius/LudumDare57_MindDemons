using UnityEngine;
using System.Collections.Generic;

public class ShopInteractions : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private InteractionEvents eventer;
    private Entity player;
    [SerializeField] private GameObject sellingItem;
    private List<DialogueLine> topic;
    private List<DialogueLine> topicYes;
    private int dailogCounter = 0;

    private void Awake()
    {
        topic = new List<DialogueLine>
        {
            new DialogueLine(
                "<b><color=#08ffe0>Spiritual guide:</color></b>",
                $"<Stops chanting>\nHello traveller would you like buy {(sellingItem != null ? sellingItem.gameObject.name : "Missing element ;D")}?" +
                "It will const you 10 Insights <i>no(q) yes (e)</i>"
            ),
            new DialogueLine(
                "<b><color=#4aa3df>You:</color></b>",
                "Nope."
            ),
            new DialogueLine(
                "<b><color=#08ffe0>Spiritual guide:</color></b>",
                "<Starts ignoring you and continues chanting>"
            )
        };
        topicYes = new List<DialogueLine>
        {
            new DialogueLine(
                "<b><color=#08ffe0>Spiritual guide:</color></b>",
                $"<Stops chanting>\nHello traveller would you like buy {(sellingItem != null ? sellingItem.gameObject.name : "Missing element ;D")}?" +
                "It will const you 10 Insights <i>no(q) yes (e)</i>"
                ),
            new DialogueLine(
                "<b><color=#4aa3df>You:</color></b>",
                "*Sigh* Well maybe just a little?"
            ),
            new DialogueLine(
                "<b><color=#08ffe0>Spiritual guide:</color></b>",
                "Have this pill.\n<Continues scribbling in his book> "
            ),
            new DialogueLine(
                "<b><color=#4aa3df>You:</color></b>",
                "<You eat a pill and feel better>"
            )
        };


        eventer = GameObject.FindWithTag("Events").GetComponent<InteractionEvents>();
        player = GameObject.FindWithTag("Player").GetComponent<Entity>();
    }

    public void Talk(bool agreed)
    {
        if ((agreed ? topicYes.Count : topic.Count) == dailogCounter)
        {
            eventer.StopTalk(agreed);
            dailogCounter = 0;
            return;
        }

        eventer.StartTalk();
        if (agreed)
        {
            eventer.WriteText($"{topicYes[dailogCounter].speaker} \n{topicYes[dailogCounter].line}");
            player.stats.health = player.stats.maxHealth;
        }
        else
        {
            eventer.WriteText($"{topic[dailogCounter].speaker} \n{topic[dailogCounter].line}");
        }

        dailogCounter++;
    }
}
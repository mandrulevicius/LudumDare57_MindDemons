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
    private List<DialogueLine> topicNothing;
    private TalkInteraction talkInteraction;
    private int dailogCounter = 0;

    private void Awake()
    {
        topic = new List<DialogueLine>
        {
            new DialogueLine(
                "<b><color=#08ffe0>Spiritual guide:</color></b>",
                $"<Stops chanting>\nHello traveller would you {(sellingItem != null ? sellingItem.gameObject.name : "Missing element ;D")}?" +
                "<i>no(q) yes (e)</i>"
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
        topicNothing = new List<DialogueLine>
        {
            new DialogueLine(
                "<b><color=#08ffe0>Spiritual guide:</color></b>",
                $"I have nothing to give you."
                ),
            new DialogueLine(
                "<b><color=#4aa3df>You:</color></b>",
                "Shit."
            )
        };
        topicYes = new List<DialogueLine>
        {
            new DialogueLine(
                "<b><color=#08ffe0>Spiritual guide:</color></b>",
                $"<Stops chanting>\nHello traveller would you like {(sellingItem != null ? sellingItem.gameObject.name : "Missing element ;D")}?" +
                "<i>no(q) yes (e)</i>"
                ),
            new DialogueLine(
                "<b><color=#4aa3df>You:</color></b>",
                "Yes!"
            )
        };


        eventer = GameObject.FindWithTag("Events").GetComponent<InteractionEvents>();
        player = GameObject.FindWithTag("Player").GetComponent<Entity>();
        talkInteraction = gameObject.GetComponent<TalkInteraction>();
    }

    public void Talk(bool agreed)
    {
        if (!sellingItem && topicNothing.Count== dailogCounter)
        {
            eventer.StopTalk(agreed);
        }
        if ((agreed ? topicYes.Count : topic.Count) == dailogCounter)
        {
            eventer.StopTalk(agreed);

            if (agreed)
            {
                if (sellingItem.gameObject.name == "Learn to Chant")
                {
                    player.stats.attackSpeed = 1;
                    
                    Destroy(sellingItem);
                }

                if (sellingItem.gameObject.name == "Safety Blanket")
                {
                    player.stats.armor = 1;
                    Destroy(sellingItem);
                }
                talkInteraction.resetTalk();
            }
            
            
            dailogCounter = 0;
            return;
        }

        eventer.StartTalk();
        if(!sellingItem)
        {
            eventer.WriteText($"{topicNothing[dailogCounter].speaker} \n{topicNothing[dailogCounter].line}");

        }
        else if (agreed)
        {
            eventer.WriteText($"{topicYes[dailogCounter].speaker} \n{topicYes[dailogCounter].line}");
        }
        else
        {
            eventer.WriteText($"{topic[dailogCounter].speaker} \n{topic[dailogCounter].line}");
        }
        talkInteraction.resetTalk();
        dailogCounter++;
    }
}
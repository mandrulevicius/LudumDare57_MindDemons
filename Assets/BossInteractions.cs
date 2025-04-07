using System.Collections.Generic;
using UnityEngine;

public class BossInteractions : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private InteractionEvents eventer;
    private Boss bossEvents;
    private TalkInteraction talkInteraction;

    private List<DialogueLine> topicYes = new List<DialogueLine>
    {
        new DialogueLine(
            "<b><color=#e89b2d>Bad You:</color></b>",
            "Are you ready to face your vanities and yourself?\nno(q) yes (e)"
        ),
        new DialogueLine(
            "<b><color=#4aa3df>You:</color></b>",
            "I'm fine, lets go!!!"
        ),
        new DialogueLine(
            "<b><color=#e89b2d>Bad You:</color></b>",
            "Let's see how well you prepared, you won't be able to run away now"
        )
    };

    private List<DialogueLine> topic = new List<DialogueLine>
    {
        new DialogueLine(
            "<b><color=#e89b2d>Bad You:</color></b>",
            "Are you ready to face your vanities and yourself?\nno(q) yes (e)"
        ),
        new DialogueLine(
            "<b><color=#4aa3df>You:</color></b>",
            "No, I'm still scared."
        ),
        new DialogueLine(
            "<b><color=#e89b2d>Bad You:</color></b>",
            "Then go and explore your depths."
        )
    };

    private int dailogCounter = 0;

    private void Awake()
    {
        eventer = GameObject.FindWithTag("Events").GetComponent<InteractionEvents>();
        talkInteraction = gameObject.GetComponent<TalkInteraction>();
        bossEvents = gameObject.GetComponentInParent<Boss>();
    }

    public void Talk(bool agreed)
    {
        
        if (!agreed && topic.Count == dailogCounter)
        {
            talkInteraction.resetTalk();
            dailogCounter = 0;
            eventer.StopTalk(agreed);
            return;
        }
        if(agreed && topicYes.Count == dailogCounter)
        {            
            dailogCounter = topicYes.Count;
            bossEvents.StartBattle();
            eventer.StopTalk(agreed);
            return;
        }
        eventer.StartTalk();
        if (agreed)
        {
            eventer.WriteText($"{topicYes[dailogCounter].speaker} \n{topicYes[dailogCounter].line}");
        dailogCounter++;
        }
        else
        {
            eventer.WriteText($"{topic[dailogCounter].speaker} \n{topic[dailogCounter].line}");
        dailogCounter++;
        }
    }
}

using System.Collections.Generic;
using UnityEngine;

public class GreedInteractions : MonoBehaviour
{
    private InteractionEvents eventer;

    private List<DialogueLine> topic = new List<DialogueLine>
    {
        new DialogueLine(
            "<b><color=green>Greed:</color></b>",
            "Do need everything?\n<i>no(q) yes (e)</i>"
        ),
        new DialogueLine(
            "<b><color=green>You:</color></b>",
            "I have everything I need."
        ),
        new DialogueLine(
            "<b><color=green>Greed:</color></b>",
            "Well then I will give you a freebie!"
        )
    };

    private List<DialogueLine> topicYes = new List<DialogueLine>
    {
        new DialogueLine(
            "<b><color=green>Greed:</color></b>",
            "Do need everything?\n<i>no(q) yes (e)</i>"
        ),
        new DialogueLine(
            "<b><color=#4aa3df>You:</color></b>",
            "Well i would like all the riches."
        ),
        new DialogueLine(
            "<b><color=green>Greed:</color></b>",
            "I like your attitude, see you later."
        )
    };

    private int dailogCounter = 0;

    private void Awake()
    {
        eventer = GameObject.FindWithTag("Events").GetComponent<InteractionEvents>();
    }

    public void Talk(bool agreed)
    {
        if ((agreed ? topicYes.Count : topic.Count) == dailogCounter)
        {
            eventer.StopTalk(agreed);
            return;
        }
        eventer.StartTalk();
        if (agreed)
            eventer.WriteText($"{topicYes[dailogCounter].speaker} \n{topicYes[dailogCounter].line}");
        else
        {
            eventer.WriteText($"{topic[dailogCounter].speaker} \n{topic[dailogCounter].line}");
        }
        dailogCounter++;
    }
}

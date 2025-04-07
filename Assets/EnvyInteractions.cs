using System.Collections.Generic;
using UnityEngine;

public class EnvyInteractions : MonoBehaviour
{
    private InteractionEvents eventer;

    private List<DialogueLine> topic = new List<DialogueLine>
    {
        new DialogueLine(
            "<b><color=#2f59d7>Envy:</color></b>",
            "I wish i had your shoes.\n<i>no(q) yes (e)</i>"
        ),
        new DialogueLine( 
            "<b><color=#4aa3df>You:</color></b>",
            "No."
        ),
        new DialogueLine(
            "<b><color=#2f59d7>Envy:</color></b>",
            "I will take them from you!"
        ) 
    };

    private List<DialogueLine> topicYes = new List<DialogueLine>
    {
        new DialogueLine(
            "<b><color=#2f59d7>Envy:</color></b>",
            "I wish i had your shoes.\n<i>no(q) yes (e)</i>"
        ),
        new DialogueLine(
            "<b><color=#4aa3df>You:</color></b>",
            "I like your shoes."  
        ),
        new DialogueLine(
            "<b></b>",
            "<You exchange shoes>" 
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

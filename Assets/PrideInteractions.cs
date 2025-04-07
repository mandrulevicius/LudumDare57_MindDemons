using System.Collections.Generic;
using UnityEngine;

public class PrideInteractions : MonoBehaviour
{
    private InteractionEvents eventer;

    private List<DialogueLine> topic = new List<DialogueLine>
    {
        new DialogueLine(
            "<b><color=#f700f7>Pride:</color></b>",
            "You look like a king.\n<i>no(q) yes (e)</i>"
        ),
        new DialogueLine( 
            "<b><color=#4aa3df>You:</color></b>",
            "No, I'm just a regular chad."
        ),
        new DialogueLine(
            "<b><color=#f700f7>Pride:</color></b>",
            "Let's see how chad you really are!"
        ) 
    };

    private List<DialogueLine> topicYes = new List<DialogueLine>
    {
        new DialogueLine(
            "<b><color=#f700f7>Pride:</color></b>",
            "You look like a king.\n<i>no(q) yes (e)</i>"
        ),
        new DialogueLine(
            "<b><color=#4aa3df>You:</color></b>",
            "I look like a super model!"  
        ),
        new DialogueLine(
            "<b><color=#f700f7>Pride:</color></b>",
            "Indeed we do <Smiles at you proudly>\nWe will meet again."
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

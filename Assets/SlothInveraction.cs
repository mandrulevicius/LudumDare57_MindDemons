using UnityEngine;
using System.Collections.Generic;
public class SlothInveraction : MonoBehaviour
{
    private InteractionEvents eventer;

    private List<DialogueLine> topic = new List<DialogueLine>
    {
        new DialogueLine(
            "<b><color=#e89b2d>SLOTH:</color></b>",
            "Hey dude, maybe just chill???\nno(q) yes (e)"
        ),
        new DialogueLine(
            "<b><color=#4aa3df>HAWK:</color></b>",
            "I'm fine I will just kick your ass"
        ),
        new DialogueLine(
            "<b><color=#e89b2d>SLOTH:</color></b>",
            "You will regret this"
        )
    };

    private List<DialogueLine> topicYes = new List<DialogueLine>
    {
        new DialogueLine(
            "<b><color=#e89b2d>SLOTH:</color></b>",
            "Hey dude, maybe just chill???\nno(q) yes (e)"
        ),
        new DialogueLine(
            "<b><color=#4aa3df>HAWK:</color></b>",
            "Ye sure!!"
        ),
        new DialogueLine(
            "<b><color=#e89b2d>SLOTH:</color></b>",
            "We will meet again"
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
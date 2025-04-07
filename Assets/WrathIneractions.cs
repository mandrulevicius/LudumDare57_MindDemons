using System.Collections.Generic;
using UnityEngine;

public class WrathIneractions : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private InteractionEvents eventer;

    private List<DialogueLine> topic = new List<DialogueLine>
    {
        new DialogueLine(
            "<b><color=red>Wrath:</color></b>",
            "Are you angry?\n<i>no(q) yes (e)</i>"
        ),
        new DialogueLine(
            "<b><color=#4aa3df>You:</color></b>",
            "Nope, I'm feeling quite calm at this momment."
        ),
        new DialogueLine(
            "<b><color=red>Wrath:</color></b>",
            "Well you wont now!"
        )
    };

    private List<DialogueLine> topicYes = new List<DialogueLine>
    {
        new DialogueLine(
            "<b><color=red>Wrath:</color></b>",
            "Are you angry?\n<i>no(q) yes (e)</i>"
        ),
        new DialogueLine(
            "<b><color=#4aa3df>You:</color></b>",
            "I really am!!!"
        ),
        new DialogueLine(
            "<b><color=red>Wrath:</color></b>",
            "That's my boy, see you later."
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

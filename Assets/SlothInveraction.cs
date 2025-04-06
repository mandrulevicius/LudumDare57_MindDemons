using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public struct DialogueLine
{
    public string speaker;
    public string line;

    public DialogueLine(string speaker, string line)
    {
        this.speaker = speaker;
        this.line = line;
    }
}

public class SlothInveraction : MonoBehaviour
{
    private InteractionEvents eventer;

    private List<DialogueLine> topic = new List<DialogueLine>
    {
        new DialogueLine(
            "<b><color=#e89b2d>SLOTH:</color></b>",
            "Hey dude, maybe just chill???"
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
            "Hey dude, maybe just chill???"
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
        Debug.Log(agreed);
        if (topic.Count == dailogCounter)
        {
            eventer.StopTalk(agreed);
            return;
        }

        eventer.StartTalk();
        if (agreed)
            eventer.WriteText($"{topic[dailogCounter].speaker} \n{topic[dailogCounter].line}");
        else
        {
            eventer.WriteText($"{topicYes[dailogCounter].speaker} \n{topicYes[dailogCounter].line}");
            
        }
        dailogCounter++;
    }

    public void SayNo()
    {
    }
}
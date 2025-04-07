using UnityEngine;
using System.Collections.Generic;
public class GluttonyInteractions : MonoBehaviour
{
    private InteractionEvents eventer;

    private List<DialogueLine> topic = new List<DialogueLine>
    {
        new DialogueLine(
            "<b><color=#7c61d6>Gluttony:</color></b>",
            "<Sloth stops eating>\nI have so much food, want some brother?\n<i>no(q) yes (e)</i>"
        ),
        new DialogueLine(
            "<b><color=#4aa3df>You:</color></b>",
            "Sorry, but I'm on a diet."
        ),
        new DialogueLine(
            "<b><color=#7c61d6>Gluttony:</color></b>",
            "Then I will eat you instead!!!!"
        )
    };

    private List<DialogueLine> topicYes = new List<DialogueLine>
    {
        new DialogueLine(
            "<b><color=#7c61d6>Gluttony:</color></b>",
            "<Sloth stops eating>\nI have so much food, want some brother?\n<i>no(q) yes (e)</i>"
        ),
        new DialogueLine(
            "<b><color=#4aa3df>You:</color></b>",
            "Maybe you have a tasty banana?"
        ),
        new DialogueLine(
            "<b><color=#7c61d6>Gluttony:</color></b>",
            "Nope, but have a piece of cake. We will see each other later\n<He continues eating>"
        ),
        new DialogueLine(
            "<b><color=#4aa3df>You:</color></b>",
            "<You eat a piece of some delicious cake>"
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

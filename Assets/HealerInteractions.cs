using System.Collections.Generic;
using UnityEngine;

public class HealerInteractions : MonoBehaviour
{
    private InteractionEvents eventer;
    private Entity player;
    private List<DialogueLine> topic = new List<DialogueLine>
    {
        new DialogueLine(
            "<b><color=white>Psychiatrist:</color></b>",
            "<Looks at you>\nDo you feel insane?\n<i>no(q) yes (e)</i>"
        ),
        new DialogueLine(
            "<b><color=#4aa3df>You:</color></b>",
            "Nope."
        ),
        new DialogueLine(
            "<b><color=white>Psychiatrist:</color></b>",
            "Well then try harder"
        )
    };

    private List<DialogueLine> topicYes = new List<DialogueLine>
    {
        new DialogueLine(
            "<b><color=white>Psychiatrist:</color></b>",
            "<Looks at you>\nDo you feel insane?\n<i>no(q) yes (e)</i>"
        ),
        new DialogueLine(
            "<b><color=#4aa3df>You:</color></b>",
            "*Sigh* Well maybe just a little?"
        ),
        new DialogueLine(
            "<b><color=white>Psychiatrist:</color></b>",
            "Have this pill.\n<Continues scribbling in his book> "
        ),
        new DialogueLine(
            "<b><color=#4aa3df>You:</color></b>",
            "<You eat a pill and feel better>"
        )
    };

    private int dailogCounter = 0;

    private void Awake()
    {
        eventer = GameObject.FindWithTag("Events").GetComponent<InteractionEvents>();
        player = GameObject.FindWithTag("Player").GetComponent<Entity>();
        
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

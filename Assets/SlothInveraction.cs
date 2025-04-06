using System;
using UnityEngine;

public class SlothInveraction : MonoBehaviour
{
    public void StartTalk()
    {
        var eventer = GameObject.FindWithTag("Events").GetComponent<InteractionEvents>();

    eventer.StartTalk();
    eventer.WriteText("<b><color=#e89b2d>SLOTH:</color></b> \nHey dude, maybe just chill???");
    }
}

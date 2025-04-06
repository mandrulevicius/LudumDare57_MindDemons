using System;
using UnityEngine;

public class SlothInveraction : MonoBehaviour
{
    public void StartTalk()
    {
        var eventer = GameObject.FindWithTag("Events").GetComponent<InteractionEvents>();

    eventer.StartTalk();
    eventer.WriteText("The Fuck you want?");
    }
}

using System;
using UnityEngine;

public class SlothInveraction : MonoBehaviour
{
    private InteractionEvents eventer;
    private void Start()
    {
        
        InteractionEvents eventer = GameObject.FindWithTag("Events").GetComponent<InteractionEvents>();

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void StartTalk()
    {

        Debug.Log("You are talking");
        
        
    }
}

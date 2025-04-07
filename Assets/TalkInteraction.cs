using UnityEngine;
using System.Collections.Generic;
using System;

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
public class TalkInteraction : MonoBehaviour
{
    [SerializeField] MonoBehaviour targetScript; // Accepts any MonoBehaviour-derived component
    private bool agreed = true;
    private int talkCount = 0;
    private InteractionEvents eventer;
    private Sprite playerSprite;

    private void Awake()
    {
        eventer = GameObject.FindWithTag("Events").GetComponent<InteractionEvents>();
        playerSprite = GameObject.FindWithTag("Player").GetComponent<SpriteRenderer>().sprite;

    }

    public void resetTalk()
    {
        talkCount = -1;
    }
    public void Interact(bool agree, Sprite sinSprite)
    {
        Debug.Log(talkCount);
        if (talkCount % 2 == 0)
        {
            eventer.changePicture(sinSprite);
        }
        else
        {
            eventer.changePicture(playerSprite);
        }
        if ( talkCount==1)
        {
            agreed = agree;
        }
        if (targetScript == null) return;
        var method = targetScript.GetType().GetMethod("Talk");
        if (method != null)
        {
            method.Invoke(targetScript, new object[] { agreed });
        }
        talkCount++;
    }
}
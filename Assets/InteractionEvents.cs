using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Image = UnityEngine.UI.Image;

public class InteractionEvents : MonoBehaviour
{
    [SerializeField] private GameObject talkingObject;
    [SerializeField] private Image picture;
    [SerializeField] private TextMeshProUGUI textBox;
    [SerializeField] private GameObject interactionTextBox;
    public GameObject npc;

   public void onnpcChanged()
    {
        Debug.Log("yas");
        if (npc == null)
        {interactionTextBox.active = false;}
        else
        {interactionTextBox.active=true;}
    }

    public void interaction(bool agree)
    {
        if(npc == null){return;}
        npc.GetComponent<TalkInteraction>().Interact(agree);
    }
    public void StartTalk()
    {
        if (talkingObject.active == true) return;
        interactionTextBox.active = false;
        talkingObject.active = true;
    }

    public void StopTalk(bool agreed)
    {
        talkingObject.active = false;
        // npc.GetComponent<Entity>.inConbat = true
        npc = null;
        
    }
    public void WriteText(string text)
    {
        textBox.text = text + "\n";
    }
}

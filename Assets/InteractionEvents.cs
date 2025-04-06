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
        npc.GetComponent<TalkInteraction>().Interact(agree,npc.GetComponentInChildren<SpriteRenderer>().sprite);
    }
    public void StartTalk()
    {
        Time.timeScale = 0;
        if (talkingObject.active == true) return;
        interactionTextBox.active = false;
        talkingObject.active = true;
    }

    public void  changePicture(Sprite sprite)
    {
        picture.sprite = sprite;
    }
    public void StopTalk(bool agreed)
    {
        Time.timeScale = 1;
        talkingObject.active = false;
        if(npc.GetComponent<Entity>())
        npc.GetComponent<Entity>().inCombat = !agreed;
        npc = null;
        
    }
    public void WriteText(string text)
    {
        textBox.text = text + "\n";
    }
}

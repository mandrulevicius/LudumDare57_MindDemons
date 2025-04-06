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

    public void interaction()
    {
        if(npc == null){return;}
        npc.GetComponent<TalkInteraction>().Interact();
    }
 
    public void StartTalk()
    {
        interactionTextBox.active = false;
        talkingObject.active = true;
    }
    public void WriteText(string text)
    {
        textBox.text = text + "\n";
    }
}

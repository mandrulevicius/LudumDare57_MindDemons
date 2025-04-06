using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Image = UnityEngine.UI.Image;

public class InteractionEvents : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Image picture;
    [SerializeField] private TextMeshProUGUI textBox;
    [SerializeField] private TextMeshProUGUI interactionTextBox;
    public GameObject npc;

   public void onnpcChanged()
    {
        Debug.Log("yas");
        if (npc == null)
        {interactionTextBox.enabled = false;}
        else
        {interactionTextBox.enabled=true;}
    }

    public void interaction()
    {
        if(npc == null){return;}
        npc.GetComponent<InteractionEvent>().Interact();
    }

    public void writeText(string text)
    {
        textBox.text += text + "\n";
    }
}

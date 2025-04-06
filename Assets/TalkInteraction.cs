using UnityEngine;

public class TalkInteraction : MonoBehaviour
{
    [SerializeField] MonoBehaviour targetScript; // Accepts any MonoBehaviour-derived component

    public void Interact()
    {
        if (targetScript == null) return;
 
        var method = targetScript.GetType().GetMethod("StartTalk");
        if (method != null)
        {
            method.Invoke(targetScript, null);
        }
    }
}
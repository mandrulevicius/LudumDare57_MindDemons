using UnityEngine;

public class TalkInteraction : MonoBehaviour
{
    [SerializeField] MonoBehaviour targetScript; // Accepts any MonoBehaviour-derived component
    private bool agreed = true;
    private int talkCount = 0;
    public void Interact(bool agree)
    {
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
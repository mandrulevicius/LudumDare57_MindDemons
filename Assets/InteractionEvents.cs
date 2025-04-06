using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class InteractionEvents : MonoBehaviour
{
    [SerializeField] private GameObject talkingObject;
    [SerializeField] private Image picture;
    [SerializeField] private TextMeshProUGUI textBox;
    [SerializeField] private GameObject interactionTextBox;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] public Slider mentalSlider;
    public AudioClip audioClip;
    public AudioClip genericClip;
    public GameObject npc;

    public void onnpcChanged()
    {
        if (npc == null)
        {
            Invoke(nameof(PlayGenericClip), audioSource.clip.length - audioSource.time);
            interactionTextBox.active = false;
        }
        else
        {
            interactionTextBox.active = true;
        }
    }
    
    public void PlayGenericClip()
    {
        audioSource.clip = genericClip;
        audioSource.Play();
    }
    public void PlayNewAudio()
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }
    public void playAudio()
    {
        Invoke(nameof(PlayNewAudio), audioSource.clip.length - audioSource.time);
    }

    public void interaction(bool agree)
    {
        if (npc == null)
        {
            return;
        }

        npc.GetComponent<TalkInteraction>().Interact(agree, npc.GetComponentInChildren<SpriteRenderer>().sprite);
    }

    public void StartTalk()
    {
        Time.timeScale = 0;
        if (talkingObject.active == true) return;
        interactionTextBox.active = false;
        talkingObject.active = true;
    }

    public void changePicture(Sprite sprite)
    {
        picture.sprite = sprite;
    }

    public void StopTalk(bool agreed)
    {
        Time.timeScale = 1;
        talkingObject.active = false;
        if (npc.GetComponent<Entity>())
        {
            npc.GetComponent<Entity>().inCombat = !agreed;
            npc.GetComponent<Entity>().isShooting = !agreed;
        }
        npc = null;
    }

    public void WriteText(string text)
    {
        textBox.text = text + "\n";
    }
}
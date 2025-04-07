using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class InteractionEvents : MonoBehaviour
{
    public GameObject npc;
    [SerializeField] private GameObject talkingObject;
    [SerializeField] private Image picture;
    [SerializeField] private TextMeshProUGUI textBox;
    [SerializeField] private GameObject interactionTextBox;
    [SerializeField] public Slider mentalSlider;
    [SerializeField] private AudioSource audioSource;
    public AudioClip audioClip;
    public AudioClip endOfBattleAudio;
    [SerializeField] private AudioSource genericAudioSource;
    public AudioClip genericClip;
    public float fadeDuration = 1.5f;

    public void onnpcChanged()
    {
        if (npc == null)
        {
            interactionTextBox.active = false;
            StartCoroutine(CrossFadeRoutine(audioSource, genericAudioSource));
        }
        else
        {
            interactionTextBox.active = true;
        }
    }

    public void PlayGenericClip()
    {
        StartCoroutine(CrossFadeRoutine(audioSource, genericAudioSource));
    }

    public void PlayNewAudio()
    {
        audioSource.clip = audioClip;
        StartCoroutine(CrossFadeRoutine(genericAudioSource, audioSource));
    }

    private IEnumerator CrossFadeRoutine(AudioSource fromSource, AudioSource toSource)
    {
        float time = 0f;
        float fromStart = fromSource.volume;
        float toStart = toSource.volume;

        if (!toSource.isPlaying)
            toSource.Play();

        while (time < fadeDuration)
        {
            float t = time / fadeDuration;
            fromSource.volume = Mathf.Lerp(fromStart, 0f, t);
            toSource.volume = Mathf.Lerp(toStart, 1f, t);
            time += Time.deltaTime;
            yield return null;
        }
        fromSource.volume = 0f;
        toSource.volume = 1f;
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
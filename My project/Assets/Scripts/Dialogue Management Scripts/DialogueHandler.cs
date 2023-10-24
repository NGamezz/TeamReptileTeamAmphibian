using System;
using System.Collections;
using TMPro;
using UnityEngine;

[Serializable]
public class Dialogue
{
    public string Text = string.Empty;
}

public class DialogueHandler : MonoBehaviour
{
    public static DialogueHandler Instance { get; private set; }

    [SerializeField] private bool typeWriterEffect = false;

    [SerializeField] private TMP_Text dialogueText;

    [Tooltip("The amount of delay between each character, in seconds.")]
    [SerializeField] private float typingSpeed = 0.02f;

    [Tooltip("How long should the text remain until it clears itself again?, in seconds.")]
    [SerializeField] private float amountOfLingerTimeForText = 2.0f;
    private Coroutine displayLineCoroutine;

    public void PlayDialogue(Dialogue dialogueToPlay)
    {
        if (!typeWriterEffect)
        {
            dialogueText.text = dialogueToPlay.Text;
            return;
        }

        if (displayLineCoroutine != null)
        {
            StopCoroutine(displayLineCoroutine);
        }
        displayLineCoroutine = StartCoroutine(GoThroughText(dialogueToPlay.Text));
    }

    private IEnumerator GoThroughText(string textToDisplay)
    {
        dialogueText.text = string.Empty;

        foreach (char character in textToDisplay.ToCharArray())
        {
            dialogueText.text += character;
            yield return new WaitForSeconds(typingSpeed);
        }

        yield return new WaitForSeconds(amountOfLingerTimeForText);
        dialogueText.text = string.Empty;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void OnDisable()
    {
        Instance = null;
    }
}
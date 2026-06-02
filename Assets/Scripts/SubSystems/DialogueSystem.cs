using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour {
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI dialogueText;
    public Image leftPortrait;
    public Image rightPortrait;
    public GameObject dialoguePanel;
    public float textDelay = 0.025f;
    private bool skip;
    private bool nextClicked;

    public void StartDialogue(Dialogue newDialogue) {
        Dialogue(newDialogue);
    }

    public Coroutine Dialogue(Dialogue newDialogue) {
        dialoguePanel.SetActive(true);
        return StartCoroutine(WriteAllDialogue(newDialogue));
    }

    public void Next() {
        nextClicked = true;
    }

    public void Skip() {
        skip = true;
        nextClicked = true;
    }

    IEnumerator WriteAllDialogue(Dialogue dialogue) {
        skip = false;
        dialoguePanel.SetActive(true);
        foreach (var VARIABLE in dialogue.lines) {
            yield return TypeDialogue(VARIABLE);
            if(skip) break;
        }
        dialoguePanel.SetActive(false);
    }
    
    IEnumerator TypeDialogue(DialogueStructure dialogue) {
        if(titleText) titleText.text = dialogue.characterName;
        if(leftPortrait) leftPortrait.overrideSprite = dialogue.leftCharacter? dialogue.leftCharacter.UISprite : null;
        if(rightPortrait) rightPortrait.overrideSprite = dialogue.rightCharacter ? dialogue.rightCharacter.UISprite : null;
        dialogueText.text = dialogue.dialogueText;
        dialogueText.maxVisibleCharacters = 0;
        yield return null;
        var parsedText = dialogueText.GetParsedText();
        foreach (char letter in parsedText) {
            if(skip) yield break;
            
            dialogueText.maxVisibleCharacters++;
            if(nextClicked) {
                nextClicked = false;
                dialogueText.maxVisibleCharacters = parsedText.Length;
                break;
            }
            yield return new WaitForSeconds(textDelay);
        }
        yield return new WaitUntil(()=> nextClicked);
        nextClicked = false;
    }
}
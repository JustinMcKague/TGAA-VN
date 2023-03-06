using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TypeWriter : MonoBehaviour
{
    public GameObject tsvReader;
    public GameObject namePlate;
    public Image characterSprite;

    private TSVReader reader;

    private string previousSpriteName;

    [Header("Text Settings")]
    [SerializeField][TextArea] private string[] lineOfDialogue;
    [SerializeField] private float textSpeed = 0.01f;

    [Header("UI Elements")]
    [SerializeField] private TMP_Text dialogueText;
    private int dialogueIndex = 0;

    private void Start()
    {
        reader = tsvReader.GetComponent<TSVReader>();
    }

    public void PopulateText()
    {
        lineOfDialogue = new string[reader.sceneDialogList.dialogue.Length];
        for (int i = 0; i < reader.sceneDialogList.dialogue.Length; i++)
        {
            lineOfDialogue[i] = reader.sceneDialogList.dialogue[i].dialogue;
        }
        StartText();
    }

    public void NextDialogue()
    {
        if (dialogueIndex < lineOfDialogue.Length - 1)
        {
            dialogueIndex++;
            ResetText();
        }
    }

    public void ResetText()
    {
        dialogueText.text = "";
        StartText();
    }

    public void StartText()
    {
        StartCoroutine(TextScroll());
    }

    IEnumerator TextScroll()
    {   
        string spriteName = reader.sceneDialogList.dialogue[dialogueIndex].characterSprite;
        if (reader.sceneDialogList.dialogue[dialogueIndex].characterSprite == "")
        {
            spriteName = previousSpriteName;
        }
        else
        {
            previousSpriteName = spriteName;
        }
        characterSprite.sprite = Resources.Load<Sprite>($"Sprites/{spriteName}");
        namePlate.GetComponent<SetCharacterName>().UpdateNamePlate(reader.sceneDialogList.dialogue[dialogueIndex].characterName);

        for (int i = 0; i < reader.sceneDialogList.dialogue[dialogueIndex].dialogue.Length + 1; i++)
        {
            dialogueText.text = lineOfDialogue[dialogueIndex].Substring(0, i);
            yield return new WaitForSeconds(textSpeed);
        }
    }

}

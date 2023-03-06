using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;

public class TSVReader : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject namePlate;
    public GameObject dialogBox;

    public TextAsset dialogueFile;

    private int columns;

    private DataTable dataTable;

    [System.Serializable]
    public class DialogueAsset
    {
        public string characterName;
        public string characterSprite;
        public string dialogue;
    }

    [System.Serializable]
    public class DialogueList
    {
        public DialogueAsset[] dialogue;
    }

    [SerializeField]
    public DialogueList sceneDialogList = new DialogueList();

    // Start is called before the first frame update
    void Start()
    {
        string[] howManyColumns = dialogueFile.text.Split(new string[] { "\n" }, StringSplitOptions.None);
        string firstRow = howManyColumns[0];
        char comma = '\t';

        columns = firstRow.Count(f => (f == comma));
        columns += 1;

        Debug.Log($"The number of columns in this tsv file is: {columns}.");

        ReadTSV();
    }

    void ReadTSV()
    {
        string[] data = dialogueFile.text.Split(new string[] { "\t", "\n" }, StringSplitOptions.None);

        int tableSize = data.Length / columns - 1;
        sceneDialogList.dialogue = new DialogueAsset[tableSize];

        for (int i = 0; i < tableSize; i++)
        {
            sceneDialogList.dialogue[i] = new DialogueAsset();
            sceneDialogList.dialogue[i].dialogue = data[columns * (i + 1) + 2];
            sceneDialogList.dialogue[i].characterName = data[columns * (i + 1)];
            sceneDialogList.dialogue[i].characterSprite = data[columns * (i + 1) + 1];
        }
        dialogBox.GetComponent<TypeWriter>().PopulateText();
    }
}

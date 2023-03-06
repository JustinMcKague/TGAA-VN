using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetCharacterName : TSVReader
{

    private TMP_Text nameText;

    // Start is called before the first frame update
    void Start()
    {
        nameText = GetComponent<TMP_Text>();
    }

    public void UpdateNamePlate(string characterName)
    {
        nameText.text = characterName;
    }
}

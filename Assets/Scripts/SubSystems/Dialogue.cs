using Data;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/Dialogue", fileName = "New Dialogue")]
public class Dialogue : ScriptableObject {
    public List<DialogueStructure> lines;
}

[Serializable]
public class DialogueStructure {
    public string characterName;
    [TextArea(0,10)]
    public string dialogueText;
    public PartyMember leftCharacter;
    public PartyMember rightCharacter;
}
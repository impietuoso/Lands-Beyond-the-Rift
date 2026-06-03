using DialogueSystem.DialogueEffects;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace DialogueSystem {
    [CreateAssetMenu(menuName = "Scriptable/Dialogue", fileName = "New Dialogue")]
    public class Dialogue : ScriptableObject {
        public List<DialogueStructure> lines;
    }

    [Serializable]
    public class DialogueStructure {
        public string characterName;
        [TextArea(0,10)]
        public string dialogueText;
        [SerializeReference] public IDialogueEffect effect;
        public DialogueCharacter leftCharacter;
        public DialogueCharacter rightCharacter;
    }
}
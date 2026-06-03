using System.Collections;

namespace DialogueSystem.DialogueEffects {
    public interface IDialogueEffect {
        public IEnumerator Play(DialogueSystem sys);
    }
}

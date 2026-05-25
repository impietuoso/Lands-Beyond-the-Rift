using TurnBasedRPG.Data;
using TurnBasedRPG.UI.Views;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TurnBasedRPG.Controller
{
    public class SkillManager : MonoBehaviour  {
        public PartyMemberView memberView;
    
        public void SwapSkills(GameObject drop, PointerEventData eventData) {
            var target = drop.GetComponent<SkillView>().Data;
            var data = eventData.pointerDrag.GetComponent<SkillView>().Data;
            var dropParentList = (ObservableList<ISkill>)drop.GetComponentInParent<ListView>().Data;
            var eventParentList = (ObservableList<ISkill>)eventData.pointerDrag.GetComponentInParent<ListView>().Data;
            var targetIndex = drop.transform.GetSiblingIndex() - 1;
            var dataIndex = eventData.pointerDrag.transform.GetSiblingIndex() - 1;
            dropParentList[targetIndex] = data;
            if(target == null && eventParentList != memberView.Data.EquipedSkills) eventParentList.RemoveAt(dataIndex);
            else eventParentList[dataIndex] = target;
        }

        public void UnequipSkill(SkillView view) {
            if (view.Data == null) return;
        
            var targetIndex = view.transform.GetSiblingIndex() - 1;
            var removedSkill = memberView.Data.EquipedSkills[targetIndex];
            memberView.Data.SetSkill(targetIndex , null);
            memberView.Data.LearnSkill(removedSkill);
        }
    }
}
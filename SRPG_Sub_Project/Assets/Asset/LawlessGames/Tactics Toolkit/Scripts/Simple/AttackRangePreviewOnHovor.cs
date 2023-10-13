using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TacticsToolkit {
    public class AttackRangePreviewOnHovor : MonoBehaviour
    {
        public GameEventGameObject showAttackRange;
        public GameEvent hideAttackRange;

        //Displays a characters attack range on hover. null will default to character position. 
        public void DisplayRangePreview(BaseEventData eventData)
        {
            if(gameObject.GetComponent<Button>().IsInteractable())
             showAttackRange.Raise(null);
        }

        //Hides a characters attack range.
        public void HideRangePreview(BaseEventData eventData)
        {
            hideAttackRange.Raise();
        }
    }
}

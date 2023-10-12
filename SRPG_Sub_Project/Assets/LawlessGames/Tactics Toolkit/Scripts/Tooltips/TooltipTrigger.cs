using UnityEngine;
using UnityEngine.EventSystems;

namespace TacticsToolkit
{
    public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public string title;
        public string description;
        public void OnPointerEnter(PointerEventData eventData)
        {
            RectTransform rectTransform = GetComponent<RectTransform>();
            Vector3 position = rectTransform.position;
            Vector2 dimensions = new Vector2(rectTransform.rect.width, rectTransform.rect.height);


            TooltipManager.Show(title, description, position, dimensions);
        }


        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            TooltipManager.Hide();
        }
    }
}

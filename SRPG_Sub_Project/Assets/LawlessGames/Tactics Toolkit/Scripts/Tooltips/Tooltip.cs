using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace TacticsToolkit
{
    [ExecuteInEditMode()]
    public class Tooltip : MonoBehaviour
    {
        public Text title;
        public Text description;

        public LayoutElement layoutElement;

        public float xPadding = 0;
        public float yPadding = 0;

        public int characterLimit;

        public bool isFixedPosition;

        public TooltipAlignment defaultAlignment;
        private TooltipAlignment alignment;

        public enum TooltipAlignment
        {
            TopLeft,
            Top,
            TopRight,
            Right,
            BottomRight,
            Bottom,
            BottomLeft,
            Left
        }

        private void Start()
        {
            alignment = defaultAlignment;
        }


        [SerializeField]
        private RectTransform canvas;

        public void SetContent(string title, string description, Vector3 position, Vector2 dimensions)
        {
            this.title.text = title;
            this.description.text = description;

            int titleLenght = this.title.text.Length;
            int descriptionLenght = this.description.text.Length;
            layoutElement.enabled = titleLenght > characterLimit || descriptionLenght > characterLimit;

            if (!isFixedPosition)
            {
                StartCoroutine(MoveTooltip(position, dimensions));
            }
        }

        public RectTransform buttonRectTransform;
        private IEnumerator MoveTooltip(Vector3 position, Vector2 dimensions)
        {
            yield return new WaitForEndOfFrame();
            RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
            Vector2 tooltipDimensions = new Vector2(rectTransform.rect.width, rectTransform.rect.height);
            ChooseValidAlignment(position, dimensions, tooltipDimensions);
        }

        private void MoveToAlignment(Vector3 position, Vector2 dimensions, Vector2 tooltipDimensions)
        {
            Vector3 tooltipPosition = position; switch (alignment)
            {
                case TooltipAlignment.TopLeft:
                    tooltipPosition += new Vector3(-dimensions.x / 2 - tooltipDimensions.x / 2, dimensions.y / 2 + tooltipDimensions.y / 2, 0);
                    break;
                case TooltipAlignment.Top:
                    tooltipPosition += new Vector3(0, dimensions.y / 2 + tooltipDimensions.y / 2, 0);
                    break;
                case TooltipAlignment.TopRight:
                    tooltipPosition += new Vector3(dimensions.x / 2 + tooltipDimensions.x / 2, dimensions.y / 2 + tooltipDimensions.y / 2, 0);
                    break;
                case TooltipAlignment.Right:
                    tooltipPosition += new Vector3(dimensions.x / 2 + tooltipDimensions.x / 2, 0, 0);
                    break;
                case TooltipAlignment.BottomRight:
                    tooltipPosition += new Vector3(dimensions.x / 2 + tooltipDimensions.x / 2, -dimensions.y / 2 - tooltipDimensions.y / 2, 0);
                    break;
                case TooltipAlignment.Bottom:
                    tooltipPosition += new Vector3(0, -dimensions.y / 2 - tooltipDimensions.y / 2, 0);
                    break;
                case TooltipAlignment.BottomLeft:
                    tooltipPosition += new Vector3(-dimensions.x / 2 - tooltipDimensions.x / 2, -dimensions.y / 2 - tooltipDimensions.y / 2, 0);
                    break;
                case TooltipAlignment.Left:
                    tooltipPosition += new Vector3(-dimensions.x / 2 - tooltipDimensions.x / 2, 0, 0);
                    break;
            }

            // Set the position of the tooltip
            transform.position = tooltipPosition;
        }

        void ChooseValidAlignment(Vector3 position, Vector2 dimensions, Vector2 tooltipDimensions)
        {
            MoveToAlignment(position, dimensions, tooltipDimensions);
            Vector3[] objectCorners = new Vector3[4];
            GetComponent<RectTransform>().GetWorldCorners(objectCorners);
            bool isValid = objectCorners.All(x => x.x < Screen.width && x.x > 0 && x.y < Screen.height && x.y > 0);

            TooltipAlignment[] alignments = (TooltipAlignment[])Enum.GetValues(alignment.GetType());

            int ogAlignment = Array.IndexOf(alignments, alignment);


            while (!isValid)
            {
                int j = Array.IndexOf(alignments, alignment) + 1;

                if (ogAlignment == j)
                    break;

                alignment = (alignments.Length == j) ? alignments[0] : alignments[j];
                MoveToAlignment(position, dimensions, tooltipDimensions);
                GetComponent<RectTransform>().GetWorldCorners(objectCorners);
                isValid = objectCorners.All(x => x.x < Screen.width && x.x > 0 && x.y < Screen.height && x.y > 0);


            }
        }


        public void ResetContent()
        {
            this.title.text = "";
            this.description.text = "";
        }

    }
}

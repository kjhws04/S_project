using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace TacticsToolkit
{
    public class TooltipManager : MonoBehaviour
    {
        public static TooltipManager instance;

        public Tooltip tooltip;
        public float fadeOnTime = 1f;
        public float waitTime = 1f;

        public void Awake()
        {
            instance = this;
        }

        public static void Show(string title, string description, Vector3 position, Vector2 dimensions) => instance.StartCoroutine(instance.ShowTooltipOverTime(title, description, position, dimensions));

        private IEnumerator ShowTooltipOverTime(string title, string description, Vector3 position, Vector2 dimensions)
        {
            if (instance)
            {
                yield return new WaitForSeconds(waitTime);

                tooltip.gameObject.SetActive(true);
                tooltip.SetContent(title, description, position, dimensions);

                yield return new WaitForEndOfFrame();
                // Get all the child components with Image or Text components
                var children = tooltip.GetComponentsInChildren<Component>()
                    .Where(c => c is Image || c is Text)
                    .Select(c => (c as Graphic))
                    .ToList();

                // Initialize alpha values for all the child components
                var startAlphas = children.Select(c => c.color.a).ToList();
                var endAlphas = Enumerable.Repeat(1f, children.Count).ToList();

                // Initialize start time and elapsed time
                float startTime = Time.time;
                float elapsedTime = 0f;

                // Fade in all the child components over the specified duration
                while (elapsedTime < fadeOnTime)
                {
                    float t = elapsedTime / fadeOnTime;
                    for (int i = 0; i < children.Count; i++)
                    {
                        var child = children[i];
                        var startAlpha = startAlphas[i];
                        var endAlpha = endAlphas[i];

                        // Interpolate the alpha value for this child component
                        var alpha = Mathf.Lerp(startAlpha, endAlpha, t);

                        // Set the alpha value for this child component
                        var color = child.color;
                        color.a = alpha;
                        child.color = color;
                    }
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }

                // Set final alpha values for all the child components
                for (int i = 0; i < children.Count; i++)
                {
                    var child = children[i];
                    var endAlpha = endAlphas[i];

                    var color = child.color;
                    color.a = endAlpha;
                    child.color = color;
                }
            }
        }

        public static void Hide()
        {
            if (instance)
            {
                instance.StopAllCoroutines();
                instance.tooltip.GetComponent<Image>().color = new Color(1, 1, 1, 0);
                instance.tooltip.ResetContent();
                instance.tooltip.gameObject.SetActive(false);
            }
        }
    }
}

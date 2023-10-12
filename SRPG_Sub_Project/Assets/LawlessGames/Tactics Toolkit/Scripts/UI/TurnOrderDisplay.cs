using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TacticsToolkit
{
    public class TurnOrderDisplay : MonoBehaviour
    {
        public GameObject portraitPrefab;
        // Start is called before the first frame update

        private List<string> turnOrder;

        void Start()
        {
            turnOrder = new List<string>();
        }

        public void SetTurnOrderDisplay(List<GameObject> characters)
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
                turnOrder.RemoveRange(0, turnOrder.Count);
            }

            turnOrder = new List<string>();

            //the order should be consistent
            foreach (var item in characters)
            {
                var spawnedObject = Instantiate(portraitPrefab, transform);
                spawnedObject.GetComponent<Image>().sprite = item.GetComponent<SpriteRenderer>().sprite;

                turnOrder.Add(item.ToString() + " - " + item.name);
            }
        }
    }
}

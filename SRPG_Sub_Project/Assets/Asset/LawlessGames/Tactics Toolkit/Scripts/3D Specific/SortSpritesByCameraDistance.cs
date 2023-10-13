using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TacticsToolkit
{
    public class SortSpritesByCameraDistance : MonoBehaviour
    {
        public int sortingOrder = 0;
        public int minimumSortingOrder = -1000;
        private List<SpriteRenderer> combinedList;
        Vector3 previousCameraPosition;
        void Start()
        {
            previousCameraPosition = Camera.main.transform.position;

            var playerCharacters = GameObject.FindGameObjectsWithTag("Player").Select(x => x.GetComponent<CharacterManager>()).ToList();
           var enemyCharacters = GameObject.FindGameObjectsWithTag("Enemy").Select(x => x.GetComponent<EnemyController>()).ToList();
            combinedList = new List<SpriteRenderer>();
            combinedList.AddRange(playerCharacters.Select(x => x.gameObject.GetComponent<SpriteRenderer>()));
            combinedList.AddRange(enemyCharacters.Select(x => x.gameObject.GetComponent<SpriteRenderer>()));
        }

        void FixedUpdate()
        {
            if (Camera.main.transform.position != previousCameraPosition)
            {
                foreach (SpriteRenderer sr in combinedList)
                {
                    if (sr.isVisible)
                    {
                        float distance = Vector3.Distance(Camera.main.transform.position, sr.transform.position);
                        int newSortingOrder = -(int)(distance * 10);

                        //if the character is dead put them one back so other characters can stand over them. 
                        if (!sr.gameObject.GetComponent<Entity>().isAlive)
                            newSortingOrder--;

                        sr.sortingOrder = Mathf.Max(newSortingOrder, minimumSortingOrder);
                        var healthBar = sr.gameObject.GetComponentInChildren<Canvas>();
                        if (healthBar)
                            healthBar.sortingOrder = Mathf.Max(newSortingOrder, minimumSortingOrder);
                    }
                }
                previousCameraPosition = Camera.main.transform.position;
            }
        }
    }
}

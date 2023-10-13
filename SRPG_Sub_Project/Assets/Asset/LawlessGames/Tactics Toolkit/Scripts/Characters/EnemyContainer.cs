using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace TacticsToolkit
{
    //Just something to keep the enemies in. 
    public class EnemyContainer : MonoBehaviour
    {
        public List<EnemyController> enemyList;

        // Start is called before the first frame update
        void Start()
        {
            enemyList = GetComponentsInChildren<EnemyController>().ToList();
        }

        //if a new characterspawns. Let every enemy know.
        public void SpawnPlayerCharacter(GameObject newCharacter)
        {
            //is ally team
            if (newCharacter.GetComponent<Entity>().teamID == 1)
            {
                foreach (var enemy in enemyList)
                {
                    enemy.playerCharacters.Add(newCharacter.GetComponent<CharacterManager>());
                }
            }
        }
    }
}

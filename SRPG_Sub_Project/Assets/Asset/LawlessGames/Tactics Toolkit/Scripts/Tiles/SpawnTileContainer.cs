using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace TacticsToolkit
{
    public class SpawnTileContainer : MonoBehaviour
    {
        public List<SpawnTile> spawnTiles;
        public int TeamID = 0;

        private void Start()
        {
            spawnTiles = GetComponentsInChildren<SpawnTile>().ToList();
        }
    }
}

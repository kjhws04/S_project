using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace TacticsToolkit
{
    //A tile type to be attached to the overlay tiles. Currently only using effect but there's lots of potential usages here. 
    [CreateAssetMenu(fileName = "TileData", menuName = "ScriptableObjects/TileData")]
    public class TileData : ScriptableObject
    {
        public List<TileBase> baseTiles;

        public bool hasTooltip;
        public string tooltipName;
        [TextArea(3, 10)]
        public string tooltipDescription;
        public TileTypes type = TileTypes.Traversable;
        public int MoveCost = 1;
        public ScriptableEffect effect;

        [Header("Use this for 3D maps, ignore for 2D")]
        public List<Material> Tiles3D;
    }
}
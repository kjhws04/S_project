using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace TacticsToolkit
{
    public class MapManager3D : MapManager
    {
        public TileDataRuntimeSet tileData3D;
        new void Awake()
        {
            base.Awake();
        }

        public override void SetMap()
        {
            Tilemap[] childTilemaps = gameObject.GetComponentsInChildren<Tilemap>();
            map = new Dictionary<Vector2Int, OverlayTile>();

            mapBounds = new MapBounds();
            foreach (var tilemap in childTilemaps)
            {
                foreach (Transform child in tilemap.transform)
                {
                    var gridLocation = tilemap.WorldToCell(child.position);
                    var meshBounds = child.gameObject.GetComponent<MeshRenderer>().bounds;
                    var overlayTile = Instantiate(overlayTilePrefab, overlayContainer.transform);

                    foreach (var tileData in tileTypeList.items)
                    {
                        foreach (var material in tileData.Tiles3D)
                        {
                            if(material == child.GetComponent<MeshRenderer>().sharedMaterial)
                            {
                                overlayTile.tileData = tileData;
                            }
                        }
                    }

                    overlayTile.transform.position = new Vector3(child.position.x, child.position.y + meshBounds.extents.y + 0.0001f, child.position.z);
                    overlayTile.gridLocation = gridLocation;
                    var tileKey = new Vector2Int(gridLocation.x, gridLocation.y);
                    SetMapBounds(gridLocation);

                    if (!map.ContainsKey(tileKey))
                    {
                        map.Add(tileKey, overlayTile);
                    }
                    else
                    {
                        map.Remove(tileKey);
                        map.Add(tileKey, overlayTile);
                    }
                }
            }
        }

        private void SetMapBounds(Vector3Int gridLocation)
        {
            if (mapBounds.yMin > gridLocation.y)
                mapBounds.yMin = gridLocation.y;

            if (mapBounds.yMax < gridLocation.y)
                mapBounds.yMax = gridLocation.y;

            if (mapBounds.xMin > gridLocation.x)
                mapBounds.xMin = gridLocation.x;

            if (mapBounds.xMax > gridLocation.x)
                mapBounds.xMax = gridLocation.x;
        }
    }
}

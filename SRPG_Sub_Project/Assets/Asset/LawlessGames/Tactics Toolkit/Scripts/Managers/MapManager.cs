using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace TacticsToolkit
{
    public class MapManager : MonoBehaviour
    {
        private static MapManager _instance;
        public static MapManager Instance { get { return _instance; } }

        public OverlayTile overlayTilePrefab;
        public GameObject overlayContainer;
        public TileDataRuntimeSet tileTypeList;
        public Dictionary<Vector2Int, OverlayTile> map;
        public Dictionary<TileBase, TileData> dataFromTiles = new Dictionary<TileBase, TileData>();

        public Tilemap tilemap;

        public MapBounds mapBounds;

        private Entity activeCharacter;

        public void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
            }

            SetMap();
        }

        public virtual void SetMap()
        {
            if (tileTypeList)
            {
                foreach (var tileData in tileTypeList.items)
                {
                    foreach (var item in tileData.baseTiles)
                    {
                        dataFromTiles.Add(item, tileData);
                    }
                }
            }

            tilemap = gameObject.GetComponentInChildren<Tilemap>();
            map = new Dictionary<Vector2Int, OverlayTile>();
            BoundsInt bounds = tilemap.cellBounds;

            mapBounds = new MapBounds(bounds.xMax, bounds.yMax, bounds.xMin, bounds.yMin);

            //loop through the tilemap and create all the overlay tiles
            for (int z = bounds.max.z; z >= bounds.min.z; z--)
            {
                for (int y = bounds.min.y; y < bounds.max.y; y++)
                {
                    for (int x = bounds.min.x; x < bounds.max.x; x++)
                    {
                        var tileLocation = new Vector3Int(x, y, z);
                        var tileKey = new Vector2Int(x, y);
                        if (tilemap.HasTile(tileLocation) && !map.ContainsKey(tileKey))
                        {
                            var overlayTile = Instantiate(overlayTilePrefab, overlayContainer.transform);
                            var cellWorldPosition = tilemap.GetCellCenterWorld(tileLocation);
                            var baseTile = tilemap.GetTile(tileLocation);
                            overlayTile.transform.position = new Vector3(cellWorldPosition.x, cellWorldPosition.y, cellWorldPosition.z + 1);
                            overlayTile.GetComponent<SpriteRenderer>().sortingOrder = tilemap.GetComponent<TilemapRenderer>().sortingOrder;
                            overlayTile.gridLocation = tileLocation;

                            if (dataFromTiles.ContainsKey(baseTile))
                            {
                                overlayTile.tileData = dataFromTiles[baseTile];
                                if (dataFromTiles[baseTile].type == TileTypes.NonTraversable)
                                    overlayTile.isBlocked = true;
                            }

                            map.Add(tileKey, overlayTile);
                        }
                    }
                }
            }
        }

    public void SetActiveCharacter(GameObject activeCharacter)
        {
            this.activeCharacter = activeCharacter.GetComponent<Entity>();
        }

        //Get all tiles next to a tile
        public List<OverlayTile> GetNeighbourTiles(OverlayTile currentOverlayTile, List<OverlayTile> searchableTiles, bool ignoreObstacles = false, bool walkThroughAllies = true, int remainingRange = 10)
        {
            Dictionary<Vector2Int, OverlayTile> tileToSearch = new Dictionary<Vector2Int, OverlayTile>();

            if (searchableTiles.Count > 0)
            {
                foreach (var item in searchableTiles)
                {
                    tileToSearch.Add(item.grid2DLocation, item);
                }
            }
            else
            {
                tileToSearch = map;
            }

           List<OverlayTile> neighbours = new List<OverlayTile>();
            if (currentOverlayTile != null)
            {
                //top
                foreach (var direction in GetDirections())
                {
                    Vector2Int locationToCheck = currentOverlayTile.grid2DLocation + direction;
                    ValidateNeighbour(currentOverlayTile, ignoreObstacles, walkThroughAllies, tileToSearch, neighbours, locationToCheck, remainingRange);
                }
            }

            return neighbours;
        }

        //Check the neighbouring tile is valid.
        private static void ValidateNeighbour(OverlayTile currentOverlayTile, bool ignoreObstacles, bool walkThroughAllies, Dictionary<Vector2Int, OverlayTile> tilesToSearch, List<OverlayTile> neighbours, Vector2Int locationToCheck, int remainingRange)
        {
            bool canMoveToLocation = false;

            if (tilesToSearch.ContainsKey(locationToCheck))
            {
                OverlayTile tile = tilesToSearch[locationToCheck];
                bool isBlocked = tile.isBlocked;
                bool isActiveCharacter = tile.activeCharacter != null && Instance.activeCharacter != null;
                bool isSameTeam = isActiveCharacter && tile.activeCharacter.teamID == Instance.activeCharacter.teamID;
                bool canWalkThroughAllies = !isBlocked && isActiveCharacter && isSameTeam;

                if (ignoreObstacles || (!ignoreObstacles && !isBlocked) || canWalkThroughAllies)
                {
                    if(remainingRange == 0)
                    {
                        Debug.Log("test");
                    }
                    if(currentOverlayTile.grid2DLocation == new Vector2Int(-2, 1) && tile.grid2DLocation == new Vector2Int(-1, 1))
                    {
                        Debug.Log("test2");
                    }

                    if (tile.GetMoveCost() <= remainingRange)
                    {
                        canMoveToLocation = true;
                    }
                }

                if (canMoveToLocation)
                {
                    //artificial jump height. 
                    if (Mathf.Abs(currentOverlayTile.gridLocation.z - tile.gridLocation.z) <= 1)
                        neighbours.Add(tilesToSearch[locationToCheck]);
                }
            }

           
        }

        private IEnumerable<Vector2Int> GetDirections()
        {
            yield return Vector2Int.up;
            yield return Vector2Int.down;
            yield return Vector2Int.right;
            yield return Vector2Int.left;
        }

        //Hide all overlayTiles currently being shown.
        public void ClearTiles()
        {
            foreach (var item in map.Values)
            {
                item.HideTile();
            }
        }

        //Get a tile by world position. 
        public OverlayTile GetOverlayByTransform(Vector3 position)
        {
            var gridLocation = tilemap.WorldToCell(position);
            if(map.ContainsKey(new Vector2Int(gridLocation.x, gridLocation.y)))
                return map[new Vector2Int(gridLocation.x, gridLocation.y)];

            return null;
        }

        //Get list of overlay tiles by grid positions. 
        public List<OverlayTile> GetOverlayTilesFromGridPositions(List<Vector2Int> positions)
        {
            List<OverlayTile> overlayTiles = new List<OverlayTile>();

            foreach (var item in positions)
            {
                overlayTiles.Add(map[item]);
            }

            return overlayTiles;
        }

        //Get overlay tile by grid position. 
        public OverlayTile GetOverlayTileFromGridPosition(Vector2Int position) => map[position];
    }

    public class MapBounds
    {
        public int xMax = 0;
        public int yMax = 0;
        public int xMin = 0;
        public int yMin = 0;

        public MapBounds(int xMax, int yMax, int xMin, int yMin)
        {
            this.xMax = xMax;
            this.yMax = yMax;
            this.xMin = xMin;
            this.yMin = yMin;
        }

        public MapBounds()
        {
        }
    }
}

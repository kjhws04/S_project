using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TacticsToolkit
{
    public class RangeFinder
    {
        //Gets all tiles within a range
        public List<OverlayTile> GetTilesInRange(OverlayTile startingTile, int range, bool ignoreObstacles = false, bool walkThroughAllies = true)
        {
            var inRangeTiles = new List<OverlayTile>();
            int stepCount = 0;
            startingTile.remainingMovement = range;
            inRangeTiles.Add(startingTile);

            // List to hold the tiles in the previous step
            var tileForPreviousStep = new List<OverlayTile>
            {
                startingTile
            }; 

            while (stepCount < range)
            {
                var surroundingTiles = new List<OverlayTile>(); // List to hold the tiles in the current step

                foreach (var item in tileForPreviousStep)
                {
                    int moveCost = !ignoreObstacles ? item.GetMoveCost() : 1; // Calculate the move cost

                    var newNeighbours = MapManager.Instance.GetNeighbourTiles(item, new List<OverlayTile>(), ignoreObstacles, walkThroughAllies, item.remainingMovement); // Get the neighbouring tiles of the current tile

                    foreach (var tile in newNeighbours)
                    {
                        if(tile.gridLocation == new UnityEngine.Vector3Int(-1, 1, 1) || tile.gridLocation == new UnityEngine.Vector3Int(-3, 1, 1) || tile.gridLocation == new UnityEngine.Vector3Int(-2, 1, 1))
                        {
                            Debug.Log("Tile is: " + tile.gridLocation);
                        }
                        int heightDifference = CalculateHeightCost(ignoreObstacles, item, tile);

                        var remainingMovement = item.remainingMovement - tile.GetMoveCost() - heightDifference;

                        if (remainingMovement > tile.remainingMovement)
                            tile.remainingMovement = remainingMovement; // Calculate the remaining movement of the tile
                    }

                    surroundingTiles.AddRange(newNeighbours.Where(x => x.remainingMovement >= 0).ToList()); // Add the neighbouring tiles to the list of surrounding tiles
                }

                inRangeTiles.AddRange(surroundingTiles); // Add the surrounding tiles to the list of in-range tiles
                tileForPreviousStep = surroundingTiles; // Set the previous step tiles to the surrounding tiles
                stepCount++; // Increment the step count
            }

            //reset movement
            foreach (var item in inRangeTiles)
            {
                item.remainingMovement = 0;
            }

            return inRangeTiles.Distinct().ToList();
        }

        //This is an example of calculating heightcost. It assumes the cost of moving up is one.
        private static int CalculateHeightCost(bool ignoreObstacles, OverlayTile item, OverlayTile tile)
        {
            int heightDifference = tile.gridLocation.z - item.gridLocation.z;
            heightDifference = !ignoreObstacles && heightDifference > 0 ? heightDifference : 0;
            return heightDifference;
        }
    }
}
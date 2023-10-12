using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MapEditor : MonoBehaviour
{
#if UNITY_EDITOR
    // % : Ctrl, # : Shift, & : Alt
    
    [MenuItem("Tools/GenerateStage %#t")] //%#g <= ctrl + shift + G ´ÜÃàÅ°
    private static void GenerateStage()
    {
        GameObject[] gos =  Resources.LoadAll<GameObject>("Prefabs/Stage");
        foreach (GameObject go in gos)
        {
            Tilemap tmBase = Util.FindChild<Tilemap>(go, "Tilemap_Base", true);
            Tilemap tm = Util.FindChild<Tilemap>(go, "Tilemap_Col", true);

            using (var writer = File.CreateText($"Assets/Resources/Stage/{go.name}.txt"))
            {
                writer.WriteLine(tm.cellBounds.xMin);
                writer.WriteLine(tm.cellBounds.xMax - 1);
                writer.WriteLine(tm.cellBounds.yMin);
                writer.WriteLine(tm.cellBounds.yMax - 1);

                for (int y = tm.cellBounds.yMax - 1; y >= tm.cellBounds.yMin; y--)
                {
                    for (int x = tm.cellBounds.xMin; x <= tm.cellBounds.xMax - 1; x++)
                    {
                        TileBase tile = tm.GetTile(new Vector3Int(x, y, 0));
                        if (tile != null)
                            writer.Write("1");
                        else
                            writer.Write("0");
                    }
                    writer.WriteLine();
                }
            }
        }
    }
#endif
}

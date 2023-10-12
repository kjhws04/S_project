using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class StageManager
{
    #region Stage Load
    public Grid CurrentGrid { get; private set; }

    public int MinX { get; set; }
    public int MaxX { get; set; }
    public int MinY { get; set; }
    public int MaxY { get; set; }

    bool[,] _col;
    #endregion

    // <summary>
    // ���� col�� �޾� 1�� �̵� �Ұ� ����, 0�� �̵� ���� �������� ����
    // </summary>
    public bool CanGo(Vector3Int cellPos)
    {
        if (cellPos.x < MinX || cellPos.x > MaxX)
            return false;
        if (cellPos.y < MinX || cellPos.y > MaxX)
            return false;

        int x = cellPos.x - MinX;
        int y = MaxY = cellPos.y;
        return !_col[y, x];
    }

    // <summary>
    // stageid�� �޾� ���� �ε��ϴ� �Լ� Managers.Stage.LoadMap(1);
    // </summary>
    public void LoadMap(int stageid)
    {
        string stageName = "Stage_" + stageid;
        GameObject go = Managers.Resource.Instantiate($"Stage/{stageName}");
        go.name = "Stage";

        GameObject col = Util.FindChild(go, "Tilemap_Col", true);
        if (col != null)
            col.SetActive(false);

        CurrentGrid = go.GetComponent<Grid>();

        TextAsset txt = Managers.Resource.Load<TextAsset>($"Stage/{stageName}");
        StringReader reader = new StringReader(txt.text);

        MinX = int.Parse(reader.ReadLine());
        MaxX = int.Parse(reader.ReadLine());
        MinY = int.Parse(reader.ReadLine());
        MaxY = int.Parse(reader.ReadLine());

        int xCount = MaxX - MinX + 1;
        int yCount = MaxY - MinY + 1;
        _col = new bool[yCount, xCount];
        for (int y = 0; y < yCount; y++)
        {
            string line = reader.ReadLine();
            for (int x = 0; x < xCount; x++)
            {
                _col[y, x] = (line[x] == '1' ? true : false);
            }
        }
    }

    // <summary>
    // ���������� �ı��ϴ� �Լ� Clear()���� ���
    // </summary>
    public void DestroyMap(GameObject stage)
    {
        if (stage != null)
        {
            GameObject.Destroy(stage);
            CurrentGrid = null;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class StageManager
{
    public int ExpItem = 0;

    #region Stage Load
    public Grid CurrentGrid { get; private set; }

    public int MinX { get; set; }
    public int MaxX { get; set; }
    public int MinY { get; set; }
    public int MaxY { get; set; }

    bool[,] _col;
    #endregion

    #region Stage Setting Test
    // <summary>
    // 맵의 col을 받아 1은 이동 불가 지역, 0은 이동 가능 지역으로 설정
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
    // stageid를 받아 맵을 로드하는 함수 Managers.Stage.LoadMap(1);
    // </summary>
    public void LoadMap(int stageid)
    {
        string stageName = "Stage_" + stageid;
        GameObject go = Managers.Resource.Instantiate($"Stage/{stageName}");
        go.name = "Stage";

        //GameObject col = Util.FindChild(go, "Tilemap_Col", true);
        //if (col != null)
        //    col.SetActive(false);

        //CurrentGrid = go.GetComponent<Grid>();

        //TextAsset txt = Managers.Resource.Load<TextAsset>($"Stage/{stageName}");
        //StringReader reader = new StringReader(txt.text);

        //MinX = int.Parse(reader.ReadLine());
        //MaxX = int.Parse(reader.ReadLine());
        //MinY = int.Parse(reader.ReadLine());
        //MaxY = int.Parse(reader.ReadLine());

        //int xCount = MaxX - MinX + 1;
        //int yCount = MaxY - MinY + 1;
        //_col = new bool[yCount, xCount];
        //for (int y = 0; y < yCount; y++)
        //{
        //    string line = reader.ReadLine();
        //    for (int x = 0; x < xCount; x++)
        //    {
        //        _col[y, x] = (line[x] == '1' ? true : false);
        //    }
        //}
    }
    #endregion

    // <summary>
    // 스테이지를 파괴하는 함수 Clear()에서 사용
    // </summary>
    public void DestroyMap(GameObject stage)
    {
        if (stage != null)
        {
            GameObject.Destroy(stage);
            CurrentGrid = null;
        }
    }

    public void StageSetting(List<Stat> stat, int stage)
    {
        switch (stage)
        {
            case 1:
                Stage1(stat);
                break;
            case 2:
                Stage2(stat);
                break;
            case 3:
                Stage3(stat);
                break;
            case 4:
                Stage4(stat);
                break;
            case 5:
                Stage5(stat);
                break;
        }
    }

    void InitializeEnemyStats(List<Stat> stat, string[] enemyTypes, float[,] statValues)
    {
        for (int i = 0; i < enemyTypes.Length; i++)
        {
            Stat newStat = Managers.Resource.Instantiate($"Character/Enemy/{enemyTypes[i]}").GetComponent<Stat>();
            newStat.Hp = (int)statValues[i, 0];
            newStat.Str = (int)statValues[i, 1];
            newStat.Int = (int)statValues[i, 2];
            newStat.Tec = (int)statValues[i, 3];
            newStat.Spd = (int)statValues[i, 4];
            newStat.Def = (int)statValues[i, 5];
            newStat.MDef = (int)statValues[i, 6];
            newStat.Luk = (int)statValues[i, 7];
            newStat._unitAR = statValues[i, 8];
            newStat._unitAS = statValues[i, 9];
            stat.Add(newStat); //_p2Unit에 새로운 스텟 추가
        }
    }

    // <summary>
    // 스테이지의 몬스터 종류와 stat 저장
    // </summary>
    void Stage1(List<Stat> stat)
    {
        ExpItem = 3;
        string[] enemyTypes = { "Orc_Sword", "Orc_Sword", "Orc_Ax", "Orc_Bow", "Orc_Ax", "Orc_Bow" };
        float[,] statValues = new float[,]
        {
        {40, 7, 0, 7, 6, 2, 2, 1, 1f, 1f},
        {40, 7, 0, 7, 6, 2, 2, 1, 1f, 1f},
        {40, 8, 0, 7, 6, 2, 2, 1, 1f, 1f},
        {40, 6, 0, 7, 6, 2, 2, 1, 4f, 1f},
        {40, 8, 0, 7, 6, 2, 2, 1, 1f, 1f},
        {40, 6, 0, 7, 6, 2, 2, 1, 4f, 1f}
    };
        InitializeEnemyStats(stat, enemyTypes, statValues);
    }
    void Stage2(List<Stat> stat)
    {
        ExpItem = 3;
        string[] enemyTypes = { "Orc_Sword", "Orc_Sword", "Orc_Ax", "Orc_Bow", "Orc_Ax", "Orc_Bow" };
        float[,] statValues = new float[,]
        {
        {40, 8, 0, 7, 6, 2, 2, 1, 1f, 1f},
        {40, 8, 0, 7, 6, 2, 2, 1, 1f, 1f},
        {40, 9, 0, 7, 6, 2, 2, 1, 1f, 1f},
        {40, 7, 0, 7, 6, 2, 2, 1, 4f, 1f},
        {40, 9, 0, 7, 6, 2, 2, 1, 1f, 1f},
        {40, 7, 0, 7, 6, 2, 2, 1, 4f, 1f}
    };

        InitializeEnemyStats(stat, enemyTypes, statValues);
    }
    void Stage3(List<Stat> stat)
    {
        ExpItem = 4;
        string[] enemyTypes = { "Orc_Sword", "Orc_Sword", "Orc_Sword", "Orc_Bow", "Demon_Bagic", "Orc_Bow" };
        float[,] statValues = new float[,]
        {
        {40, 10, 0, 8, 7, 3, 3, 1, 1f, 1f},
        {40, 10, 0, 8, 7, 3, 3, 1, 1f, 1f},
        {40, 10, 0, 8, 7, 3, 3, 1, 1f, 1f},
        {40, 8, 0, 7, 6, 2, 2, 1, 4f, 1f},
        {80, 11, 11, 8, 8, 4, 4, 4, 0.7f, 1f},
        {40, 8, 0, 7, 6, 2, 2, 1, 4f, 1f}
    };

        InitializeEnemyStats(stat, enemyTypes, statValues);
    }
    void Stage4(List<Stat> stat)
    {
        ExpItem = 4;
        string[] enemyTypes = { "Orc_Sword", "Orc_Sword", "Orc_Sword", "Demon_Bagic", "Demon_Bagic", "Demon_Bagic" };
        float[,] statValues = new float[,]
        {
        {40, 10, 0, 8, 7, 3, 3, 1, 1f, 1f},
        {40, 10, 0, 8, 7, 3, 3, 1, 1f, 1f},
        {40, 10, 0, 8, 7, 3, 3, 1, 1f, 1f},
        {80, 11, 11, 8, 8, 4, 4, 4, 1f, 1f},
        {80, 11, 11, 8, 8, 4, 4, 4, 1f, 1f},
        {80, 11, 11, 8, 8, 4, 4, 4, 1f, 1f},
    };

        InitializeEnemyStats(stat, enemyTypes, statValues);
    }
    void Stage5(List<Stat> stat)
    {
        ExpItem = 5;
        string[] enemyTypes = { "Orc_Bow", "Orc_Bow", "Orc_Bow", "Orc_Bow", "Elf_Boss", "Orc_Bow" };
        float[,] statValues = new float[,]
        {
        {50, 10, 0, 8, 7, 3, 3, 1, 3f, 1f},
        {50, 10, 0, 8, 7, 3, 3, 1, 3f, 1f},
        {50, 10, 0, 8, 7, 3, 3, 1, 3f, 1f},
        {50, 10, 0, 8, 7, 3, 3, 1, 3f, 1f},
        {240, 15, 15, 9, 9, 5, 5, 5, 6f, 0.7f},
        {50, 10, 0, 8, 7, 3, 3, 1, 3f, 1f},
    };

        InitializeEnemyStats(stat, enemyTypes, statValues);
    }
}

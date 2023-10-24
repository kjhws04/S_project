using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowthRateData
{
    // <summary>
    // ĳ���� ��, ����� hp, str, int, tec, spd, def, mde, luk ���� : ��8�� �׸�
    // </summary>
    #region Character Grownth
    public float[] g_Aqua = { 0.5f, 0.3f, 0.1f, 0.35f, 0.3f, 0.2f, 0.25f, 0.4f };
    public float[] g_Broyna = { 0.55f, 0.4f, 0.1f, 0.3f, 0.25f, 0.25f, 0.25f, 0.2f };
    public float[] g_Dmitry = { 0.45f, 0.3f, 0.2f, 0.25f, 0.3f, 0.4f, 0.4f, 0.1f };
    public float[] g_Elie = { 0.3f, 0.25f, 0.25f, 0.4f, 0.4f, 0.15f, 0.2f, 0.5f };
    public float[] g_Kan = { 0.3f, 0.5f, 0.15f, 0.5f, 0.5f, 0.25f, 0.25f, 0.25f };
    public float[] g_Knight = { 0.7f, 0.3f, 0.3f, 0.3f, 0.2f, 0.4f, 0.3f, 0.2f };
    public float[] g_Mars = { 0.3f, 0.25f, 0.3f, 0.35f, 0.5f, 0.15f, 0.2f, 0.4f };
    public float[] g_Sakura = { 0.35f, 0.25f, 0.1f, 0.3f, 0.35f, 0.25f, 0.25f, 0.25f };
    public float[] g_Shy = { 0.3f, 0.2f, 0.5f, 0.4f, 0.35f, 0.15f, 0.15f, 0.4f };
    public float[] g_Van = { 0.3f, 0.25f, 0.45f, 0.35f, 0.3f, 0.2f, 0.2f, 0.3f };
    public float[] g_Zion = { 0.4f, 0.1f, 0.4f, 0.2f, 0.25f, 0.25f, 0.25f, 0.3f };
    #endregion

    // <summary>
    // �Ʒ� for���� ���÷� ���� ������ ������� ���� 
    // </summary>
    public static bool CheckGrowth(float growthRate)
    {
        return Random.value < growthRate;
    }

    //public void LevelUp()
    //{
    //    level++;
    //    for (int i = 0; i < stats.Length; i++)
    //    {
    //        if (CheckGrowth([i]))
    //        {
    //        }
    //    }
    //}
}

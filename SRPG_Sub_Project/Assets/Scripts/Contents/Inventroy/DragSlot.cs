using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragSlot : MonoBehaviour
{
    static public DragSlot _instance;
    public CharSelSlot dragSlot;
    [SerializeField]
    private Image charProfile;
    public Stat tempStat;

    private void Start()
    {
        _instance = this;
        charProfile = GetComponent<Image>();
        tempStat = GetComponent<Stat>();
    }

    public void DragSetImage(Stat _orgStat, Sprite _charProfile)
    {
        charProfile.sprite = _charProfile;
        CopyFrom(_orgStat);
        SetColor(1);
    }

    public void CopyFrom(Stat _stat)
    {
        tempStat.modelImg = _stat.modelImg;
        tempStat.cardImage = _stat.cardImage;
        tempStat.proflieImg = _stat.proflieImg;

        tempStat.attackType = _stat.attackType;
        tempStat.Level = _stat.Level;
        tempStat.Name = _stat.Name;

        tempStat.CurClass = _stat.CurClass;
        tempStat.WeaponClass = _stat.WeaponClass;
        tempStat.MainWeapon = _stat.MainWeapon;

        tempStat.Hp = _stat.Hp;
        tempStat.Str = _stat.Str;
        tempStat.Int = _stat.Int;
        tempStat.Tec = _stat.Tec;
        tempStat.Spd = _stat.Spd;
        tempStat.Def = _stat.Def;
        tempStat.MDef = _stat.MDef;
        tempStat.Luk = _stat.Luk;
        tempStat.Wei = _stat.Wei;

        tempStat._unitFR = _stat._unitFR;
        tempStat._unitAR = _stat._unitAR;
        tempStat._unitAS = _stat._unitAS;
    }

    public void SetColor(float _alpha)
    {
        Color color = charProfile.color;
        color.a = _alpha;
        charProfile.color = color;
    }
}

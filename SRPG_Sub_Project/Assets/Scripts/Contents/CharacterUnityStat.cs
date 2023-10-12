using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterUnityStat : Stat
{
    // <surmmary>
    // 캐릭터의 현재 스텟 (myUnit)
    // </surmmary>
    //#region Character Current Stat
    //[SerializeField] int _currentHp;
    //[SerializeField] int _currentStr;
    //[SerializeField] int _currentInt;
    //[SerializeField] int _currentTec;
    //[SerializeField] int _currentSpd;
    //[SerializeField] int _currentDef;
    //[SerializeField] int _currentMDef;
    //[SerializeField] int _currentLuk;
    //[SerializeField] int _currentWei;
    //[SerializeField] int _currentMov;

    //public int CurrentHp { get { return _currentHp; } set { _currentHp = value; } }
    //public int CurrentStr { get { return _currentStr; } set { _currentStr = value; } }
    //public int CurrentInt { get { return _currentInt; } set { _currentInt = value; } }
    //public int CurrentTec { get { return _currentTec; } set { _currentTec = value; } }
    //public int CurrentSpd { get { return _currentSpd; } set { _currentSpd = value; } }
    //public int CurrentDef { get { return _currentDef; } set { _currentDef = value; } }
    //public int CurrentMDef { get { return _currentMDef; } set { _currentMDef = value; } }
    //public int CurrentLuk { get { return _currentLuk; } set { _currentLuk = value; } }
    //public int CurrentWei { get { return _currentWei; } set { _currentWei = value; } }
    //public int CurrentMove { get { return _currentMov; } set { _currentMov = value; } }
    //#endregion

    // <surmmary>
    // 캐릭터의 상한 스텟 (myUnit)
    // </surmmary>
    #region Character Max Stat
    [SerializeField] int _maxHp;
    [SerializeField] int _maxStr;
    [SerializeField] int _maxInt;
    [SerializeField] int _maxTec;
    [SerializeField] int _maxSpd;
    [SerializeField] int _maxDef;
    [SerializeField] int _maxMDef;
    [SerializeField] int _maxLuk;

    public int MaxHp { get { return _maxHp; } set { _maxHp = value; } }
    public int MaxStr { get { return _maxStr; } set { _maxStr = value; } }
    public int MaxInt { get { return _maxInt; } set { _maxInt = value; } }
    public int MaxTec { get { return _maxTec; } set { _maxTec = value; } }
    public int MaxSpd { get { return _maxSpd; } set { _maxSpd = value; } }
    public int MaxDef { get { return _maxDef; } set { _maxDef = value; } }
    public int MaxMDef { get { return _maxMDef; } set { _maxMDef = value; } }
    public int MaxLuk { get { return _maxLuk; } set { _maxLuk = value; } }
    #endregion

    // <surmmary>
    // 캐릭터의 성장률 스텟 (myUnit)
    // </surmmary>
    #region Character Max Stat
    [SerializeField] int _growthHp;
    [SerializeField] int _growthStr;
    [SerializeField] int _growthInt;
    [SerializeField] int _growthTec;
    [SerializeField] int _growthSpd;
    [SerializeField] int _growthDef;
    [SerializeField] int _growthMDef;
    [SerializeField] int _growthLuk;

    public int GrouwthHp { get { return _growthHp; } set { _growthHp = value; } }
    public int GrouwthStr { get { return _growthStr; } set { _growthStr = value; } }
    public int GrouwthInt { get { return _growthInt; } set { _growthInt = value; } }
    public int GrouwthTec { get { return _growthTec; } set { _growthTec = value; } }
    public int GrouwthSpd { get { return _growthSpd; } set { _growthSpd = value; } }
    public int GrouwthDef { get { return _growthDef; } set { _growthDef = value; } }
    public int GrouwthMDef { get { return _growthMDef; } set { _growthMDef = value; } }
    public int GrouwthLuk { get { return _growthLuk; } set { _growthLuk = value; } }
    #endregion
}

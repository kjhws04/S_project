using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterUnityStat : MonoBehaviour
{
    // <surmmary>
    // 카드 속성
    // </surmmary>
    #region Character Level
    public enum CardType { Character, Item }
    public Sprite modelImg; //모델 이미지
    public Sprite cardImage; //카드 이미지
    public Sprite proflieImg; //프로필 이미지
    public CardType cardType; //카드 종류
    public Weapon.WeaponType attackType; //무기 속성으로 생성했지만 동일하니 걍 사용 AD, AP
    #endregion

    // <surmmary>
    // 캐릭터의 레벨
    // </surmmary>
    #region Character Level
    [SerializeField] int _level = 1;
    [SerializeField] int _maxLevel = 50;
    [SerializeField] int _exp;
    [SerializeField] int _maxExp = 100;
    [SerializeField] int _charPiece = 0;

    public int Level { get { return _level; } set { _level = value; } }
    public int MaxLevel { get { return _maxLevel; } set { _maxLevel = value; } }
    public int Exp { get { return _exp; } set { _exp = value; } }
    public int MaxExp { get { return _maxExp; } set { _maxExp = value; } }
    public int CharPiece { get { return _charPiece; } set { _charPiece = value; } }
    #endregion

    // <surmmary>
    // 캐릭터의 기본 속성
    // </surmmary>
    #region Character Attributes
    [SerializeField] string _name;
    [SerializeField] CharacterDefine.Gender _gender = CharacterDefine.Gender.Unknown;
    [SerializeField] CharacterDefine.Tribe _tribe = CharacterDefine.Tribe.Unknown;
    [SerializeField] CharacterDefine.Belong _belong = CharacterDefine.Belong.Unknown;
    [SerializeField] CharacterDefine.Rating _rating = CharacterDefine.Rating.Unknown;
    [SerializeField] int _rank; //랭킹
    [SerializeField] bool _magicTalent; //마술재능
    [SerializeField] bool _sPower; //특별한힘
    [SerializeField] string _explanation; //캐릭터info
    [SerializeField] int _height; //신장

    public string Name { get { return _name; } set { _name = value; } }
    public CharacterDefine.Gender Gender { get { return _gender; } set { _gender = value; } }
    public CharacterDefine.Tribe Tribe { get { return _tribe; } set { _tribe = value; } }
    public CharacterDefine.Belong Belong { get { return _belong; } set { _belong = value; } }
    public CharacterDefine.Rating Rating { get { return _rating; } set { _rating = value; } }
    public int Rank { get { return _rank; } set { _rank = value; } }
    public bool MagicTalent { get { return _magicTalent; } set { _magicTalent = value; } }
    public bool SPower { get { return _sPower; } set { _sPower = value; } }
    public string Explanation { get { return _explanation; } set { _explanation = value; } }
    public int Height { get { return _height; } set { _height = value; } }
    #endregion

    // <surmmary>
    // 캐릭터의 클래스, 스킬 속성
    // </surmmary>
    #region Character Skill
    [SerializeField] Class.CurClass _class = Class.CurClass.Unknown;
    [SerializeField] Class.WeaponClass _weaponClass = Class.WeaponClass.Unknown;
    [SerializeField] Class.VehicleClass _vehicleClass = Class.VehicleClass.Unknown;
    [SerializeField] Skill.PassiveSkill _passive = Skill.PassiveSkill.Unknown;
    [SerializeField] Skill.EquipSkill _equipSkill = Skill.EquipSkill.Unknown;
    [SerializeField] Skill.ActiveSkill _active = Skill.ActiveSkill.Unknown;

    public Class.CurClass CurClass { get { return _class; } set { _class = value; } }
    public Class.WeaponClass WeaponClass { get { return _weaponClass; } set { _weaponClass = value; } }
    public Class.VehicleClass VehicleClass { get { return _vehicleClass; } set { _vehicleClass = value; } }
    public Skill.PassiveSkill PassiveSkill { get { return _passive; } set { _passive = value; } }
    public Skill.EquipSkill EquipSkill { get { return _equipSkill; } set { _equipSkill = value; } }
    public Skill.ActiveSkill ActiveSkill { get { return _active; } set { _active = value; } }
    #endregion

    // <surmmary>
    // 캐릭터의 무기, 기사단 속성
    // </surmmary>
    #region Character KnightOrder
    [SerializeField] bool _haveOrder = false;
    [SerializeField] Order.KnightOrder _knight = Order.KnightOrder.None;
    [SerializeField] WeaponStat _mainWeapon;
    [SerializeField] Weapon.SubWeapon _subWeapon = Weapon.SubWeapon.None;

    public bool HaveOrder { get { return _haveOrder; } set { _haveOrder = value; } }
    public Order.KnightOrder KnightOrder { get { return _knight; } set { _knight = value; } }
    public WeaponStat MainWeapon { get { return _mainWeapon; } set { _mainWeapon = value; } }
    public Weapon.SubWeapon SubWeapon { get { return _subWeapon; } set { _subWeapon = value; } }
    #endregion

    // <surmmary>
    // 캐릭터의 스텟 (체력, 근력, 지능, 기술, 속도, 수비, 마방, 행운, 무게, 이동)
    // </surmmary>
    #region Character Stat
    [SerializeField] int _hp;
    [SerializeField] int _str;
    [SerializeField] int _int;
    [SerializeField] int _tec;
    [SerializeField] int _spd;
    [SerializeField] int _def;
    [SerializeField] int _Mdef;
    [SerializeField] int _luk;
    [SerializeField] int _wei;
    [SerializeField] int _mov;
    [SerializeField] int _currentHp;

    public int Hp { get { return _hp; } set { _hp = value; } }
    public int Str { get { return _str; } set { _str = value; } }
    public int Int { get { return _int; } set { _int = value; } }
    public int Tec { get { return _tec; } set { _tec = value; } }
    public int Spd { get { return _spd; } set { _spd = value; } }
    public int Def { get { return _def; } set { _def = value; } }
    public int MDef { get { return _Mdef; } set { _Mdef = value; } }
    public int Luk { get { return _luk; } set { _luk = value; } }
    public int Wei { get { return _wei; } set { _wei = value; } }
    public int Move { get { return _mov; } set { _mov = value; } }
    public int CurrentHp { get { return _currentHp; } set { _currentHp = value; } }
    #endregion

    // <surmmary>
    // 무기의 종류에 따른 스텟 변화용 함수
    // </surmmary>
    public void WeaponApply(WeaponStat _weapon, bool _isUesd)
    {
        if (_isUesd)
        {
            switch (_weapon.weaponAttackType)
            {
                case Weapon.WeaponType.AD:
                    _str += _weapon.increaseVal;
                    break;
                case Weapon.WeaponType.AP:
                    _int += _weapon.increaseVal;
                    break;
            }
        }
        else
        {
            switch (_weapon.weaponAttackType)
            {
                case Weapon.WeaponType.AD:
                    _str -= _weapon.increaseVal;
                    break;
                case Weapon.WeaponType.AP:
                    _int -= _weapon.increaseVal;
                    break;
            }
        }
    }

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

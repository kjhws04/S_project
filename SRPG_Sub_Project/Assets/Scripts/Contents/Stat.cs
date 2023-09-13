using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stat : MonoBehaviour
{
    // <surmmary>
    // ī�� �Ӽ�
    // </surmmary>
    #region Character Level
    public enum CardType { Character, Item }
    public Sprite modelImg; //�� �̹���
    public Sprite cardImage; //ī�� �̹���
    public CardType cardType; //ī�� ����
    #endregion

    // <surmmary>
    // ĳ������ ����
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
    // ĳ������ �⺻ �Ӽ�
    // </surmmary>
    #region Character Attributes
    [SerializeField] string _name;
    [SerializeField] CharacterDefine.Gender _gender = CharacterDefine.Gender.Unknown;
    [SerializeField] CharacterDefine.Tribe _tribe = CharacterDefine.Tribe.Unknown;
    [SerializeField] CharacterDefine.Belong _belong = CharacterDefine.Belong.Unknown;
    [SerializeField] CharacterDefine.Rating _rating = CharacterDefine.Rating.Unknown;
    [SerializeField] int _rank; //��ŷ
    [SerializeField] bool _magicTalent; //�������
    [SerializeField] bool _sPower; //Ư������
    [SerializeField] string _explanation; //ĳ����info
    [SerializeField] int _height; //����

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
    // ĳ������ Ŭ����, ��ų �Ӽ�
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
    // ĳ������ ����, ���� �Ӽ�
    // </surmmary>
    #region Character KnightOrder
    [SerializeField] bool _haveOrder = false;
    [SerializeField] Order.KnightOrder _knight = Order.KnightOrder.None;
    [SerializeField] Weapon.MainWeapon _mianWeapon = Weapon.MainWeapon.None;
    [SerializeField] Weapon.SubWeapon _subWeapon = Weapon.SubWeapon.None;

    public bool HaveOrder { get { return _haveOrder; } set { _haveOrder = value; } }
    public Order.KnightOrder KnightOrder { get { return _knight; } set { _knight = value; } }
    public Weapon.MainWeapon MainWeapon { get { return _mianWeapon; } set { _mianWeapon = value; } }
    public Weapon.SubWeapon SubWeapon { get { return _subWeapon; } set { _subWeapon = value; } }
    #endregion

    // <surmmary>
    // ĳ������ ���� (ü��, �ٷ�, ����, ���, �ӵ�, ����, ����, ���, ����, �̵�)
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
    #endregion
}

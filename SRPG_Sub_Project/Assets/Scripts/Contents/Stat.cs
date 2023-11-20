using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Stat : MonoBehaviour
{
    #region All Stat
    // <surmmary>
    // 카드 속성
    // </surmmary>
    #region Character Level
    public enum CardType { Character, Item }
    public Sprite modelImg; //모델 이미지
    public Sprite cardImage; //카드 이미지
    public Sprite proflieImg; //프로필 이미지
    public Sprite backGroundImg; //배경 이미지
    public CardType cardType; //카드 종류
    public Weapon.WeaponType attackType; //무기 속성으로 생성했지만 동일하니 걍 사용 AD, AP
    public bool isSettingUsed = false; //캐릭터 세팅 여부 true면 선택됨
    public int settingNum = 0; //캐릭터 세팅 순서 번호

    public bool IsSettingUsed { get { return isSettingUsed; } set { isSettingUsed = value; } }
    public int SettingNum { get { return settingNum; } set { settingNum = value; } }
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
    // 캐릭터의 성장률 스텟 (체력, 근력, 지능, 기술, 속도, 수비, 마방, 행운, 무게, 이동)
    // </surmmary>
    #region Character Grown Stat
    [SerializeField] float g_hp; bool b_hp = false;
    [SerializeField] float g_str; bool b_str = false;
    [SerializeField] float g_int; bool b_int = false;
    [SerializeField] float g_tec; bool b_tec = false;
    [SerializeField] float g_spd; bool b_spd = false;
    [SerializeField] float g_def; bool b_def = false;
    [SerializeField] float g_Mdef; bool b_Mdef = false;
    [SerializeField] float g_luk; bool b_luk = false;

    public float g_Hp { get { return g_hp; } set { g_hp = value; } }
    public bool b_Hp { get { return b_hp; } set { b_hp = value; } }
    public float g_Str { get { return g_str; } set { g_str = value; } }
    public bool b_Str { get { return b_str; } set { b_str = value; } }
    public float g_Int { get { return g_int; } set { g_int = value; } }
    public bool b_Int { get { return b_int; } set { b_int = value; } }
    public float g_Tec { get { return g_tec; } set { g_tec = value; } }
    public bool b_Tec { get { return b_tec; } set { b_tec = value; } }
    public float g_Spd { get { return g_spd; } set { g_spd = value; } }
    public bool b_Spd { get { return b_spd; } set { b_spd = value; } }
    public float g_Def { get { return g_def; } set { g_def = value; } }
    public bool b_Def { get { return b_def; } set { b_def = value; } }
    public float g_MDef { get { return g_Mdef; } set { g_Mdef = value; } }
    public bool b_MDef { get { return b_Mdef; } set { b_Mdef = value; } }
    public float g_Luk { get { return g_luk; } set { g_luk = value; } }
    public bool b_Luk { get { return b_luk; } set { b_luk = value; } }
    #endregion
    #endregion

    // <surmmary>
    // 무기 스텟 적용 함수
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

    #region Level Up System
    // <surmmary>
    // 레벨업 & 경험치 아이템 사용
    // </surmmary>
    public void AddExp(int exp)
    {
        Exp += exp;
        if (Exp >= MaxExp)
        {
            Level++;
            Exp -= MaxExp;
            LevelUp_Popup level = Managers.UI.ShowPopupUI<LevelUp_Popup>();
            LevelUpAfterStat();
            level.StatSetting(this);
        }
    }

    // <surmmary>
    // 유닛의 성장률 비교 후 실체 스텟에 적용 (후에 db에 저장)
    // </surmmary>
    public void LevelUpAfterStat()
    {
        if (CheckGrowth(g_Hp)) { Hp++; b_hp = true; }
        if (CheckGrowth(g_str)) { Str++; b_str = true; }
        if (CheckGrowth(g_Int)) { Int++; b_int = true; }
        if (CheckGrowth(g_Tec)) { Tec++; b_tec = true; }
        if (CheckGrowth(g_Spd)) { Spd++; b_spd = true; }
        if (CheckGrowth(g_Def)) { Def++; b_def = true; }
        if (CheckGrowth(g_MDef)) { MDef++; b_Mdef = true; }
        if (CheckGrowth(g_Luk)) { Luk++; b_luk = true; }
    }

    // <surmmary>
    // 랜덤 값과 유닛의 성장률 비교 후 bool값을 리턴하는 함수
    // </surmmary>
    bool CheckGrowth(float growthRate)
    {
        return Random.value < growthRate;
    }
    #endregion

    #region SPUM Stat
    public SPUM_Prefabs _spumPref;
    public Stat _target; //공격 타켓

    public float _unitFR;
    public float _unitAR; //공격 범위
    public float _unitAS; //공격 속도
    private float _moveSpeed = 1f; //이동속도

    public Vector2 _dir; //방향 백터
    public Vector2 _tempDis; //새로운 타켓 백터

    public float findTimer; //대상을 찾는 타이머 초기화 변수
    public float attackTimer; //공격 속도 타이머 변수

    private Animator _anim;
    #endregion

    public enum UnitState
    {
        idle,
        run,
        attack,
        stun,
        skill,
        death
    }

    // <surmmary>
    // 전투할 때, 실행할 함수 (유닛의 체력바, spum_prefabs 를 달아줌)
    // </surmmary>
    public void BattleStart()
    {
        _spumPref = GetComponent<SPUM_Prefabs>();
        GameObject _root = Util.FindChild(gameObject, "UnitRoot", false);
        _anim = _root.GetComponent<Animator>();
        Managers.UI.Make2DUI<UI_HpBar>(transform);
        _currentHp = _hp;
    }

    private void Update()
    {
        if (Managers.Battle.StepType == Define.GameStep.Battle)
        {
            CheckState();
        }
    }

    #region Battle Func
    public UnitState _unitState = UnitState.idle;

    // <surmmary>
    // UnitState(에니메이션)에 따른 행동 양식 함수
    // </surmmary>
    void CheckState()
    {
        switch (_unitState)
        {
            case UnitState.idle:
                FindTarget();
                break;
            case UnitState.run:
                FindTarget();
                DoMove();
                break;
            case UnitState.attack:
                CheckAttack();
                break;
            case UnitState.stun:
                break;
            case UnitState.skill:
                break;
            case UnitState.death:
                break;
        }
    }

    // <surmmary>
    // UnitState(에니메이션)에 따른 에니메이션 재생
    // </surmmary>
    void SetState(UnitState state)
    {
        _unitState = state;
        switch (_unitState)
        {
            case UnitState.idle:
                _anim.SetFloat("RunState", 0f);
                break;
            case UnitState.run:
                _anim.SetFloat("RunState", 0.5f);
                break;
            case UnitState.attack:
                break;
            case UnitState.stun:
                _anim.SetFloat("RunState", 1f);
                break;
            case UnitState.skill:
                _anim.SetFloat("AttackState", 1f);
                _anim.SetTrigger("Attack");
                break;
            case UnitState.death:
                _anim.SetTrigger("Die");
                break;
        }
    }

    // <surmmary>
    // 방향 * _moveSpeed * time.deltaTime으로 이동 방향 + 속도
    // </surmmary>
    void DoMove()
    {
        if (!CheckTarget()) return;
        CheckDistance();

        _dir = _tempDis.normalized;
        SetDirection();

        transform.position += (Vector3)_dir * _moveSpeed * Time.deltaTime;
        //길찾기 알고리즘
    }

    // <surmmary>
    // spum 이미지를 정방향으로 놓기
    // </surmmary>
    void SetDirection()
    {
        if (_dir.x >= 0)
        {
            _spumPref._anim.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            _spumPref._anim.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    // <surmmary>
    // 유닛이 목표 지점까지의 거리를 체크하여, 적절한 상태로 전환하는 함수
    // </surmmary>
    bool CheckDistance()
    {
        _tempDis = (Vector2)(_target.transform.localPosition - transform.position);

        float tDis = _tempDis.sqrMagnitude;

        if (tDis <= _unitAR * _unitAR) // 거리가 유닛의 공격 범위의 제곱 이내인지 확인
        {
            SetState(UnitState.attack); 
            return true;
        }
        else
        {
            if (!CheckTarget()) // 목표 확인
                SetState(UnitState.idle); // 없으면 대기
            else 
                SetState(UnitState.run); // 있으면 해당 목표로 이동

            return false;
        }
    }

    // <surmmary>
    // 유닛이 실제 공격을 수행할지 판단 후, 조건에 맞으면 공격을 수행하는 함수
    // </surmmary>
    void CheckAttack()
    {
        if (!CheckTarget()) 
            return; // 목표가 없으면 리턴
        if (!CheckDistance()) 
            return; // 거리가 너무 멀면 리턴

        _anim.SetFloat("RunState", 0f);

        attackTimer += Time.deltaTime; // 공격 딜레이
        if (attackTimer > _unitAS) // 딜레이와 공격 속도 비교
        {
            DoAttack(); //공격 실행
            attackTimer = 0;
        }
    }

    // <surmmary>
    // 공격 실행 함수 (애니메이션 적용)
    // </surmmary>
    void DoAttack()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer > _unitAS)
        {
            _anim.SetFloat("AttackState", 0f);
            _anim.SetTrigger("Attack");
            SetAttackType();
        }
    }

    // <surmmary>
    // 사용하는 무기에 따른 공격 방식 구분
    // </surmmary>
    void SetAttackType()
    {
        switch (WeaponClass)
        {
            case Class.WeaponClass.Unknown:
            case Class.WeaponClass.Sword:
            case Class.WeaponClass.Spear:
            case Class.WeaponClass.Ax:
            case Class.WeaponClass.Dagger:
            case Class.WeaponClass.Shield:
            default:
                SetAttackNear();
                break;
            case Class.WeaponClass.Bow:
            case Class.WeaponClass.Pistol:
            case Class.WeaponClass.Staff:
                StartCoroutine(SetAttackMissile());
                break;
        }
    }

    // <surmmary>
    // 공격 방식 설계
    // </surmmary>
    public void SetAttackNear()
    {
        int damage = Managers.Combat.Combat(this, _target);
        bool _isMiss = Managers.Combat.isMiss;
        bool _isBlock = Managers.Combat.isBlock;
        bool _isCritical = Managers.Combat.isCritical;
        _target.SetDamage(damage, _isMiss, _isBlock, _isCritical);
    }

    public IEnumerator SetAttackMissile()
    {
        GameObject missilePrefab = null;
        switch (WeaponClass)
        {
            case Class.WeaponClass.Bow:
                yield return new WaitForSeconds(0.5f);
                missilePrefab = Managers.Resource.Instantiate("Item/Arrow");
                break;
            case Class.WeaponClass.Pistol:
                missilePrefab = Managers.Resource.Instantiate("Item/Bullet");
                break;
            case Class.WeaponClass.Staff:
                missilePrefab = Managers.Resource.Instantiate("Item/Fireball");
                break;
        }

        if (_target != null && missilePrefab != null)
        {
            missilePrefab.GetComponent<Missile>().TargetSetting(this, _target, Managers.Combat.Combat(this, _target),
                                                                Managers.Combat.isMiss,
                                                                Managers.Combat.isBlock,
                                                                Managers.Combat.isCritical);
        }
    }

    // <surmmary>
    // 공격 계산 함수 (타켓의 남은 체력 - 공격력), hp가 0이면 사망
    // </surmmary>
    public void SetDamage(int damage, bool _isMiss, bool _isBlock, bool _isCritical)
    {
        UI_DamageText _text = Managers.UI.Make2DUI<UI_DamageText>(transform);
        _text.ShowDamageText(damage, _isBlock, _isMiss, _isCritical);

        _currentHp -= damage;

        if (_currentHp <= 0)
        {
            SetState(UnitState.death);
            DeathSetting();
        }
    }

    // <surmmary>
    // 캐릭터가 죽었을 때 진형(battle manager) 리스트, hp바 및 col등 삭제
    // </surmmary>
    void DeathSetting()
    {
        switch (gameObject.tag)
        {
            case "P1":
                Managers.Battle._p1UnitCheckList.Remove(this);
                break;
            case "P2":
                Managers.Battle._p2UnitList.Remove(this);
                break;
        }
        GetComponent<CapsuleCollider>().enabled = false;
        Destroy(GetComponent<Rigidbody>());

        if (Managers.Battle._p1UnitCheckList == null || Managers.Battle._p1UnitCheckList.Count == 0)
        {
            Managers.Battle.Lose();
        }
        if (Managers.Battle._p2UnitList == null || Managers.Battle._p2UnitList.Count == 0)
        {
            if (Managers.Battle.win)
                Managers.Battle.Win();
        }

        StartCoroutine(DestroyCoroutine(2.0f));
    }

    IEnumerator DestroyCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay); // 지정된 시간(2초)만큼 대기

        Destroy(gameObject);
    }

    // <surmmary>
    // 타켓을 찾는 함수
    // </surmmary>
    void FindTarget()
    {
        findTimer += Time.deltaTime;
        if (findTimer > Managers.Battle._findTimer)
        {
            _target = Managers.Battle.GetTarget(this);
            if (_target != null) 
                SetState(UnitState.run);
            else SetState(UnitState.idle);

            findTimer = 0;
        }
    }

    // <surmmary>
    // 타켓의 상태 체크 (null, death일 경우 false) //CheckAttack()
    // </surmmary>
    bool CheckTarget()
    {
        bool val = true;
        if (_target == null)
        {
            val = false;
            return val;
        }
        if (_target._unitState == UnitState.death) val = false;
        if (!_target.gameObject.activeInHierarchy) val = false;

        if (!val)
        {
            SetState(UnitState.idle);
        }

        return val;
    }

    #endregion
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserData : MonoBehaviour
{
    #region UserInfo
    [SerializeField] string _userName = "ShyAI5364";
    [SerializeField] int _userLevel = 15;
    [SerializeField] int _heart = 200;
    [SerializeField] int _ticket1 = 100;
    [SerializeField] int _ticket2 = 100;
    [SerializeField] int _ticketFriend = 100;
    [SerializeField] int _expItem = 100;
    [SerializeField] Stat _currentChar;
    public Sprite _modelImg;
    public Sprite _moedlDotImg;
    public Sprite _backGroundImg;
    public int _stageCount = 1;
    
    public string UserName { get { return _userName; } set { _userName = value; } }
    public int UserLevel { get { return _userLevel; } set { _userLevel = value; } }
    public int Heart { get { return _heart; } set { _heart = value; } }
    public int Ticket1 { get { return _ticket1; } set { _ticket1 = value; } }
    public int Ticket2 { get { return _ticket2; } set { _ticket2 = value; } }
    public int TicketFriend { get { return _ticketFriend; } set { _ticketFriend = value; } }
    public int ExpItem { get { return _expItem; } set { _expItem = value; } }
    public Stat CurrentChar { get { return _currentChar; } set { _currentChar = value; } }
    public int StageCount { get { return _stageCount; } set { _stageCount = value; } }
    #endregion

    #region Character/Weapon Data
    //보유 캐릭터 & 무기 데이터
    public Dictionary<string, Stat> _userCharData = new Dictionary<string, Stat>();
    public Dictionary<string, WeaponStat> _userWeaponData = new Dictionary<string, WeaponStat>();
    #endregion

    #region Mission Data
    public int Stage = 0;
    public int Gacha = 0;
    public int LevelUp = 0;
    #endregion

    private void Awake()
    {
        //TODO 현재 아무씬에서 동작하면 awake로 knight가 생성
        GameObject go = Managers.Resource.Instantiate("Character/Knight");
        go.transform.position = new Vector3(2000f, 2000f, 0f);
        go.GetComponent<CapsuleCollider>().enabled = false;
        go.GetComponent<Stat>().enabled = false;
        //go.GetComponent<UI_HpBar>().enabled = false;

        _modelImg = go.GetComponent<Stat>().modelImg;
    }

    public void AddCharater(string _charName, Stat _charStat)
    {
        if(_userCharData.ContainsKey(_charName))
            _charStat.CharPiece += 20; //캐릭터가 중복일 때
        else
            _userCharData.Add(_charName, _charStat);
    }
    
    public void AddWeapon(string _weaponName, WeaponStat weaponStat)
    {
        if (_userWeaponData.ContainsKey(_weaponName))
        {
            //무기가 중복일 때, TODO
        }
        else
            _userWeaponData.Add(_weaponName, weaponStat);
    }

    public void ChangeModel()
    {
        Managers.UI.ShowPopupUI<ModelChange_Popup>();
    }
}

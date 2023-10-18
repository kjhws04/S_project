using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CharSelSlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    private Vector3 orginPos;
    public Image glowImage;
    Image _orgGlowImage;
    UserData _userData;

    Image charImage;
    Stat _stat;
    Stat copy;

    private void Start()
    {
        orginPos = transform.position;
        charImage = GetComponent<Image>();
        _stat = GetComponent<Stat>();
        _userData = Managers.Game.GetUserData().GetComponent<UserData>(); //딕셔너리 가져옴
    }

    void SetColor(string charName)
    {
        if (gameObject.CompareTag("charSlot"))
        {
            Color color = _userData._userCharData[charName].IsSettingUsed ? Color.gray : Color.white;
            charImage.color = color;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (_stat != null)
        {
            DragSlot._instance.dragSlot = this;
            DragSlot._instance.DragSetImage(_stat, _stat.proflieImg);
            DragSlot._instance.transform.position = eventData.position;
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (_stat != null)
            DragSlot._instance.transform.position = eventData.position;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        DragSlot._instance.SetColor(0);
        DragSlot._instance.dragSlot = null;
    }
    public void OnDrop(PointerEventData eventData)
    {
        Stat temp = DragSlot._instance.tempStat; //드레그 슬롯이 들고 있는 stat 정보

        if (_userData._userCharData.ContainsKey(temp.Name)) //검산
        {
            bool check = _userData._userCharData[temp.Name].IsSettingUsed;
            if (DragSlot._instance.dragSlot != null && !check)
                ChangeSlot();
        }
        //else 캐릭터가 이미 배틀세팅slot에 세팅되어 있음
    }

    private void ChangeSlot()
    {
        if (DragSlot._instance.dragSlot.CompareTag("charSlot") && gameObject.CompareTag("setSlot"))
        {
            Stat temp = DragSlot._instance.tempStat; //드레그 슬롯이 들고 있는 stat 정보
            copy = gameObject.GetOrAddComponent<Stat>();
            CopyFrom(copy, temp);

            _orgGlowImage = glowImage; //더하기 이미지 저장
            glowImage.sprite = copy.proflieImg; //더하기 이미지 => 캐릭터 프로필 이미지

            if (_userData._userCharData.ContainsKey(copy.Name)) //검산
            {
                _userData._userCharData[copy.Name].IsSettingUsed = true; //딕셔너리에서 IsUesd를 true로 만듬
            }

            Managers.Battle._p1UnitList.Add(_userData._userCharData[copy.Name]);
            // X버튼 활성화
        }
    }

    public void CopyFrom(Stat _copy,Stat _stat)
    {
        _copy.modelImg = _stat.modelImg;
        _copy.cardImage = _stat.cardImage;
        _copy.proflieImg = _stat.proflieImg;

        _copy.attackType = _stat.attackType;
        _copy.Level = _stat.Level;
        _copy.Name = _stat.Name;

        _copy.CurClass = _stat.CurClass;
        _copy.WeaponClass = _stat.WeaponClass;
        _copy.MainWeapon = _stat.MainWeapon;

        _copy.Hp = _stat.Hp;
        _copy.Str = _stat.Str;
        _copy.Int = _stat.Int;
        _copy.Tec = _stat.Tec;
        _copy.Spd = _stat.Spd;
        _copy.Def = _stat.Def;
        _copy.MDef = _stat.MDef;
        _copy.Luk = _stat.Luk;
        _copy.Wei = _stat.Wei;

        _copy._unitFR = _stat._unitFR;
        _copy._unitAR = _stat._unitAR;
        _copy._unitAS = _stat._unitAS;
    }
}

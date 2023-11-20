using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// <surmmary>
// charcter scene의 무기 장착 코드
// </surmmary>
public class WeaponCardSlot : MonoBehaviour, IPointerClickHandler
{
    UserData _weaponData;
    CharacterScene _charScene;
    WeaponSlot_Popup _popup;

    string _currentWeaponName;
    Image _thisImg;

    void Start()
    {
        _charScene = FindObjectOfType<CharacterScene>();
        _popup = FindObjectOfType<WeaponSlot_Popup>();
        _weaponData = Managers.Game.GetUserData().GetComponent<UserData>();
        _currentWeaponName = GetComponent<Image>().sprite.name;
        _thisImg = GetComponent<Image>();
    }

    // <surmmary>
    // 캐릭터의 무기를 장착하는 코드
    // </surmmary>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (_weaponData.CurrentChar.MainWeapon == null) //현재 아이템이 없다면,
        {
            if (_weaponData._userWeaponData[_currentWeaponName].isUsed) //이미지의 무기가 사용 중이라면, 
                return; //리턴
            //Start Code MainWeapon은 클릭한 이미지의 Dic에서 WeaponStat 적용;
            _weaponData.CurrentChar.MainWeapon = _weaponData._userWeaponData[_currentWeaponName]; //현재 캐릭터의 메인 웨폰 적용
            AddWeapon(_weaponData._userWeaponData[_currentWeaponName].isUsed);
        }
        else
        {
            if (_weaponData.CurrentChar.MainWeapon == _weaponData._userWeaponData[_currentWeaponName]) //같은 무기 클릭
            {
                _charScene.WeaponBaseReset(); //캐릭터의 무기 슬롯 리셋
                DeleteWeapon();
            }
            else //다른 무기 선택 (기존 무기 장비 해제 => 새로운 무기 장비)
            {
                if (_weaponData._userWeaponData[_currentWeaponName].isUsed) //이미지의 무기가 사용 중이라면, 
                    return; //리턴

                DeleteWeapon();
                _weaponData.CurrentChar.MainWeapon = _weaponData._userWeaponData[_currentWeaponName]; //다른 무기 적용
                AddWeapon(_weaponData._userWeaponData[_currentWeaponName].isUsed);
            }
        }
    }

    // <surmmary>
    // 무기를 장착함 (무기 귀속 : 사용중/무기 dic : 사용중/무기 장착 스텟 변화/Slot 색 변화/작용 스텟 화면에 표시)
    // </surmmary>
    private void AddWeapon(bool _isUsed) 
    {
        _weaponData.CurrentChar.MainWeapon.isUsed = !_isUsed;
        _weaponData._userWeaponData[_currentWeaponName].isUsed = !_isUsed;
        _weaponData.CurrentChar.WeaponApply(_weaponData.CurrentChar.MainWeapon, !_isUsed);
        _popup.ResetSlotColor();
        _charScene.ModelInfoChange(_weaponData.CurrentChar); //적용 스텟 표시
    }

    // <surmmary>
    // 무기를 해제함 (무기 귀속 : 비사용중/무기 dic : 비사용중/무기 장착 스텟 변화/캐릭터 메인 무기 : null/Slot 색 변화/작용 스텟 화면에 표시)
    // </surmmary>
    private void DeleteWeapon()
    {
        _weaponData.CurrentChar.MainWeapon.isUsed = false;
        _weaponData._userWeaponData[_currentWeaponName].isUsed = false;
        _weaponData.CurrentChar.WeaponApply(_weaponData.CurrentChar.MainWeapon, false);
        _weaponData.CurrentChar.MainWeapon = null;
        _popup.ResetSlotColor();
        _charScene.ModelInfoChange(_weaponData.CurrentChar);
    }

    public void SetGray()
    {
        _thisImg.color = Color.gray;
    }
}

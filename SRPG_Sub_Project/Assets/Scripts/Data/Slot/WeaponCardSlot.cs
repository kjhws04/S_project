using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// <surmmary>
// charcter scene�� ���� ���� �ڵ�
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
    // ĳ������ ���⸦ �����ϴ� �ڵ�
    // </surmmary>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (_weaponData.CurrentChar.MainWeapon == null) //���� �������� ���ٸ�,
        {
            if (_weaponData._userWeaponData[_currentWeaponName].isUsed) //�̹����� ���Ⱑ ��� ���̶��, 
                return; //����
            //Start Code MainWeapon�� Ŭ���� �̹����� Dic���� WeaponStat ����;
            _weaponData.CurrentChar.MainWeapon = _weaponData._userWeaponData[_currentWeaponName]; //���� ĳ������ ���� ���� ����
            AddWeapon(_weaponData._userWeaponData[_currentWeaponName].isUsed);
        }
        else
        {
            if (_weaponData.CurrentChar.MainWeapon == _weaponData._userWeaponData[_currentWeaponName]) //���� ���� Ŭ��
            {
                _charScene.WeaponBaseReset(); //ĳ������ ���� ���� ����
                DeleteWeapon();
            }
            else //�ٸ� ���� ���� (���� ���� ��� ���� => ���ο� ���� ���)
            {
                if (_weaponData._userWeaponData[_currentWeaponName].isUsed) //�̹����� ���Ⱑ ��� ���̶��, 
                    return; //����

                DeleteWeapon();
                _weaponData.CurrentChar.MainWeapon = _weaponData._userWeaponData[_currentWeaponName]; //�ٸ� ���� ����
                AddWeapon(_weaponData._userWeaponData[_currentWeaponName].isUsed);
            }
        }
    }

    // <surmmary>
    // ���⸦ ������ (���� �ͼ� : �����/���� dic : �����/���� ���� ���� ��ȭ/Slot �� ��ȭ/�ۿ� ���� ȭ�鿡 ǥ��)
    // </surmmary>
    private void AddWeapon(bool _isUsed) 
    {
        _weaponData.CurrentChar.MainWeapon.isUsed = !_isUsed;
        _weaponData._userWeaponData[_currentWeaponName].isUsed = !_isUsed;
        _weaponData.CurrentChar.WeaponApply(_weaponData.CurrentChar.MainWeapon, !_isUsed);
        _popup.ResetSlotColor();
        _charScene.ModelInfoChange(_weaponData.CurrentChar); //���� ���� ǥ��
    }

    // <surmmary>
    // ���⸦ ������ (���� �ͼ� : ������/���� dic : ������/���� ���� ���� ��ȭ/ĳ���� ���� ���� : null/Slot �� ��ȭ/�ۿ� ���� ȭ�鿡 ǥ��)
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

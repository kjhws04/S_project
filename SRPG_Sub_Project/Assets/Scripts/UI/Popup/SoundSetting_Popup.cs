using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// <summary>
// ���� ȭ�鿡�� ����� ��ư�� ������ �� popup
// </summary>
public class SoundSetting_Popup : UI_Popup
{
    AudioSource bgmSource;
    AudioSource sfxSource;   

    public Scrollbar bgm;
    public Scrollbar sfx;

    private void Start()
    {
        InitSource();
        InitScrollbars();
    }

    // <summary>
    // @Sound�� ã�� ������ AudioSource �Ҵ�
    // </summary>
    private void InitSource()
    {
        GameObject sound = GameObject.Find("@Sound");
        if (sound != null)
        {
            bgmSource = Util.FindChild(sound, "Bgm").GetComponent<AudioSource>();
            sfxSource = Util.FindChild(sound, "Effect").GetComponent<AudioSource>();
        }
        else
        {
            Debug.LogError("Sound GameObject not found.");
        }
    }

    // <summary>
    // popup�� ��ũ�ѹٿ� value �ʱ� ����
    // </summary>
    private void InitScrollbars()
    {
        if (bgmSource != null && bgm != null)
        {
            bgm.value = bgmSource.volume;
            bgm.onValueChanged.AddListener(BgmChanged);
        }

        if (sfxSource != null && sfx != null)
        {
            sfx.value = sfxSource.volume;
            sfx.onValueChanged.AddListener(SfxChanged);
        }
    }

    // <summary>
    // ��ũ�ѹ��� ��ȭ�� ����� ��ȭ�� ���� ������ ����
    // </summary>
    private void BgmChanged(float value)
    {
        if (bgmSource != null)
        {
            bgmSource.volume = value;
            Managers.Sound.bgmPer = value;
        }
    }
    private void SfxChanged(float value)
    {
        if (sfxSource != null)
        {
            sfxSource.volume = value;
            Managers.Sound.sfxPer = value;
        }
    }

    public void BtnExit()
    {
        Managers.UI.ClosePopupUI();
    }
}
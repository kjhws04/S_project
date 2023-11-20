using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// <summary>
// 메인 화면에서 오디오 버튼을 눌렸을 때 popup
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
    // @Sound를 찾아 변수에 AudioSource 할당
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
    // popup의 스크롤바에 value 초기 설정
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
    // 스크롤바의 변화가 생기면 변화에 따른 볼륨을 조절
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
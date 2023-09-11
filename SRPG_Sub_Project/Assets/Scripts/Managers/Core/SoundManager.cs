using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// <summary>
// AudioManager는 2D 형식의 Sound방식만 처리할 수 있다.
// 3D사운드, 또는 중간에 Effect Sound를 끊어서 처리해야 하는 경우는, 따로 AudioSource Component가 필요하다.
// </summary>
public class SoundManager
{
    AudioSource[] _audioSource = new AudioSource[(int)Define.Sound.MaxCount];

    // <summary>
    // AudioClip(Effect)처럼 자주 사용 되는 기능을 매번 로드하면, 부하가 발생하므로 Pooling방식으로 관리
    // </summary>
    Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();

    // <summary>
    // AudioSource를 들고 있는 Sound(go)를 만들고, BGM용과 EFFECT용으로 나누어 사용
    // </summary>
    public void Init()
    {
        GameObject root = GameObject.Find("@Sound");
        if (root == null)
        {
            root = new GameObject { name = "@Sound" };
            Object.DontDestroyOnLoad(root);

            string[] soundNames = System.Enum.GetNames(typeof(Define.Sound));
            for (int i = 0; i < soundNames.Length - 1; i++)
            {
                GameObject go = new GameObject { name = soundNames[i] };
                _audioSource[i] = go.AddComponent<AudioSource>();
                go.transform.parent = root.transform;
            }

            _audioSource[(int)Define.Sound.Bgm].loop = true;
        }
    }

    // <summary>
    // AudioClip을 연결하여, 상황에 맞게 재생(경로, BGM or EFFECT, 피치)
    // </summary>
    public void Play(string path, Define.Sound type = Define.Sound.Effect, float pitch =  1.0f)
    {
        if (path.Contains("Sounds/") == false)
            path = $"Souns/{path}";

        if (type == Define.Sound.Bgm)
        {
            AudioClip audioClip = Managers.Resource.Load<AudioClip>(path);
            if (audioClip == null)
            {
                Debug.Log($"AudioClip Missing! : {path}");
                return;
            }

            AudioSource audioSource = _audioSource[(int)Define.Sound.Bgm];

            if (audioSource.isPlaying)
                audioSource.Stop();

            audioSource.pitch = pitch;
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else
        {
            AudioClip audioClip = GetorAddAudioClip(path);
            if (audioClip == null)
            {
                Debug.Log($"AudioClip Missing! : {path}");
                return;
            }

            AudioSource audioSource = _audioSource[(int)Define.Sound.Effect];
            audioSource.pitch = pitch;
            audioSource.PlayOneShot(audioClip);
        }
    }

    // <summary>
    // AudioClip이 Dic에 없으면 Load하고, 있다면 재활용 (캐싱)
    // </summary>
    AudioClip GetorAddAudioClip(string path)
    {
        AudioClip audioClip = null;
        if (!_audioClips.TryGetValue(path, out audioClip))
        {
            audioClip = Managers.Resource.Load<AudioClip>(path);
            _audioClips.Add(path, audioClip);
        }
        return audioClip;
    }

    // <summary>
    // Dic이 쌓이면 메모리가 낭비 되기 때문에, 적당히 Clear로 초기화
    // </summary>
    public void Clear()
    {
        foreach (AudioSource audioSource in _audioSource)
        {
            audioSource.clip = null;
            audioSource.Stop();
        }
        _audioClips.Clear();
    }
}

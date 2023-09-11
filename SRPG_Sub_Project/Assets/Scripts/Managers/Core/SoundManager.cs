using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// <summary>
// AudioManager�� 2D ������ Sound��ĸ� ó���� �� �ִ�.
// 3D����, �Ǵ� �߰��� Effect Sound�� ��� ó���ؾ� �ϴ� ����, ���� AudioSource Component�� �ʿ��ϴ�.
// </summary>
public class SoundManager
{
    AudioSource[] _audioSource = new AudioSource[(int)Define.Sound.MaxCount];

    // <summary>
    // AudioClip(Effect)ó�� ���� ��� �Ǵ� ����� �Ź� �ε��ϸ�, ���ϰ� �߻��ϹǷ� Pooling������� ����
    // </summary>
    Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();

    // <summary>
    // AudioSource�� ��� �ִ� Sound(go)�� �����, BGM��� EFFECT������ ������ ���
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
    // AudioClip�� �����Ͽ�, ��Ȳ�� �°� ���(���, BGM or EFFECT, ��ġ)
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
    // AudioClip�� Dic�� ������ Load�ϰ�, �ִٸ� ��Ȱ�� (ĳ��)
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
    // Dic�� ���̸� �޸𸮰� ���� �Ǳ� ������, ������ Clear�� �ʱ�ȭ
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

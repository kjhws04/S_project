using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_DamageText : UI_Base
{
    public bool isBlock = false;
    public bool isMiss = false;
    public bool isCritical = false;

    enum Texts
    {
        Damage
    }

    public override void Init()
    {
        
    }

    // <summary>
    // block, miss, cri ���θ� Ȯ���ؼ� ĳ������ �Ӹ� ���� damage�� ǥ���ϴ� �Լ�
    // </summary>
    public void ShowDamageText(int _damage, bool _isBlock, bool _isMiss, bool _isCritical)
    {
        Bind<TextMeshProUGUI>(typeof(Texts));

        Transform parent = transform.parent;
        transform.position = parent.position + Vector3.up * (parent.GetComponent<CapsuleCollider>().bounds.size.y) * 2f;
        transform.rotation = Camera.main.transform.rotation;

        if (_isBlock == true)
        {
            GetTextMeshProUGUI((int)Texts.Damage).color = Color.black;
            GetTextMeshProUGUI((int)Texts.Damage).text = $"Block";
            return;
        }
        if (_isMiss == true)
        {
            GetTextMeshProUGUI((int)Texts.Damage).color = Color.gray;
            GetTextMeshProUGUI((int)Texts.Damage).text = $"Miss";
            return;
        }

        if (_isCritical == true)
        {
            GetTextMeshProUGUI((int)Texts.Damage).color = Color.yellow;
            GetTextMeshProUGUI((int)Texts.Damage).text = $"{_damage}";
        }
        else
        {
            GetTextMeshProUGUI((int)Texts.Damage).color = Color.red;
            GetTextMeshProUGUI((int)Texts.Damage).text = $"{_damage}";
        }
    }

    // <summary>
    // �ִϸ��̼� ������ destroy
    // </summary>
    public void DestroyText()
    {
        Destroy(gameObject);
        //StartCoroutine(DestroyCharacter(2f));
    }

    IEnumerator DestroyCharacter(float delay)
    {
        yield return new WaitForSeconds(delay); // ������ �ð�(2��)��ŭ ���

        Managers.Resource.Destroy(gameObject);
    }
}

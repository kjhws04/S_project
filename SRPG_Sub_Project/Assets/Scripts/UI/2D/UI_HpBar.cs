using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HpBar : UI_Base
{
    Stat _stat;

    enum GameObjects
    {
        HpBar
    }

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
        _stat = transform.parent.GetComponent<Stat>();
    }

    private void Update()
    {
        if (_stat._unitState == Stat.UnitState.death)
        {
            Destroy(gameObject);
            return;
        }

        Transform parent = transform.parent;
        transform.position = parent.position + Vector3.up * (parent.GetComponent<CapsuleCollider>().bounds.size.y) * 1.7f;
        transform.rotation = Camera.main.transform.rotation;

        float ratio = _stat.CurrentHp / (float)_stat.Hp;
        SetHpRatio(ratio);
    }

    public void SetHpRatio(float ratio)
    {
        GetObject((int)GameObjects.HpBar).GetComponent<Slider>().value = ratio;
    }
}

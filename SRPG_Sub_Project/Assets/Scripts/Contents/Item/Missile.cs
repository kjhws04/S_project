using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    private float speed;
    private Rigidbody rb;
    Stat target;
    int damage = 0;
    bool isMiss; 
    bool isBlock; 
    bool isCritical;

    private void Update()
    {
        if (target != null)
        {
            float distance = Vector3.Distance(transform.position, target.transform.position);
            if (distance < 1f)
            {
                //������ ���� �κ�
                target.SetDamage(damage, isMiss, isBlock, isCritical);
                Managers.Resource.Destroy(gameObject);
            }
        }
    }

    // <summary>
    // ���� �ð��� ������ ȭ�� �ڵ� �ı��ϴ� �ڷ�ƾ
    // </summary>
    IEnumerator DestroyMissile(float delay)
    {
        yield return new WaitForSeconds(delay); // ������ �ð���ŭ ���

        Managers.Resource.Destroy(gameObject);
    }

    // <summary>
    // �̻���, ����ü�� ���ư��� ������ ���� (�Լ��� ����)
    // </summary>
    public void TargetSetting(Stat _owner, Stat _target, int _damage, bool _isMiss, bool _isBlock, bool _isCritical)
    {
        isMiss =_isMiss; isBlock = _isBlock; isCritical = _isCritical;

        StartCoroutine(DestroyMissile(5));// 5�� �Ŀ� �̻��� �ı��� ���� �ڷ�ƾ ����

        damage = _damage;
        target = _target;
        transform.position = _owner.transform.position + new Vector3(0,0.4f,0);

        switch (_owner.WeaponClass) // �̻����� ������ ���� �ٸ� ����
        {
            case Class.WeaponClass.Bow:
                SetRotation(_owner);
                speed = 7.0f;
                break;
            case Class.WeaponClass.Pistol:
                speed = 14.0f;
                break;
            case Class.WeaponClass.Staff:
                speed = 5.0f;
                break;
        }

        rb = GetComponent<Rigidbody>();
        Vector3 dic = (target.gameObject.transform.position - transform.position).normalized + new Vector3(0, 0.1f, 0);
        rb.velocity = dic * speed; // �̻��Ͽ� ����� �ӵ� ����
    }

    // <summary>
    // ȭ��� ����, �յڰ� ���еǴ� ����ü�� ������ ��������ִ� �Լ�
    // </summary>
    void SetRotation(Stat _owner) 
    {
        Vector3 targetPos = target.transform.position;
        Vector3 t_dic = new Vector3(target.transform.position.x - _owner.transform.position.x,
                                    target.transform.position.y - _owner.transform.position.y,
                                    target.transform.position.z - _owner.transform.position.z + 1f);
        transform.up = t_dic;
    }
}

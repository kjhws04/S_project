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
                //데미지 적용 부분
                target.SetDamage(damage, isMiss, isBlock, isCritical);
                Managers.Resource.Destroy(gameObject);
            }
        }
    }

    // <summary>
    // 일정 시간이 지나면 화살 자동 파괴하는 코루틴
    // </summary>
    IEnumerator DestroyMissile(float delay)
    {
        yield return new WaitForSeconds(delay); // 지정된 시간만큼 대기

        Managers.Resource.Destroy(gameObject);
    }

    // <summary>
    // 미사일, 투사체가 날아가는 동작을 설정 (함수안 설명)
    // </summary>
    public void TargetSetting(Stat _owner, Stat _target, int _damage, bool _isMiss, bool _isBlock, bool _isCritical)
    {
        isMiss =_isMiss; isBlock = _isBlock; isCritical = _isCritical;

        StartCoroutine(DestroyMissile(5));// 5초 후에 미사일 파괴를 위한 코루틴 시작

        damage = _damage;
        target = _target;
        transform.position = _owner.transform.position + new Vector3(0,0.4f,0);

        switch (_owner.WeaponClass) // 미사일의 종류에 따라 다른 설정
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
        rb.velocity = dic * speed; // 미사일에 방향과 속도 적용
    }

    // <summary>
    // 화살과 같이, 앞뒤가 구분되는 투사체의 방향을 변경시켜주는 함수
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

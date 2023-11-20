using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Unit_zPos : MonoBehaviour
{
    // <surmmary>
    // 2d가 아닌 3d상에서 2d를 표현하기 때문에 원근감을 표시하기 위한 코드
    // tr.pos의 z값에 y값(위로 위치할 수록 멀어짐을 표현)을 곱해 원근감을 표현함
    // </surmmary>
    private void Update()
    {
        if(!Application.isPlaying)
        {
            Vector3 tPos = new Vector3((int)transform.position.x, (int)transform.position.y, 0);
            transform.position = tPos;
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y * 0.1f);
        }
    }
}

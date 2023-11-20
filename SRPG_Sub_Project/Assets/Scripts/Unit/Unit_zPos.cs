using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Unit_zPos : MonoBehaviour
{
    // <surmmary>
    // 2d�� �ƴ� 3d�󿡼� 2d�� ǥ���ϱ� ������ ���ٰ��� ǥ���ϱ� ���� �ڵ�
    // tr.pos�� z���� y��(���� ��ġ�� ���� �־����� ǥ��)�� ���� ���ٰ��� ǥ����
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

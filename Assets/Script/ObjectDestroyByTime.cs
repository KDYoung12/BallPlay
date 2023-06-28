using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroyByTime : MonoBehaviour
{
    [SerializeField]
    private float destroyTime;

    private void Awake()
    {
        // destroyTime 시간 뒤에 gameObject 삭제
        Destroy(gameObject, destroyTime);
    }
}

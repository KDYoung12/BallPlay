using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float wDelta = 0.35f;
    private float hDelta = 0.6f;

    public void Setup(int width, int height)
    {
        // 카메라 시야 설정 (전체 맵이 화면 안에 들어오도록 높이를 기준으로 맞춤)
        float size = (width > height) ? width * wDelta : height * hDelta;

        // 카메라의 시야 크기 설정
        GetComponent<Camera>().orthographicSize = size;
    }
}

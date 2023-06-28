using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class IntroSceneController : MonoBehaviour
{
    private void Awake()
    {
        // 현재 게임 윈도우가 포커스 되어 있지 않아도 게임이  실행되도록 설정
        Application.runInBackground = true;

        // 게임을 새로 시작헀을 때 항상 스테이지가 1부터 시작하도록 설정
        PlayerPrefs.SetInt("StageIndex", 0);

        // StreamingAssets 폴더 정보를 얻어온다
        DirectoryInfo directory = new DirectoryInfo(Application.streamingAssetsPath);

        // GetFiles()로 폴더에 있는 모든 파일 정보를 얻어온다
        // 맵 데이터 .json 파일과 .meta 파일이 있기 때문에 /2를 한 값이 맵 데이터 개수
        StageController.maxStageCount = directory.GetFiles().Length / 2;
    }

    private void Update()
    {
        // 아무 키나 누르면
        if (Input.anyKeyDown)
        {
            // "Stage" 씬으로 씬 변경
            SceneLoader.LoadScene("Stage");
        }
    }
}

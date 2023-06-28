using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    public static int maxStageCount; // 최대 스테이지 개수 (정적 변수. Intro씬에서 설정)

    [SerializeField]
    private Tilemap2D tilemap2D; // MapData 정보를 바탕으로 맵을 생성하기 위한 Tilemap2D
    [SerializeField]
    private PlayerController playerController; // 플레이어 Setup() 메소드 호출을 위한 PlayerController
    [SerializeField]
    private CameraController cameraController; // 카메라 시야 설정을 위한 CameraController
    [SerializeField]
    private StageUI stageUI;


    private void Awake()
    {
        // MapDataLoader 클래스 인스턴스 생성 및 메모리 할당
        MapDataLoader mapDataLoader = new MapDataLoader();

        // PlayerPrefs를 이용해 디바이스에 저장되어 있는 현재 스테이지 인덱스 불러오기
        // 저장하는 값은 0부터 시작하고, 스테이지는 1부터 시작하기 때문에 +1
        int index = PlayerPrefs.GetInt("StageIndex") + 1;

        // 인덱스가 10보다 작으면 01, 02와 같이 0을 붙여주고, 10 이상이면 그대로 사용 
        string currentStage = index < 10 ? $"Stage0{index}" : $"Stage{index}";

        // 현재 저장되어 있는 json 파일이 Stage01, Stage02.. 이기 때문에 "Stage01" 데이터를 불러온다
        MapData mapData = mapDataLoader.Load(currentStage);

        // mapData 정보를 바탕으로 타일 형태의 맵 생성
        tilemap2D.GenerateTilemap(mapData);

        // mapData.playerPosition 정보를 바탕으로 플레이어 위치 설정
        playerController.Setup(mapData.playerPosition, mapData.mapSize.y);

        // 맵의 크기 정보 (mapData.mapSize)를 바탕으로 카메라 시야 크기  설정
        cameraController.Setup(mapData.mapSize.x, mapData.mapSize.y);

        // 현재 스테이지의 정보를 UI에 출력
        stageUI.UpdateTextStage(currentStage);
    }

    public void GameClear()
    {
        // Debug.Log("Game Clear");

        // 현재 스테이지 인덱스 정보를 디바이스의 "StageIndex" 키로부터 불러온다
        int index = PlayerPrefs.GetInt("StageIndex");

        // 불러온 index 정보가 "최대 스테이지 개수 - 1 " 보다 작으면
        // 아직 다음 스테이지가 남아있기 때문에
        if(index < maxStageCount - 1)
        {
            // index를 1 증가시키고, 수정된 index 값을 디바이스에 "StageIndex" 키로 저장
            index++;
            PlayerPrefs.SetInt("StageIndex", index);

            // 현재 씬을 다시 로드
            // 씬이 다시 로드될 때 StageIndex 값이 바뀌었기 때문에 다음 스테이지의 .json 파일을 열게 된다.
            SceneLoader.LoadScene();
        }
        // 마지막 스테이지를 클리어했기 때문에
        else
        {
            // "Intro" 씬으로 돌아간다. (엔딩이 있으면 엔딩 씬을 로드하도록 수정)
            SceneLoader.LoadScene("Intro");
        }
    }
}

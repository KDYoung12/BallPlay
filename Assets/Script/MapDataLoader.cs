using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MapDataLoader : MonoBehaviour
{
    public MapData Load(string fileName)
    {
        // fileName�� ".json" ������ ������ �Է����ش�.
        // ex) "Stage01" => "Stage01.json"
        if ( fileName.Contains(".json") == false)
        {
            fileName += ".json";
        }

        // ������ ���, ���ϸ��� �ϳ��� ��ĥ �� ���
        // Application.streamingAssetsPath : ���� ����Ƽ ������Ʈ - Assets - StreamingAssets ���� ���
        fileName = Path.Combine(Application.streamingAssetsPath, fileName);

        // "filename" ���Ͽ� �ִ� ������ "dataAsJson" ������ ���ڿ� ���·� ����
        string dataAsJson = File.ReadAllText(fileName);

        // ������ȭ�� �̿��� dataAsJson ������ �ִ� ���ڿ� �����͸� MapData Ŭ���� �ν��Ͻ��� ����
        MapData mapData = new MapData();
        mapData = JsonUtility.FromJson<MapData>(dataAsJson);

        return mapData;
    }
}

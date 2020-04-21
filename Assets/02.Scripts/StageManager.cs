using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public GameObject goodPrefab;
    public GameObject badPrefab;

    public int goodCount = 30;
    public int badCount  = 30;
    public float range = 50.0f;

    public List<GameObject> itemList = new List<GameObject>();

    void Start()
    {
        MakeStage();
    }

    public void MakeStage()
    {
        //기존에 생성된 Good, Bad 전부 삭제
        foreach(var obj in itemList)
        {
            Destroy(obj);
        }

        //List 초기화
        itemList.Clear();

        //Good 생성
        for(int i=0; i<goodCount; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-range * 0.5f, range * 0.5f)
                                     , 0.05f
                                     , Random.Range(-range * 0.5f, range * 0.5f));
            Quaternion rot = Quaternion.Euler(0, Random.Range(0,360), 0);

            var obj = Instantiate<GameObject>(goodPrefab, transform.position + pos, rot, this.transform);
            itemList.Add(obj);
        }

        //Bad 생성
        for(int i=0; i<badCount; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-range * 0.5f, range * 0.5f)
                                     , 0.05f
                                     , Random.Range(-range * 0.5f, range * 0.5f));
            Quaternion rot = Quaternion.Euler(0, Random.Range(0,360), 0);

            var obj = Instantiate<GameObject>(badPrefab, transform.position + pos, rot, this.transform);
            itemList.Add(obj);            
        }
    }
}

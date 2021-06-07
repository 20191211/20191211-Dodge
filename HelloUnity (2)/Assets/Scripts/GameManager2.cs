using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager2 : MonoBehaviour
{
    public Text timeText;
    public GameObject bulletSpawnerPrefab;
    public GameObject itemPrefab;
    public GameObject level;
    int prevTime;
    int spawnCounter = 0;
    private float surviveTime;
    private bool isGamevoer;

    int prevEventTime;

    //리스트 & 제너릭을 사용해야함!
    List<GameObject> itemList = new List<GameObject>();
    List<GameObject> spawnerList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        surviveTime = 0;
        isGamevoer = false;
        prevTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isGamevoer)
        {
            surviveTime += Time.deltaTime;
            timeText.text = "Time: " + (int)surviveTime;

            int currTime = (int)(surviveTime % 5f);
            Debug.Log(prevTime + ", " + currTime);
            //5초마다 불렛스파너 추가!
            if(currTime == 0 && prevTime != currTime)
            {
                Vector3 randpossBullet = new Vector3(Random.Range(-20f, 20f), 0f, Random.Range(-8f, 8f));
                GameObject bulletSpawner = Instantiate(bulletSpawnerPrefab, randpossBullet, Quaternion.identity);
                bulletSpawner.transform.parent = level.transform;
                bulletSpawner.transform.localPosition = randpossBullet;
                spawnerList.Add(bulletSpawner);
                Vector3 randpossItem = new Vector3(Random.Range(-20f, 20f), 0f, Random.Range(-8f, 8f));
                GameObject item = Instantiate(itemPrefab, randpossItem, Quaternion.identity);
                bulletSpawner.transform.parent = level.transform;
                bulletSpawner.transform.localPosition = randpossItem;
                itemList.Add(item);
            }
            prevTime = currTime;
            int eventTime = (int)(surviveTime % 10f);
            if(eventTime == 0 && prevEventTime != eventTime)
            {
                foreach(GameObject item in itemList)
                {
                    Destroy(item);
                }
                itemList.Clear();
            }
            prevEventTime = eventTime;
        }
    }
}

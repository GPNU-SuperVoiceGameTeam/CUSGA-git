using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RebornPoint : MonoBehaviour
{
    public GameObject[] reactiveItemPrefabs;
    public Transform spawnPoint;
    private List<GameObject> currentReactiveItems = new List<GameObject>();
    public Vector2 spawnPosition = new Vector2(0, 0);
    void Start()
    {
        // 初始化时生成reactiveItems
        GenerateReactiveItems();
    }

    public void GenerateReactiveItems()
    {
        // 销毁现有的reactiveItems
        foreach (var item in currentReactiveItems)
        {
            Destroy(item);
        }
        currentReactiveItems.Clear();

        // 根据预制体生成新的reactiveItems
        foreach (var prefab in reactiveItemPrefabs)
        {
            if (prefab != null)
            {
                GameObject newItem = Instantiate(prefab, spawnPosition, Quaternion.identity);
                currentReactiveItems.Add(newItem);
            }
        }
    }

    public void OnPlayerDeath()
    {
        // 销毁当前的reactiveItems
        foreach (var item in currentReactiveItems)
        {
            Destroy(item);
        }
        currentReactiveItems.Clear();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomItem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.Object[] possibleItems = Resources.LoadAll<GameObject>("Prefabs/ItemDrops/ItemsStats");
        int rangeMax = possibleItems.Length;
        int randomNumber = UnityEngine.Random.Range(0, rangeMax);
        Object newItem = possibleItems[randomNumber];

        Instantiate(newItem, transform.position, Quaternion.identity);
        Destroy(gameObject);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

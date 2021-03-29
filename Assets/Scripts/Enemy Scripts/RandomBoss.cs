using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBoss : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.Object[] possibleBosses = Resources.LoadAll<GameObject>("Prefabs/Bosses");
        int rangeMax = possibleBosses.Length;
        int randomNumber = UnityEngine.Random.Range(0, rangeMax);
        Object newBoss = possibleBosses[randomNumber];

        GameObject g = Instantiate(newBoss, transform.position, Quaternion.identity) as GameObject;
        Destroy(gameObject);

        GetComponentInParent<RoomUnlocker>().checks.Add(g);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

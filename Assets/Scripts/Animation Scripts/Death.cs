using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
     
    }

    private void OnDestroy()
    {
        GameObject g = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/FishmanDying"), transform.position, Quaternion.identity);
        g.GetComponent<SpriteRenderer>().flipX = GetComponentInChildren<SpriteRenderer>().flipX;
        g.transform.SetParent(GetComponentInParent<RoomManager>().transform);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

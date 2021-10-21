using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReSpawn : MonoBehaviour
{
    public GameObject[] charPrefabs;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = Instantiate(charPrefabs[(int)DataMgr.instance.currentCharacter]);
        player.transform.position = transform.position;
    }
}

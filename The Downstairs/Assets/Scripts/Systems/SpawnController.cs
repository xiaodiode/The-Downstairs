using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField] private List<GameObject> spawnables;
    private bool objectSpawned;
    private GameObject spawnObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spawn(){
        spawnObject = spawnables[Random.Range(0, spawnables.Count)];
        Instantiate(spawnObject, spawnObject.transform.position, spawnObject.transform.rotation);
        

    }
}

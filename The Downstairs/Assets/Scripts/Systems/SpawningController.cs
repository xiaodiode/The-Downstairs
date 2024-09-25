using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningController : MonoBehaviour
{
    [SerializeField] private List<Transform> spawnablePositions = new();
    [SerializeField] private Match match;
    [SerializeField] private Candle candle;

    private Dictionary<Vector3, bool> occupiedPositions = new();
    public static SpawningController instance {get; private set;}

    int randomInt;
    bool full;

    // Start is called before the first frame update
    void Awake()
    {
        if(instance != null && instance != this){
            Destroy(this);
        }
        else{
            instance = this;
        }

    }
    // Start is called before the first frame update
    IEnumerator Start()
    {
        randomInt = 0;
        full = true;

        foreach(Transform transform in spawnablePositions)
        {
            occupiedPositions.Add(transform.position, false);
        }

        spawnCandleRandomly();
        yield return new WaitForSeconds(3);

        spawnMatchRandomly();
        yield return new WaitForSeconds(3);

        spawnCandleRandomly();
        yield return new WaitForSeconds(3);

        spawnMatchRandomly();
        yield return new WaitForSeconds(3);

        spawnCandleRandomly();
        yield return new WaitForSeconds(3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawnCandleRandomly()
    {
        randomInt = Random.Range(0, spawnablePositions.Count);

        StartCoroutine(spawnAtValidPosition(candle));
    }

    public void spawnMatchRandomly()
    {
        randomInt = Random.Range(0, spawnablePositions.Count);

        StartCoroutine(spawnAtValidPosition(match));
    }

    private IEnumerator spawnAtValidPosition(Candle candle)
    {
        foreach(Transform transform in spawnablePositions)
        {
            if(!occupiedPositions[transform.position])
            {
                full = false;
            }
        }

        if(!full){
            while(occupiedPositions[spawnablePositions[randomInt].position])
            {
                randomInt = Random.Range(0, spawnablePositions.Count);
                yield return null;
            }

            Instantiate(candle, spawnablePositions[randomInt].position, Quaternion.identity);
            
            occupiedPositions[spawnablePositions[randomInt].position] = true;
        }
        
        full = true;
    }

    private IEnumerator spawnAtValidPosition(Match match)
    {
        foreach(Transform transform in spawnablePositions)
        {
            if(!occupiedPositions[transform.position])
            {
                full = false;
            }
        }

        if(!full){
            while(occupiedPositions[spawnablePositions[randomInt].position])
            {
                randomInt = Random.Range(0, spawnablePositions.Count);
                yield return null;
            }

            Instantiate(match, spawnablePositions[randomInt].position, Quaternion.identity);

            occupiedPositions[spawnablePositions[randomInt].position] = true;
        }
        
        full = true;
    }

}

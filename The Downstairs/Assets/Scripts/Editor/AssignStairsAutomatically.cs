using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class AssignStairsAutomatically: MonoBehaviour
{
    // Start is called before the first frame update
    [MenuItem("Utils/AssignStairs")]
    static void AssignStairs()
    {
        GameObject doorPrefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Door.prefab", typeof(GameObject)) as GameObject;
        var doors = Object.FindObjectsOfType(typeof(Door));
        foreach (Door door in doors)
        {
            var name = door.gameObject.name;
            var color = door.GetComponent<SpriteRenderer>().color;
            var target = door.target;
            var selfScene = door.selfScene;
            var newobj = PrefabUtility.ConnectGameObjectToPrefab(door.gameObject, doorPrefab);
            var newdoor = newobj.GetComponent<Door>();
            newobj.name = name;
            newobj.GetComponent<SpriteRenderer>().color = color;
            newdoor.target = target;
            newdoor.selfScene = selfScene;
        }
    }
    
    
}

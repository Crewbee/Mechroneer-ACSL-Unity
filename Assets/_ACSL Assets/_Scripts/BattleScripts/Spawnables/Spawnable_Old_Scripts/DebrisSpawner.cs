//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//TODO: Delete this
//public class DebrisSpawner : MonoBehaviour
//{
//    //ObjectPooler objectPooler;

//    public GameObject Owner;

//    private BattleSceneManager manager;

//    private void Start()
//    {
//        //objectPooler = ObjectPooler.Instance;
//        manager = BattleSceneManager.instance;


//        //Owner = manager.Enemy;
//        transform.position = Owner.transform.position; //Update the spawner's position to the owning objects position

//    }

//    public void SpawnDebris() //makes sure the spawner is at the owner's position then gets the object pooler to spawn 50 debris prefabs at its location
//    {
//        transform.position = Owner.transform.position;

//        for (int i = 0; i < 50; i++)
//        {
//            //objectPooler.SpawnFromPool(SpawnableTypes.ST_DEBRIS, transform.position, Quaternion.identity);
//        }

//    }

//}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCreator : MonoBehaviour
{
    public GameObject BallPrefab;

//update
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            var instance = Instantiate(BallPrefab);
            instance.transform.position = Vector3.zero;
            instance.GetComponent<MoveBall>().Speed = Random.Range(0.5f, 5f);
        }
    }

}

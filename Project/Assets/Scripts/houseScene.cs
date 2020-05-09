using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class houseScene : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player")
        {
            //SceneManager.LoadScene(2);
        }


    }
}

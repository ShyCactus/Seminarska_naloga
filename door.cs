using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class door : MonoBehaviour
{
    public float time;
    bool canExit;
    public TMP_Text text;


    void Awake()
    {

        time = 0;
    }
    private void FixedUpdate()
    {
        time += Time.deltaTime;
        if (time > 45) {

            canExit = false;
            SceneManager.LoadScene(6);
            
        }
        if (time < 45)
        {

            canExit = true;
            text.text = (45 - time).ToString();

        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.name == "Player" && canExit) {

            coinkeep.coins = PickupTreasure.money;

            SceneManager.LoadScene(2);
            
        }
    }
}

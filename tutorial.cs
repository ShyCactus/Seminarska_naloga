using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class tutorial : MonoBehaviour
{
    public int stage;
    public Drive d;

    public GameObject[] arrows = new GameObject[12];
    public GameObject hole;

    public TMP_Text t1;
    public TMP_Text t2;
    public TMP_Text t3;
    public TMP_Text t4;
    public TMP_Text t5;

    private void Awake()
    {
        for (int i = 0; i < arrows.Length; i++) {

            arrows[i].SetActive(false);

        }
        t1.color = Color.red;
        t2.color = Color.red;
        t3.color = Color.red;
    }
    private void FixedUpdate()
    {
        if (stage == 1) {
            t1.color = Color.yellow;
            t4.text = "(1/3)";
            arrows[0].SetActive(true);
            if (d.WheelEquipped && (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.E))) {

                stage++;
            }
        }if (stage == 2) {
            t4.text = "(2/3)";
            t1.color = Color.green;
            t2.color = Color.yellow;

            arrows[0].SetActive(false);
            arrows[1].SetActive(true);
            arrows[2].SetActive(true);
            arrows[3].SetActive(true);
            arrows[4].SetActive(true);
            
            if(d.Cannonballs <10) 
            {

                stage++;
            }
        }
        if (stage == 3) {
            t4.text = "(3/3)";
            t2.color = Color.green;
            t3.color = Color.yellow;
            arrows[1].SetActive(false);
            arrows[2].SetActive(false);
            arrows[3].SetActive(false);
            arrows[4].SetActive(false);
            arrows[5].SetActive(true);
            arrows[6].SetActive(true);
            if (hole == null) {
                stage++;
            }

        }
        if (stage == 4) {
            t3.color = Color.green;
            for (int i = 0; i < arrows.Length; i++)
            {

                arrows[i].SetActive(false);
            }
            t5.gameObject.SetActive(true);
            if (Input.GetKey(KeyCode.E)) {
                SceneManager.LoadScene(3);
            
            }
        }
    }
    
}

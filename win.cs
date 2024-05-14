using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class win : MonoBehaviour
{
    public Slider s;
    public TMP_Text t;
    public TMP_Text t2;

    public GameObject i1;
    public GameObject i2;
    public GameObject i3;
    public GameObject i4;



    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        if (coinkeep.totalcoins == 0) {

            coinkeep.totalcoins = 1;
        }
        s.maxValue = coinkeep.totalcoins;
        s.value = coinkeep.coins;
        t.text = "You kept" + " " + ((coinkeep.coins / coinkeep.totalcoins)*100).ToString() + "% (" + coinkeep.coins.ToString() +")";


        if ((coinkeep.coins / coinkeep.totalcoins) > .95f)
        {

            t2.text = "Your rank is: Master of the Sea";
            i1.SetActive(true);
        }
        else if ((coinkeep.coins / coinkeep.totalcoins) > .7)
        {

            t2.text = "Your rank is: Proffesion Pirate";
            i2.SetActive(true);
        }
        else if ((coinkeep.coins / coinkeep.totalcoins) > .5f)
        {

            t2.text = "Your rank is: Solid stealer ";
            i3.SetActive(true);
        }
        else {
            t2.text = "Your rank is: Survivor ";
            i4.SetActive(true);
        }
       // coinkeep.coins = 0;
       // coinkeep.totalcoins = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

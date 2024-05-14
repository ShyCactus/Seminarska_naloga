using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tips : MonoBehaviour
{
    public GameObject Tipsobj;
    public GameObject l1;
    public GameObject l2;
    public GameObject l3;
    public Drive dr;
    private void FixedUpdate()
    {



        if (dr.level == 1)
        {
            l1.SetActive(true);
            l2.SetActive(false);
            l3.SetActive(false);

        }
        if (dr.level == 2)
        {
            l1.SetActive(false);
            l2.SetActive(true);
            l3.SetActive(false);

        }
        if (dr.level == 3)
        {
            l1.SetActive(false);
            l2.SetActive(false);
            l3.SetActive(true);

        }
    }

    public void Hide() {

        gameObject.SetActive(false);
    }
    public void Show()
    {
        gameObject.SetActive(true);

    }
}

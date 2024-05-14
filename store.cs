using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class store : MonoBehaviour
{

    public Drive dr;
    public moveBoat mb;
    public Swim swm;
    public PickupTreasure pt;
    public TMP_Text mny;
    
    private void FixedUpdate()
    {
        mny.text = PickupTreasure.money.ToString();
        Cursor.lockState = CursorLockMode.None;
    }
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
    }
    
    public void Cannon()
    {

        if(PickupTreasure.money >= 100)
        {
            dr.Cannonballs += 5;
            PickupTreasure.money -= 100;
            coinkeep.coins -= 100;
        }
        

    }
    public void Planks()
    {

        
        if (PickupTreasure.money >= 150)
        {
            dr.planks += 3;
            PickupTreasure.money -= 150;
            coinkeep.coins -= 150;
        }

    }
    public void Revolverbuff() {
        if (PickupTreasure.money >= 150)
        {
            dr.revolverDamage += 5;
            PickupTreasure.money -= 150;
            coinkeep.coins -= 150;
        }


    }
    public void Sniperbuff()
    {
        if (PickupTreasure.money >= 150)
        {
            dr.SniperDamage += 10;
            PickupTreasure.money -= 150;
            coinkeep.coins -= 150;
        }
        

    }
    public void Exit() {
        Cursor.lockState = CursorLockMode.Locked;
        this.gameObject.SetActive(false);

    }
    public void Swordbuff()
    {
        if (PickupTreasure.money >= 150)
        {
            dr.swordDamage += 10;
            PickupTreasure.money -= 150;
            coinkeep.coins -= 150;
        }
        

    }
    public void Heals()
    {
        if (PickupTreasure.money >= 150)
        {
            dr.healsAmount += 5;
            PickupTreasure.money -= 150;
            coinkeep.coins -= 150;
        }
        

    }
    public void Blessing()
    {
        if (PickupTreasure.money >= 250)
        {
            mb.pba += 1;
            PickupTreasure.money -= 250;
            coinkeep.coins -= 250;
        }
       

    }
    public void SwimmSpeed ()
    {
        
        if (PickupTreasure.money >= 350)
        {
            swm.swimspeed = 10;
            PickupTreasure.money -= 350;
            coinkeep.coins -= 350;
        }

    }
}

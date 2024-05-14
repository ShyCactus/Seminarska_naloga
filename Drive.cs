using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.Rendering;

public class Drive : MonoBehaviour
{
    public bool WheelEquipped;
    public Transform wheel;
    public float wheelrot;
    public Transform boat;
    public bool SpeedControl;
    public Transform psk;
    public Vector3 prevpskpos = new Vector3(0, 0, 0);
    public float lerpSpeed;
    float startlerpSpeed;
    public float maxSpeedBoat;
    public moveBoat mb;
    public bool onBoat;
    public float stairSpeed;
    public bool swimming;
    public float wheelRotReset;
    public Transform stick;
    public float canstick;
    public Transform bullethole;
    public Transform camera;


    public Transform Revolver;
    public Transform Sword;
    public Transform Sniper;
    public Transform Heal;
    public int healsAmount = 5;

    public float HealthAmount = 100;
    public Slider HealthBar;
    public Transform[] WeaponSlots = new Transform[16];
    public int WeaponSlot;
    public turtle _turtle;
    public GameObject SwordHit;
    public float RevolverFireRate;
    private float SRevolverFireRate;

    public float SniperrFireRate;
    private float SSniperFireRate;


    public float SwordSwingRate;
    private float SSwordSwingRate;
    public GameObject CurrentHole;
    public float planks = 25;

    public int RevolverBullets;
    public int SRevolverBullets;
    public float RevolverReloadBullets;
    public float SRevolverReloadBullets;

    public TMP_Text ItemText;
    public Slider ItemTimer;
    public TMP_Text bullets;


    public Transform currentcanon;
    public bool usingcanon;
    public Transform cball;
    public float timetillnextcballshoot = 3;



    public float Cannonballs;
    public bool[] canonReloaded = new bool[4];
    float abivonurability;
    public int revolverDamage = 10;
    public int SniperDamage = 50;
    public int swordDamage = 40;



    public Image rvimg;
    public Image swimg;
    public Image snpimg;
    public Image currimg;
    public Transform rvlvr;
    public Transform swrd;
    public Transform snpr;
    bool msu;
    bool msd;
    public ParticleSystem rps;
    public ParticleSystem sps;


    public TMP_Text dmg;

    Vector3 swordstart;


    public Volume volume;



    public AudioSource aus;
    public AudioClip shootrevolveraudio;
    public AudioClip shootsniperaudio;
    public AudioClip swingSwordAudio;
    public AudioClip Cannonaud;
   


    public Transform waterinshipexcl;
    public Transform waterinship;


    public TMP_Text cballstxt;
    public TMP_Text plankstxt;
    public TMP_Text waterinshiptxt;
    public TMP_Text foodText;


    public GameObject Turtle;
    public GameObject Aboleth;
    public GameObject RocksEnemy;
    public GameObject Skelly;
    public GameObject TrutleHealthbar;
    public GameObject AbolethHealthbar;
    public GameObject EnemyShip;
    public Transform playerSpawn;
    public int level = 1;
    public static bool canEnterNextLevel;
    public Transform StoreUi;
    public TMP_Text instructions;

  
    public KeepLevels kl;

    public GameObject enterleveltext;
    private void Awake()
    {
        if(kl!= null)
        if (KeepLevels.level != 1) {
            level = KeepLevels.level-1;
        
        }
    }
    void Start()
    {

        psk.transform.parent = boat;
        StartCoroutine("disableDrag");
        mb = boat.GetComponent<moveBoat>();
        startlerpSpeed = lerpSpeed;
        SRevolverFireRate = RevolverFireRate;
        SSniperFireRate = SniperrFireRate;
        SSwordSwingRate = SwordSwingRate;
        SRevolverBullets = RevolverBullets;
        SRevolverReloadBullets = RevolverReloadBullets;
        swordstart = swrd.localEulerAngles;
        Levelstart();
    }

    public  void Levelstart()
    {
        
        moveBoat.waterCanChange = true;
        boat.position = new Vector3(31.8149f, 1.5f, -294.9302f);
        boat.eulerAngles = new Vector3(0, 0, 0);
        transform.position = playerSpawn.position;
        transform.eulerAngles = new Vector3(0, 0, 0);
        if (level == 1) {
            Turtle.SetActive(true);
            TrutleHealthbar.SetActive(true);
            mb.speed = 30;
        }
        if (level == 2)
        {
            
            TrutleHealthbar.SetActive(false);
           Aboleth.SetActive(true);
            AbolethHealthbar.SetActive(true);
            mb.speed = 30;
        }if (level == 3) {

            
            AbolethHealthbar.SetActive(false);
            RocksEnemy.SetActive(true);
            Skelly.SetActive(true);
            mb.speed = 275;
        }if (level == 4) {
            
            mb.speed = 225;
        }

        canEnterNextLevel = false;
        level++;


    }
    



    
    // Update is called once per frame
    void FixedUpdate()
    {
        if (canEnterNextLevel)
        {
            enterleveltext.SetActive(true);

        }
        else
        {
            enterleveltext.SetActive(false);
        }

        if (level >2) {
            if (Turtle != null) {
                Turtle.SetActive(false);
                TrutleHealthbar.SetActive(false);
            }
        }
        if (level > 3)
        {
            if (Aboleth != null)
            {
                Aboleth.SetActive(false);
                AbolethHealthbar.SetActive(false);
            }
        }
        foodText.text = healsAmount.ToString();
        cballstxt.text = Cannonballs.ToString();
        plankstxt.text = planks.ToString();
        waterinshiptxt.text = Mathf.Round(mb.waterAmount).ToString() + "%"; 

        abivonurability -= Time.deltaTime;
        ItemText.text = WeaponSlots[WeaponSlot].name;
        if ((psk.position != prevpskpos) && (prevpskpos != new Vector3 (0,0,0))) {
            this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(this.transform.position.x + (psk.position.x - prevpskpos.x), this.transform.position.y, this.transform.position.z + (psk.position.z - prevpskpos.z)), lerpSpeed * Time.deltaTime);
        
        
        }
        if (WheelEquipped) {
            if (Input.GetKey(KeyCode.Q) && wheelrot > -720) {

                wheel.Rotate(new Vector3(0, 0, 8), Space.Self);
                wheelrot -= 8;
            }
            if (Input.GetKey(KeyCode.E) && wheelrot < 720)
            {

                wheel.Rotate(new Vector3(0, 0, -8), Space.Self);
                wheelrot += 8;
            }
        }
        else{
            if (wheelrot > 0) {
                wheelrot -= wheelRotReset;
                wheel.Rotate(new Vector3(0, 0, wheelRotReset), Space.Self);

            }
            if (wheelrot < 0) {
                wheelrot += wheelRotReset;
                wheel.Rotate(new Vector3(0, 0, -wheelRotReset), Space.Self);

            }
        }
        if (SpeedControl) {

            if (mb.speed < maxSpeedBoat ) {

                if (Input.GetKey(KeyCode.E)) {

                    mb.speed += 2.5f;
                }
                
            }
            if (Input.GetKey(KeyCode.Q) && mb.speed > 0)
            {

                mb.speed -= 2.5f;
            }
        }
        prevpskpos = psk.position;
        /*
        RaycastHit air;
        if (Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.up), out air, (transform.localScale.y /4) * 3)) {

            canstick -= Time.deltaTime;
            
        }
        else
        {
            canstick = 1;
            Debug.Log("air");
        }
        if (Input.GetKey(KeyCode.Space)) {

            canstick = 1;
        }
        */
            if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)/* && !Input.GetKey(KeyCode.Space) && canstick < 0*/|| usingcanon) {
            
            
           
            if ((!swimming) && stick.parent == null)
            {
                
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.up), out hit, Mathf.Infinity))
                {
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                    //Debug.Log(hit.transform.name);
                    if (hit.transform.tag == "boatFloor")
                    {
                        stick.parent = boat;
                    }
                    hit.point = stick.position;
                    stick.position = stick.position - -new Vector3(0, transform.localScale.y / 10, 0);
                    lerpSpeed = 0;


                }
            }
            else if (!swimming)
            {
                transform.position = new Vector3(stick.position.x, transform.position.y,stick.position.z);
                lerpSpeed = 0;
                

            }
            
        
        }
        else
        {
            stick.position = transform.position;
            stick.parent = null;
            lerpSpeed = startlerpSpeed;
            
        }






        //items
        
        
       
        if (WeaponSlots[WeaponSlot] == Revolver) {
            currimg.sprite = rvimg.sprite;
            rvlvr.gameObject.SetActive(true);
            swrd.gameObject.SetActive(false);
            snpr.gameObject.SetActive(false);
            ItemTimer.maxValue = SRevolverFireRate;
            if (RevolverBullets > 0)
                ItemTimer.value = RevolverFireRate;
            else {
                ItemTimer.value = 0;
            
            }
        }
        if (WeaponSlots[WeaponSlot] == Revolver)
        {
            RevolverFireRate -= Time.deltaTime;
            ItemTimer.maxValue = SRevolverFireRate;
            ItemTimer.value = RevolverFireRate;
            bullets.text = RevolverBullets.ToString();
        }
        if (WeaponSlots[WeaponSlot] == Sniper)
        {
            currimg.sprite = snpimg.sprite;
            rvlvr.gameObject.SetActive(false);
            swrd.gameObject.SetActive(false);
            snpr.gameObject.SetActive(true);
            SniperrFireRate -= Time.deltaTime;
            ItemTimer.maxValue = SSniperFireRate;
            ItemTimer.value = SniperrFireRate;
            if (SniperrFireRate < 0)
            {

                bullets.text = "1";
            }
            else {
                bullets.text = "0";
            }
        }
        if (WeaponSlots[WeaponSlot] == Sword)
        {
            currimg.sprite = swimg.sprite;
            rvlvr.gameObject.SetActive(false);
            swrd.gameObject.SetActive(true);
            snpr.gameObject.SetActive(false);
            SwordSwingRate -= Time.deltaTime;
            ItemTimer.maxValue = SSwordSwingRate;
            ItemTimer.value = SwordSwingRate;
            bullets.text = "";
        }
        if (WeaponSlots[WeaponSlot] == Revolver & RevolverBullets < 1)
        {
            RevolverReloadBullets -= Time.deltaTime;
            if (RevolverReloadBullets < 0)
            {
                RevolverReloadBullets = SRevolverReloadBullets;
                RevolverBullets = SRevolverBullets;
            }
        }
        if (Input.GetMouseButton(0) && !usingcanon)
        {
            
            if (WeaponSlots[WeaponSlot] == Revolver && RevolverFireRate < 0 && RevolverBullets >0)
            {
                aus.clip = shootrevolveraudio;
                aus.Play();
                
                rps.Play();
                RevolverBullets-=1;
                RaycastHit shoot;
                RevolverFireRate = SRevolverFireRate;
                if (Physics.Raycast(camera.position, camera.forward, out shoot, Mathf.Infinity))
                {

                    //GameObject bh = GameObject.Instantiate(bullethole.gameObject, shoot.point, transform.rotation);

                    if (shoot.transform.tag == "Enemy") {

                        _turtle.TakeDamage(revolverDamage);
                        dmg.text = revolverDamage.ToString();
                        StartCoroutine("disabledmgtext");
                    }
                    if (shoot.transform.tag == "AbEnemy")
                    {
                        aboleth abol = shoot.transform.GetComponent<aboleth>();
                        abol.health -= revolverDamage;
                        dmg.text = revolverDamage.ToString();
                        StartCoroutine("disabledmgtext");
                    }
                    if (shoot.transform.tag == "Skely")
                    {

                        Skelleton abol = shoot.transform.GetComponent<Skelleton>();
                        if (abol.health > 0 && abol.canTakedamage) {

                            abol.health -= revolverDamage;
                            dmg.text = revolverDamage.ToString();
                            StartCoroutine("disabledmgtext");

                        }
                        
                    }
                    if (shoot.transform.tag == "AbmEnemy")
                    {
                        aboleth_mb abol = shoot.transform.GetComponent<aboleth_mb>();
                        abol.health -= revolverDamage;
                        dmg.text = revolverDamage.ToString();
                        StartCoroutine("disabledmgtext");
                    }

                }
            }
            
            if (WeaponSlots[WeaponSlot] == Sniper && SniperrFireRate < 0)
            {
                aus.clip = shootsniperaudio;
                aus.Play();
                
                sps.Play();
                RaycastHit shoot;
                SniperrFireRate = SSniperFireRate;
                if (Physics.Raycast(camera.position, camera.forward, out shoot, Mathf.Infinity))
                {

                    //GameObject bh = GameObject.Instantiate(bullethole.gameObject, shoot.point, transform.rotation);

                    if (shoot.transform.tag == "Enemy")
                    {

                        _turtle.TakeDamage(SniperDamage);
                        dmg.text = SniperDamage.ToString();
                        StartCoroutine("disabledmgtext");
                    }
                    if (shoot.transform.tag == "AbEnemy")
                    {
                        aboleth abol = shoot.transform.GetComponent<aboleth>();
                        abol.health -= SniperDamage;
                        dmg.text = SniperDamage.ToString();
                        StartCoroutine("disabledmgtext");
                    }
                    if (shoot.transform.tag == "Skely")
                    {
                        Skelleton abol = shoot.transform.GetComponent<Skelleton>();
                        if (abol.health > 0 && abol.canTakedamage) {

                            abol.health -= SniperDamage;
                            dmg.text = SniperDamage.ToString();
                            StartCoroutine("disabledmgtext");
                        }
                        
                    }
                    if (shoot.transform.tag == "AbmEnemy")
                    {
                        aboleth_mb abol = shoot.transform.GetComponent<aboleth_mb>();
                        abol.health -= SniperDamage;
                        dmg.text = SniperDamage.ToString();
                        StartCoroutine("disabledmgtext");
                    }

                }


            }
            if (WeaponSlots[WeaponSlot] == Sword && SwordSwingRate < 0)
            {
                StartCoroutine("SwordSwingAnim");
                aus.clip = swingSwordAudio;
                aus.Play();
                

                SwordSwingRate = SSwordSwingRate;
                RaycastHit shoot;
                
                if (Physics.Raycast(camera.position, camera.forward, out shoot, 3))
                {

                    //GameObject bh = GameObject.Instantiate(bullethole.gameObject, shoot.point, transform.rotation);

                    if (shoot.transform.tag == "Enemy")
                    {
                        dmg.text = swordDamage.ToString();
                        _turtle.TakeDamage(swordDamage);
                        StartCoroutine("disabledmgtext");
                    }
                    if (shoot.transform.tag == "AbEnemy")
                    {
                        dmg.text = swordDamage.ToString();
                        aboleth abol = shoot.transform.GetComponent<aboleth>();
                        abol.health -= swordDamage;
                        StartCoroutine("disabledmgtext");
                    }
                    if (shoot.transform.tag == "Skely")
                    {
                        
                        Skelleton abol = shoot.transform.GetComponent<Skelleton>();
                        if (abol.health > 0 && abol.canTakedamage) {
                            dmg.text = swordDamage.ToString();
                            abol.health -= swordDamage;
                            StartCoroutine("disabledmgtext");
                            abol.health -= swordDamage;
                        }
                        
                        
                    }
                    if (shoot.transform.tag == "AbmEnemy")
                    {
                        dmg.text = swordDamage.ToString();
                        aboleth_mb abol = shoot.transform.GetComponent<aboleth_mb>();
                        abol.health -= swordDamage;
                        StartCoroutine("disabledmgtext");
                    }
                } else if (Physics.Raycast(transform.position, transform.forward, out shoot, 3)) {

                    //GameObject bh = GameObject.Instantiate(bullethole.gameObject, shoot.point, transform.rotation);

                    if (shoot.transform.tag == "Enemy")
                    {
                        dmg.text = swordDamage.ToString();
                        _turtle.TakeDamage(swordDamage);
                        StartCoroutine("disabledmgtext");
                    }
                    if (shoot.transform.tag == "AbEnemy")
                    {
                        dmg.text = swordDamage.ToString();
                        aboleth abol = shoot.transform.GetComponent<aboleth>();
                        abol.health -= swordDamage;
                        StartCoroutine("disabledmgtext");
                    }
                    if (shoot.transform.tag == "Skely")
                    {
                        
                        Skelleton abol = shoot.transform.GetComponent<Skelleton>();
                        
                        if (abol.health > 0 && abol.canTakedamage) {
                            dmg.text = swordDamage.ToString();
                            abol.health -= swordDamage;
                            StartCoroutine("disabledmgtext");
                        }
                    }
                    if (shoot.transform.tag == "AbmEnemy")
                    {
                        dmg.text = swordDamage.ToString();
                        aboleth_mb abol = shoot.transform.GetComponent<aboleth_mb>();
                        abol.health -= swordDamage;
                        StartCoroutine("disabledmgtext");
                    }
                }



            }
        }

        if (Input.GetMouseButton(1)){

            if (WeaponSlots[WeaponSlot] == Sniper)
            {
                Zoom z = transform.GetComponentInChildren<Zoom>();
                z.currentZoom = 1;
                if (volume.profile.TryGet(out Vignette vignette)) // for e.g set vignette intensity to .4f
                {
                    vignette.intensity.value = .758f;
                }

            }
            else {
                Zoom z = transform.GetComponentInChildren<Zoom>();
                z.currentZoom = 0;
                if (volume.profile.TryGet(out Vignette vignette)) // for e.g set vignette intensity to .4f
                {
                    vignette.intensity.value = 0;
                }

            }


        }
        else {
            Zoom z = transform.GetComponentInChildren<Zoom>();
            z.currentZoom = 0;
            if (volume.profile.TryGet(out Vignette vignette)) // for e.g set vignette intensity to .4f
            {
                vignette.intensity.value = 0;
            }
        }
        if (Input.GetKey(KeyCode.Alpha1)) {
            WeaponSlot = 0;
            ResetWeaponTimers();


        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            WeaponSlot = 1;
            ResetWeaponTimers();

        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            WeaponSlot = 2;
            ResetWeaponTimers();

        }
        if (Input.GetKey(KeyCode.Alpha4))
        {
            WeaponSlot = 3;
            ResetWeaponTimers();

        }

        if (msd)
        {
            swrd.localEulerAngles = new Vector3(swrd.localEulerAngles.x + 100 * Time.deltaTime, swrd.localEulerAngles.y, swrd.localEulerAngles.z);

        }
        else if (msu)
        {
            swrd.localEulerAngles = new Vector3(swrd.localEulerAngles.x - 100 * Time.deltaTime, swrd.localEulerAngles.y, swrd.localEulerAngles.z);

        }
        else {
            swrd.localEulerAngles = swordstart;
        }



        //woodfix

        if (CurrentHole != null) {
            instructions.text = "Press F to fix the hole";
        }

        if (CurrentHole != null && Input.GetKeyDown(KeyCode.F) && planks > 0) {
            instructions.text = "";
            mb.holecount--;
            Destroy(CurrentHole.gameObject);
            planks-=1;
        }




        // canons
        if (currentcanon == null && instructions.text == "Press F to use the cannon") {
            instructions.text = "";
        }
        if (currentcanon != null && !WheelEquipped) {
            instructions.text = "Press F to use the cannon";
            currentcanon.localEulerAngles = new Vector3(currentcanon.localEulerAngles.x, currentcanon.localEulerAngles.y, currentcanon.localEulerAngles.z);
            if (Input.GetKey(KeyCode.F) && !usingcanon)
            {
                usingcanon = true;
                timetillnextcballshoot = 3;
                

            }
            if (Input.GetKey(KeyCode.G)) {
                usingcanon = false;
                FirstPersonMovement.canMove = true;
                dmg.text = "";
                instructions.text = "";
            }
            if (usingcanon) {
                instructions.text = "Aim with WASD, exit cannon with G";
                FirstPersonMovement.canMove = false;
                
                Debug.Log(currentcanon.localEulerAngles.x + "" +currentcanon.localEulerAngles.y);



                if (currentcanon.name == "rig_cannon_01" && Cannonballs >0)
                {
                    if (canonReloaded[0] == false) {
                        timetillnextcballshoot -= Time.deltaTime;
                        dmg.text = "reloading";
                        
                    }
                }
                if (currentcanon.name == "rig_cannon_02" && Cannonballs > 0)
                {
                    if (canonReloaded[1] == false)
                    {
                        timetillnextcballshoot -= Time.deltaTime;
                        dmg.text = "reloading";
                    }
                }
                if (currentcanon.name == "rig_cannon_03" && Cannonballs > 0)
                {
                    if (canonReloaded[2] == false)
                    {
                        timetillnextcballshoot -= Time.deltaTime;
                        dmg.text = "reloading";
                    }
                }
                if (currentcanon.name == "rig_cannon_04" && Cannonballs > 0)
                {
                    if (canonReloaded[3] == false)
                    {
                        timetillnextcballshoot -= Time.deltaTime;
                        dmg.text = "reloading";
                    }
                }
                if (timetillnextcballshoot < 0) {
                    dmg.text = "";
                    timetillnextcballshoot = 3;
                    Cannonballs -= 1;
                    if (currentcanon.name == "rig_cannon_01")
                    {
                        canonReloaded[0] = true;
                    }
                    if (currentcanon.name == "rig_cannon_02")
                    {
                        canonReloaded[1] = true;
                    }
                    if (currentcanon.name == "rig_cannon_03")
                    {
                        canonReloaded[2] = true;
                    }
                    if (currentcanon.name == "rig_cannon_04")
                    {
                        canonReloaded[3] = true;
                    }

                }




                if (Input.GetKey(KeyCode.A)) {
                    
                  if(currentcanon.localEulerAngles.y > 65)
                    {
                        
                        currentcanon.localEulerAngles = new Vector3(currentcanon.localEulerAngles.x, currentcanon.localEulerAngles.y - .2f, currentcanon.localEulerAngles.z);

                    }
                    
                
                }
                if (Input.GetKey(KeyCode.D))
                {

                    if (currentcanon.localEulerAngles.y < 115)
                    {

                        currentcanon.localEulerAngles = new Vector3(currentcanon.localEulerAngles.x, currentcanon.localEulerAngles.y + .2f, currentcanon.localEulerAngles.z);

                    }


                }
                if (Input.GetKey(KeyCode.W) && currentcanon.localEulerAngles.x<298) {


                    currentcanon.localEulerAngles = new Vector3(currentcanon.localEulerAngles.x+.5f, currentcanon.localEulerAngles.y , currentcanon.localEulerAngles.z);

                }
                if (Input.GetKey(KeyCode.S) && currentcanon.localEulerAngles.x > 272)
                {

                    currentcanon.localEulerAngles = new Vector3(currentcanon.localEulerAngles.x -.5f, currentcanon.localEulerAngles.y, currentcanon.localEulerAngles.z);
                }

                if (Input.GetMouseButton(0)) {

                    
                    if (currentcanon.name == "rig_cannon_01" && canonReloaded[0]) {
                        aus.clip = Cannonaud;
                        aus.Play();
                        Transform sp = currentcanon.GetChild(0);
                        GameObject _cb = GameObject.Instantiate(cball.gameObject, sp.position, sp.rotation);
                        Rigidbody cbr = _cb.GetComponent<Rigidbody>();
                        cbr.AddForce(_cb.transform.up * 200000);
                        timetillnextcballshoot = 3;
                        canonReloaded[0] = false;
                        
                    }
                    if (currentcanon.name == "rig_cannon_02" && canonReloaded[1])
                    {
                        aus.clip = Cannonaud;
                        aus.Play();
                        Transform sp = currentcanon.GetChild(0);
                        GameObject _cb = GameObject.Instantiate(cball.gameObject, sp.position, sp.rotation);
                        Rigidbody cbr = _cb.GetComponent<Rigidbody>();
                        cbr.AddForce(_cb.transform.up * 200000);
                        timetillnextcballshoot = 3;
                        canonReloaded[1] = false;
                        
                    }
                    if (currentcanon.name == "rig_cannon_03" && canonReloaded[2])
                    {
                        aus.clip = Cannonaud;
                        aus.Play();
                        Transform sp = currentcanon.GetChild(0);
                        GameObject _cb = GameObject.Instantiate(cball.gameObject, sp.position, sp.rotation);
                        Rigidbody cbr = _cb.GetComponent<Rigidbody>();
                        cbr.AddForce(_cb.transform.up * 200000);
                        timetillnextcballshoot = 3;
                        canonReloaded[2] = false;
                        
                    }
                    if (currentcanon.name == "rig_cannon_04" && canonReloaded[3])
                    {
                        aus.clip = Cannonaud;
                        aus.Play();
                        Transform sp = currentcanon.GetChild(0);
                        GameObject _cb = GameObject.Instantiate(cball.gameObject, sp.position, sp.rotation);
                        Rigidbody cbr = _cb.GetComponent<Rigidbody>();
                        cbr.AddForce(_cb.transform.up * 200000);
                        timetillnextcballshoot = 3;
                        canonReloaded[3] = false;
                       
                    }

                }


            }
        
        }


        if (Input.GetKeyDown(KeyCode.G) && !usingcanon && HealthAmount < 100) {

            if (healsAmount > 0) {
                healsAmount -= 1;
                HealthAmount += 20;
                if (HealthAmount > 100) HealthAmount = 100;
                HealthBar.value = HealthAmount;
            }
        
        }
        if ( stick.parent == boat || !swimming || transform.position.y > -.5f)
        {
            waterinship.gameObject.active = true;
            waterinshipexcl.gameObject.active = true;
            
        }
        else {
            waterinship.gameObject.active = false;
            waterinshipexcl.gameObject.active = false;
        }
        if (Input.GetKey(KeyCode.G)) {

            usingcanon = false;
            FirstPersonMovement.canMove = true;
            dmg.text = "";
        }




        if (canEnterNextLevel && Input.GetKey(KeyCode.E)) {


            StoreUi.gameObject.SetActive(true);
            moveBoat.waterCanChange = false;
            
        }
    }
    
    public void Canonballtxt() {

        dmg.text = 150.ToString();
        StartCoroutine("disabledmgtext");
    }
    public void ResetWeaponTimers()
    {
        if (SniperrFireRate > 0)
        {
            SniperrFireRate = SSniperFireRate;
        }
        else {
           // SniperrFireRate = SSniperFireRate / 3;
        
        }
        if(SwordSwingRate <SSwordSwingRate / 2)
        {

            SwordSwingRate = SSwordSwingRate / 2;

        }
        
        RevolverFireRate = SRevolverFireRate;


    }
    IEnumerator SwordSwingAnim() {
        msd = true;
        msu = false;
        yield return new WaitForSeconds(.35f);
        msd = false;
        msu = true;
        yield return new WaitForSeconds(.35f);
        msu = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "wheel") { WheelEquipped = true; 
            wheel = other.transform;
            instructions.text = "Steer the ship with Q and E";
        }
        if (other.tag == "BoatSpeed")
        {
            SpeedControl = true;
        }

        if (other.tag == "Stairs")
        {
            Transform stairtop = other.transform.GetChild(0);
            transform.position = stairtop.position;
            
        }
        if (other.tag == "Sea")
        {

            swimming = true;
        }
        if (other.tag == "Hole") {

            CurrentHole = other.gameObject;
        }
        if (other.tag == "Canon") {

            currentcanon = other.transform;
        }
        if (other.tag == "AbEnemy" && abivonurability < 0) {
            TakeDamage(10);
            abivonurability = .8f;
            Rigidbody ab = other.transform.GetComponent<Rigidbody>();
            ab.AddForce(-other.transform.forward * 3, ForceMode.Impulse);
            
        }
        if (other.tag == "Skely")
        {
            Skelleton sk = other.transform.GetComponent<Skelleton>();
            if (sk.canDamage) {
                TakeDamage(30);
                abivonurability = .8f;
                Rigidbody ab = other.transform.GetComponent<Rigidbody>();
                ab.AddForce(-other.transform.forward * 3, ForceMode.Impulse);
                sk.canDamage = false;
            }
            

        }


    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "wheel") { WheelEquipped = false; instructions.text = ""; }
        if (other.tag == "BoatSpeed")
        {
            SpeedControl = false;
        }
        if (other.tag == "Sea")
        {

            swimming = false;
        }
        if (other.tag == "Hole")
        {
            instructions.text = "";
            CurrentHole =null;
        }
        if (other.tag == "Canon" && currentcanon == other.transform)
        {
            
            currentcanon = null;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Stairs")
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + stairSpeed * Time.fixedDeltaTime, transform.position.z);
        }
        if (other.tag == "stairboost")
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 1 * Time.fixedDeltaTime, transform.position.z);
        }
        if (other.tag == "Sea") {

            swimming = true;
        }
        if (other.tag == "AbEnemy" && abivonurability < 0)
        {
            TakeDamage(15);
            abivonurability = .8f;
            Rigidbody ab = other.transform.GetComponent<Rigidbody>();
            ab.AddForce(-other.transform.forward * 3, ForceMode.Impulse);

        }
        if (other.tag == "Skely")
        {
            Skelleton sk = other.transform.GetComponent<Skelleton>();
            if (sk.canDamage)
            {
                TakeDamage(30);
                abivonurability = .8f;
                Rigidbody ab = other.transform.GetComponent<Rigidbody>();
                ab.AddForce(-other.transform.forward * 3, ForceMode.Impulse);
                sk.canDamage = false;
            }


        }

    }
    
    IEnumerator disabledmgtext() {
        yield return new WaitForSeconds(.1f);
        dmg.text = "";
    
    
    
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "boatFloor")
        {
            psk.transform.parent = boat;
            onBoat = true;
        }
    }
 
    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "boatFloor")
        {
            if (collision.transform.tag == "boatFloor")
            {
                StartCoroutine("disableDrag");
                onBoat = false;
            }
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.tag == "boatFloor")
        {
            psk.transform.parent = boat;
            onBoat = true;
        }
    }



    public void TakeDamage(float damage) {

        HealthAmount -= damage;
        HealthBar.value = HealthAmount;
        if (HealthAmount <= 0) {
            KeepLevels.level = level-1;
            

            SceneManager.LoadScene(4);
        }
    
    }
    public IEnumerator disableDrag() {
        yield return new WaitForSeconds(4);
        if (onBoat == false)
        {
            yield return new WaitForSeconds(4);
            if (onBoat == false && swimming)
            {
                
                psk.transform.parent = null;
            }
            
        }
        

    }
    
}

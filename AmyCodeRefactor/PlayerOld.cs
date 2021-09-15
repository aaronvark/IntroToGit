using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Alles wat met de speler te maken heeft
public class PlayerOld : MonoBehaviour
{
    public float speed = 3; //snelheid van speler
    public float jumpPower = 5; //springhoogte van speler
    public float sensitivity = 200; //kijksnelheid van speler
    public float grabDistance = 2; //afstand om met interactable te interacteren

    public GameObject progress; //progress object
    public Slider slider; //progress slider
    public GameObject toolbar; //toolbar object
    public GameObject inventory; //inventory object
    public Transform toolSpot; //object waar equipped items komen
    public Transform center; //center wat gedraaid moet worden met camera
    public Transform image; //Image voor het vasthouden van een item
    private Slider healthBar;
    public GameObject redVeil; //redveil voor ondamage


    public GameObject esc; //Escape menu
    public GameObject gameOver; //gameOver menu

    public ItemManager itemManager; //ItemManager
    Vector3 _hitVector =  new Vector3(0,0,0); //de hitvector wanneer geraakt
    public Vector3 hitVector { //custom setter voor het resetten van de timer en aanzetten van de redveil
        get{return _hitVector;}
        set{hitTimer = hitTimerDefault;
            redVeil.SetActive(true);
            _hitVector = value;}

    }
    float hitTimerDefault = .2f; //hittimer values
    float hitTimer = 0;

    [SerializeField]
    int _health = 100; //health value
    public int health{ //custom setter voor het aanzetten van gameover scherm
        get{return _health;}
        set{healthBar.value = value/100f;
            if(value <= 0){
                Time.timeScale = 0;
                gameOver.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
            }
            _health = value;}
    }
    
    //Dictionary voor items zodat de slot & item goed berijkbaar zijn
    Dictionary<int,Item> Inventory = new Dictionary<int, Item>();
    int selected = -1; //selected item

    //kleur veranderen van toolbar slot als het geselecteerd is
    Color defaultColor = new Color(255/255f,255/255f,255/255f,221/255f);
    Color selectedColor = new Color(97/255f,97/255f,97/255f,221/255f);

    //lijst met alle inventory slots (toolbar, inventory)
    List<RectTransform> InventoryList = new List<RectTransform>();

    //rotatie van player
    float rotationX, rotationY;

    //benodigde componenten
    Rigidbody rb;
    Camera cam;

    bool slammin = false; //of het aan het slaan is
    bool debounce = false; //zodat slaan niet nog een keer gebeurt

    Vector3 top = new Vector3(-34.326f,8.716f,0); //top rotatie van arm
    Vector3 bottom = new Vector3(26.831f,-10.219f,0); //bototm rotatie van arm

    void Start()
    {
        //componenten pakken
        rb = GetComponent<Rigidbody>();
        cam = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked; //lockstate veranderen

        //InventoryList initialiseren 
        InventoryList.Add(toolbar.transform.Find("0").GetComponent<RectTransform>());
        foreach(RectTransform tr in toolbar.transform){
            if(tr.name != "0")
                InventoryList.Add(tr);
        }
        foreach(RectTransform tr in inventory.transform.Find("Inventory")){
            InventoryList.Add(tr);
        }
        SetupClick(); //linksklik en rechtklik connecten aan de inventory items
        
        //standaard bijl and pikhouweel aan speler geven
        Inventory.Add(1,new Item(itemManager.items.Find(x => x.title == "Axe")));
        UpdateInventory(1);
        Inventory.Add(2,new Item(itemManager.items.Find(x => x.title == "Pickaxe")));
        UpdateInventory(2);
    }

    //functie voor bewegen van speler om de absolute snelheid te pakken
    Vector3 getAbs(Vector3 V3)
    {
        float Length = Mathf.Sqrt(Mathf.Pow(Mathf.Abs(V3.x),2) + Mathf.Pow(Mathf.Abs(V3.z),2));

        Vector3 newV3 = Vector3.Lerp(Vector3.zero, V3, 1 / Length);


        return newV3;
    }

    //door alle items loopen en functies connecteren
    void SetupClick(){
        for(int i = 0; i < InventoryList.Count;i++){
            int nr = i;
            RectTransform rect = InventoryList[i];
            InventoryList[i].GetComponent<ClickDetector>().onLeft.AddListener(() => {Click(nr);});
            InventoryList[i].GetComponent<ClickDetector>().onRight.AddListener(() => {RightClick(nr);});
        }
    }


    //het item dat je aan het bewegen ben
    Item _taken;
    Item taken { //custom setter om de image, text en sprite goed te zetten
            get{return _taken;}
            set{
                if(value == null){
                    image.gameObject.SetActive(false);
                }else{
                    image.GetComponent<Image>().sprite = value.icon; 
                    image.gameObject.SetActive(true);
                    if(value.stack > 1)
                        image.GetComponentInChildren<Text>().text = value.stack.ToString();
                    else
                        image.GetComponentInChildren<Text>().text = string.Empty;
                }
                _taken = value;
            }
        }

    //bij linksklik
    void Click(int slot){
        if(taken == null && Inventory.ContainsKey(slot)){ //als taken leeg is en het sloot iets bevat
            taken = Inventory[slot]; //overnemen
            Inventory.Remove(slot); //verwijderen
                UpdateInventory(slot); //updaten
                if(slot == selected) //als het equipped is, unequippen
                    taken.UnEquip();
        }else if(taken != null){ //als taken niet null is
            if(!Inventory.ContainsKey(slot)){ //en inventory slot leeg is
                Inventory.Add(slot,taken); //taken aan inventory toevoegen
                if(slot == selected) //equippen warneer slot geselecteerd is
                    //taken.Equip(toolSpot);
                taken = null; //taken legen
                UpdateInventory(slot); //updaten
                
            }else{//als inventory slot niet leeg is
                Item lastItem = Inventory[slot]; //item
                if(lastItem.title == taken.title && lastItem.stack < lastItem.maxStack){ //als het dezelfde item is en er meer op past
                    int rest = lastItem.stack + taken.stack; //totale hoeveelheid
                    lastItem.stack = Mathf.Clamp(lastItem.stack + taken.stack,0,taken.maxStack); //item opvullen
                    if(rest <= lastItem.maxStack){ //als taken op is, leeg halen
                        taken = null;
                    }else{ //als er meer items zijn dan de maxStack het overige toevoegen aan taken
                        taken.stack = rest % lastItem.maxStack;
                        image.GetComponentInChildren<Text>().text = taken.stack.ToString();
                    }
                    UpdateInventory(slot); //updaten

                }else if(lastItem.title != taken.title){ //als items niet hetzelfde zijn
                    Item newItem = RecipeManager.Instance.Check(taken,Inventory[slot]); //kijken of er een recept is
                    if(newItem == null){ //zoniet 
                        if(slot == selected){ //als slot geselecteerd is, taken equippen, lastitem unequippen
                            lastItem.UnEquip();
                            //taken.Equip(toolSpot);
                        }

                        Inventory[slot] = taken; //verwisselen van items
                        taken = lastItem;
                        UpdateInventory(slot); //slot updaten
                    }else{ //als recept bestaat
                        if(slot == selected) //unequipped if selected
                            lastItem.UnEquip(); 
                        taken = null; //taken leeg halen
                        Inventory[slot] = newItem; //slot vervangen met resultaat uit recept
                        UpdateInventory(slot); //updaten
                    }
                }
            }
        }
    }

    //bij rechtsklik
    void RightClick(int slot){
        if(taken == null && Inventory.ContainsKey(slot)){ //hetzelfde als linksklik maar er maar 1 pakken
            taken = new Item(Inventory[slot]);
            taken.stack = 1;
            image.GetComponentInChildren<Text>().text = "";
            Inventory[slot].stack -= 1;
                if(Inventory[slot].stack == 0){
                    if(slot == selected)
                        Inventory[slot].UnEquip();
                    Inventory.Remove(slot);
                }
            UpdateInventory(slot);
        }else if(taken != null){
            if(!Inventory.ContainsKey(slot)){//hetzelfde als linksklilk
                Inventory.Add(slot,taken);
                if(slot == selected)
                    //taken.Equip(toolSpot);
                taken = null;
                UpdateInventory(slot);
            }else{ //als het hetzelfde item is en het erbij past, 1 aan toevoegen en 1 verwijderen uit slot
                Item lastItem = Inventory[slot];
                if(lastItem.title == taken.title && taken.maxStack > taken.stack){
                    taken.stack += 1;
                    image.GetComponentInChildren<Text>().text = taken.stack.ToString();
                    lastItem.stack -= 1;
                    if(Inventory[slot].stack == 0){
                        if(slot == selected)
                            Inventory[slot].UnEquip();
                        Inventory.Remove(slot);
                    }
                    UpdateInventory(slot);
                }
            }
        }
    }

    //functie om te kijken wat de eerste inventory slot is waar nog een item in kan
    //if statements zorgen ervoor zodat 0 na 9 komt en niet voor 1
    int getFree(){
        for(int i = 0; i < 47; i++){
            if(i < 9 && !Inventory.ContainsKey(i+1)) //
                return i+1;
            else if(i==9 && !Inventory.ContainsKey(0))
                return 0;
            else if(i > 9 && !Inventory.ContainsKey(i))
                return i;
        }
        return -1;
    }

    //functie om te kijken of er een bestaande item is van hetzelfde type waar het bij kan
    //if statements zorgen ervoor zodat 0 na 9 komt en niet voor 1
    int getItem(string name){
        for(int i = 0; i < 47; i++){
            bool found = false;
            int nr = -1;
            if(i < 9 && Inventory.ContainsKey(i+1)){
                nr = i+1;
                found = true;
            }
            else if(i==9 && Inventory.ContainsKey(0)){
                nr = 0;
                found = true;
            }
            else if(Inventory.ContainsKey(i)){
                nr = i;
                found = true;
            }
            if(found && Inventory[nr].title == name && Inventory[nr].stack < Inventory[nr].maxStack)
                return nr;
        }
        return -1;
    }

    //Inventory slot updaten met sprite en text
    void UpdateInventory(int slot = -1){
        if(slot != -1){
            Transform invSlot = InventoryList[slot];
            Image image = invSlot.GetChild(0).GetComponent<Image>();
            Text txt = invSlot.GetComponentInChildren<Text>();
            if(Inventory.ContainsKey(slot)){
                image.sprite = Inventory[slot].icon;
                image.color = new Color(1,1,1,1);
                txt.text = Inventory[slot].stack == 1 ? "" : Inventory[slot].stack.ToString();
            }else{
                image.color = new Color(1,1,1,0);
                txt.text = "";
            }
        }
    }

    //Updates bij verandering selectie
    void updateSelect(int newSelected){
        if(selected != -1){ //als selected van nu niet -1 is de selected van nu omzetten naar default color, en unequippen
            toolbar.transform.Find(selected.ToString()).GetComponent<Image>().color = defaultColor;
            if(Inventory.ContainsKey(selected))
                Inventory[selected].UnEquip();
            if(newSelected == selected){ //wanner newSelected hetzelfde is, selected naar -1 zetten en functie stoppen
                selected = -1;
                return;
            }
        }
        //nieuw item omzetten naar selected color en equippen
        toolbar.transform.Find((newSelected).ToString()).GetComponent<Image>().color = selectedColor;
            if(Inventory.ContainsKey(newSelected))
                //Inventory[newSelected].Equip(toolSpot);
        selected = newSelected;
    }

    //cmon and slam and welcome to the jam (space jam: a new legacy (2021) reference)
    //functie voor animatie van slaan
    IEnumerator Slam(){
        debounce = true; //zodat het niet aangeroepen wordt voordat het eindigt
        float progress = 0; //progress van lerp
        Vector3 v3 = center.localEulerAngles; //start punt
        while(slammin){
            while(slammin && progress < 1){ //omhoog gaan
                progress += Time.deltaTime*2;
                center.localRotation = Quaternion.Lerp(Quaternion.Euler(v3), Quaternion.Euler(top),progress);
                yield return new WaitForEndOfFrame();
            }
            v3 = center.localEulerAngles;
            progress = 0;
            
            bool slammed = false;
            while(slammin && progress < 1){//omlaag gaan
                if(!slammed && progress > .5f){ //wanneer progress halverwege is kijken of er een enemy is en het slaan :)
                    slammed = true;
                    Vector3 screenCenter = cam.ScreenToWorldPoint(new Vector3(Screen.width/2, Screen.height/2, cam.nearClipPlane));
                    RaycastHit hit;
                    if(Physics.Raycast(screenCenter,cam.transform.forward, out hit, 4)){
                        GameObject obj = hit.transform.gameObject;
                        var enemy = obj.GetComponentInParent<Enemy>();
                        if(enemy != null){
                            enemy.switchState(Enemy.stateEnum.Hit);
                            //als ijzer zwaard equipped is, doe meer damage
                            if(Inventory.ContainsKey(selected) && Inventory[selected].title == "IronSword")
                                enemy.health -= 51;
                            else
                                enemy.health -= 25;
                        }
                    }
                }
                progress += Time.deltaTime*5;
                center.localRotation = Quaternion.Lerp(Quaternion.Euler(v3), Quaternion.Euler(bottom),progress);
                yield return new WaitForEndOfFrame();
            }
            v3 = center.localEulerAngles;
            progress = 0;
            
        }//als het gestopt is, arm terug halen naar normale positie
        while(progress < 1){
            progress += Time.deltaTime*10;
            center.localRotation = Quaternion.Lerp(Quaternion.Euler(v3), Quaternion.Euler(Vector3.zero),progress);
            yield return new WaitForEndOfFrame();
        }
            center.localRotation = Quaternion.Euler(Vector3.zero);

        debounce = false;
    }

    //functie voor beweging bij te houden
    void Movement()
    {
        Vector3 velocity = Vector3.zero; //velocity initializeren

        velocity += Input.GetAxis("Horizontal") * transform.right; //horizontal input toevoegen
        velocity += Input.GetAxis("Vertical") * transform.forward; //verticale input toevoegen

        velocity = getAbs(velocity) * speed; //vermeelvoudegen met snelheid en absolut maken
        if (Input.GetKey(KeyCode.Space) && Mathf.Abs(rb.velocity.y) <= .05) //als spatie ingedrukt is en val snelheid lager of gelijk is aan .05
            velocity += Vector3.up * jumpPower; //springen 

        if(hitTimer > 0) //hitTimer bijhouden en redveil uitzetten
            hitTimer -= Time.deltaTime;
        else if(hitVector != Vector3.zero){
            hitVector = Vector3.zero;
            redVeil.SetActive(false);
        }

        rb.velocity = velocity + new Vector3(0,rb.velocity.y,0) + hitVector; //velocity aanpassen

    }

    //Rotatie bijhouden
    void Rotation()
    {
        if(!inventory.activeSelf){ //als inventory niet actief is
            rotationX += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime; //xrotatie aanpassen met nieuwe mouseX positie. gebruik van time.deltaTime zodat je altijd evenveel draaid
            rotationY = Mathf.Clamp(rotationY + -Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime, -89, 89); //yrotatie aanpassen, Mathf.Clamp() zodat je niet volledig 360 kan blijven draaien
            transform.localRotation = Quaternion.Euler(0, rotationX, 0); //xrotatie toepassen op lichaam
            cam.transform.localRotation = Quaternion.Euler(rotationY, 0, 0); //yrotatie toepassen op camera
        }
    }
    //functie om te kijken of je naar een interactable kijk
    void LookAt(){
        Vector3 screenCenter = cam.ScreenToWorldPoint(new Vector3(Screen.width/2, Screen.height/2, cam.nearClipPlane));
        RaycastHit hit;
        if(Physics.Raycast(screenCenter,cam.transform.forward, out hit, 4)){
            GameObject obj = hit.transform.gameObject;
            var interactable = obj.GetComponentInParent<Interactable>();
            if(interactable != null){ //als het een interactable heeft, en je bent aan het slaan, en de benodigde tool is selected 
                if(slammin && interactable.tool != "" && Inventory.ContainsKey(selected) && interactable.tool == Inventory[selected].title)
                    interactable.progress += interactable.step; //progress aanpassen
                progress.SetActive(true); //progress bar laten zien en updaten
                slider.value = interactable.progress;
            }
        }else{
            progress.SetActive(false);
            slider.value = 0;
        }
    }
    //functies voor Inventory en selecties te veranderen
    void checkInventory(){
        if(Input.GetKeyDown(KeyCode.E)){ //openen waarneer E wordt ingedrukt
            inventory.SetActive(!inventory.activeSelf);
            if(Cursor.lockState == CursorLockMode.Locked)
                Cursor.lockState = CursorLockMode.None;
            else
                Cursor.lockState = CursorLockMode.Locked;
        }
        if(Input.GetAxis("Mouse ScrollWheel") > 0f){ //wanneer je omhoog scroll
            if(selected != -1){
                updateSelect(selected != 0 ? selected-1 : 9);
            }else{
                updateSelect(0);
            }
        }else if(Input.GetAxis("Mouse ScrollWheel") < 0f){ //wanneer je omlaag scroll
            if(selected != -1){
                updateSelect((selected+1)%10);
            }else{
                updateSelect(1);
            }
        }
        for(int i = 0; i < 10; i++){ //wanneer 0-9 wordt ingedrukt 
            if(Input.GetKeyDown(i.ToString())){
                updateSelect(i);
            }
        }
    }

    //loopen door spawnedItems om te kijken of je iets kan oppakken
    void checkSpawnedItems(){
        List<timeObject> spawnedItems = itemManager.spawnedItems;
        for(int i = spawnedItems.Count-1; i >= 0; i--){
            timeObject item = spawnedItems[i];
            
            if(item.cd > 0){ //cooldown timer verlagen, continue zodat rest niet wordt gebruikt
                item.cd -= Time.deltaTime;
                continue;
            }
            //als afstand dichtbij genoeg is
            if(Vector3.Distance(item.obj.transform.localPosition, transform.localPosition) < grabDistance){
                
                int itemSlot = getItem(item.obj.name); 
                bool success = false;
                if(itemSlot != -1){ //als er een bestaand item is waar het in past
                    Inventory[itemSlot].stack += 1;
                    UpdateInventory(itemSlot);
                    success = true;
                }else{
                    int freeSlot = getFree(); //als er een lege slot is
                    if(freeSlot != -1){
                        Item itemsItem = itemManager.items.Find(x => x.title == item.obj.name);
                        Inventory.Add(freeSlot,new Item(itemsItem));
                        UpdateInventory(freeSlot);
                        success = true;
                    }
                }
                if(success){ //obj verwijderen wanneer opgepakt
                    Destroy(item.obj);
                    spawnedItems.Remove(item);
                }
            }
        }
    }

    //update voor verschillende perUpdate functies.
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){ //als escape is ingedrukt
            if(esc.activeSelf){
                esc.SetActive(false);
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.Locked;
            }else{
                esc.SetActive(true);
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.None;
            }
        }
        if(esc.activeSelf || gameOver.activeSelf) //als een menu open staat de rest niet doen
            return;

        if(itemManager != null){ //justbecause
            Movement();
            Rotation();
            LookAt();
            checkInventory();
            checkSpawnedItems();
        }
        if(Input.GetKeyDown(KeyCode.Mouse0) && !inventory.activeSelf){ //als inventory niet open is, slaan stoppen/starten
            if(!debounce){
                slammin = true;
                StartCoroutine(Slam());
            }
        }else if(Input.GetKeyUp(KeyCode.Mouse0))
            slammin = false;

        if(taken != null && !taken.Equals(null)){ //de taken item bij de cursor houden
            image.transform.position = Input.mousePosition;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Alles wat met de speler te maken heeft
public class Player : MonoBehaviour
{
    
    public float sensitivity = 200; //kijksnelheid van speler
    public float grabDistance = 2; //afstand om met interactable te interacteren

    public GameObject progress; //progress object
    public Slider slider; //progress slider
    public Transform toolSpot; //object waar equipped items komen
    public Transform center; //center wat gedraaid moet worden met camera

    public GameObject esc; //Escape menu
    public GameObject gameOver; //gameOver menu

    public ItemManager itemManager; //ItemManager

    bool hasInventoryOpen = false;
    
    int selected = -1;
    Item selectedItem = null;

    //rotatie van player
    float rotationX, rotationY;

    //benodigde componenten
    Camera cam;

    bool slammin = false; //of het aan het slaan is
    bool debounce = false; //zodat slaan niet nog een keer gebeurt

    Vector3 top = new Vector3(-34.326f,8.716f,0); //top rotatie van arm
    Vector3 bottom = new Vector3(26.831f,-10.219f,0); //bototm rotatie van arm

    void Start()
    {
        //componenten pakken
        cam = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked; //lockstate veranderen
        
        EventSystem.RaiseEvent(EventType.SetItem,new System.Tuple<int,Item>(1,new Item(itemManager.items.Find(x => x.title == "Axe"))));
        EventSystem.RaiseEvent(EventType.UpdateInventorySlot,new System.Tuple<int,Item>(1,new Item(itemManager.items.Find(x => x.title == "Axe"))));
        EventSystem.RaiseEvent(EventType.SetItem,new System.Tuple<int,Item>(2,new Item(itemManager.items.Find(x => x.title == "Pickaxe"))));
        EventSystem.RaiseEvent(EventType.UpdateInventorySlot,new System.Tuple<int,Item>(2,new Item(itemManager.items.Find(x => x.title == "Pickaxe"))));
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
                            if(selectedItem != null && selectedItem.title == "IronSword")
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

    void Rotation()
    {
        if(!hasInventoryOpen){ //als inventory niet actief is
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
                if(slammin && interactable.tool != "" && selectedItem != null && interactable.tool == selectedItem.title){
                    interactable.progress += interactable.step; //progress aanpassen
                }
                progress.SetActive(true); //progress bar laten zien en updaten
                slider.value = interactable.progress;
            }
        }else{
            progress.SetActive(false);
            slider.value = 0;
        }
    }
    //functies voor Inventory en selecties te veranderen
    void CheckInventory(){
        if(Input.GetKeyDown(KeyCode.E)){ //openen waarneer E wordt ingedrukt
            hasInventoryOpen = !hasInventoryOpen;
            EventSystem.RaiseEvent(EventType.ToggleInventory,hasInventoryOpen);
            if(Cursor.lockState == CursorLockMode.Locked){
                Cursor.lockState = CursorLockMode.None;
            }
            else{
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
        if(Input.GetAxis("Mouse ScrollWheel") > 0f){ //wanneer je omhoog scroll
            if(selected != -1){
                selectedItem = (Item)EventSystem.RaiseEvent(EventType.UpdateSelected,new System.Tuple<int,int>(selected,selected != 0 ? selected-1 : 9));
                selected = (int)EventSystem.RaiseEvent(EventType.UpdateSelectedItem,new System.Tuple<int,int>(selected,selected != 0 ? selected-1 : 9));
            }else{
                selectedItem = (Item)EventSystem.RaiseEvent(EventType.UpdateSelected,new System.Tuple<int,int>(selected,0));
                selected = (int)EventSystem.RaiseEvent(EventType.UpdateSelectedItem,new System.Tuple<int,int>(selected,0));
            }
        }else if(Input.GetAxis("Mouse ScrollWheel") < 0f){ //wanneer je omlaag scroll
            if(selected != -1){
                selectedItem = (Item)EventSystem.RaiseEvent(EventType.UpdateSelected,new System.Tuple<int,int>(selected,(selected+1)%10));
                selected = (int)EventSystem.RaiseEvent(EventType.UpdateSelectedItem,new System.Tuple<int,int>(selected,(selected+1)%10));
            }else{
                selectedItem = (Item)EventSystem.RaiseEvent(EventType.UpdateSelected,new System.Tuple<int,int>(selected,1));
                selected = (int)EventSystem.RaiseEvent(EventType.UpdateSelectedItem,new System.Tuple<int,int>(selected,1));
            }
        }
        for(int i = 0; i < 10; i++){ //wanneer 0-9 wordt ingedrukt 
            if(Input.GetKeyDown(i.ToString())){
                selectedItem = (Item)EventSystem.RaiseEvent(EventType.UpdateSelected,new System.Tuple<int,int>(selected,i));
                selected = (int)EventSystem.RaiseEvent(EventType.UpdateSelectedItem,new System.Tuple<int,int>(selected,i));
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
        if(esc.activeSelf || gameOver.activeSelf){ return; } //als een menu open staat de rest niet doen

        if(itemManager != null){ //justbecause
            Rotation();
            LookAt();
            CheckInventory();
        }
        if(Input.GetKeyDown(KeyCode.Mouse0) && !hasInventoryOpen){ //als inventory niet open is, slaan stoppen/starten
            if(!debounce){
                slammin = true;
                StartCoroutine(Slam());
            }
        }else if(Input.GetKeyUp(KeyCode.Mouse0)){
            slammin = false;
        }

        
    }
}

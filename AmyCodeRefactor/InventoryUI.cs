using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    //lijst met alle inventory slots (toolbar, inventory)
    List<RectTransform> inventoryList = new List<RectTransform>();

       //kleur veranderen van toolbar slot als het geselecteerd is
    Color defaultColor = new Color(255/255f,255/255f,255/255f,221/255f);
    Color selectedColor = new Color(97/255f,97/255f,97/255f,221/255f);

    public GameObject toolbar; //toolbar object
    public GameObject inventory; //inventory object

    public Transform image; //Image voor het vasthouden van een item

    private void Start(){//InventoryList initialiseren 
        inventoryList.Add(toolbar.transform.Find("0").GetComponent<RectTransform>());
        foreach(RectTransform tr in toolbar.transform){
            if(tr.name != "0")
                inventoryList.Add(tr);
        }
        foreach(RectTransform tr in inventory.transform.Find("Inventory")){
            inventoryList.Add(tr);
        }
        
        for(int i = 0; i < inventoryList.Count;i++){
            int nr = i;
            RectTransform rect = inventoryList[i];
            inventoryList[i].GetComponent<ClickDetector>().onLeft.AddListener(() => {EventSystem.RaiseEvent(EventType.ItemLeftClicked,nr);});
            inventoryList[i].GetComponent<ClickDetector>().onRight.AddListener(() => {EventSystem.RaiseEvent(EventType.ItemRightClicked,nr);});
        }
    }

    private void Awake(){
        EventSystem.Subscribe(EventType.UpdateInventorySlot,UpdateInventory);
        EventSystem.Subscribe(EventType.UpdateSelectedItem,UpdateSelect);
        EventSystem.Subscribe(EventType.UpdateTaken,UpdateTaken);
        EventSystem.Subscribe(EventType.ToggleInventory,ToggleInventory);
    }

    private void Update(){
        if(image.gameObject.activeSelf){ //de taken item bij de cursor houden
            image.transform.position = Input.mousePosition;
        }
    }

    private object ToggleInventory(object _open){
        inventory.SetActive((bool)_open);
        return null;
    }

    //Inventory slot updaten met sprite en text
    private object UpdateInventory(object _tuple){
        System.Tuple<int, Item> args = (System.Tuple<int,Item>)_tuple;
        int slot = args.Item1;
        Item item = args.Item2;
        if(slot != -1){
            Transform invSlot = inventoryList[slot];
            Image image = invSlot.GetChild(0).GetComponent<Image>();
            Text txt = invSlot.GetComponentInChildren<Text>();
            if(item != null){
                image.sprite = item.icon;
                image.color = new Color(1,1,1,1);
                txt.text = item.stack == 1 ? "" : item.stack.ToString();
            }else{
                image.color = new Color(1,1,1,0);
                txt.text = "";
            }
        }
        return null;
    }
//Updates bij verandering selectie
    private object UpdateSelect(object _tuple){
        System.Tuple<int, int> args = (System.Tuple<int,int>)_tuple;
        int selected = args.Item1;
        int newSelected = args.Item2;

        if(selected != -1){ //als selected van nu niet -1 is de selected van nu omzetten naar default color, en unequippen
            inventoryList[selected].GetComponent<Image>().color = defaultColor;
            if(newSelected == selected){ //wanner newSelected hetzelfde is, selected naar -1 zetten en functie stoppen
                selected = -1;
                return -1;
            }
        }
        //nieuw item omzetten naar selected color en equippen
        inventoryList[newSelected].GetComponent<Image>().color = selectedColor;
        return newSelected;
    }

    private object UpdateTaken(object _item){
        if(_item == null){
            image.gameObject.SetActive(false);
        }else{
            image.GetComponent<Image>().sprite = ((Item)_item).icon; 
            image.gameObject.SetActive(true);
            if(((Item)_item).stack > 1)
                image.GetComponentInChildren<Text>().text = ((Item)_item).stack.ToString();
            else
                image.GetComponentInChildren<Text>().text = string.Empty;
        }
        return null;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //Dictionary voor items zodat de slot & item goed berijkbaar zijn
    private Dictionary<int,Item> inventorySlots = new Dictionary<int, Item>();
    private int selected = -1; //selected item
    
    private Item taken;

    
    private void Awake(){
        EventSystem.Subscribe(EventType.RemoveItem,RemoveItem);
        EventSystem.Subscribe(EventType.SetItem,AddItem);
        EventSystem.Subscribe(EventType.ItemRightClicked,RightClick);
        EventSystem.Subscribe(EventType.ItemLeftClicked,Click);
        EventSystem.Subscribe(EventType.UpdateSelected,UpdateSelected);
        EventSystem.Subscribe(EventType.PickupItem,PickupItem);
    }

        //functie om te kijken wat de eerste inventory slot is waar nog een item in kan
    //if statements zorgen ervoor zodat 0 na 9 komt en niet voor 1
    private int getFree(){
        for(int i = 0; i < 47; i++){
            if(i < 9 && !inventorySlots.ContainsKey(i+1)) //
                return i+1;
            else if(i==9 && !inventorySlots.ContainsKey(0))
                return 0;
            else if(i > 9 && !inventorySlots.ContainsKey(i))
                return i;
        }
        return -1;
    }

    //functie om te kijken of er een bestaande item is van hetzelfde type waar het bij kan
    //if statements zorgen ervoor zodat 0 na 9 komt en niet voor 1
    private int getItem(string _name){
        int nr = -1;
        for(int i = 1; i < 10; i++){
            if(inventorySlots.ContainsKey(i)){
                nr = i;
            }
        }
        if(inventorySlots.ContainsKey(0)){
            nr = 0;
        }
        for(int i = 10; i < 47; i++){
            if(inventorySlots.ContainsKey(i)){
                nr = i;
            }
        }
        if(nr != -1 && inventorySlots[nr].title == _name && inventorySlots[nr].stack < inventorySlots[nr].maxStack){
            return nr;
        }
        return -1;
    }

    private object PickupItem(object _obj){
        Item item = (Item)_obj;
        int itemSlot = getItem(item.title); 
        bool success = false;
        if(itemSlot != -1){ //als er een bestaand item is waar het in past
            inventorySlots[itemSlot].stack += 1;
                EventSystem.RaiseEvent(EventType.UpdateInventorySlot,new System.Tuple<int,Item>(itemSlot,inventorySlots[itemSlot]));
            success = true;
        }else{
            int freeSlot = getFree(); //als er een lege slot is
            if(freeSlot != -1){
                inventorySlots.Add(freeSlot,new Item(item));
                EventSystem.RaiseEvent(EventType.UpdateInventorySlot,new System.Tuple<int,Item>(freeSlot,inventorySlots[freeSlot]));
                success = true;
            }
        }
        return success;
    }

    private object RemoveItem(object _slot){
        inventorySlots.Remove((int)_slot);
        return null;
    }

    private object AddItem(object _tuple){
        System.Tuple<int,Item> args = (System.Tuple<int,Item>)_tuple;
        inventorySlots.Add(args.Item1,args.Item2);
        return null;
    }

    private object UpdateSelected(object _tuple){
        System.Tuple<int, int> args = (System.Tuple<int,int>)_tuple;
        int selected = args.Item1;
        int newSelected = args.Item2;
        if(inventorySlots.ContainsKey(selected)){
            inventorySlots[selected].UnEquip();
        }
        if(inventorySlots.ContainsKey(newSelected) && selected != newSelected){
            inventorySlots[newSelected].Equip();   
            return inventorySlots[newSelected];
        }
        return null;
    }
    //bij linksklik
    private object Click(object _obj){
        int slot = (int)_obj;
        if(taken == null && inventorySlots.ContainsKey(slot))
        { //als taken leeg is en het sloot iets bevat
            taken = inventorySlots[slot]; //overnemen
            inventorySlots.Remove(slot); //verwijderen
            EventSystem.RaiseEvent(EventType.UpdateInventorySlot,new System.Tuple<int,Item>(slot,null));
            if(slot == selected) { 
                taken.UnEquip();
            }
                
        }else if(taken != null){ //als taken niet null is
            if(!inventorySlots.ContainsKey(slot)){ //en inventory slot leeg is
                inventorySlots.Add(slot, taken); //taken aan inventory toevoegen
                if(slot == selected) //equippen warneer slot geselecteerd is
                    taken.Equip();
                EventSystem.RaiseEvent(EventType.UpdateInventorySlot,new System.Tuple<int,Item>(slot,taken));
                taken = null; //taken legen
            }else{//als inventory slot niet leeg is
                Item lastItem = inventorySlots[slot]; //item
                if(lastItem.title == taken.title && lastItem.stack < lastItem.maxStack){ //als het dezelfde item is en er meer op past
                    int rest = lastItem.stack + taken.stack; //totale hoeveelheid
                    lastItem.stack = Mathf.Clamp(lastItem.stack + taken.stack,0, taken.maxStack); //item opvullen
                    if(rest <= lastItem.maxStack){ //als taken op is, leeg halen
                        taken = null;
                    }else{ //als er meer items zijn dan de maxStack het overige toevoegen aan taken
                        taken.stack = rest % lastItem.maxStack;
                    }
                    EventSystem.RaiseEvent(EventType.UpdateInventorySlot,new System.Tuple<int,Item>(slot,lastItem));
                }else if(lastItem.title != taken.title){ //als items niet hetzelfde zijn
                    Item newItem = RecipeManager.Instance.Check(taken, inventorySlots[slot]); //kijken of er een recept is
                    if(newItem == null){ //zoniet 
                        if(slot == selected)
                        { //als slot geselecteerd is, taken equippen, lastitem unequippen
                            lastItem.UnEquip();
                            taken.Equip();
                        }

                        inventorySlots[slot] = taken; //verwisselen van items
                        EventSystem.RaiseEvent(EventType.UpdateInventorySlot,new System.Tuple<int,Item>(slot,taken));
                        taken = lastItem;
                    }else{ //als recept bestaat
                        if(slot == selected) //unequipped if selected
                            lastItem.UnEquip();
                        taken = null; //taken leeg halen
                        inventorySlots[slot] = newItem; //slot vervangen met resultaat uit recept
                        EventSystem.RaiseEvent(EventType.UpdateInventorySlot,new System.Tuple<int,Item>(slot,newItem));
                    }
                }
            }
        }
        EventSystem.RaiseEvent(EventType.UpdateTaken,taken);
        return null;
    }

    //bij rechtsklik
    private object RightClick(object _obj){
        int slot = (int)_obj;
        if(taken == null && inventorySlots.ContainsKey(slot))
        { //hetzelfde als linksklik maar er maar 1 pakken
            taken = new Item(inventorySlots[slot]);
            taken.stack = 1;
            inventorySlots[slot].stack -= 1;
                if(inventorySlots[slot].stack == 0){
                    if(slot == selected)
                        inventorySlots[slot].UnEquip();
                    inventorySlots.Remove(slot);
                }
            EventSystem.RaiseEvent(EventType.UpdateInventorySlot,new System.Tuple<int,Item>(slot,inventorySlots.ContainsKey(slot) ? inventorySlots[slot] : null));
        }else if(taken != null){
            if(!inventorySlots.ContainsKey(slot)){//hetzelfde als linksklilk
                inventorySlots.Add(slot, taken);
                if(slot == selected)
                    taken.Equip();
                EventSystem.RaiseEvent(EventType.UpdateInventorySlot,new System.Tuple<int,Item>(slot,taken));
                taken = null;
            }else{ //als het hetzelfde item is en het erbij past, 1 aan toevoegen en 1 verwijderen uit slot
                Item lastItem = inventorySlots[slot];
                if(lastItem.title == taken.title && taken.maxStack > taken.stack){
                    taken.stack += 1;
                    lastItem.stack -= 1;
                    if(lastItem.stack == 0){
                        if(slot == selected)
                            lastItem.UnEquip();
                        inventorySlots.Remove(slot);
                    }
                    EventSystem.RaiseEvent(EventType.UpdateInventorySlot,new System.Tuple<int,Item>(slot,lastItem));
                }
            }
        }
        EventSystem.RaiseEvent(EventType.UpdateTaken,taken);
        return null;
    }
}
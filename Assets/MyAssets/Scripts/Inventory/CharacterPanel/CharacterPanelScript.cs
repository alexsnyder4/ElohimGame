using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPanelScript : MonoBehaviour
{
    public Items[] playersItems = new Items[7];
    public Transform Head;
    public Transform Chest;
    public Transform Legs;
    public Transform Gloves;
    public Transform Boots;
    public Transform RHand;
    public Transform LHand;
    public PlayerData pd;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void UpdateCharacterLoadout()
    {
        
        foreach(Items item in pd.charKit)
        {
            if(item == null)
            {
                continue;
            }
            GameObject instantiatedItem = null;
            switch(item.equipSlot)
            {
                case 0:
                    ResetParent(Head);
                    instantiatedItem = Instantiate(item.prefab, Head.transform);
                    Collider collider = instantiatedItem.GetComponent<Collider>();
                    collider.enabled = false;
                break;

                case 1:
                    ResetParent(Chest);
                    instantiatedItem = Instantiate(item.prefab, Head.transform);
                    collider = instantiatedItem.GetComponent<Collider>();
                    collider.enabled = false;
                break;

                case 2:
                    ResetParent(Legs);
                    instantiatedItem = Instantiate(item.prefab, Head.transform);
                    collider = instantiatedItem.GetComponent<Collider>();
                    collider.enabled = false;
                break;

                case 3:
                    ResetParent(Gloves);
                    instantiatedItem = Instantiate(item.prefab, Head.transform);
                    collider = instantiatedItem.GetComponent<Collider>();
                    collider.enabled = false;
                break;

                case 4:
                    ResetParent(Boots);
                    instantiatedItem = Instantiate(item.prefab, Head.transform);
                    collider = instantiatedItem.GetComponent<Collider>();
                    collider.enabled = false;
                break;

                case 5:
                    Debug.Log("Case 5");
                    ResetParent(RHand);
                    instantiatedItem = Instantiate(item.prefab, RHand.transform);
                    instantiatedItem.GetComponent<Collider>().enabled = true;
                    instantiatedItem.GetComponent<Collider>().isTrigger = true;


                break;

                case 6:
                    ResetParent(LHand);
                    instantiatedItem = Instantiate(item.prefab, LHand.transform);
                    instantiatedItem.GetComponent<Collider>().enabled = true;
                    instantiatedItem.GetComponent<Collider>().isTrigger = true;
                break;
            }
            
            Rigidbody rigidbody = instantiatedItem.GetComponent<Rigidbody>();
            rigidbody.isKinematic = true;
        }
    }   

    void ResetParent(Transform parent)
    {
        Debug.Log("Reset");
        foreach (Transform child in parent)
        {
            Debug.Log(child.gameObject);
            Destroy(child.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPanelScript : MonoBehaviour
{
    public Items[] playersItems = new Items[7];
    public Transform Head;
    public GameObject Chest;
    public GameObject Legs;
    public GameObject Gloves;
    public GameObject Boots;
    public Transform RHand;
    public GameObject LHand;
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
        ResetParent(RHand);
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
                    instantiatedItem = Instantiate(item.prefab, Head.transform);
                break;

                case 1:
                break;

                case 2:
                break;

                case 3:
                break;

                case 4:
                break;

                case 5:
                    Debug.Log("Case 5");
                    instantiatedItem = Instantiate(item.prefab, RHand.transform);

                break;

                case 6:
                break;
            }
            Collider collider = instantiatedItem.GetComponent<Collider>();
            collider.enabled = false;
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

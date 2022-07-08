using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Item : MonoBehaviour
{
    // Start is called before the first frame update
    //Collider Trigger
    [SerializeField] private enum InteractionType { NONE,PickUp,Examine }
    [SerializeField] private InteractionType type;

    [SerializeField] public string descriptionText;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact()
    {
        switch (type)
        {
            case InteractionType.PickUp:
                //Add obj to the PickUpItem list item
                FindObjectOfType<InteractionSystem>().PickUpItem(gameObject);
                //destroy after picked up
                gameObject.SetActive(false);
                break;
            case InteractionType.Examine:
                //Display an Examine Window
                //Show the item's image in the middle
                //Whrite description text underneath the image
                FindObjectOfType<InteractionSystem>().ExamineItem(this);
                Debug.Log("Examine");
                break;
            default:
                Debug.Log("None");
                break;

        }
    }

    private void Reset()
    {
        GetComponent<Collider2D>().isTrigger = true;
        gameObject.layer = 3;
    }
}

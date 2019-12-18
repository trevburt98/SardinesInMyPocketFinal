using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoeyPickupBehaviour : MonoBehaviour
{
    public Transform rightHand;
    public Transform leftHand;
    public Transform movePos;
    public GameObject camera;
    public GameObject shovelIcon;

    public Mesh mesh;
    public Material equippableHighlight;
    public Material moveableHighlight;
    public Material switchHighlight;

    private GameObject currentObj;
    private GameObject currentEquipment;
    private Rigidbody currentObjRB;
    private RaycastHit hitInfo;
    private Vector3 fwd;

    private int moveableLayer = 8;
    private int equippableLayer = 9;
    private int switchLayer = 11;
    int layerMask = 1 << 10;

    void Start()
    {
        layerMask = ~layerMask;
    }

    void Update()
    {
        //Will move currentObject if currently grabbing an object
        MoveObj();

        fwd = camera.transform.TransformDirection(Vector3.forward);
        Debug.DrawRay(camera.transform.position, fwd * 3, Color.yellow);
        

        if (Input.GetKeyDown(KeyCode.E))
        {
            Unequip();
        }

        if (Physics.Raycast(camera.transform.position, fwd, out hitInfo, 3, layerMask)) {
            if (currentObj == null) {
                //Movable Item
                if (hitInfo.transform.gameObject.layer == moveableLayer) {
                    Graphics.DrawMesh(mesh, hitInfo.transform.position, hitInfo.transform.rotation, moveableHighlight, 0);
                    
                    if (Input.GetKeyDown(KeyCode.Mouse0)) { //Left Click to Move Object
                        currentObj = hitInfo.transform.gameObject;
                    }
                
                }
                //Equippable Item
                else if (hitInfo.transform.gameObject.layer == equippableLayer) {
                    Graphics.DrawMesh(mesh, hitInfo.transform.position, hitInfo.transform.rotation, equippableHighlight, 0);

                    
                    if (Input.GetKeyDown(KeyCode.Mouse0)) { //Left Click to Move Object
                        currentObj = hitInfo.transform.gameObject;
                    }
                    if (Input.GetKeyDown(KeyCode.E))        //if press E, equip
                    {
                        Equip(hitInfo.transform.gameObject);
                        Destroy(shovelIcon);
                    }
                }
                //Switch/Button
                if (hitInfo.transform.gameObject.layer ==switchLayer) {
                    Graphics.DrawMesh(mesh, hitInfo.transform.position, hitInfo.transform.rotation, switchHighlight, 0);
                    
                    if (Input.GetKeyDown(KeyCode.Mouse0)) {
                        //On left click, Toggle Switch
                        hitInfo.transform.gameObject.GetComponent<ParkerDoorSwitch>().toggleSwitch();
                    }
                
                }
            } else if (Input.GetKeyDown(KeyCode.Mouse0)) {  //Left Click to Drop Object
                DropObj();
            }
        } 



    }

    //Moves the currentObj in front of the player. Only works if the objects layer == movableLayer (8)
    public void MoveObj() {
        if (currentObj != null) {
            currentObjRB = currentObj.GetComponent<Rigidbody>();
            currentObjRB.useGravity = false;
            //currentObjRB.ResetInertiaTensor();
            //rb.MovePosition(movePos.position * Time.fixedDeltaTime);
            currentObj.transform.position = Vector3.MoveTowards(currentObj.transform.position, movePos.position, 10 * Time.deltaTime);
        }
    }

    //Resets Object's RigidBody, then Drops Object
    public void DropObj() {
        currentObjRB.useGravity = true;
        currentObj = null;
     }

    //Set currentEquipment to passed GameObject Item and attaches to hand
    public void Equip(GameObject item)
    {
        //Drop currently equipped item (if applicable)
        Unequip();

        //Freeze Objects Rotation
        Rigidbody rb = item.GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeAll;

        //equip item (attach to hand)
        currentEquipment = item;
        item.transform.position = rightHand.position;
        item.transform.rotation = rightHand.rotation;
        item.transform.parent = rightHand.transform;
    }

    //If there is currentEquipment, drop it
        public void Unequip()
    {
        //unequip item
        if (currentEquipment != null)
        {
            Rigidbody rb = currentEquipment.GetComponent<Rigidbody>();
            rb.useGravity = true;
            rb.constraints = RigidbodyConstraints.None;
            currentEquipment.transform.parent = null;
            currentEquipment = null;
        }
    }
}

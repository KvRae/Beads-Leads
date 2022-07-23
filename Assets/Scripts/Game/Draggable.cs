using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    public delegate void DragEndedDelegate(Draggable Bead);
    
    public DragEndedDelegate dragEndedCallBack ;

    private bool isDragged = false ;
    
    private Vector3 mouseDragStartPosition ;

    //hold the position of the bead when starting the drag operation
    public Vector3 spriteDragStarPosition ;
    //hold the initial position of the bead
    public Vector3 initPos;
    //lock player beads for each turn
    public bool locked = false ;
    
    public bool notValid = false ;
    private DragController dragController ;

    public Collider2D beadcollider ;
     
    //change the bead assets 
     void Start() {
        
        dragController = FindObjectOfType<DragController>();


        // change beads colors
        if (Settings.beadColorIndex ==0)
        {
            if (beadcollider.gameObject.CompareTag("Player1"))
            {
                GetComponent<Renderer>().material.color = Color.blue;           
            }
        
            if (beadcollider.gameObject.CompareTag("Player2"))
            {
                GetComponent<Renderer>().material.color = Color.red; 
            }
        }
        else
        {
            if (beadcollider.gameObject.CompareTag("Player1"))
            {
                GetComponent<Renderer>().material.color = Color.green;           
            }
        
            if (beadcollider.gameObject.CompareTag("Player2"))
            {
                GetComponent<Renderer>().material.color = Color.yellow; 
            }
        }


        
        initPos = transform.position ;
        
    }
    public void stop ()
    {
        this.enabled = false ;
    }

    

    //left click of the mouse (click to drag)
    private void OnMouseDown() {
       isDragged = true ;
       mouseDragStartPosition =  Camera.main.ScreenToWorldPoint(Input.mousePosition);
       spriteDragStarPosition = transform.localPosition ;
        
    }
    //holding left click of the mouse (dragging)
    private void OnMouseDrag() {
        if (isDragged && !locked){
            transform.localPosition = spriteDragStarPosition + (Camera.main.ScreenToWorldPoint(Input.mousePosition) - mouseDragStartPosition);
        
        }
    }
    //releasing the left click
    private void OnMouseUp() {
        isDragged = false ;
        dragEndedCallBack(this);  
    }


    
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Player1") || other.gameObject.CompareTag("Player2")|| other.gameObject.CompareTag("CPU"))
        {
            notValid = true;
        }
    }
     private void OnTriggerStay2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Player1") || other.gameObject.CompareTag("Player2")|| other.gameObject.CompareTag("CPU"))
        {
            notValid = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player1") || other.gameObject.CompareTag("Player2")|| other.gameObject.CompareTag("CPU"))
        {
            notValid = false;
        }
    }
   


}
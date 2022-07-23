using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragController : MonoBehaviour
{
    //list of snap point to be on our points
    public List<Transform> snapPoints;
    //list of beads that will be placed on the board
    public List<Draggable> draggableBeads;
    //the range that lets bead get into the snap point
    public Draggable LastDragged => LastDragged ;
    public float snapRange = 0.7f;
    //number of beads

    public int beadPlaced = 6 ; 
    //current player's bead 
    public int currentPlayer;
    
    public GameOverScreen gameOverScreen ;

    public GameManager gameManager;

    public AudioSource dragSFX ;
    public AudioSource failSFX ;
    public AudioSource gameOverSFX ;
    
   
    
    
     void Start() 
    {
        foreach (Draggable draggable in draggableBeads)
        {
            draggable.dragEndedCallBack = onDragEnded; 
        }
    }





    // Drag treatment including snaping into the right position
    private void onDragEnded(Draggable draggable){
        float closestDistance = -1;
        Transform closestSnapPoint = null;

        foreach(Transform snapPoint in snapPoints)
        {
            float currentDistance = Vector2.Distance(draggable.transform.localPosition, snapPoint.localPosition);
            if(closestSnapPoint == null || currentDistance < closestDistance)
            {
                closestSnapPoint = snapPoint ;
                closestDistance = currentDistance ;  
            }

           

        }
        // move the bead to the closest snappoint
        if(closestSnapPoint != null && closestDistance <= snapRange && draggable.notValid == false && 
        closestSnapPoint.localPosition != draggable.spriteDragStarPosition) 
        {
            
            
            draggable.transform.localPosition =  closestSnapPoint.localPosition;


            //Check if the GameObject is active then Play SFX
            if(gameManager.audioManager.activeSelf == true)
            {
                dragSFX.Play();
            }
            
            

            //count how much beads placed on the board
            if(draggable.spriteDragStarPosition != draggable.transform.localPosition)
            {
                beadPlaced -=1 ;
            }


            //see who placed the bead player 1 or player 2
            turnManager(draggable);
                  
        
            //reset bead starter position
            draggable.spriteDragStarPosition = draggable.transform.localPosition;
            


        }
        // return to the initial position if failed to move to a snap point
        else  
        {
        draggable.transform.localPosition = new Vector2(draggable.spriteDragStarPosition.x, draggable.spriteDragStarPosition.y);
        if(gameManager.audioManager.activeSelf == true)
            {
                failSFX.Play();
            }
        }
        gameOver (); 
    }



    public void turnManager(Draggable draggable) 
    {
        if (draggable.gameObject.CompareTag("Player1"))
            {
                currentPlayer = 1;
                turnBase(currentPlayer);
            }
            else if (draggable.gameObject.CompareTag("Player2"))
            {
                currentPlayer=2;
                turnBase(currentPlayer);
            }
    }
    
   


    //manage the turns between players
    private void turnBase(int player)
    {
        if (player == 1)
        {
            foreach(Draggable draggable in draggableBeads)
            {
                if (draggable.gameObject.CompareTag("Player1"))
                {                     
                    draggable.locked = true ;
                }
                else 
                {   
                    draggable.locked = false ; 
                }

            }
        }
        else 
        {
            foreach(Draggable draggable in draggableBeads)
            {
                if (draggable.gameObject.CompareTag("Player2"))
                {
                    draggable.locked = true ;  
                }
                else 
                {  
                    draggable.locked = false ;
                }
            }
 
        }
    }




        // check the x axis to find a winner
        public int checkWinX(float pos)
        {  
            int P1Beads=0 ;
            int P2Beads=0 ;
            int winner=0 ;
            
            if(beadPlaced<=2)
            {  
                foreach(Draggable draggable in draggableBeads)
                {
                    if (draggable.gameObject.CompareTag("Player1") )
                    {
                        if ((draggable.transform.localPosition.x == pos) || (draggable.transform.localPosition.x == pos) || (draggable.transform.localPosition.x == pos))
                            P1Beads+= 1; 
                    }
                    else if (draggable.gameObject.CompareTag("Player2"))
                    {
                       if ((draggable.transform.localPosition.x == pos) || (draggable.transform.localPosition.x == pos) || (draggable.transform.localPosition.x == pos))
                            P2Beads+= 1; 
                    }

                }
                
            }
            if (P1Beads==3)
            {
                winner = 1;
            }
            if (P2Beads==3)
            {
                winner = 2;
            }
            
            return winner;
        }

        // check the y axis to find a winner
        public int checkWinY(float pos)
        {  
            int P1Beads=0 ;
            int P2Beads=0 ;
            int winner=0 ;
            
            //if(beadPlaced<=2)
             
                foreach(Draggable draggable in draggableBeads)
                {
                    if (draggable.gameObject.CompareTag("Player1") )
                    {
                        if ((draggable.transform.localPosition.y == pos) || (draggable.transform.localPosition.y == pos) || (draggable.transform.localPosition.y == pos))
                            P1Beads+= 1; 
                    }
                    else if (draggable.gameObject.CompareTag("Player2"))
                    {
                       if ((draggable.transform.localPosition.y == pos) || (draggable.transform.localPosition.y == pos) || (draggable.transform.localPosition.y == pos))
                            P2Beads+= 1; 
                    }

                }
                
            
            if (P1Beads==3)
            {
                winner = 1;
            }
            if (P2Beads==3)
            {
                winner = 2;
            }
            
            return winner;
        }
        //check the diagonals to find a winner
        public int checkWinXY1()
        {  
            int P1Beads=0 ;
            int P2Beads=0 ;
            int winner=0 ;
            
            //if(beadPlaced<=2)
              
                foreach(Draggable draggable in draggableBeads)
                {
                    if (draggable.gameObject.CompareTag("Player1") )
                    {
                        if ( draggable.transform.localPosition.x == draggable.transform.localPosition.y)
                            P1Beads+= 1; 
                    }
                    else if (draggable.gameObject.CompareTag("Player2"))
                    {
                       if (draggable.transform.localPosition.x == draggable.transform.localPosition.y)
                            P2Beads+= 1; 
                    }

                }
                
            
            if (P1Beads==3)
            {
                winner = 1;
            }
            if (P2Beads==3)
            {
                winner = 2;
            }
            
            return winner;
        }

        public int checkWinXY2()
        {  
            int P1Beads=0 ;
            int P2Beads=0 ;
            int winner=0 ;
            
            
             
                foreach(Draggable draggable in draggableBeads)
                {
                    if (draggable.gameObject.CompareTag("Player1") )
                    {
                        if ( (draggable.transform.localPosition.x + draggable.transform.localPosition.y) == 0 )
                            P1Beads+= 1; 
                    }
                    else if (draggable.gameObject.CompareTag("Player2"))
                    {
                       if ( (draggable.transform.localPosition.x + draggable.transform.localPosition.y) == 0)
                            P2Beads+= 1; 
                    }

                }
                
            
            if (P1Beads==3)
            {
                winner = 1;
            }
            if (P2Beads==3)
            {
                winner = 2;
            }
            
            return winner;
        }

         public void stopAll ()
        {
            foreach (Draggable bead in draggableBeads)
            {
            bead.locked = true ;
            }
        }

        public void resumeGame ()
        {
            foreach (Draggable bead in draggableBeads)
            {
                turnBase(currentPlayer);      
            }
        }

        
        // stops the game and reveal the winner 
        private void gameOver ()
        {
            if ((checkWinX(0f) ==1) || (checkWinX(1.8f) ==1) || (checkWinX(-1.8f) ==1) 
            || ((checkWinY(0f) ==1)|| (checkWinY(1.8f) ==1)|| (checkWinY(-1.8f) ==1)) 
            || ((checkWinXY1()==1) || (checkWinXY2() == 1)) )
            {
                gameOverScreen.setup(1, 3);
                if(gameManager.audioManager.activeSelf == true)
                {
                    gameOverSFX.Play();
                }
                AccountManager.gamesCount +=1;
                AccountManager.gamesWonCount +=1;
                AccountManager.loadStat();
            }
            else if ( ((checkWinX(0f) == 2) || (checkWinX(1.8f) == 2) || (checkWinX(-1.8f) == 2)) 
            || ((checkWinY(0f) == 2) || (checkWinY(1.8f) == 2) || (checkWinY(-1.8f) == 2)) 
            || ((checkWinXY1() == 2) || (checkWinXY2() == 2)) )
            {
                gameOverScreen.setup(2, 3);
                if(gameManager.audioManager.activeSelf == true)
                {
                    gameOverSFX.Play();
                }
                AccountManager.gamesCount +=1;
                AccountManager.loadStat();
            }
            

        }
   

}















































    /* public Draggable LastDragged => lastDragged ;
    private bool isDragActive = false;
    private Vector2 screenPosition;
    private Vector3 worldPosition;
    private Draggable lastDragged ;
    

    void Awake()
    {
        DragController[] controllers = FindObjectsOfType<DragController>();
        if (controllers.Length > 1)
        {
            Destroy(gameObject);
        }
    }

   
    void Update()
    {
        if(isDragActive) 
        {
            if (Input.GetMouseButtonUp(0) || (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended)) 
            {
                Drop() ;
                return ;
            }
        }
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Input.mousePosition;
            screenPosition = new Vector2(mousePos.x, mousePos.y);
        }
        else if  (Input.touchCount > 0)
        {
        screenPosition = Input.GetTouch(0).position ;
        }
        else  
        {
        return ;
        }
        worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        if (isDragActive) {
            Drag();
        }
        else {
            RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);
            if (hit.collider != null)
            {
                Draggable draggable = hit.transform.gameObject.GetComponent<Draggable>();
                if (draggable != null)
                {
                    lastDragged = draggable;
                    InitDrag();
                }
            }

        }


    }

    void InitDrag() 
    {
       UpdateDragStatus(true) ;
    }
    void Drag ()
    {
        lastDragged.transform.position = new Vector2( worldPosition.x, worldPosition.y);
    }
    void Drop () 
    {
       UpdateDragStatus(false) ;
    }

    void UpdateDragStatus(bool isDragging) 
    {
        isDragActive = lastDragged.IsDragging = isDragging;
        if (isDragging) 
        {
            lastDragged.gameObject.layer = Layers.Dragging ; 
        }
        else
            {
                lastDragged.gameObject.layer = Layers.Default ;
            }
    }*/




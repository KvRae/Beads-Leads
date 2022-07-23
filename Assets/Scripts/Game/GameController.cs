using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    //list of snap point to be on our points
    public List<Transform> snapPoints;
    //list of beads that will be placed on the board
    public List<Draggable> draggableBeads;
    public List<CpuPlayer> cpuBeads;
    //the range that lets bead get into the snap point
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
    private void onDragEnded(Draggable draggable)
    {
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
        if(closestSnapPoint != null && closestDistance <= snapRange && draggable.notValid == false )//&& !checkMove(draggable)== true 
        {
            
            
            draggable.transform.localPosition =  closestSnapPoint.localPosition;
            if(gameManager.audioManager.activeSelf == true)
            {
                dragSFX.Play();
            }
             

            
            foreach(CpuPlayer bot in cpuBeads)
            {
                if(bot.gameObject.CompareTag("CPU") && (bot.gameObject.name == draggable.gameObject.name))
                {
                    bot.cpuMove(moveAi (bot));
                    break;
                }
            }
            
            

            //count how much beads placed on the board
            if(draggable.spriteDragStarPosition != draggable.transform.localPosition)
            {
                beadPlaced -=1 ;
            }

            //reset bead starter position
            draggable.spriteDragStarPosition = draggable.transform.localPosition;

            


        }
        // return to the initial position if faild to move to a snap point
        else  
        {
        draggable.transform.localPosition = new Vector2(draggable.spriteDragStarPosition.x, draggable.spriteDragStarPosition.y);
        if(gameManager.audioManager.activeSelf == true)
        {
            failSFX.Play();
        }
        }
        gameOver(); 
    }

    private bool checkValid (Transform pos)
    {
        int notValid = 0 ;
        bool valid = true ;
        foreach (Draggable bead in draggableBeads)
        {
            if (bead.transform.localPosition == pos.localPosition)
            {
                notValid += 1 ;
            }
        }
        foreach (CpuPlayer cpu in cpuBeads)
        {
            if (cpu.transform.localPosition == pos.localPosition)
            {
                notValid +=1;
            }
        }

        if (notValid >0)
        {
            valid = false ;
        }
        return valid ;
    }

    private Transform moveAi (CpuPlayer cpu)
    {
        Transform posCpu= cpu.transform; 
        foreach (Transform snap in snapPoints)
        {
            if(checkValid(snap) == true && snap.localPosition != cpu.initPos && cpu.locked == false)
                posCpu.localPosition = snap.localPosition ;
        }
        return posCpu ;

    }





        // check the x axis to find a winner
        public int checkWinX(float pos)
        {  
            int P1Beads=0 ;
            int P2Beads=0 ;
            int winner=0 ;
            
            //if(beadPlaced<=2)
             
                foreach(Draggable draggable in draggableBeads)
                {
                    if (draggable.gameObject.CompareTag("Player1") )
                    {
                        if ((draggable.transform.localPosition.x == pos) || (draggable.transform.localPosition.x == pos) || (draggable.transform.localPosition.x == pos))
                            P1Beads+= 1; 
                    }
                }

                 foreach(CpuPlayer cpu in cpuBeads)
                {
                    if (cpu.gameObject.CompareTag("CPU") )
                    {
                        if ((cpu.transform.localPosition.x == pos) || (cpu.transform.localPosition.x == pos) || (cpu.transform.localPosition.x == pos))
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
                }
                foreach(CpuPlayer cpu in cpuBeads)
                {
                    if (cpu.gameObject.CompareTag("CPU") )
                    {
                        if ((cpu.transform.localPosition.y == pos) || (cpu.transform.localPosition.y == pos) || (cpu.transform.localPosition.y == pos))
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
                }

                foreach(CpuPlayer cpu in cpuBeads)
                {
                    if (cpu.gameObject.CompareTag("CPU") )
                    {
                        if (cpu.transform.localPosition.y == cpu.transform.localPosition.x )
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
                }

                foreach(CpuPlayer cpu in cpuBeads)
                {
                    if (cpu.gameObject.CompareTag("CPU") )
                    {
                        if ((cpu.transform.localPosition.x + cpu.transform.localPosition.y)==0 )
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
            foreach (CpuPlayer cpu in cpuBeads)
            {
                cpu.locked = true ; 
            }

        }

        public void resumeGame ()
        {
            foreach (Draggable bead in draggableBeads)
            {
                bead.locked = false ;        
            }
            foreach (CpuPlayer cpu in cpuBeads)
            {
                cpu.locked = false ;        
            }            

        }

        
        // stops the game and reveal the winner 
        private void gameOver ()
        {
            if ((checkWinX(0f) ==1) || (checkWinX(1.8f) ==1) || (checkWinX(-1.8f) ==1) 
            || ((checkWinY(0f) ==1)|| (checkWinY(1.8f) ==1)|| (checkWinY(-1.8f) ==1)) 
            || ((checkWinXY1()==1) || (checkWinXY2() == 1)) )
            {
                gameOverScreen.setup(1,2);
                stopAll();
                if(gameManager.audioManager.activeSelf == true)
                {
                    gameOverSFX.Play();
                }
                AccountManager.gamesCount +=1;
                AccountManager.gamesWonCount +=1;
                AccountManager.setStat();
            }
            else if ( ((checkWinX(0f) == 2) || (checkWinX(1.8f) == 2) || (checkWinX(-1.8f) == 2)) 
            || ((checkWinY(0f) == 2) || (checkWinY(1.8f) == 2) || (checkWinY(-1.8f) == 2)) 
            || ((checkWinXY1() == 2) || (checkWinXY2() == 2)) )
            {
                gameOverScreen.setup(2,2);
                stopAll();
                if(gameManager.audioManager.activeSelf == true)
                {
                    gameOverSFX.Play();
                }
                AccountManager.gamesCount +=1;
            }
            

        }
   

}
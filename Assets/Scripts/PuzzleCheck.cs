using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleCheck : MonoBehaviour

       
{    
    void OnCollisionEnter(Collision puzzleBox)
    {
        Debug.Log("Puzzle 2 Complete");
        Destroy(this);
    }
}
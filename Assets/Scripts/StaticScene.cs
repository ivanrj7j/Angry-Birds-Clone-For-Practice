using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class StaticScene{

    public static string next;
    public static string current;
    public static void setNextLevel(string nextLevel){
        next = nextLevel;
    }

    public static void setCurrentLevel(string currentLevel){
        current = currentLevel;
    }

    public static string getNextLevel(){
        return next;
    }
    public static string getCurrentLevel(){
        return current;
    }
}
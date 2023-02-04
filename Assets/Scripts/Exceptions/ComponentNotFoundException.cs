using System;
using UnityEngine;

class ComponentNotFoundException : Exception {
    public ComponentNotFoundException(string errorMessage) 
    { 
        Debug.Log(errorMessage);
    }
}
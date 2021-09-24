using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Stof", menuName = "ScriptableObjects/Stof", order = 1)]
public class Stofinformatiehouder : ScriptableObject
    {

        public string Naam;
        public float Brandpunt;
        public float Kookpunt;
        public Color Kleur;
        public bool Vloeistof;
        public bool zuur;
        public bool explosief;

    
}

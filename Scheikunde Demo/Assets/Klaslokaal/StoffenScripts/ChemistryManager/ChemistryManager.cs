using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Chemistry/ChemistryManager")]
class ChemistryManager : ScriptableObject
{
    [System.Serializable]
    public class Recept
    {
        public Stofinformatiehouder IngredientA, IngredientB;
        public Stofinformatiehouder Result;
    }

    public Recept[] recepten;

    public Stofinformatiehouder Mix(Stofinformatiehouder a, Stofinformatiehouder b)
    {
        //Look up recept with a and b, return that one!
        //You can fill in `recepten` in the editor, lets say you put in 3 different ones
        //This foreach will make it go through each one, so it runs 3 times.
        //Every time `recept` points to a different one.
        foreach (var recept in recepten)
        {

            bool isIngredientAEqual = recept.IngredientA == a;
            bool isIngredientBEqual = recept.IngredientB == b;

            //Debug.Log($"{recept.IngredientA} == {a} : {isIngredientAEqual}");
            //Debug.Log($"{recept.IngredientB} == {b} : {isIngredientBEqual}");

            if (isIngredientAEqual && isIngredientBEqual)
            {
                QuestManager.instance.SendObjectiveInputToQuests(Objectives.Reaction); //Sends a quest objective//
                QuestManager.instance.SendObjectiveInputToQuests(Objectives.SpecificResult, recept.Result); //Sends a quest objective//
                return recept.Result;
            }

            //if so; return recept.Result;

            //TODO: Make these true if the recept.ingredient is the same as what we put in the mix method
            

        }
        //ohoh! Nothing was found at all, what do we do now?

        if(a != null)
        {
            return a;
        }else if(b != null)
        {
            return b;
        }
        else
        {
            return null;
        }
    }
}
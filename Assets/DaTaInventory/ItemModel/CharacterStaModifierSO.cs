using UnityEngine;
using UnityEngine.TextCore.Text;

public abstract class CharacterStaModifierSO : ScriptableObject
{
    public abstract void AffectCharacter(GameObject character, float val);
   
}

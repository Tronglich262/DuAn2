using UnityEngine;

[CreateAssetMenu]
public class CharacterStaHealthModifierSO : CharacterStaModifierSO
{
    public override void AffectCharacter(GameObject character, float val)
    {
        PlayerHealth health = character.GetComponent<PlayerHealth>();
        if (health != null)
        {
            if (val > 0)
            {
                health.IncreaseHealth((int)val); // Hồi máu nếu `val` > 0
            }
            else
            {
                health.TakeDamage(Mathf.Abs((int)val)); // Gây sát thương nếu `val` < 0
            }
        }
    }
}
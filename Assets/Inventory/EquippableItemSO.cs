using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

namespace Inventory.Model
{
    [CreateAssetMenu]
    public class EquippableItemSO : ItemSO, IDestroyableItem, IItemAction
    {
        public string ActionName => "Equip";
        [field: SerializeField]
        public AudioClip actionSFX { get; private set; }

        public bool PerforAction(GameObject character, List<ItemParameter> itemState = null)
        {
            throw new System.NotImplementedException();
        }
    }
}

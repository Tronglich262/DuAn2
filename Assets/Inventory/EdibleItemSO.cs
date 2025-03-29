using System;
using System.Collections.Generic;
using Inventory.Model;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

namespace Inventory.Model
{
    [CreateAssetMenu]
    public class EdibleItemSO : ItemSO, IDestroyableItem , IItemAction
    {
        [SerializeField]
        private List<ModifierData> modifiersData = new List<ModifierData>();
        public string ActionName => "Consume";
        [field: SerializeField]
        public AudioClip actionSFX { get; private set; }
        public bool PerforAction(GameObject character, List<ItemParameter> itemState = null)
        {
            foreach (ModifierData data in modifiersData)
            {
                data.statModifier.AffectCharacter(character, data.value);
            }

            return true;
        }
    }

    public interface IDestroyableItem
    {
    }

    public interface IItemAction
    {
        public string ActionName { get; }
        public AudioClip actionSFX { get; }
        bool PerforAction(GameObject character,List<ItemParameter> itemState);
    }
   [Serializable]
    public class ModifierData
    {
        public CharacterStaModifierSO statModifier;
        public float value;
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created

}


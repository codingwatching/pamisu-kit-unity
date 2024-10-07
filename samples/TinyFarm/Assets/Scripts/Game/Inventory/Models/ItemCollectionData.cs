﻿using System.Collections.Generic;
using Game.Framework;

namespace Game.Inventory.Models
{
    public class ItemCollectionData : ISerializee
    {
        public string Id;
        public List<Item> Items;
        
        public void PreSerialize()
        {
        }

        public void PostDeserialize()
        {
            Items ??= new List<Item>();
        }
    }
    
}
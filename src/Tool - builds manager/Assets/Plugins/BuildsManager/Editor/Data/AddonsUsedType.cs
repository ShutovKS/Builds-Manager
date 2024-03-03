using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace BuildsManager.Data
{
    [Serializable]
    public class AddonsUsedData : ScriptableObject
    {
        public List<AddonUsed> addonsUsed;
    }
    
    [Serializable]
    public class AddonUsed
    {
        public bool isUsed;
        public string name;
        public string[] defines;
    }
}
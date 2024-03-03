using System;
using System.Collections.Generic;
using UnityEditor;

namespace BuildsManager.Data
{
    [Serializable]
    public class BuildData : ICloneable
    {
        public bool isEnabled = true;
        public bool isCompress = false;

        public string buildPath = "";

        public List<AddonUsed> addonsUsed;
        public BuildOptions options = BuildOptions.None;
        public BuildTarget target = BuildTarget.NoTarget;
        public BuildTargetGroup targetGroup = BuildTargetGroup.Unknown;

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
using System;
using System.Collections.Generic;
using UnityEngine;

namespace BuildsManager.Data
{
    [Serializable]
    public class GeneralBuildData : ScriptableObject, ICloneable
    {
        public bool isReleaseBuild = false;
        public bool isNeedZip = false;

        public List<BuildData> builds = new();

        public string generalScriptingDefineSymbols;

        public string outputRoot;
        public string middlePath;
        public string dirPathForPostProcess;

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
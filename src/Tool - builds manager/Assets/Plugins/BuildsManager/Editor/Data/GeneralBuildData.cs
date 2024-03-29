using System;
using System.Collections.Generic;
using UnityEngine;

namespace BuildsManager.Data
{
    [Serializable]
    public class GeneralBuildData : ScriptableObject, ICloneable
    {
        public bool isReleaseBuild = false;

        public List<BuildData> builds = new(1);
        
        public AddonsUsedData addonsUsedData;
        public string generalScriptingDefineSymbols;

        public string outputRoot = "Builds/";
        public string middlePath = "$NAME_$VERSION_$PLATFORM/$NAME_$VERSION/$NAME$EXECUTABLE";
        public string dirPathForPostProcess = "$NAME_$VERSION_$PLATFORM";

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
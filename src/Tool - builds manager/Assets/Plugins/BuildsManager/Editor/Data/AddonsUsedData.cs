using System;
using System.Collections.Generic;
using UnityEngine;

namespace BuildsManager.Data
{
    [Serializable]
    public class AddonsUsedData : ScriptableObject
    {
        [field: SerializeField] public List<AddonUsedDetailed> AddonsUsed { get; set; }

        private void OnValidate()
        {
            FixNullName();
            CheckDuplicate();
        }

        private void FixNullName()
        {
            for (var i = 0; i >= 0; i--)
            {
                if (!string.IsNullOrEmpty(AddonsUsed[i].Name))
                {
                    continue;
                }

                AddonsUsed[i] = new AddonUsedDetailed
                {
                    Name = "Default",
                    Defines = AddonsUsed[i].Defines
                };
            }
        }

        private void CheckDuplicate()
        {
            for (var i = AddonsUsed.Count - 1; i >= 0; i--)
            {
                for (var ii = i - 1; ii >= 0; ii--)
                {
                    if (AddonsUsed[i].Name != AddonsUsed[ii].Name)
                    {
                        continue;
                    }

                    AddonsUsed[ii] = new AddonUsedDetailed
                    {
                        Name = $"{AddonsUsed[i].Name}_Duplicate",
                        Defines = AddonsUsed[i].Defines
                    };
                }
            }
        }

        public void AddNewAddon()
        {
            var newAddon = new AddonUsedDetailed
            {
                Name = "Default",
                Defines = Array.Empty<string>()
            };

            AddonsUsed.Add(newAddon);
        }
    }

    [Serializable]
    public struct AddonUsedDetailed : IAddonUsed
    {
        [field: SerializeField] public string Name { get; set; }
        [field: SerializeField] public string[] Defines { get; set; }
    }

    [Serializable]
    public struct AddonUsedInformation : IAddonUsed
    {
        [field: SerializeField] public bool IsUsed { get; set; }
        [field: SerializeField] public string Name { get; set; }
    }

    public interface IAddonUsed
    {
        string Name { get; }
    }
}
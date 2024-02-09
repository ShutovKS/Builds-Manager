using BuildsManager.Data;

namespace BuildsManager.Tools
{
    public static class AddonsUsed
    {
        public static string GetScriptingDefineSymbols(AddonsUsedType type)
        {
            return type switch
            {
                AddonsUsedType.None => string.Empty,
                _ => string.Empty
            };
        }

        public static string GetAddonName(AddonsUsedType type)
        {
            return type switch
            {
                AddonsUsedType.None => string.Empty,
                _ => string.Empty
            };
        }
    }
}
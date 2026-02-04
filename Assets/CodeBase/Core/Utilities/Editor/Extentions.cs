using System.Text.RegularExpressions;
using UnityEditor;

namespace Core.Utilities.Extentions
{
    public static class Extentions
    {
        public static int GetArrayIndex(this SerializedProperty property)
        {
            Match match = Regex.Match(property.propertyPath, @"\[(\d+)\]");

            if (match.Success)
            {
                return int.Parse(match.Groups[1].Value);
            }

            return 0;
        }
    }
}
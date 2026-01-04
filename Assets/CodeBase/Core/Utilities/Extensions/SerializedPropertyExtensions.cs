using System.Text.RegularExpressions;
using UnityEditor;

namespace Core.Utilities.Extensions
{
    public static class SerializedPropertyExtensions
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
using Xamarin.Forms;

namespace Waves.UI.Xamarin.Extensions
{
    /// <summary>
    /// Tools for resources.
    /// </summary>
    public static class ApplicationExtensions
    {
        /// <summary>
        /// Adds resource to framework element.
        /// </summary>
        /// <param name="application">Application.</param>
        /// <param name="dictionary">Resource dictionary.</param>
        public static void AddResource(this Application application, ResourceDictionary dictionary)
        {
            var resources = application.Resources;

            var dictionaries = resources.MergedDictionaries;

            dictionaries.Add(dictionary);
        }
    }
}

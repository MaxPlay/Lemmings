using Lemmings.Exceptions;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml;
using System;

namespace Lemmings.Localization
{
    public static class Localizer
    {
        #region Private Fields

        private static string currentCulture;

        private static string fallback;

        private static Dictionary<string, CultureInfo> installedCultures;

        private static Dictionary<string, Dictionary<string, string>> localizations;

        #endregion Private Fields

        #region Public Properties

        public static CultureInfo CurrentCulture
        {
            get { return installedCultures[currentCulture]; }
        }

        public static string Fallback
        {
            get
            {
                return fallback;
            }

            set
            {
                fallback = value;
            }
        }

        public static string[] InstalledCultures { get { return installedCultures.Keys.ToArray(); } }

        #endregion Public Properties

        #region Public Methods

        public static void LoadLocalization()
        {
            installedCultures = new Dictionary<string, CultureInfo>();
            localizations = new Dictionary<string, Dictionary<string, string>>();
            string[] files = Directory.GetFiles(Game1.ApplicationDirectory + "\\Localization");

            foreach (string file in files)
            {
                if (Path.GetExtension(file) != ".xml")
                    continue;

                string culture = Path.GetFileNameWithoutExtension(file);
                installedCultures.Add(culture, new CultureInfo(culture));
                localizations.Add(culture, ParseXMLStrings(file));
            }

            if (installedCultures.ContainsKey("en-US"))
                currentCulture = "en-US";
            else if (installedCultures.Count > 0)
                currentCulture = installedCultures.Keys.FirstOrDefault();
            else
                currentCulture = CultureInfo.InvariantCulture.Name;
        }

        #endregion Public Methods

        #region Private Methods

        public static string GetString(string identifier)
        {
            if (!localizations.ContainsKey(currentCulture))
                throw new LocalizationNotFoundException(currentCulture);

            if (!localizations[currentCulture].ContainsKey(identifier))
                return fallback;

            return localizations[currentCulture][identifier];
        }

        private static Dictionary<string, string> ParseXMLStrings(string file)
        {
            Dictionary<string, string> strings = new Dictionary<string, string>();

            using (FileStream stream = File.OpenRead(file))
            {
                using (XmlReader reader = XmlReader.Create(stream))
                {
                    while (reader.Read())
                    {
                        if (reader.NodeType == XmlNodeType.Element)
                            if (reader.Name == "string")
                            {
                                if (reader.IsEmptyElement)
                                    continue;

                                string key = reader.GetAttribute("name");
                                reader.Read();
                                string value = reader.Value;
                                strings.Add(key, value);
                            }
                    }
                }
            }

            return strings;
        }

        public static void ChangeCulture(string identifier)
        {
            if (installedCultures.ContainsKey(identifier))
            {
                currentCulture = identifier;
                OnCultureChanged();
            }
        }

        private static void OnCultureChanged()
        {
            if (CultureChanged != null)
                CultureChanged(currentCulture);
        }

        public delegate void CultureChangedHandler(string culture);
        public static event CultureChangedHandler CultureChanged;

        #endregion Private Methods
    }
}
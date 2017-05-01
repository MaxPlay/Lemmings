using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Lemmings
{
    [Serializable()]
    public class Settings
    {
        #region Public Fields

        [XmlElement("User")]
        public User UserSettings;

        [XmlElement("Video")]
        public Video VideoSettings;

        #endregion Public Fields

        #region Private Fields

        private static Settings instance;

        #endregion Private Fields

        #region Public Constructors

        public Settings()
        {
            VideoSettings = Video.Default();
            UserSettings = User.Default();
        }

        #endregion Public Constructors

        #region Public Enums

        public enum UserInput
        {
            Mouse,
            Keyboard,
            GamePad
        }

        #endregion Public Enums

        #region Public Properties

        [XmlIgnore()]
        public static Settings Instance
        {
            get
            {
                if (instance == null)
                    instance = new Settings();
                return instance;
            }
        }

        #endregion Public Properties

        #region Public Methods

        public void Load()
        {
            if (!File.Exists("settings.xml"))
            {
                Save();
                return;
            }

            XmlSerializer deserializer = new XmlSerializer(typeof(Settings));
            using (TextReader reader = new StreamReader("settings.xml"))
            {
                instance = (Settings)deserializer.Deserialize(reader);
            }

            Input.ChangeCursorVisiblity(instance.UserSettings.Input == UserInput.Mouse);
        }

        public void Save()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Settings));
            using (TextWriter writer = new StreamWriter("settings.xml"))
            {
                serializer.Serialize(writer, this);
            }
        }

        #endregion Public Methods

        #region Public Structs

        public struct User
        {
            #region Public Fields

            [XmlElement("InputType")]
            public UserInput Input;

            #endregion Public Fields

            #region Public Methods

            public static User Default()
            {
                return new User() { Input = UserInput.Mouse };
            }

            #endregion Public Methods
        }

        public struct Video
        {
            #region Public Fields

            [XmlAttribute]
            public uint Height;

            [XmlAttribute]
            public uint Width;

            #endregion Public Fields

            #region Public Methods

            public static Video Default()
            {
                return new Video() { Width = 800, Height = 600 };
            }

            #endregion Public Methods
        }

        #endregion Public Structs
    }
}
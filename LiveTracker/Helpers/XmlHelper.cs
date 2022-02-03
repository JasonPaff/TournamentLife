using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Xml;
using System.Xml.Schema;
using Tournament_Life.ViewModels;
using Tournament_Life.Views;

namespace Tournament_Life.Helpers
{

    public static class XmlHelper
    {
        public static readonly string SchemaTargetNameSpace = "Schemas";
        public static readonly string ProfilesFileName = "Profiles.xml";
        public static readonly string DataPrefsFileName = "DataPrefs.xml";
        public static readonly string RunningsPrefsFileName = "RunningPrefs.xml";
        public static readonly string RunningTournamentsFileName = "RunningTournaments.xml";
        public static readonly string RecentTournamentsFileName = "RecentTournaments.xml";
        public static readonly string SessionDataViewPrefsFileName = "SessionDataViewPrefs.xml";
        public static readonly string SessionsFileName = "Sessions.xml";
        public static readonly string TemplatesFileName = "Templates.xml";
        public static readonly string TemplateDataFileName = "TemplateData.xml";
        public static readonly string TournamentDataViewPrefsFileName = "TournamentDataViewPrefs.xml";
        public static readonly string ViewPrefsFileName = "ViewPrefs.xml";
        public static readonly string FiltersFileName = "Filters.xml";
        public static readonly string PreferencesFileName = "Preferences.xml";
        public static readonly string BankrollFileName = "Bankroll.xml";
        public static readonly string PreferencesFolderName = "Preferences\\";
        public static void Validate()
        {
            try
            {

            }
            catch (Exception exception)
            {

                // ok view model
                var theVm = new OkViewModel(exception.Message, "Error");

                // create/show ok window
                var theWindow = new OkView(theVm)
                {
                    Owner = Application.Current.MainWindow,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                theWindow.ShowDialog();

                throw;
            }
        }

        public static string ExtractNumber(string original)
        {
            return new string(original.Where(char.IsDigit).ToArray());
        }

        public static XmlDocument LoadXmlFile(string fileName, string schemaName = null)
        {
            if (File.Exists(fileName) is false) return null;

            var xmlDocument = new XmlDocument();
            XmlReaderSettings settings;

            if (!(schemaName is null))
            {
                settings = new XmlReaderSettings()
                {
                    ValidationType = ValidationType.Schema,
                };
                settings.Schemas.Add(XmlHelper.SchemaTargetNameSpace, schemaName);
                settings.ValidationEventHandler += XmlHelper.ValidationCallBack;
            }
            else
            {
                settings = new XmlReaderSettings();
            }

            try
            {
                var reader = XmlReader.Create(fileName, settings);
                xmlDocument.Load(reader);
                reader.Dispose();
            }
            catch (Exception exception)
            {
                // ok view model
                var theVm = new OkViewModel(exception.Message, "Error");

                // create/show ok window
                var theWindow = new OkView(theVm)
                {
                    Owner = Application.Current.MainWindow,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                theWindow.ShowDialog();

                throw;
            }

            return xmlDocument;
        }

        public static void ValidationCallBack(object sender, ValidationEventArgs args)
        {
            if (args is null) return;

            if (args.Severity == XmlSeverityType.Warning)
            {
                // ok view model
                var theVm = new OkViewModel("Warning: Matching schema not found.  No validation occurred." + args.Message, "Error");

                // create/show ok window
                var theWindow = new OkView(theVm)
                {
                    Owner = Application.Current.MainWindow,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                theWindow.ShowDialog();
            }
            else
            {
                // ok view model
                var theVm = new OkViewModel("Validation error: " + args.Message, "Error");

                // create/show ok window
                var theWindow = new OkView(theVm)
                {
                    Owner = Application.Current.MainWindow,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                theWindow.ShowDialog();
            }
        }
    }
}

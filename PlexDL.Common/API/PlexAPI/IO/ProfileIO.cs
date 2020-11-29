using PlexDL.Common.Logging;
using PlexDL.Common.Structures.AppOptions;
using System;
using System.IO;
using System.Xml.Serialization;
using UIHelpers;

// ReSharper disable InconsistentNaming

namespace PlexDL.Common.API.PlexAPI.IO
{
    public static class ProfileIO
    {
        public static ApplicationOptions ProfileFromFile(string fileName, bool silent = false)
        {
            try
            {
                if (File.Exists(fileName))
                    return File.ReadAllText(fileName).ProfileFromXml();
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, "LoadProfileError");
                if (!silent)
                    UIMessages.Error(ex.ToString(), @"XML Load Error");
            }

            //default
            return null;
        }

        public static void ProfileToFile(string fileName, ApplicationOptions options, bool silent = false)
        {
            try
            {
                //delete the existing file if there is one; the user was asked if they wanted to replace it.
                if (File.Exists(fileName))
                    File.Delete(fileName);

                //write the profile to the XML file
                File.WriteAllText(fileName, options.ProfileToXml());
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, "@SaveProfileError");
                if (!silent)
                    UIMessages.Error(ex.ToString(), @"XML Save Error");
            }
        }

        public static string ProfileToXml(this ApplicationOptions options)
        {
            try
            {
                var xsSubmit = new XmlSerializer(typeof(ApplicationOptions));

                using (var sww = new StringWriter())
                {
                    xsSubmit.Serialize(sww, options);

                    return sww.ToString();
                }
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, @"ProfileToXmlError");
            }

            //default
            return @"";
        }

        public static ApplicationOptions ProfileFromXml(this string optionsXml)
        {
            try
            {
                var serializer = new XmlSerializer(typeof(ApplicationOptions));
                var reader = new StringReader(optionsXml);
                var subReq = (ApplicationOptions)serializer.Deserialize(reader);

                reader.Close();

                return subReq;
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, "ProfileFromXmlError");
            }

            //default
            return null;
        }
    }
}
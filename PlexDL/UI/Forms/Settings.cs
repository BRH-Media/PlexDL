using PlexDL.Common;
using PlexDL.Common.Components.Forms;
using PlexDL.Common.Extensions;
using PlexDL.Common.Globals.Providers;
using PlexDL.Common.Logging;
using PlexDL.Common.Structures.AppOptions;
using System;
using System.Windows.Forms;
using UIHelpers;

// ReSharper disable InvertIf

#pragma warning disable 1591

namespace PlexDL.UI.Forms
{
    public partial class Settings : DoubleBufferedForm
    {
        //this does not change...even when the original object does. This is a snapshot to revert
        //changes made in this form.
        private ApplicationOptions Snapshot { get; } = ObjectProvider.Settings.DeepClone();

        //used to prevent a call-stack overflow when closing the form
        private bool AlreadyClosing { get; set; }

        //used to prevent a settings revert when the user clicks 'OK'
        private bool ChangesApplied { get; set; }

        public Settings()

            //Windows Forms intialiser
            => InitializeComponent();

        private void Settings_Load(object sender, EventArgs e)

            =>  //assign grid to global settings provider
                settingsGrid.SelectedObject = ObjectProvider.Settings;

        private void BtnCancel_Click(object sender, EventArgs e)

            //perform cancel operation
            => DoCancel();

        private void BtnApply_Click(object sender, EventArgs e)

            //perform apply operation
            => DoApplySettings();

        private void ItmCommitToDefault_Click(object sender, EventArgs e)

            //perform commit to default operation
            => DoCommitDefault();

        private void ItmReset_Click(object sender, EventArgs e)

            //perform reset operation
            => DoReset();

        private void Settings_Closing(object sender, FormClosingEventArgs e)

            //perform cancel operation
            => DoCancel();

        private void ItmCreateAssociations_Click(object sender, EventArgs e)

            //perform create file associations operation
            => DoCreateAssociations();

        private void DoApplySettings()
        {
            try
            {
                //disable reverting
                ChangesApplied = true;

                //disable cancel operation
                AlreadyClosing = true;

                //ensure a correct DialogResult of 'OK'
                DialogResult = DialogResult.OK;

                //close the form
                Close();
            }
            catch (Exception ex)
            {
                //log the error
                LoggingHelpers.RecordException(ex.Message, @"SettingsDialogApplyError");
            }
        }

        private void DoCancel()
        {
            try
            {
                if (!AlreadyClosing && !ChangesApplied)
                {
                    //revert changes by assigning the snapshot taken at the start to the main setting
                    //provider.
                    ObjectProvider.Settings = Snapshot;

                    //return of 'Cancel'
                    DialogResult = DialogResult.Cancel;

                    //disable further calls
                    AlreadyClosing = true;

                    //close the form
                    Close();
                }
            }
            catch (Exception ex)
            {
                //record error
                LoggingHelpers.RecordException(ex.Message, @"SettingsDialogCancelError");
            }
        }

        private static void DoCommitDefault()
        {
            try
            {
                //null validation
                if (ObjectProvider.Settings != null)
                {
                    //returns true if the commit operation succeeded
                    if (ObjectProvider.Settings.CommitDefaultSettings())

                        //alert user
                        UIMessages.Info(@"Successfully saved settings");
                    else

                        //alert user
                        UIMessages.Error(@"An unknown error occurred whilst saving settings");
                }
                else

                    //alert user
                    UIMessages.Error(@"Couldn't export settings because they were null");
            }
            catch (Exception ex)
            {
                //record error
                LoggingHelpers.RecordException(ex.Message, @"SaveDefaultError");

                //alert user
                UIMessages.Error($"Error exporting to default\n\n{ex}");
            }
        }

        private void DoReset()
        {
            try
            {
                //query user
                if (UIMessages.Question(@"Are you sure? This will clear all settings in the current session."))
                {
                    //do the reset
                    ObjectProvider.Settings = new ApplicationOptions();

                    //refresh PropertyGrid on this form
                    settingsGrid.SelectedObject = ObjectProvider.Settings;
                    settingsGrid.Refresh();

                    //show alert
                    UIMessages.Info(@"Settings reset");
                }
            }
            catch (Exception ex)
            {
                //record error
                LoggingHelpers.RecordException(ex.Message, @"ResetSettingsError");

                //alert user
                UIMessages.Error($"Error while resetting\n\n{ex}");
            }
        }

        private static void DoCreateAssociations()
        {
            try
            {
                //query user
                if (UIMessages.Question(
                    "You are about to create PlexDL file associations for:\n\n*.pxz\n*.pmxml\n*.prof\n\nProceed?"))
                {
                    //create the file associations
                    FileAssociationsManager.EnsureAssociationsSet();

                    //alert user to success
                    UIMessages.Info(@"Done!");
                }
            }
            catch (Exception ex)
            {
                //record error
                LoggingHelpers.RecordException(ex.Message, @"FileAssociationError");

                //alert user
                UIMessages.Error($"Error whilst trying to set PlexDL file associations:\n\n{ex}");
            }
        }
    }
}
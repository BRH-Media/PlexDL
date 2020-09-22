using PlexDL.Common;
using PlexDL.Common.Extensions;
using PlexDL.Common.Globals.Providers;
using PlexDL.Common.Logging;
using PlexDL.Common.Structures.AppOptions;
using System;
using System.Windows.Forms;
using UIHelpers;

namespace PlexDL.UI
{
    public partial class Settings : Form
    {
        //this does not change...even when the original object does. This is a snapshot to revert
        //changes made in this form.
        private ApplicationOptions Snapshot { get; } = ObjectProvider.Settings.DeepClone();

        public Settings()
        {
            InitializeComponent();
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            settingsGrid.SelectedObject = ObjectProvider.Settings;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            //revert changes by assigning the snapshot taken at the start to the main setting
            //provider.
            ObjectProvider.Settings = Snapshot;

            //exit and close with a return of 'Cancel'
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void ItmCommitToDefault_Click(object sender, EventArgs e)
        {
            try
            {
                if (ObjectProvider.Settings != null)
                {
                    ObjectProvider.Settings.CommitDefaultSettings();
                    UIMessages.Info(@"Successfully saved settings");
                }
                else
                    UIMessages.Error(@"Couldn't export settings because they were null");
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, @"SaveDefaultError");
                UIMessages.Error($"Error exporting to default\n\n{ex}");
            }
        }

        private void ItmReset_Click(object sender, EventArgs e)
        {
            try
            {
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
                LoggingHelpers.RecordException(ex.Message, @"ResetSettingsError");
                UIMessages.Error($"Error while resetting\n\n{ex}");
            }
        }

        private void ItmCreateAssociations_Click(object sender, EventArgs e)
        {
            try
            {
                if (UIMessages.Question(
                    "You are about to create PlexDL file associations for:\n\n*.pxz\n*.pmxml\n*.prof\n\nProceed?"))
                {
                    FileAssociationManager.EnsureAssociationsSet();
                    UIMessages.Info(@"Done!");
                }
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, @"FileAssociationError");
                UIMessages.Error($"Error whilst trying to set PlexDL file associations:\n\n{ex}");
            }
        }
    }
}
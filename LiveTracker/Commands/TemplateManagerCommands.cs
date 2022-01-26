using Syncfusion.UI.Xaml.Utility;
using System.Linq;
using System.Windows.Input;
using System.Windows;
using LiveTracker.Views.Template_Manager_Views;
using LiveTracker.ViewModels.Template_Manager_ViewModels;
using LiveTracker.Helpers;
using LiveTracker.Models.Tournaments;
using System.Collections.Generic;
using LiveTracker.Models;
using LiveTracker.Views;
using LiveTracker.ViewModels.Menu_ViewModels;
using LiveTracker.Enums;
using LiveTracker.ViewModels.Datagrid_ViewModels;
using LiveTracker.Views.Session_Manager_Views;
using LiveTracker.ViewModels.Session_Manager_ViewModels;
using Syncfusion.UI.Xaml.Grid;
using LiveTracker.Views.Menu_Views;
using Tournament_Life.ViewModels;
using Tournament_Life.Views;

namespace LiveTracker.Commands
{
    public static class TemplateManagerCommands
    {
        public static ICommand MenuItem => new BaseCommand(MenuItemCommand);
        public static ICommand RecentlyDeletedCommand => new BaseCommand(RecentlyDeleted);

        /// <summary>
        /// Change the templates favorite status
        /// </summary>
        private static void ChangeFavoriteStatus()
        {
            // find template manager view model
            if (Application.Current.Windows.OfType<TemplateManagerView>()?.FirstOrDefault()?.DataContext is not TemplateManagerViewModel templateManagerViewModel)
                return;

            // find menu view model
            if (Application.Current.Windows.OfType<LiveTrackerWindowView>()?.FirstOrDefault()?.LiveTrackerMenu?.DataContext is not MenuViewModel menuViewModel)
                return;

            // data grid view model
            if (Application.Current.Windows.OfType<LiveTrackerWindowView>().FirstOrDefault()?.TournamentsDataGrid?.DataContext is not DataGridViewModel dataGridViewModel)
                return;

            // null check on selected template
            if (templateManagerViewModel.SelectedTemplate is null)
                return;

            // flip favorite status
            templateManagerViewModel.SelectedTemplate.IsFavorite = !templateManagerViewModel.SelectedTemplate.IsFavorite;

            // update template file
            TournamentTemplateHelper.SaveTournamentTemplate(templateManagerViewModel.SelectedTemplate, true);

            // reload menu view model templates
            menuViewModel.UpdateFavoriteTemplates();

            // update running tournaments
            dataGridViewModel.UpdateRunningTournaments();
        }

        /// <summary>
        /// create a copy of a template for editing
        /// </summary>
        private static void Copy()
        {
            CopyTemplate(TemplateManagerMode.Copy);
        }

        /// <summary>
        /// Copy template data into new template
        /// </summary>
        /// <param name="mode">copy or edit mode</param>
        private static void CopyTemplate(TemplateManagerMode mode)
        {
            // find template manager view model
            if (Application.Current.Windows.OfType<TemplateManagerView>()?.FirstOrDefault()?.DataContext is not TemplateManagerViewModel templateManagerViewModel)
                return;

            // null check on selected template
            if (templateManagerViewModel.SelectedTemplate is null)
                return;

            // yes/no view model
            var vm = new YesNoViewModel($"Are you sure you want to copy {templateManagerViewModel.SelectedTemplate.TournamentName}?", "Copy Tournament");

            if (mode is TemplateManagerMode.Edit)
                vm = new YesNoViewModel($"Are you sure you want to edit {templateManagerViewModel.SelectedTemplate.TournamentName}?", "Edit Tournament");

            // create/show yes/no window
            var window = new YesNoView(vm)
            {
                Owner = Application.Current.Windows.OfType<TemplateManagerView>().FirstOrDefault(),
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
            };

            var defaultTournament = TournamentTemplateDataHelper.LoadDefaultValues();

            if (TournamentTemplate.Equals(templateManagerViewModel.Template, defaultTournament) is false)
            {
                // show confirm message
                window.ShowDialog();

                // not saved, exit
                if (vm.Saved is false)
                    return;
            }

            // copy template into new template
            templateManagerViewModel.Template = new TournamentTemplate(templateManagerViewModel.SelectedTemplate);

            // set favorite to false
            templateManagerViewModel.Template.IsFavorite = false;

            // see if new game type is in the combo box, add it if not
            if (templateManagerViewModel.GameTypes.Any(i => i == templateManagerViewModel.Template.GameType) is false)
                templateManagerViewModel.GameTypes.Add(templateManagerViewModel.Template.GameType);

            // set selected game type
            templateManagerViewModel.SelectedGameType = templateManagerViewModel.Template.GameType;

            // add any new venue
            if (templateManagerViewModel.Venues.Any(i => i == templateManagerViewModel.Template.Venue) is false)
                templateManagerViewModel.Venues.Add(templateManagerViewModel.Template.Venue);

            // set selected venue
            templateManagerViewModel.SelectedVenue = templateManagerViewModel.Template.Venue;

            // add any new formats to the formats list
            foreach (var newFormat in templateManagerViewModel.Template.Formats)
                if (templateManagerViewModel.Formats.Any(i => i == newFormat) is false)
                    templateManagerViewModel.Formats.Add(newFormat);

            // create format check list box items
            templateManagerViewModel.CreateFormatListBoxItems();
            templateManagerViewModel.DeselectFormats();

            // set selected formats
            foreach (var item in templateManagerViewModel.FormatsList)
                if (templateManagerViewModel.Template.Formats.Any(i => i == item.DisplayString))
                    item.IsSelected = true;

            // set sng
            templateManagerViewModel.SngCheckBox = templateManagerViewModel.Template.IsSng;

            // set Bovada
            templateManagerViewModel.BovadaCheckBox = templateManagerViewModel.Template.IsBovadaBounty;

            // set mode
            templateManagerViewModel.Mode = mode;
        }

        /// <summary>
        /// Delete the selected template
        /// </summary>
        public static void DeleteTemplate()
        {
            // find template manager view model
            if (Application.Current.Windows.OfType<TemplateManagerView>()?.FirstOrDefault()?.DataContext is not TemplateManagerViewModel templateManagerViewModel)
                return;

            // null check on selected template
            if (templateManagerViewModel.SelectedTemplate is null)
                return;

            // tournament name
            var tournamentName = templateManagerViewModel.SelectedTemplate.TournamentName;

            // yes/no view model
            var vm = new YesNoViewModel($"Are you sure you want to delete {tournamentName}?", "Delete Tournament");

            // create/show yes/no window
            var window = new YesNoView(vm)
            {
                Owner = Application.Current.Windows.OfType<TemplateManagerView>().FirstOrDefault(),
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
            };
            window.ShowDialog();

            // not saved, exit
            if (vm.Saved is false)
                return;

            // find menu view model
            if (Application.Current.Windows.OfType<LiveTrackerWindowView>()?.FirstOrDefault()?.LiveTrackerMenu?.DataContext is not MenuViewModel menuViewModel)
                return;

            // data grid view model
            if (Application.Current.Windows.OfType<LiveTrackerWindowView>().FirstOrDefault()?.TournamentsDataGrid?.DataContext is not DataGridViewModel dataGridViewModel)
                return;

            // remove template from any sessions it might be in
            SessionTemplateHelper.RemoveTemplateFromSessions(templateManagerViewModel.SelectedTemplate);

            // delete selected template
            TournamentTemplateHelper.DeleteTournamentTemplate(templateManagerViewModel.SelectedTemplate);

            // reload menu view model templates
            menuViewModel.UpdateFavoriteTemplates();

            // reload view model sessions
            menuViewModel.UpdateFavoriteSessions();

            // reload template manager templates
            templateManagerViewModel.LoadTemplates();

            // update running tournaments
            dataGridViewModel.UpdateRunningTournaments();

            // reload default values on template manager
            templateManagerViewModel.LoadDefaults();

            // update template manager title
            templateManagerViewModel.SetTitle();

            // get session manager view model and update templates
            if (Application.Current.Windows.OfType<SessionManagerView>().FirstOrDefault()?.DataContext is SessionManagerViewModel sessionManagerViewModel)
                sessionManagerViewModel.UpdateTemplates();

            // get select tournaments view model and update templates
            if (Application.Current.Windows.OfType<SelectTemplatesView>().FirstOrDefault()?.DataContext is SelectTemplatesViewModel selectTemplatesViewModel)
                selectTemplatesViewModel.Reload();

            // create/show success message
            var okayVM1 = new OkViewModel($"{tournamentName} deleted successfully", "Deleted");
            var okayWindow1 = new OkView(okayVM1)
            {
                Owner = Application.Current.Windows.OfType<TemplateManagerView>().FirstOrDefault(),
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
            };
            okayWindow1.ShowDialog();
        }

        /// <summary>
        /// Launch the delete tournaments window
        /// </summary>
        private static void DeleteTemplates()
        {
            // create/show delete templates window
            var vm = new TemplateDeleteViewModel();
            var window = new TemplateDeleteView(vm)
            {
                Owner = Application.Current.Windows.OfType<TemplateManagerView>().FirstOrDefault(),
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            window.ShowDialog();

            // find template manager view model
            if (Application.Current.Windows.OfType<TemplateManagerView>()?.FirstOrDefault()?.DataContext is not TemplateManagerViewModel templateManagerViewModel)
                return;

            // find menu view model
            if (Application.Current.Windows.OfType<LiveTrackerWindowView>()?.FirstOrDefault()?.LiveTrackerMenu?.DataContext is not MenuViewModel menuViewModel)
                return;

            // data grid view model
            if (Application.Current.Windows.OfType<LiveTrackerWindowView>().FirstOrDefault()?.TournamentsDataGrid?.DataContext is not DataGridViewModel dataGridViewModel)
                return;

            // reload menu view model templates
            menuViewModel.UpdateFavoriteTemplates();

            // update running tournaments
            dataGridViewModel.UpdateRunningTournaments();

            // reload template manager templates
            templateManagerViewModel.LoadTemplates();

            // update template manager title
            templateManagerViewModel.SetTitle();

            // get session manager view model and update templates
            if (Application.Current.Windows.OfType<SessionManagerView>().FirstOrDefault()?.DataContext is SessionManagerViewModel sessionManagerViewModel)
                sessionManagerViewModel.UpdateTemplates();

            // get select tournaments view model and update templates
            if (Application.Current.Windows.OfType<SelectTemplatesView>().FirstOrDefault()?.DataContext is SelectTemplatesViewModel selectTemplatesViewModel)
                selectTemplatesViewModel.Reload();
        }

        /// <summary>
        /// Edit the selected template
        /// </summary>
        private static void Edit()
        {
            CopyTemplate(TemplateManagerMode.Edit);
        }

        /// <summary>
        ///  launch edit defaults window
        /// </summary>
        private static void EditDefaults()
        {
            // create/show edit defaults window
            var vm = new EditDefaultsViewModel();
            var window = new EditDefaultsView(vm)
            {
                Owner = Application.Current.Windows.OfType<TemplateManagerView>()?.FirstOrDefault(),
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            window.ShowDialog();
        }

        /// <summary>
        /// launch edit formats window
        /// </summary>
        private static void EditFormats()
        {
            // create/show edit formats window
            var vm = new EditFormatsViewModel();
            var window = new EditFormatsView(vm)
            {
                Owner = Application.Current.Windows.OfType<TemplateManagerView>()?.FirstOrDefault(),
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            window.ShowDialog();
        }

        /// <summary>
        /// launch edit game types window
        /// </summary>
        private static void EditGameTypes()
        {
            // create/show edit game types window
            var vm = new EditGameTypesViewModel();
            var window = new EditGameTypesView(vm)
            {
                Owner = Application.Current.Windows.OfType<TemplateManagerView>()?.FirstOrDefault(),
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            window.ShowDialog();
        }

        /// <summary>
        /// Launch template manager edit venues window
        /// </summary>
        private static void EditVenues()
        {
            // create/show edit venues window
            var vm = new EditVenuesViewModel();
            var window = new EditVenuesView(vm)
            {
                Owner = Application.Current.Windows.OfType<TemplateManagerView>()?.FirstOrDefault(),
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            window.ShowDialog();
        }

        /// <summary>
        /// Close template manager window
        /// </summary>
        private static void ExitTemplateManager()
        {
            Application.Current.Windows.OfType<TemplateManagerView>()?.FirstOrDefault()?.Close();
        }

        /// <summary>
        /// launch export templates window
        /// </summary>
        private static void ExportTemplates()
        {
            var vm = new TemplateExportViewModel();
            var window = new TemplateExportView(vm)
            {
                Owner = Application.Current.Windows.OfType<TemplateManagerView>()?.FirstOrDefault(),
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            window.ShowDialog();
        }

        /// <summary>
        /// Move checked formats to the top of the checklist box
        /// </summary>
        private static void FormatChecked()
        {
            // find template manager view model
            if (Application.Current.Windows.OfType<TemplateManagerView>()?.FirstOrDefault()?.DataContext is not TemplateManagerViewModel templateManagerViewModel)
                return;

            // update format list box items
            templateManagerViewModel.UpdateFormatListBoxItems();
        }

        /// <summary>
        /// gets the template managers view model
        /// </summary>
        /// <returns>view model</returns>
        public static TemplateManagerViewModel GetTemplateManagerViewModel()
        {
            return Application.Current.Windows.OfType<TemplateManagerView>().FirstOrDefault()?.DataContext as TemplateManagerViewModel;
        }

        /// <summary>
        /// launch import templates window
        /// </summary>
        private static void ImportTemplates()
        {
            var vm = new TemplateImportViewModel();
            var window = new TemplateImportView(vm)
            {
                Owner = Application.Current.Windows.OfType<TemplateManagerView>()?.FirstOrDefault(),
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            window.ShowDialog();

            // didn't import
            if (!vm.Saved)
                return;

            // find template manager view model
            if (Application.Current.Windows.OfType<TemplateManagerView>()?.FirstOrDefault()?.DataContext is not TemplateManagerViewModel templateManagerViewModel)
                return;

            // find menu view model
            if (Application.Current.Windows.OfType<LiveTrackerWindowView>()?.FirstOrDefault()?.LiveTrackerMenu?.DataContext is not MenuViewModel menuViewModel)
                return;

            // data grid view model
            if (Application.Current.Windows.OfType<LiveTrackerWindowView>().FirstOrDefault()?.TournamentsDataGrid?.DataContext is not DataGridViewModel dataGridViewModel)
                return;

            // reload menu view model templates
            menuViewModel.UpdateFavoriteTemplates();

            // update running tournaments
            dataGridViewModel.UpdateRunningTournaments();

            // reload template manager templates
            templateManagerViewModel.LoadTemplates();

            // update template manager title
            templateManagerViewModel.SetTitle();

            // get session manager view model and update templates
            if (Application.Current.Windows.OfType<SessionManagerView>().FirstOrDefault()?.DataContext is SessionManagerViewModel sessionManagerViewModel)
                sessionManagerViewModel.UpdateTemplates();

            // get select tournaments view model and update templates
            if (Application.Current.Windows.OfType<SelectTemplatesView>().FirstOrDefault()?.DataContext is SelectTemplatesViewModel selectTemplatesViewModel)
                selectTemplatesViewModel.Reload();
        }

        /// <summary>
        /// Parse the menu item clicked parameter into method calls
        /// </summary>
        /// <param name="parameter">menu item that was clicked name parameter</param>
        private static void MenuItemCommand(object parameter)
        {
            switch (parameter as string)
            {
                case "CopyTemplate":
                    Copy();
                    break;
                case "DeleteTemplate":
                    DeleteTemplate();
                    break;
                case "DeleteTemplates":
                    DeleteTemplates();
                    break;
                case "EditTemplate":
                    Edit();
                    break;
                case "EditDefaults":
                    EditDefaults();
                    break;
                case "EditFormats":
                    EditFormats();
                    break;
                case "EditGameTypes":
                    EditGameTypes();
                    break;
                case "EditVenues":
                    EditVenues();
                    break;
                case "Exit":
                    ExitTemplateManager();
                    break;
                case "ExportTemplates":
                    ExportTemplates();
                    break;
                case "Favorite":
                    ChangeFavoriteStatus();
                    break;
                case "ImportTemplates":
                    ImportTemplates();
                    break;
                case "ItemChecked":
                    FormatChecked();
                    break;
                case "ResetTemplate":
                    ResetTemplate();
                    break;
                case "SaveTemplate":
                    SaveTemplate();
                    break;
                case "UpdateColumnWidth":
                    UpdateColumnWidth();
                    break;
                case "UpdateColumnOrder":
                    UpdateColumnOrder();
                    break;
                case "WindowLoaded":
                    WindowLoaded();
                    break;
            }
        }

        /// <summary>
        /// restore a recently deleted template
        /// </summary>
        /// <param name="parameter">template to restore</param>
        private static void RecentlyDeleted(object parameter)
        {
            var template = parameter as TournamentTemplate;

            if(template is null)
                return;
        }

        /// <summary>
        /// Reset template data
        /// </summary>
        private static void ResetTemplate()
        {
            // find template manager view model
            if (Application.Current.Windows.OfType<TemplateManagerView>()?.FirstOrDefault()?.DataContext is not TemplateManagerViewModel templateManagerViewModel)
                return;

            // yes/no view model
            var vm = new YesNoViewModel("Are you sure you want to reset this tournament?", "Reset Tournament");

            // create/show yes/no window
            var window = new YesNoView(vm)
            {
                Owner = Application.Current.Windows.OfType<TemplateManagerView>().FirstOrDefault(),
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
            };

            var defaultTournament = TournamentTemplateDataHelper.LoadDefaultValues();

            if (TournamentTemplate.Equals(templateManagerViewModel.Template, defaultTournament) is false)
            {
                // show confirm message
                window.ShowDialog();

                // not saved, exit
                if (vm.Saved is false)
                    return;
            }

            // reload default values
            templateManagerViewModel.ResetToDefaults();
        }

        /// <summary>
        /// Save template
        /// </summary>
        private static void SaveTemplate()
        {
            // find template manager view model
            if (Application.Current.Windows.OfType<TemplateManagerView>()?.FirstOrDefault()?.DataContext is not TemplateManagerViewModel templateManagerViewModel)
                return;

            // check for blank name
            if (templateManagerViewModel.Template.TournamentName.Length is 0)
            {
                // ok view model
                var theVm = new OkViewModel("Tournament name can't be blank", "Blank Name");

                // create/show ok window
                var theWindow = new OkView(theVm)
                {
                    Owner = Application.Current.Windows.OfType<TemplateManagerView>().FirstOrDefault(),
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                theWindow.ShowDialog();

                // exit
                return;
            }

            // check for length over 50
            if (templateManagerViewModel.Template.TournamentName.Length > 50)
            {
                // ok view model
                var theVm = new OkViewModel("Tournament name can't be over 50 characters", "Name Too Long");

                // create/show ok window
                var theWindow = new OkView(theVm)
                {
                    Owner = Application.Current.Windows.OfType<TemplateManagerView>().FirstOrDefault(),
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                theWindow.ShowDialog();

                // exit
                return;
            }

            // yes/no view model
            var vm = new YesNoViewModel("Are you sure you want to save this tournament?", "Save Tournament");

            // create/show yes/no window
            var window = new YesNoView(vm)
            {
                Owner = Application.Current.Windows.OfType<TemplateManagerView>().FirstOrDefault(),
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
            };
            window.ShowDialog();

            // not saved, exit
            if (vm.Saved is false)
                return;

            // find menu view model
            if (Application.Current.Windows.OfType<LiveTrackerWindowView>()?.FirstOrDefault()?.LiveTrackerMenu?.DataContext is not MenuViewModel menuViewModel)
                return;

            // data grid view model
            if (Application.Current.Windows.OfType<LiveTrackerWindowView>().FirstOrDefault()?.TournamentsDataGrid?.DataContext is not DataGridViewModel dataGridViewModel)
                return;

            // null check
            if (templateManagerViewModel.Template is null)
                return;

            // get template data
            var template = new TournamentTemplate(templateManagerViewModel.Template);

            // get selected formats
            template.Formats.Clear();
            foreach(var format in new List<TemplateListBoxItem>(templateManagerViewModel.FormatsList.Where(i => i.IsSelected)))
                template.Formats.Add(format.DisplayString);

            // get selected game type
            template.GameType = templateManagerViewModel.SelectedGameType;

            // get selected venue
            template.Venue = templateManagerViewModel.SelectedVenue;

            // get template id number for new saves and copies
            if(templateManagerViewModel.Mode is TemplateManagerMode.New || templateManagerViewModel.Mode is TemplateManagerMode.Copy)
                template.TemplateId = TournamentTemplateHelper.GetNewTemplateId();

            // check sng
            if (templateManagerViewModel.SngCheckBox)
                template.IsSng = true;

            // check Bovada bounty
            if(templateManagerViewModel.BovadaCheckBox)
                template.IsBovadaBounty = true;

            // saving a new template
            if(templateManagerViewModel.Mode is TemplateManagerMode.New)
                TournamentTemplateHelper.SaveTournamentTemplate(template);

            // saving an edit template
            if(templateManagerViewModel.Mode is TemplateManagerMode.Edit)
                TournamentTemplateHelper.SaveTournamentTemplate(template, true);

            // saving a copy template
            if (templateManagerViewModel.Mode is TemplateManagerMode.Copy)
                TournamentTemplateHelper.SaveTournamentTemplate(template);

            // add new game types to template data
            TournamentTemplateDataHelper.AddGameType(template.GameType);

            // add new venues to template data
            TournamentTemplateDataHelper.AddVenue(template.Venue);

            // add new formats to template data
            foreach (var format in template.Formats)
                TournamentTemplateDataHelper.AddFormat(format);

            // reload menu view model templates
            menuViewModel.UpdateFavoriteTemplates();

            // update running tournaments
            dataGridViewModel.UpdateRunningTournaments();

            // reload template manager templates
            templateManagerViewModel.LoadTemplates();

            // reload default values
            templateManagerViewModel.ResetToDefaults();

            // update template manager title
            templateManagerViewModel.SetTitle();

            // get session manager view model and update templates
            if (Application.Current.Windows.OfType<SessionManagerView>().FirstOrDefault()?.DataContext is SessionManagerViewModel sessionManagerViewModel)
                sessionManagerViewModel.UpdateTemplates();

            // get select tournaments view model and update templates
            if (Application.Current.Windows.OfType<SelectTemplatesView>().FirstOrDefault()?.DataContext is SelectTemplatesViewModel selectTemplatesViewModel)
                selectTemplatesViewModel.Reload();

            // success message
            var theVmm = new OkViewModel($"{template.TournamentName} saved successfully", "Saved");

            // create/show ok window
            var theWindoww = new OkView(theVmm)
            {
                Owner = Application.Current.Windows.OfType<TemplateManagerView>().FirstOrDefault(),
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
            };
            theWindoww.ShowDialog();
        }

        /// <summary>
        /// update the data grid column orders
        /// </summary>
        private static void UpdateColumnOrder()
        {
            // datagrid columns
            var columns = Application.Current.Windows.OfType<TemplateManagerView>().FirstOrDefault()?.TemplatesDataGrid.Columns;

            // null check
            if (columns is null)
                return;

            // find columns
            var addonBaseCostColumn = columns?.FirstOrDefault(i => i.MappingName is "AddonBaseCost");
            var addonRakeCostColumn = columns?.FirstOrDefault(i => i.MappingName is "AddonRakeCost");
            var addonTotalCostColumn = columns?.FirstOrDefault(i => i.MappingName is "AddonTotalCost");
            var blindLevelsColumn = columns?.FirstOrDefault(i => i.MappingName is "BlindLevels");
            var bountyColumn = columns?.FirstOrDefault(i => i.MappingName is "Bounty");
            var buyinBaseCostColumn = columns?.FirstOrDefault(i => i.MappingName is "BuyinBaseCost");
            var buyinRakeCostColumn = columns?.FirstOrDefault(i => i.MappingName is "BuyinRakeCost");
            var buyinTotalCostColumn = columns?.FirstOrDefault(i => i.MappingName is "BuyinTotalCost");
            var entrantsColumn = columns?.FirstOrDefault(i => i.MappingName is "Entrants");
            var entrantsPaidColumn = columns?.FirstOrDefault(i => i.MappingName is "EntrantsPaid");
            var isBovadaBountyColumn = columns?.FirstOrDefault(i => i.MappingName is "IsBovadaBounty");
            var isSngColumn = columns?.FirstOrDefault(i => i.MappingName is "IsSng");
            var formatStringColumn = columns?.FirstOrDefault(i => i.MappingName is "FormatString");
            var gameTypeColumn = columns?.FirstOrDefault(i => i.MappingName is "GameType");
            var guaranteeColumn = columns?.FirstOrDefault(i => i.MappingName is "Guarantee");
            var lateRegColumn = columns?.FirstOrDefault(i => i.MappingName is "LateReg");
            var rebuyBaseCostColumn = columns?.FirstOrDefault(i => i.MappingName is "RebuyBaseCost");
            var rebuyRakeCostColumn = columns?.FirstOrDefault(i => i.MappingName is "RebuyRakeCost");
            var rebuyTotalCostColumn = columns?.FirstOrDefault(i => i.MappingName is "RebuyTotalCost");
            var stackSizeStartingColumn = columns?.FirstOrDefault(i => i.MappingName is "StackSizeStarting");
            var stackSizeAddonColumn = columns?.FirstOrDefault(i => i.MappingName is "StackSizeAddon");
            var stackSizeRebuyColumn = columns?.FirstOrDefault(i => i.MappingName is "StackSizeRebuy");
            var startTimeColumn = columns?.FirstOrDefault(i => i.MappingName is "StartTime");
            var tableSizeColumn = columns?.FirstOrDefault(i => i.MappingName is "TableSize");
            var tournamentNameColumn = columns?.FirstOrDefault(i => i.MappingName is "TournamentName");
            var venueColumn = columns?.FirstOrDefault(i => i.MappingName is "Venue");

            // save column orders
            if (addonBaseCostColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "TemplateManagerColumnOrder", "AddonBaseCostColumnOrder", columns.IndexOf(addonBaseCostColumn));
            if (addonRakeCostColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "TemplateManagerColumnOrder", "AddonRakeCostColumnOrder", columns.IndexOf(addonRakeCostColumn));
            if (addonTotalCostColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "TemplateManagerColumnOrder", "AddonTotalCostColumnOrder", columns.IndexOf(addonTotalCostColumn));
            if (blindLevelsColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "TemplateManagerColumnOrder", "BlindLevelsColumnOrder", columns.IndexOf(blindLevelsColumn));
            if (bountyColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "TemplateManagerColumnOrder", "BovadaBountyColumnOrder", columns.IndexOf(bountyColumn));
            if (isBovadaBountyColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "TemplateManagerColumnOrder", "BovadaColumnOrder", columns.IndexOf(isBovadaBountyColumn));
            if (buyinRakeCostColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "TemplateManagerColumnOrder", "BuyinRakeCostColumnOrder", columns.IndexOf(buyinRakeCostColumn));
            if (buyinBaseCostColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "TemplateManagerColumnOrder", "BuyinBaseCostColumnOrder", columns.IndexOf(buyinBaseCostColumn));
            if (buyinTotalCostColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "TemplateManagerColumnOrder", "BuyinTotalCostColumnOrder", columns.IndexOf(buyinTotalCostColumn));
            if (entrantsColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "TemplateManagerColumnOrder", "EntrantsColumnOrder", columns.IndexOf(entrantsColumn));
            if (entrantsPaidColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "TemplateManagerColumnOrder", "EntrantsPaidColumnOrder", columns.IndexOf(entrantsPaidColumn));
            if (formatStringColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "TemplateManagerColumnOrder", "FormatStringColumnOrder", columns.IndexOf(formatStringColumn));
            if (gameTypeColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "TemplateManagerColumnOrder", "GameTypeColumnOrder", columns.IndexOf(gameTypeColumn));
            if (guaranteeColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "TemplateManagerColumnOrder", "GuaranteeColumnOrder", columns.IndexOf(guaranteeColumn));
            if (isSngColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "TemplateManagerColumnOrder", "SngColumnOrder", columns.IndexOf(isSngColumn));
            if (lateRegColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "TemplateManagerColumnOrder", "LateRegColumnOrder", columns.IndexOf(lateRegColumn));
            if (rebuyBaseCostColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "TemplateManagerColumnOrder", "RebuyBaseCostColumnOrder", columns.IndexOf(rebuyBaseCostColumn));
            if (rebuyRakeCostColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "TemplateManagerColumnOrder", "RebuyRakeCostColumnOrder", columns.IndexOf(rebuyRakeCostColumn));
            if (rebuyTotalCostColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "TemplateManagerColumnOrder", "RebuyTotalCostColumnOrder", columns.IndexOf(rebuyTotalCostColumn));
            if (stackSizeStartingColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "TemplateManagerColumnOrder", "StackSizeStartingColumnOrder", columns.IndexOf(stackSizeStartingColumn));
            if (stackSizeAddonColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "TemplateManagerColumnOrder", "StackSizeAddonColumnOrder", columns.IndexOf(stackSizeAddonColumn));
            if (stackSizeRebuyColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "TemplateManagerColumnOrder", "StackSizeRebuyColumnOrder", columns.IndexOf(stackSizeRebuyColumn));
            if (startTimeColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "TemplateManagerColumnOrder", "StartTimeColumnOrder", columns.IndexOf(startTimeColumn));
            if (tableSizeColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "TemplateManagerColumnOrder", "TableSizeColumnOrder", columns.IndexOf(tableSizeColumn));
            if (tournamentNameColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "TemplateManagerColumnOrder", "TournamentNameColumnOrder", columns.IndexOf(tournamentNameColumn));
            if (venueColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "TemplateManagerColumnOrder", "VenueColumnOrder", columns.IndexOf(venueColumn));
        }

        /// <summary>
        /// update the data grid column widths
        /// </summary>
        private static void UpdateColumnWidth()
        {
            // datagrid columns
            var columns = Application.Current.Windows.OfType<TemplateManagerView>().FirstOrDefault()?.TemplatesDataGrid.Columns;

            // null check
            if (columns is null)
                return;

            // find columns
            var addonBaseCostColumn = columns?.FirstOrDefault(i => i.MappingName is "AddonBaseCost");
            var addonRakeCostColumn = columns?.FirstOrDefault(i => i.MappingName is "AddonRakeCost");
            var addonTotalCostColumn = columns?.FirstOrDefault(i => i.MappingName is "AddonTotalCost");
            var blindLevelsColumn = columns?.FirstOrDefault(i => i.MappingName is "BlindLevels");
            var bountyColumn = columns?.FirstOrDefault(i => i.MappingName is "Bounty");
            var buyinBaseCostColumn = columns?.FirstOrDefault(i => i.MappingName is "BuyinBaseCost");
            var buyinRakeCostColumn = columns?.FirstOrDefault(i => i.MappingName is "BuyinRakeCost");
            var buyinTotalCostColumn = columns?.FirstOrDefault(i => i.MappingName is "BuyinTotalCost");
            var entrantsColumn = columns?.FirstOrDefault(i => i.MappingName is "Entrants");
            var entrantsPaidColumn = columns?.FirstOrDefault(i => i.MappingName is "EntrantsPaid");
            var isBovadaBountyColumn = columns?.FirstOrDefault(i => i.MappingName is "IsBovadaBounty");
            var isSngColumn = columns?.FirstOrDefault(i => i.MappingName is "IsSng");
            var formatStringColumn = columns?.FirstOrDefault(i => i.MappingName is "FormatString");
            var gameTypeColumn = columns?.FirstOrDefault(i => i.MappingName is "GameType");
            var guaranteeColumn = columns?.FirstOrDefault(i => i.MappingName is "Guarantee");
            var lateRegColumn = columns?.FirstOrDefault(i => i.MappingName is "LateReg");
            var rebuyBaseCostColumn = columns?.FirstOrDefault(i => i.MappingName is "RebuyBaseCost");
            var rebuyRakeCostColumn = columns?.FirstOrDefault(i => i.MappingName is "RebuyRakeCost");
            var rebuyTotalCostColumn = columns?.FirstOrDefault(i => i.MappingName is "RebuyTotalCost");
            var stackSizeStartingColumn = columns?.FirstOrDefault(i => i.MappingName is "StackSizeStarting");
            var stackSizeAddonColumn = columns?.FirstOrDefault(i => i.MappingName is "StackSizeAddon");
            var stackSizeRebuyColumn = columns?.FirstOrDefault(i => i.MappingName is "StackSizeRebuy");
            var startTimeColumn = columns?.FirstOrDefault(i => i.MappingName is "StartTime");
            var tableSizeColumn = columns?.FirstOrDefault(i => i.MappingName is "TableSize");
            var tournamentNameColumn = columns?.FirstOrDefault(i => i.MappingName is "TournamentName");
            var venueColumn = columns?.FirstOrDefault(i => i.MappingName is "Venue");

            // save column orders
            if (addonBaseCostColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "TemplateManagerColumnWidth", "AddonBaseCostColumnWidth", addonBaseCostColumn.ActualWidth);
            if (addonRakeCostColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "TemplateManagerColumnWidth", "AddonRakeCostColumnWidth", addonRakeCostColumn.ActualWidth);
            if (addonTotalCostColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "TemplateManagerColumnWidth", "AddonTotalCostColumnWidth", addonTotalCostColumn.ActualWidth);
            if (blindLevelsColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "TemplateManagerColumnWidth", "BlindLevelsColumnWidth", blindLevelsColumn.ActualWidth);
            if (bountyColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "TemplateManagerColumnWidth", "BovadaBountyColumnWidth", bountyColumn.ActualWidth);
            if (isBovadaBountyColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "TemplateManagerColumnWidth", "BovadaColumnWidth", isBovadaBountyColumn.ActualWidth);
            if (buyinRakeCostColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "TemplateManagerColumnWidth", "BuyinRakeCostColumnOrder", buyinRakeCostColumn.ActualWidth);
            if (buyinBaseCostColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "TemplateManagerColumnWidth", "BuyinBaseCostColumnOrder", buyinBaseCostColumn.ActualWidth);
            if (buyinTotalCostColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "TemplateManagerColumnWidth", "BuyinTotalCostColumnOrder", buyinTotalCostColumn.ActualWidth);
            if (entrantsColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "TemplateManagerColumnWidth", "EntrantsColumnOrder", entrantsColumn.ActualWidth);
            if (entrantsPaidColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "TemplateManagerColumnWidth", "EntrantsPaidColumnOrder", entrantsPaidColumn.ActualWidth);
            if (formatStringColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "TemplateManagerColumnWidth", "FormatStringColumnOrder", formatStringColumn.ActualWidth);
            if (gameTypeColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "TemplateManagerColumnWidth", "GameTypeColumnOrder", gameTypeColumn.ActualWidth);
            if (guaranteeColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "TemplateManagerColumnWidth", "GuaranteeColumnOrder", guaranteeColumn.ActualWidth);
            if (isSngColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "TemplateManagerColumnWidth", "SngColumnOrder", isSngColumn.ActualWidth);
            if (lateRegColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "TemplateManagerColumnWidth", "LateRegColumnOrder", lateRegColumn.ActualWidth);
            if (rebuyBaseCostColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "TemplateManagerColumnWidth", "RebuyBaseCostColumnOrder", rebuyBaseCostColumn.ActualWidth);
            if (rebuyRakeCostColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "TemplateManagerColumnWidth", "RebuyRakeCostColumnOrder", rebuyRakeCostColumn.ActualWidth);
            if (rebuyTotalCostColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "TemplateManagerColumnWidth", "RebuyTotalCostColumnOrder", rebuyTotalCostColumn.ActualWidth);
            if (stackSizeStartingColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "TemplateManagerColumnWidth", "StackSizeStartingColumnOrder", stackSizeStartingColumn.ActualWidth);
            if (stackSizeAddonColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "TemplateManagerColumnWidth", "StackSizeAddonColumnOrder", stackSizeAddonColumn.ActualWidth);
            if (stackSizeRebuyColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "TemplateManagerColumnWidth", "StackSizeRebuyColumnOrder", stackSizeRebuyColumn.ActualWidth);
            if (startTimeColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "TemplateManagerColumnWidth", "StartTimeColumnOrder", startTimeColumn.ActualWidth);
            if (tableSizeColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "TemplateManagerColumnWidth", "TableSizeColumnOrder", tableSizeColumn.ActualWidth);
            if (tournamentNameColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "TemplateManagerColumnWidth", "TournamentNameColumnOrder", tournamentNameColumn.ActualWidth);
            if (venueColumn is not null) PreferencesHelper.UpdatePreference("LiveTracker", "TemplateManagerColumnWidth", "VenueColumnOrder", venueColumn.ActualWidth);
        }

        /// <summary>
        /// Called after the template manager loads to adjust the column locations
        /// </summary>
        private static void WindowLoaded()
        {
            // datagrid view
            if(Application.Current.Windows.OfType<TemplateManagerView>().FirstOrDefault() is not TemplateManagerView templateManagerView)
                return;

            // columns
            var columns = templateManagerView.TemplatesDataGrid.Columns;

            // null check
            if (templateManagerView.TemplatesDataGrid.Columns is null)
                return;

            // find columns
            var addonBaseCostColumn = columns?.FirstOrDefault(i => i.MappingName is "AddonBaseCost");
            var addonRakeCostColumn = columns?.FirstOrDefault(i => i.MappingName is "AddonRakeCost");
            var addonTotalCostColumn = columns?.FirstOrDefault(i => i.MappingName is "AddonTotalCost");
            var blindLevelsColumn = columns?.FirstOrDefault(i => i.MappingName is "BlindLevels");
            var bountyColumn = columns?.FirstOrDefault(i => i.MappingName is "Bounty");
            var buyinBaseCostColumn = columns?.FirstOrDefault(i => i.MappingName is "BuyinBaseCost");
            var buyinRakeCostColumn = columns?.FirstOrDefault(i => i.MappingName is "BuyinRakeCost");
            var buyinTotalCostColumn = columns?.FirstOrDefault(i => i.MappingName is "BuyinTotalCost");
            var entrantsColumn = columns?.FirstOrDefault(i => i.MappingName is "Entrants");
            var entrantsPaidColumn = columns?.FirstOrDefault(i => i.MappingName is "EntrantsPaid");
            var isBovadaBountyColumn = columns?.FirstOrDefault(i => i.MappingName is "IsBovadaBounty");
            var isSngColumn = columns?.FirstOrDefault(i => i.MappingName is "IsSng");
            var formatStringColumn = columns?.FirstOrDefault(i => i.MappingName is "FormatString");
            var gameTypeColumn = columns?.FirstOrDefault(i => i.MappingName is "GameType");
            var guaranteeColumn = columns?.FirstOrDefault(i => i.MappingName is "Guarantee");
            var lateRegColumn = columns?.FirstOrDefault(i => i.MappingName is "LateReg");
            var rebuyBaseCostColumn = columns?.FirstOrDefault(i => i.MappingName is "RebuyBaseCost");
            var rebuyRakeCostColumn = columns?.FirstOrDefault(i => i.MappingName is "RebuyRakeCost");
            var rebuyTotalCostColumn = columns?.FirstOrDefault(i => i.MappingName is "RebuyTotalCost");
            var stackSizeStartingColumn = columns?.FirstOrDefault(i => i.MappingName is "StackSizeStarting");
            var stackSizeAddonColumn = columns?.FirstOrDefault(i => i.MappingName is "StackSizeAddon");
            var stackSizeRebuyColumn = columns?.FirstOrDefault(i => i.MappingName is "StackSizeRebuy");
            var startTimeColumn = columns?.FirstOrDefault(i => i.MappingName is "StartTime");
            var tableSizeColumn = columns?.FirstOrDefault(i => i.MappingName is "TableSize");
            var tournamentNameColumn = columns?.FirstOrDefault(i => i.MappingName is "TournamentName");
            var venueColumn = columns?.FirstOrDefault(i => i.MappingName is "Venue");

            // column order preferences
            var addonBaseCostOrderColumn = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "TemplateManagerColumnOrder", "AddonBaseCostColumnOrder"));
            var addonRakeCostOrderColumn = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "TemplateManagerColumnOrder", "AddonRakeCostColumnOrder"));
            var addonTotalCostOrderColumn = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "TemplateManagerColumnOrder", "AddonTotalCostColumnOrder"));
            var blindLevelsOrderColumn = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "TemplateManagerColumnOrder", "BlindLevelsColumnOrder"));
            var bountyOrderColumn = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "TemplateManagerColumnOrder", "BovadaBountyColumnOrder"));
            var buyinBaseCostOrderColumn = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "TemplateManagerColumnOrder", "BuyinBaseCostColumnOrder"));
            var buyinRakeCostOrderColumn = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "TemplateManagerColumnOrder", "BuyinRakeCostColumnOrder"));
            var buyinTotalCostOrderColumn = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "TemplateManagerColumnOrder", "BuyinTotalCostColumnOrder"));
            var entrantsOrderColumn = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "TemplateManagerColumnOrder", "EntrantsColumnOrder"));
            var entrantsPaidOrderColumn = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "TemplateManagerColumnOrder", "EntrantsPaidColumnOrder"));
            var isBovadaBountyOrderColumn = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "TemplateManagerColumnOrder", "BovadaColumnOrder"));
            var isSngOrderColumn = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "TemplateManagerColumnOrder", "SngColumnOrder"));
            var formatOrderColumn = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "TemplateManagerColumnOrder", "FormatsColumnOrder"));
            var gameTypeOrderColumn = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "TemplateManagerColumnOrder", "GameTypeColumnOrder"));
            var guaranteeOrderColumn = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "TemplateManagerColumnOrder", "GuaranteeColumnOrder"));
            var lateRegOrderColumn = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "TemplateManagerColumnOrder", "LateRegColumnOrder"));
            var rebuyBaseCostOrderColumn = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "TemplateManagerColumnOrder", "RebuyBaseCostColumnOrder"));
            var rebuyRakeCostOrderColumn = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "TemplateManagerColumnOrder", "RebuyRakeCostColumnOrder"));
            var rebuyTotalCostOrderColumn = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "TemplateManagerColumnOrder", "RebuyTotalCostColumnOrder"));
            var stackSizeStartingOrderColumn = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "TemplateManagerColumnOrder", "StackSizeStartingColumnOrder"));
            var stackSizeAddonOrderColumn = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "TemplateManagerColumnOrder", "StackSizeAddonColumnOrder"));
            var stackSizeRebuyOrderColumn = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "TemplateManagerColumnOrder", "StackSizeRebuyColumnOrder"));
            var startTimeOrderColumn = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "TemplateManagerColumnOrder", "StartTimeColumnOrder"));
            var tableSizeOrderColumn = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "TemplateManagerColumnOrder", "TableSizeColumnOrder"));
            var tournamentNameOrderColumn = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "TemplateManagerColumnOrder", "TournamentNameColumnOrder"));
            var venuesOrderColumn = double.Parse(PreferencesHelper.FindPreference("LiveTracker", "TemplateManagerColumnOrder", "VenueColumnOrder"));

            // re-ordered columns collection
            var newColumns = new Columns();

            // add the columns in the desired order
            for (var c = 0; c < 26; c++)
            {
                if (addonBaseCostOrderColumn == c) newColumns.Add(addonBaseCostColumn);
                if (addonRakeCostOrderColumn == c) newColumns.Add(addonRakeCostColumn);
                if (addonTotalCostOrderColumn == c) newColumns.Add(addonTotalCostColumn);
                if (blindLevelsOrderColumn == c) newColumns.Add(blindLevelsColumn);
                if (bountyOrderColumn == c) newColumns.Add(bountyColumn);
                if (buyinBaseCostOrderColumn == c) newColumns.Add(buyinBaseCostColumn);
                if (buyinRakeCostOrderColumn == c) newColumns.Add(buyinRakeCostColumn);
                if (buyinTotalCostOrderColumn == c) newColumns.Add(buyinTotalCostColumn);
                if (entrantsOrderColumn == c) newColumns.Add(entrantsColumn);
                if (entrantsPaidOrderColumn == c) newColumns.Add(entrantsPaidColumn);
                if (isBovadaBountyOrderColumn == c) newColumns.Add(isBovadaBountyColumn);
                if (isSngOrderColumn == c) newColumns.Add(isSngColumn);
                if (formatOrderColumn == c) newColumns.Add(formatStringColumn);
                if (gameTypeOrderColumn == c) newColumns.Add(gameTypeColumn);
                if (guaranteeOrderColumn == c) newColumns.Add(guaranteeColumn);
                if (lateRegOrderColumn == c) newColumns.Add(lateRegColumn);
                if (rebuyBaseCostOrderColumn == c) newColumns.Add(rebuyBaseCostColumn);
                if (rebuyRakeCostOrderColumn == c) newColumns.Add(rebuyRakeCostColumn);
                if (rebuyTotalCostOrderColumn == c) newColumns.Add(rebuyTotalCostColumn);
                if (stackSizeStartingOrderColumn == c) newColumns.Add(stackSizeStartingColumn);
                if (stackSizeAddonOrderColumn == c) newColumns.Add(stackSizeAddonColumn);
                if (stackSizeRebuyOrderColumn == c) newColumns.Add(stackSizeRebuyColumn);
                if (startTimeOrderColumn == c) newColumns.Add(startTimeColumn);
                if (tableSizeOrderColumn == c) newColumns.Add(tableSizeColumn);
                if (tournamentNameOrderColumn == c) newColumns.Add(tournamentNameColumn);
                if (venuesOrderColumn == c) newColumns.Add(venueColumn);
            }

            // clear columns
            templateManagerView.TemplatesDataGrid.Columns.Clear();

            // add the adjusted columns
            foreach (var column in newColumns)
                templateManagerView.TemplatesDataGrid.Columns.Add(column);

            // refresh the grid
            templateManagerView.TemplatesDataGrid.Measure(new Size(1,1));
        }
    }
}

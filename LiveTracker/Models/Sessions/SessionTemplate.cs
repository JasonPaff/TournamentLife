using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Syncfusion.Windows.Shared;
using Tournament_Life.Helpers;
using Tournament_Life.Models.Tournaments;

namespace Tournament_Life.Models.Sessions
{
    public class SessionTemplate : NotificationObject
    {
        public SessionTemplate()
        {
            TemplateIds = new ObservableCollection<int>();
            IsFavorite = true; // temp hack
        }

        public string Description { get; set; }
        public string FontColor => PreferencesHelper.FindPreference("LiveTracker", "Window", "FontColor");
        public string FontSize => PreferencesHelper.FindPreference("LiveTracker", "Window", "FontSize");
        public bool IsFavorite { get; set; }
        public int SessionId { get; set; }
        public string SessionIdString => "Session" + SessionId;
        public string SessionName { get; set; }
        public ObservableCollection<int> TemplateIds { get; private set; }
        public string TemplateNamesString { get; set; }

        public ICommand CommandContextMenuRemove { get; set; }
        public ICommand CommandContextMenuAdd { get; set; }
        public ICommand CommandCopy { get; set; }
        public ICommand CommandDelete { get; set; }
        public ICommand CommandEdit { get; set; }
        public ICommand CommandSessionsMenu { get; set; }
        public ICommand Command { get; set; }

        public void AddTemplateToSession(int templateId)
        {
            TemplateIds.Add(templateId);
        }
        public void ClearTemplates()
        {
            TemplateIds.Clear();
        }
        public int RemoveTemplateFromSession(int removeID)
        {
            foreach (int id in TemplateIds.Where(i => i == removeID).ToList())
                TemplateIds.Remove(id);

            return TemplateIds.Where(i => i == removeID).Count();
        }
        public void UpdateTemplatesDescription(ObservableCollection<TournamentTemplate> templateList)
        {
            if (templateList is null || templateList.Count is 0 || TemplateIds.Count is 0) return;

            templateList = new ObservableCollection<TournamentTemplate>(templateList.OrderBy(x => x.StartTime));

            var description = new StringBuilder();

            // loop the passed id's and build the description from the matching templates
            foreach (int templateId in TemplateIds) description.AppendLine(templateList.FirstOrDefault(template => template.TemplateId == templateId)?.TournamentName);

            Description = description.ToString().Trim();
        }
        public void UpdateTournamentNamesString(ObservableCollection<TournamentTemplate> templates = null)
        {
            var tournamentNamesStringBuilder = new StringBuilder();

            templates ??= TournamentTemplateHelper.LoadTemplates();

            foreach (var id in TemplateIds)
                tournamentNamesStringBuilder.AppendLine(templates.FirstOrDefault(x => x.TemplateId == id)?.TournamentName);

            TemplateNamesString = tournamentNamesStringBuilder.ToString();
        }
    }
}

using System.Xml;
using System.Collections.ObjectModel;
using System.Linq;
using System.Globalization;
using LiveTracker.Models;
using LiveTracker.Models.Sessions;

namespace LiveTracker.Helpers
{
    public static class SessionTemplateHelper
    {
        /// <summary>
        /// Create list box items out of a collection of session templates
        /// </summary>
        /// <param name="sessions">sessions</param>
        /// <returns>list box items</returns>
        public static ObservableCollection<ListBoxItem> CreateListBoxItems(ObservableCollection<SessionTemplate> sessions)
        {
            // null/zero check
            if (sessions is null || sessions.Count is 0)
                return null;

            // temp collection
            var items = new ObservableCollection<ListBoxItem>();

            // create list box items
            foreach (var template in sessions)
            {
                var item = new ListBoxItem
                {
                    IsSelected = false,
                    Name = template.SessionName,
                    Id = template.SessionId,
                    Description = template.Description,
                };
                items.Add(item);
            }
            return items;
        }

        /// <summary>
        /// Deletes a session from the session file
        /// </summary>
        /// <param name="id">session to delete</param>
        public static void DeleteSession(int id)
        {
            // load sessions file
            var fileName = XmlHelper.PreferencesFolderName + ProfileHelper.GetCurrentProfile() + XmlHelper.SessionsFileName;
            var xmlDocument = XmlHelper.LoadXmlFile(fileName, null);

            if (xmlDocument is null)
                return;

            // load sessions
            var sessionNodeList = xmlDocument?.DocumentElement?.SelectNodes("Session");
            if (sessionNodeList is null)
                return;

            // find matching session and remove
            foreach (XmlNode sessionNode in sessionNodeList)
                if (int.Parse(sessionNode.Attributes?["ID"]?.Value ?? "-1", new CultureInfo("en-US")) == id)
                    xmlDocument.DocumentElement.RemoveChild(sessionNode);

            // save sessions
            xmlDocument.Save(fileName);
        }

        /// <summary>
        /// Load the session templates from file
        /// </summary>
        /// <param name="templates">templates file to use to build the sessions</param>
        /// <returns>sessions collection</returns>
        public static ObservableCollection<SessionTemplate> LoadSessionTemplates(ObservableCollection<TournamentTemplate> templates = null)
        {
            // load sessions file
            var fileName = XmlHelper.PreferencesFolderName + ProfileHelper.GetCurrentProfile() + XmlHelper.SessionsFileName;
            var xmlDocument = XmlHelper.LoadXmlFile(fileName);

            // sessions temp collection
            var sessions = new ObservableCollection<SessionTemplate>();

            // null check
            if (xmlDocument is null)
                return sessions;

            // load from file if passed collection is null (nothing was passed)
            templates ??= TournamentTemplateHelper.LoadTemplates();

            // get session nodes
            var sessionNodeList = xmlDocument?.DocumentElement?.SelectNodes("Session");
            if (sessionNodeList is null)
                return sessions;

            // load/create all the session templates from the xml file
            foreach (XmlNode sessionNode in sessionNodeList)
            {
                // create session template with name and id
                var sessionTemplate = new SessionTemplate()
                {
                    SessionId = int.Parse(sessionNode.Attributes?["ID"]?.Value ?? "-1", new CultureInfo("en-US")),
                    SessionName = sessionNode.SelectSingleNode("Name")?.InnerText ?? "Error",
                };

                // add template ids to session template
                var templateNodeList = sessionNode.SelectNodes("Template");
                if (templateNodeList is not null)
                    foreach (XmlNode templateNode in templateNodeList)
                        sessionTemplate.AddTemplateToSession(int.Parse(templateNode.InnerText, new CultureInfo("en-US")));

                // update template description
                sessionTemplate.UpdateTemplatesDescription(templates);

                // add session template to sessions collection
                sessions.Add(sessionTemplate);
            }

            // return sessions sorted by name
            return new ObservableCollection<SessionTemplate>(sessions.OrderBy(i => i.SessionName));
        }

        /// <summary>
        /// Loads the session templates from the xml file
        /// </summary>
        /// <param name="sessionId">session id</param>
        /// <param name="templates">templates to use to create sessions</param>
        /// <returns>collection of sessions</returns>
        public static SessionTemplate LoadSessionTemplate(int sessionId, ObservableCollection<TournamentTemplate> templates = null)
        {
            // load sessions file
            var fileName = XmlHelper.PreferencesFolderName + ProfileHelper.GetCurrentProfile() + XmlHelper.SessionsFileName;
            var xmlDocument = XmlHelper.LoadXmlFile(fileName, null);

            // temp session
            var session = new SessionTemplate()
            {
                SessionId = -1,
                SessionName = "Blank",
            };

            if (xmlDocument is null)
                return session;

            // load from file if no templates passed in
            templates ??= TournamentTemplateHelper.LoadTemplates();

            // get session nodes
            var sessionNodeList = xmlDocument?.DocumentElement?.SelectNodes("Session");
            if (sessionNodeList is null)
                return session;

            foreach (XmlNode sessionNode in sessionNodeList)
            {
                // if id doesn't match continue to the next sessionNode
                if (int.Parse(sessionNode.Attributes?["ID"]?.Value ?? "-1", new CultureInfo("en-US")) != sessionId)
                    continue;

                // create new session template with name and id number
                session = new SessionTemplate()
                {
                    SessionId = int.Parse(sessionNode.Attributes?["ID"]?.Value ?? "-1", new CultureInfo("en-US")),
                    SessionName = sessionNode?.SelectSingleNode("Name")?.InnerText ?? "",
                };

                // add template ids to session template
                var templateNodeList = sessionNode.SelectNodes("Template");
                if (templateNodeList is not null)
                    foreach (XmlNode templateNode in templateNodeList)
                        session.AddTemplateToSession(int.Parse(templateNode.InnerText, new CultureInfo("en-US")));

                // update session description
                session.UpdateTemplatesDescription(templates);

                // return session
                return session;
            }

            // return session
            return session;
        }

        /// <summary>
        /// Gets a new id number for a session
        /// </summary>
        /// <returns>new session id number</returns>
        public static int NewSessionId()
        {
            // load sessions file
            var fileName = XmlHelper.PreferencesFolderName + ProfileHelper.GetCurrentProfile() + XmlHelper.SessionsFileName;
            var xmlDocument = XmlHelper.LoadXmlFile(fileName, null);

            // return 1 if no file found
            if(xmlDocument is null)
                return 1;

            // return 1 if file is blank
            if (xmlDocument.GetElementsByTagName("Session").Count == 0)
                return 1;

            // grab all the id's from the xml file
            var ids = (from XmlNode sessionNode in xmlDocument.GetElementsByTagName("Session") select int.Parse(sessionNode.Attributes?["ID"]?.Value ?? "-1", new CultureInfo("en-US"))).ToList();

            // return the next number after the highest id number we found in the xml file
            return ids.Max() + 1;
        }

        /// <summary>
        /// Remove template from sessions
        /// </summary>
        /// <param name="template">template to remove</param>
        public static void RemoveTemplateFromSessions(TournamentTemplate template)
        {
            // null check
            if (template is null || template.TemplateId is -1)
                return;

            // find sessions that contain the templates id number and remove it
            foreach (var sessionTemplate in LoadSessionTemplates())
            {
                var removedCount = sessionTemplate.RemoveTemplateFromSession(template.TemplateId);

                if (removedCount > 0)
                {
                    template.NumberOfTemplatesRemoved.Add(removedCount);
                    template.RemovedFromSessionIds.Add(sessionTemplate.SessionId);
                }

                ReplaceSession(sessionTemplate);
            }
        }

        /// <summary>
        /// Replace a session in the file with an update version
        /// </summary>
        /// <param name="session">session to replace</param>
        public static void ReplaceSession(SessionTemplate session)
        {
            // null check
            if (session is null)
                return;

            // delete old session
            DeleteSession(session.SessionId);

            // save new session
            SaveSession(session.SessionName, session.TemplateIds, session.SessionId);
        }

        /// <summary>
        /// Saves a session to the file
        /// </summary>
        /// <param name="name">session name</param>
        /// <param name="templateIdList">session template ids</param>
        /// <param name="id">session id</param>
        public static void SaveSession(string name, ObservableCollection<int> templateIdList, int id = -1)
        {
            // null/zero check
            if (templateIdList is null || templateIdList.Count is 0)
                return;

            // load sessions file
            var fileName = XmlHelper.PreferencesFolderName + ProfileHelper.GetCurrentProfile() + XmlHelper.SessionsFileName;
            var xmlDocument = XmlHelper.LoadXmlFile(fileName, null);

            // create file if doesn't exist
            if (xmlDocument is null)
            {
                xmlDocument = new XmlDocument();
                var root = xmlDocument.CreateElement("Sessions");
                xmlDocument.AppendChild(root);
                xmlDocument.Save(fileName);
                xmlDocument = XmlHelper.LoadXmlFile(fileName, null);
            }

            // null check
            if (xmlDocument is null)
                return;

            // create root node
            XmlNode sessionParentNode = xmlDocument.CreateElement("Session");
            var sessionParentNodeIdAtt = xmlDocument.CreateAttribute("ID");

            // if default '-1' use the next new id number, otherwise use the id number passed
            sessionParentNodeIdAtt.Value = id is -1 ? NewSessionId().ToString(new CultureInfo("en-US")) : id.ToString(new CultureInfo("en-US"));
            sessionParentNode.Attributes?.Append(sessionParentNodeIdAtt);

            // session name
            XmlNode sessionNameChildNode = xmlDocument.CreateElement("Name");
            sessionNameChildNode.InnerText = name;
            sessionParentNode.AppendChild(sessionNameChildNode);

            // template ids
            foreach (var templateId in templateIdList)
            {
                XmlNode templateNode = xmlDocument.CreateElement("Template");
                templateNode.InnerText = templateId.ToString(new CultureInfo("en-US"));
                sessionParentNode.AppendChild(templateNode);
            }

            // add session to file
            xmlDocument?.DocumentElement?.AppendChild(sessionParentNode);

            // save file
            xmlDocument.Save(fileName);
        }

        /// <summary>
        /// Saves a session using a session template
        /// </summary>
        /// <param name="session">session to save</param>
        public static void SaveSession(SessionTemplate session)
        {
            // null check
            if (session is null)
                return;

            // save session
            SaveSession(session.SessionName, session.TemplateIds, session.SessionId);
        }

        /// <summary>
        /// Sort list box items by starting time
        /// </summary>
        /// <param name="list">items to sort</param>
        /// <returns>sorted items</returns>
        public static ObservableCollection<SessionListBoxItem> SortListBoxItemsByStartTime(ObservableCollection<SessionListBoxItem> list)
        {
            // null/zero check
            if (list is null || list.Count is 0)
                return list;

            // order list by start time minutes then hour
            list = new ObservableCollection<SessionListBoxItem>(list.OrderBy(i => i.StartTime.Minute));
            list = new ObservableCollection<SessionListBoxItem>(list.OrderBy(i => i.StartTime.Hour));

            // copy 12 a.m. to 4a.m templates to a temp list
            ObservableCollection<SessionListBoxItem> midnightToFourListBoxItems = list.Any(i => i.StartTime.Hour <= 4) ? new ObservableCollection<SessionListBoxItem>(list.Where(i => i.StartTime.Hour <= 4)) : new ObservableCollection<SessionListBoxItem>();

            // remove any 12-4 from the main list
            if (midnightToFourListBoxItems.Any())
                foreach (SessionListBoxItem item in midnightToFourListBoxItems)
                    list.Remove(item);

            // order the 12-4 list
            midnightToFourListBoxItems = new ObservableCollection<SessionListBoxItem>(midnightToFourListBoxItems.OrderBy(i => i.StartTime.Minute));
            midnightToFourListBoxItems = new ObservableCollection<SessionListBoxItem>(midnightToFourListBoxItems.OrderBy(i => i.StartTime.Hour));

            // add 12-4 list templates to the end of the main list
            foreach (SessionListBoxItem item in midnightToFourListBoxItems)
                list.Add(item);

            // return sorted list
            return list;
        }

        /// <summary>
        /// sorts a sessions templates by starting time
        /// </summary>
        /// <param name="templates">templates used by the session</param>
        /// <param name="session">session to sort</param>
        /// <returns>sorted session</returns>
        public static SessionTemplate SortTournamentsByStartTime(ObservableCollection<TournamentTemplate> templates, SessionTemplate session)
        {
            if (session is null)
                return null;

            if (templates is null || templates.Count is 0)
                return session;

            // temporary list of the session we are sorting tournament templates
            var sessionTemplates = new ObservableCollection<TournamentTemplate>();

            // find and add the matching templates to the temp list
            foreach (var id in session.TemplateIds)
                if (templates.Any(i => i.TemplateId == id))
                    sessionTemplates.Add(templates.FirstOrDefault(i => i.TemplateId == id));

            // sort temp list by starting time, minute then hour
            sessionTemplates = new ObservableCollection<TournamentTemplate>(sessionTemplates.OrderBy(i => i.StartTime.Minute));
            sessionTemplates = new ObservableCollection<TournamentTemplate>(sessionTemplates.OrderBy(i => i.StartTime.Hour));

            // copy 12 a.m. to 4a.m templates to a temp list
            var midnightToFourTemplates = sessionTemplates.Any(i => i.StartTime.Hour <= 4)
                ? new ObservableCollection<TournamentTemplate>(sessionTemplates.Where(i => i.StartTime.Hour <= 4))
                    : new ObservableCollection<TournamentTemplate>();

            // remove any 12-4 from the main list
            if (midnightToFourTemplates.Any())
                foreach (var template in midnightToFourTemplates)
                    sessionTemplates.Remove(template);

            // order the 12-4 list
            midnightToFourTemplates = new ObservableCollection<TournamentTemplate>(midnightToFourTemplates.OrderBy(i => i.StartTime.Minute));
            midnightToFourTemplates = new ObservableCollection<TournamentTemplate>(midnightToFourTemplates.OrderBy(i => i.StartTime.Hour));

            // add 12-4 list templates to the end of the main list
            foreach (var template in midnightToFourTemplates)
                sessionTemplates.Add(template);

            // clear old unordered templates
            session.ClearTemplates();

            // add new ordered templates
            foreach (var template in sessionTemplates)
                session.AddTemplateToSession(template.TemplateId);

            // return session
            return session;
        }

        /// <summary>
        /// Sorts tournaments in sessions based on date/time played
        /// </summary>
        /// <param name="tournaments">tournaments to sort</param>
        /// <returns>sorted tournaments</returns>
        public static ObservableCollection<SessionModel> SortTournamentsIntoSessions(ObservableCollection<TournamentFinished> tournaments)
        {
            // null/zero check
            if (tournaments is null || tournaments.Count is 0)
                return null;

            // sort tournaments by starting time
            tournaments = new ObservableCollection<TournamentFinished>(tournaments.OrderBy(i => i.StartTime));

            // temp session and sessions collection
            var sessions = new ObservableCollection<SessionModel>();
            var session = new SessionModel();

            // starting and ending time set to the earliest tournament in the collection
            var startTime = tournaments.ElementAt(0).StartTime;
            var endTime = tournaments.ElementAt(0).EndTime;

            // loop tournaments and sort into sessions
            foreach (var tournament in tournaments)
            {
                // check if tournament is in current session
                if (tournament.StartTime >= startTime && tournament.StartTime <= endTime)
                {
                    // check if session end time needs to be extended
                    if (tournament.EndTime >= endTime)
                        endTime = tournament.EndTime;

                    // add tournament to session
                    session.AddTournament(tournament);
                }

                // go next if its the same session
                if (tournament.StartTime <= endTime)
                    continue;

                // new session id
                session.ID = sessions.Count + 1;

                // add session to sessions collection
                sessions.Add(session);

                // create new temp session
                session = new SessionModel();

                // add tournament to session
                session.AddTournament(tournament);

                // update start and time for new session
                startTime = tournament.StartTime;
                endTime = tournament.EndTime;
            }

            // add final session to sessions collection
            sessions.Add(session);

            // set final id after adding
            session.ID = sessions.Count;

            // return sessions collection
            return sessions;
        }
    }
}
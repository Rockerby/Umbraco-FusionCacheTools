using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.Dashboards;

namespace FusionCacheTools.BackOffice.Dashboards
{
    [Weight(-10)]
    public class SettingsCacheDashboard : IDashboard
    {
        public string Alias => "fcv_settingsCacheDashboard";

        public string[] Sections => new[]
        {
            Constants.Applications.Content,
            Constants.Applications.Members,
            Constants.Applications.Settings
        };

        public string View => "/App_Plugins/FusionCacheTools/settingsCacheDashboard.html";
        public IAccessRule[] AccessRules => Array.Empty<IAccessRule>();

        //public IAccessRule[] AccessRules
        //{
        //    get
        //    {
        //        var rules = new IAccessRule[]
        //        {
        //            new AccessRule {Type = AccessRuleType.Deny, Value = Constants.Security.TranslatorGroupAlias},
        //            new AccessRule {Type = AccessRuleType.Grant, Value = Constants.Security.AdminGroupAlias}
        //        };
        //        return rules;
        //    }
        //}

    }
}

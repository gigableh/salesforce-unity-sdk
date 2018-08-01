using UnityEngine;
using Boomlagoon.JSON;

namespace Salesforce
{
    public class UnityTestSFR : SalesforceRecord
    {
        public const string BASE_QUERY = "SELECT Id, EventsName, Milliseconds FROM UnityTest";

        public string Events_Name__c;
        public string Milliseconds__c;

        public UnityTestSFR() {}

        public UnityTestSFR(string id, string eventsName, string milliseconds) : base(id) {
            this.Events_Name__c = eventsName;
            this.Milliseconds__c = milliseconds;
        }

        public override string getSObjectName() {
            return "Unity_Test__c";
        }

        public override JSONObject toJson() {
            JSONObject record = base.toJson();
            record.Add("Events_Name__c", Events_Name__c);
            record.Add("Milliseconds__c", Milliseconds__c);
            return record;
        }

        public override void parseFromJson(JSONObject jsonObject) {
            base.parseFromJson(jsonObject);
            Events_Name__c = jsonObject.GetString("Events_Name__c");
            Milliseconds__c = jsonObject.GetString("Milliseconds__c");
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Salesforce;
using Boomlagoon.JSON;
using System;

[RequireComponent(typeof(SalesforceClient))]
public class TestSalesforceClient : MonoBehaviour
{
    IEnumerator Start()
    {
        // Get Salesforce client component
        SalesforceClient sfdcClient = GetComponent<SalesforceClient>();

        // Init client & log in
        Coroutine<bool> loginRoutine = this.StartCoroutine<bool>(
            sfdcClient.login()
        );
        yield return loginRoutine.coroutine;
        try {
            loginRoutine.getValue();
            Debug.Log("Salesforce login successful.");
        }
        catch (SalesforceConfigurationException e) {
            Debug.Log("Salesforce login failed due to invalid auth configuration");
            throw e;
        }
        catch (SalesforceAuthenticationException e) {
            Debug.Log("Salesforce login failed due to invalid credentials");
            throw e;
        }
        catch (SalesforceApiException e) {
            Debug.Log(e.Message);
            //Debug.Log("Salesforce login failed");
            throw e;
        }

        // Test insert to Unity_Test object.
        StartCoroutine(sfdcClient.insert(new UnityTestSFR() {
            Events_Name__c = "Test from Unity",
            Milliseconds__c = DateTime.UtcNow.Millisecond.ToString()
        }));

        // Get some cases
        string query = CaseSFR.BASE_QUERY + " ORDER BY Subject LIMIT 5";
        Coroutine<List<CaseSFR>> getCasesRoutine = this.StartCoroutine<List<CaseSFR>>(
            sfdcClient.query<CaseSFR>(query)
        );
        yield return getCasesRoutine.coroutine;
        List<CaseSFR> cases = getCasesRoutine.getValue();
        Debug.Log("Retrieved " + cases.Count + " cases");

        // Create sample case
        CaseSFR caseRecord = new CaseSFR(null, "Test case", "New");
        Coroutine<CaseSFR> insertCaseRoutine = this.StartCoroutine<CaseSFR>(
            sfdcClient.insert(caseRecord)
        );
        yield return insertCaseRoutine.coroutine;
        insertCaseRoutine.getValue();
        Debug.Log("Case created");

        // Update sample case
        caseRecord.subject = "Updated test case";
        caseRecord.status = "Closed";
        Coroutine<string> updateCaseRoutine = this.StartCoroutine<string>(
            sfdcClient.update(caseRecord)
        );
        yield return updateCaseRoutine.coroutine;
        updateCaseRoutine.getValue();
        Debug.Log("Case updated");

        // Delete sample case
        // Coroutine<string> deleteCaseRoutine = this.StartCoroutine<string>(
        //     sfdcClient.delete(caseRecord)
        // );
        // yield return deleteCaseRoutine.coroutine;
        // deleteCaseRoutine.getValue();
        // Debug.Log("Case deleted");
    }
}

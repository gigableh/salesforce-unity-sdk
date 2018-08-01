using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="Salesforce Credentials", menuName="Salesforce Credentials")]
public class SalesforceCredentials : ScriptableObject
{
	public string consumerKey;
	public string consumerSecret;
	public string username;
	public string password;
	public string securityToken;
	
	public string getPasswordWithToken()
	{
		return password + securityToken;
	}
}

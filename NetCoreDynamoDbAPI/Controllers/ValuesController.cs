using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetCoreDynamoDbAPI.Models;
using Microsoft.AspNetCore.Http;
using Amazon;
using Amazon.Runtime;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.DataModel;

namespace NetCoreDynamoDbAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ValuesController : ControllerBase
	{
		private readonly string AWSAccessKeyId = "";
		private readonly string AWSSecretAccessKey = "";
		private readonly string tableName = "";

		// GET api/values
		[HttpGet]
		public async Task<IActionResult> GetAsync()
		{
			var credentials = new BasicAWSCredentials(AWSAccessKeyId, AWSSecretAccessKey);
			var client = new AmazonDynamoDBClient(credentials, RegionEndpoint.USWest2);

			ScanFilter scanFilter = new ScanFilter();
			// scanFilter.AddCondition("FirstName", ScanOperator.Equal, "Luis");
			ScanOperationConfig soc = new ScanOperationConfig()
			{
				// AttributesToGet = new List { "Id", "Title", "ISBN", "Price" },
				Filter = scanFilter
			};
			DynamoDBContext context = new DynamoDBContext(client);
			AsyncSearch<EmployeesPTU> search = context.FromScanAsync<EmployeesPTU>(soc, null);
			List<EmployeesPTU> documentList = new List<EmployeesPTU>();
			do
			{
				documentList = await search.GetNextSetAsync(default(System.Threading.CancellationToken));
			} while (!search.IsDone);

			return Ok(documentList);
		}

		// GET api/values/5
		[HttpGet("{firstName}/{lastName}/{secondLastName}/{curp}")]
		public async Task<IActionResult> Get(string firstName, string lastName, string secondLastName, string curp)
		{
			var credentials = new BasicAWSCredentials(AWSAccessKeyId, AWSSecretAccessKey);
			var client = new AmazonDynamoDBClient(credentials, RegionEndpoint.USWest2);

			ScanFilter scanFilter = new ScanFilter();
			scanFilter.AddCondition("FirstName", ScanOperator.Equal, firstName);
			scanFilter.AddCondition("LastName", ScanOperator.Equal, lastName);
			scanFilter.AddCondition("SecondLastName", ScanOperator.Equal, secondLastName);
			scanFilter.AddCondition("CURP", ScanOperator.Equal, curp);
			ScanOperationConfig soc = new ScanOperationConfig()
			{
				// AttributesToGet = new List { "Id", "Title", "ISBN", "Price" },
				Filter = scanFilter
			};
			DynamoDBContext context = new DynamoDBContext(client);
			AsyncSearch<EmployeesPTU> search = context.FromScanAsync<EmployeesPTU>(soc, null);
			List<EmployeesPTU> documentList = new List<EmployeesPTU>();
			do
			{
				documentList = await search.GetNextSetAsync(default(System.Threading.CancellationToken));
			} while (!search.IsDone);

			return Ok(documentList);
		}

		// POST api/values
		[HttpPost]
		public IActionResult NewItem([FromBody] EmployeesPTU employee)
		{
			try
			{
				var credentials = new BasicAWSCredentials(AWSAccessKeyId, AWSSecretAccessKey);
				var client = new AmazonDynamoDBClient(credentials, RegionEndpoint.USWest2);
				Table ptuTable = Table.LoadTable(client, tableName);
				Create(ptuTable, new EmployeesPTU { 
					EmployeeID = employee.EmployeeID,
					FirstName = employee.FirstName,
					LastName = employee.LastName,
					SecondLastName = employee.SecondLastName,
					CURP = employee.CURP
				});
				return Ok();
			}
			catch (Exception e)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, e);
			}
		}

		void Create(Table dynamoTable, EmployeesPTU data)
		{
			var item = new Document();
			item["EmployeeID"] = data.EmployeeID;
			item["FirstName"] = data.FirstName;
			item["LastName"] = data.LastName;
			item["SecondLastName"] = data.SecondLastName;
			item["CURP"] = data.CURP;
			dynamoTable.PutItemAsync(item);
		}

		// DELETE api/values/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(long id)
		{
			var credentials = new BasicAWSCredentials(AWSAccessKeyId, AWSSecretAccessKey);
			var client = new AmazonDynamoDBClient(credentials, RegionEndpoint.USWest2);
			DynamoDBContext context = new DynamoDBContext(client);
			await context.DeleteAsync<EmployeesPTU>(id, default(System.Threading.CancellationToken));
			return Ok();
		}
	}
}

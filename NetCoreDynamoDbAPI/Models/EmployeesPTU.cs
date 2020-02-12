using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;

namespace NetCoreDynamoDbAPI.Models
{
	[DynamoDBTable("americas-chi-ptu")]
	public class EmployeesPTU
	{
		[DynamoDBHashKey]  //Partition key
		public long EmployeeID { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string SecondLastName { get; set; }
		public string CURP { get; set; }
	}
}

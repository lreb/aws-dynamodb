//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Amazon.DynamoDBv2;
//using Amazon.DynamoDBv2.DocumentModel;
//using Amazon.DynamoDBv2.DataModel;
//using NetCoreDynamoDbAPI.Models;

//namespace NetCoreDynamoDbAPI.Services
//{
//	public class DynamoDBServices
//	{
//		IAmazonDynamoDB dynamoDBClient { get; set; }

//		public DynamoDBServices(IAmazonDynamoDB dynamoDBClient)
//		{
//			this.dynamoDBClient = dynamoDBClient;
//		}

//		public async Task InsertBook(EmployeesPTU employee)
//		{
//			DynamoDBContext context = new DynamoDBContext(dynamoDBClient);
//			// Add a unique id for the primary key.
//			employee.EmployeeID = 1421574;// System.Guid.NewGuid().ToString();
//			await context.SaveAsync(employee, default(System.Threading.CancellationToken));
//			EmployeesPTU newBook = await context.LoadAsync(employee.EmployeeID, default(System.Threading.CancellationToken));
//			return employee;
//		}

//		public async Task GetBookAsync(string Id)
//		{
//			DynamoDBContext context = new DynamoDBContext(dynamoDBClient);
//			Book newBook = await context.LoadAsync(Id, default(System.Threading.CancellationToken));
//			return newBook;
//		}

//		public async Task UpdateBookAsync(Book book)
//		{
//			DynamoDBContext context = new DynamoDBContext(dynamoDBClient);
//			await context.SaveAsync(book, default(System.Threading.CancellationToken));
//			Book newBook = await context.LoadAsync(book.Id, default(System.Threading.CancellationToken));
//			return newBook;
//		}
//		public async Task DeleteBookAsync(string Id)
//		{
//			DynamoDBContext context = new DynamoDBContext(dynamoDBClient);
//			await context.DeleteAsync(Id, default(System.Threading.CancellationToken));
//		}
//		public async Task&lt;List&gt; GetBooksAsync()
//		{
//			ScanFilter scanFilter = new ScanFilter();
//			scanFilter.AddCondition("Id", ScanOperator.NotEqual, 0);

//			ScanOperationConfig soc = new ScanOperationConfig()
//			{
//				// AttributesToGet = new List { "Id", "Title", "ISBN", "Price" },
//				Filter = scanFilter
//			};
//			DynamoDBContext context = new DynamoDBContext(dynamoDBClient);
//			AsyncSearch search = context.FromScanAsync(soc, null);
//			List documentList = new List();
//			do
//			{
//				documentList = await search.GetNextSetAsync(default(System.Threading.CancellationToken));
//			} while (!search.IsDone);

//			return documentList;
//		}
//	}
//}

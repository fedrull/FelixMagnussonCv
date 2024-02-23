using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Data
{
    public class MongoCrud<T>
    {
        private IMongoDatabase db;











        public MongoCrud (string database)
        {
            var client = new MongoClient("");
            db = client.GetDatabase(database);
        }

		public async Task<T> Add<T>(GenericMethodsInterface g, string table)
		{
			var collection = db.GetCollection<T>(table);
			var documnet = (T)g;
			await collection.InsertOneAsync(documnet);
			return documnet;
		}

		public async Task<List<T>> GetAll(string table)
        {
            var collection = db.GetCollection<T>(table);
            var AllT = await collection.AsQueryable().ToListAsync();
            return AllT;
        }


		public async Task<bool> DeleteById(string table, string id)
		{
			var collection = db.GetCollection<T>(table);
			var filter = Builders<T>.Filter.Eq("Id", id);
			var result = await collection.DeleteOneAsync(filter);
			return result.DeletedCount > 0;
		}

		//public async Task<bool> UpdateById(string table, string id, T updateObject)
		//{
		//	var collection = db.GetCollection<T>(table);

		//	var filter = Builders<T>.Filter.Eq("Id", id); 
		//  var update = Builders<T>.Update;

		//	var result = await collection.UpdateOneAsync(filter, update);
		//	return result.ModifiedCount > 0;
		//}

		//public async Task<bool> UpdateById<T>(string table, string id, GenericMethodsInterface g) where T : GenericMethodsInterface
		//{
		//	var collection = db.GetCollection<T>(table);
		//	var filter = Builders<T>.Filter
		//	.Eq("_id", id );
		//	var documnet = (T)g;
		//	var result = await collection.ReplaceOneAsync(filter, documnet);
		//	return result.ModifiedCount > 0;
		 



		public async Task<bool> UpdateById<T>(string table, string id, GenericMethodsInterface g) where T : GenericMethodsInterface
		{
			var collection = db.GetCollection<T>(table);

			// Assuming id is a string that needs to be converted to ObjectId
			// This step is crucial if your MongoDB uses ObjectId for _id fields.
			// If your database configuration uses string IDs instead of ObjectId, you might not need this conversion.
			if (!ObjectId.TryParse(id, out var objectId))
			{
				// Handle the case where id conversion fails, might need to log or throw an exception based on your application needs
				return false;
			}
			var filter = Builders<T>.Filter.Eq("_id", objectId);

			var document = (T)g;

			// ReplaceOneAsync will replace the document that matches the filter with the new document provided
			var result = await collection.ReplaceOneAsync(filter, document, new ReplaceOptions { IsUpsert = false });

			return result.ModifiedCount > 0;
		}

		
	}
}

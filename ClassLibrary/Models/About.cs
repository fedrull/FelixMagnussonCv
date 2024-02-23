using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Models
{
    public class About : GenericMethodsInterface
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public string Fullname { get; set; }
        public int  Age { get; set; }
        public string Phonenumber { get; set; }
        public string Email { get; set; }
		public string Description { get; set; }

	}
}

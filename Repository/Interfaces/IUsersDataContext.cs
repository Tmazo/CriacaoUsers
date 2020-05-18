using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interfaces
{
    public interface IUsersDataContext
    {
        string ConnectionString();
        string MongoUrl();
        string MongoClient();
        string IMongoDatabase();        
        string IMongoCollection();        
    }
}

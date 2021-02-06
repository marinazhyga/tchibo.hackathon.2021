using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Security.Authentication;
using TchiboFamilyCircle.Entities;
using TchiboFamilyCircle.Settings;

namespace TchiboFamilyCircle.DataContext
{
    public class TchiboFamillyCircleDataContext
    {
        private readonly IOptions<AppSettings> _settings;
        private readonly IMongoDatabase _database;

        public TchiboFamillyCircleDataContext(IOptions<AppSettings> settings)
        {
            _settings = settings;

            var mongoClientSettings = MongoClientSettings.FromUrl(new MongoUrl(_settings.Value.ConnectionString));

            mongoClientSettings.SslSettings = new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };

            var client = new MongoClient(mongoClientSettings);           

            if (client != null)
            {
                _database = client.GetDatabase(_settings.Value.Database);
            }
        }

        public IMongoCollection<FamilyMemberEntity> FamilyMemberEntities
        {
            get
            {
                return _database.GetCollection<FamilyMemberEntity>(_settings.Value.CollectionName);
            }
        }
    }
}

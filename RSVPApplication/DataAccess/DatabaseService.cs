using SQLite;
using RSVPApplication.Models;

namespace RSVPApplication.DataAccess
{
    public class DatabaseService
    {
        private SQLiteAsyncConnection? database;

        private async Task Init()
        {
            if (database is not null)
                return;

            string databasePath = Path.Combine(
                FileSystem.AppDataDirectory,
                "RSVPApplication.db3");

            database = new SQLiteAsyncConnection(databasePath);

            await database.CreateTableAsync<User>();
            await database.CreateTableAsync<Event>();
            await database.CreateTableAsync<RSVP>();
        }

        public async Task<List<User>> GetUsersAsync()
        {
            await Init();
            return await database!.Table<User>().ToListAsync();
        }

        public async Task<int> AddUserAsync(User user)
        {
            await Init();
            return await database!.InsertAsync(user);
        }

        public async Task<User?> LoginAsync(string username, string password)
        {
            await Init();

            return await database!
                .Table<User>()
                .Where(u => u.Username == username && u.Password == password)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Event>> GetEventsAsync()
        {
            await Init();
            return await database!.Table<Event>().ToListAsync();
        }

        public async Task<int> AddEventAsync(Event newEvent)
        {
            await Init();
            return await database!.InsertAsync(newEvent);
        }

        public async Task<List<RSVP>> GetRSVPsAsync()
        {
            await Init();
            return await database!.Table<RSVP>().ToListAsync();
        }

        public async Task<int> AddRSVPAsync(RSVP rsvp)
        {
            await Init();
            return await database!.InsertAsync(rsvp);
        }
    }
}
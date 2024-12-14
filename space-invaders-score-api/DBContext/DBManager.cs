using Npgsql;
using space_invaders_score_api.Entities;

namespace space_invaders_score_api.DBContext
{
    public class DBManager
    {
        private readonly string _connectionString;
        private NpgsqlConnection _conn;

        // Construtor que inicializa a string de conexão
        public DBManager()
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<Player> GetPlayerList()
        {
            List<Player> players = new();

            string query = "SELECT name, score FROM player ORDER BY score DESC;";

            try
            {
                OpenConnection();
                NpgsqlCommand cmd = new (query, _conn);
                NpgsqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Player player = new()
                    {
                        Name = reader.GetString(0),  
                        Score = reader.GetInt32(1)    
                    };

                    players.Add(player);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error accessing the database: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }

            return players;
        }

        public void AddPlayer(Player player)
        {
            string query = "INSERT INTO player (name, score) VALUES (@name, @score);";

            try
            {
                OpenConnection();

                NpgsqlCommand cmd = new(query, _conn);
                cmd.Parameters.AddWithValue("@name", player.Name);
                cmd.Parameters.AddWithValue("@score", player.Score);

                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw new Exception("Error acceing the database: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        private void OpenConnection()
        {
            _conn = new(_connectionString);
            _conn.Open();
        }

        private void CloseConnection()
        {
            if (_conn != null && _conn.State == System.Data.ConnectionState.Open)
            {
                _conn.Close();
            }
        }
    }
}

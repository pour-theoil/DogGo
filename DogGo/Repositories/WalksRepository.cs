using DogGo.Models;
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogGo.Repositories
{
    public class WalksRepository : IWalksRepository
    {
        private readonly IConfiguration _config;

        public WalksRepository(IConfiguration config)
        {
            _config = config;
        }

        public SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }

        public List<Walks> GetAllWalks()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {

                    cmd.CommandText = @"select w.Date, w.Duration,w.id, w.walkerId, w.dogid, d.Name as DogName, o.Name
                                            from Walks w left join dog d on w.DogId = d.Id
                                            join Owner o on o.Id = d.OwnerId 
                                            ;";

                    SqlDataReader reader = cmd.ExecuteReader();
                    List<Walks> walks = new List<Walks>();
                    while (reader.Read())
                    {
                        Walks walk = new Walks
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                            Duration = (reader.GetInt32(reader.GetOrdinal("Duration"))) / 60,
                            WalkerId = reader.GetInt32(reader.GetOrdinal("WalkerId")),
                            DogId = reader.GetInt32(reader.GetOrdinal("DogId")),
                            DogName = reader.GetString(reader.GetOrdinal("DogName")),
                            Client = new Owner
                            {
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                            }
                        };
                        walks.Add(walk);
                    }
                    reader.Close();
                    return walks;
                }
            }

        }
        public List<Walks> GetAllWalksByWalkerId(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {

                    cmd.CommandText = @"select w.Date, w.Duration,w.id, w.walkerId, w.dogid, o.Name
                                            from Walks w left join dog d on w.DogId = d.Id
                                            join Owner o on o.Id = d.OwnerId 
                                            where w.WalkerId = @id
                                            order by o.name;";
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    List<Walks> walks = new List<Walks>();
                    while (reader.Read())
                    {
                        Walks walk = new Walks
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                            Duration = (reader.GetInt32(reader.GetOrdinal("Duration"))) / 60,
                            WalkerId = reader.GetInt32(reader.GetOrdinal("WalkerId")),
                            DogId = reader.GetInt32(reader.GetOrdinal("DogId")),
                            Client = new Owner
                            {
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                            }
                        };
                        walks.Add(walk);
                    }
                    reader.Close();
                    return walks;
                }
            }
        }


        public int GetWalkerTime(int id)

        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {

                    cmd.CommandText = @"select sum(w.Duration) as Duration
                                            from Walks w left join dog d on w.DogId = d.Id
                                            join Owner o on o.Id = d.OwnerId 
                                            where w.WalkerId = @id
                                            ";
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    int totalwalks = 0;
                    while (reader.Read())
                    {
                        if (!reader.IsDBNull(reader.GetOrdinal("Duration")))
                        {
                            totalwalks = (reader.GetInt32(reader.GetOrdinal("Duration"))) / 60;

                        }


                    }
                    return totalwalks;
                }
            }
        }

        public void AddWalks(Walks Walks)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    INSERT INTO Walks (date, dogId, duration, walkerId)
                    OUTPUT INSERTED.ID
                    VALUES (@date, @dogId, @duration, @walkerId);
                ";

                    cmd.Parameters.AddWithValue("@date", Walks.Date);
                    cmd.Parameters.AddWithValue("@dogId", Walks.DogId);
                    cmd.Parameters.AddWithValue("@duration", (Walks.Duration) * 60);
                    cmd.Parameters.AddWithValue("@walkerId", Walks.WalkerId);


                    cmd.ExecuteNonQuery();


                }
            }
        }

        public void DeleteWalk(int walkId)
        {

            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            DELETE FROM Walks
                            WHERE Id = @id
                        ";

                    cmd.Parameters.AddWithValue("@id", walkId);

                    cmd.ExecuteNonQuery();
                }
            }

        }
    }
}

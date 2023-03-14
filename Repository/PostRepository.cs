using FindMyPet.Models;
using Dapper;
using MySqlConnector;
using FindMyPet.Repository.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Linq;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;
using System.Collections.Generic;
using System.Collections;
using System.Net.NetworkInformation;
using static System.Net.Mime.MediaTypeNames;
using System.Security.Cryptography;
using System.Text;

namespace FindMyPet.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly IDbConnection _mySql;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _entityId;

        public PostRepository(IConfiguration config, IHttpContextAccessor httpContextAccessor)
        {
            _mySql = new MySqlConnection(config.GetConnectionString("mySqlGeneral"));
            _httpContextAccessor = httpContextAccessor;
            _entityId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public async Task<bool> savePost(string image, string description, double latitude, double longitude)
        {
            try
            {
                int save = await _mySql.ExecuteAsync(@"
                        Insert into `FindMyPet`.`Post`
                        (userId, Image, Description, Latitude, Longitude, Date)
                        VALUES
                        (@UserId, @Image, @Description, @Latitude, @Longitude, @Date)
                      ",
                    new
                    {
                        UserId = _entityId,
                        Image = image,
                        Description = description,
                        Latitude = latitude,
                        Longitude = longitude,
                        Date = DateTime.Now,
                    });

                if(save > 0)
                { 
                    return true;
                }
                else
                {
                    return false;
                }
                
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Feed>> selectPosts(double latitude, double longitude)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@latt", latitude);
                p.Add("@longt", longitude);
                p.Add("@radius", 20);

                var feeds = await _mySql.QueryAsync<Feed>("getDistance", p, commandType: CommandType.StoredProcedure);

                return feeds;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}


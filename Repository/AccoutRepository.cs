using FindMyPet.Models;
using Dapper;
using MySqlConnector;
using FindMyPet.Repository.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace FindMyPet.Repository
{
	public class AccoutRepository : IAccoutRepository
    {
        private static RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();
        private readonly IDbConnection _mySql;

        public AccoutRepository(IConfiguration config)
        {
            _mySql = new MySqlConnection(config.GetConnectionString("mySqlGeneral"));
        }

        public async Task<UserAuthenticate> ViewAuthenticate(string user, string pass)
        {
            try
            {
                var md5 = MD5.Create();
                byte[] bytes = System.Text.Encoding.ASCII.GetBytes(pass);
                byte[] hash = md5.ComputeHash(bytes);

                StringBuilder sb = new StringBuilder();
                for(int i = 0;  i < hash.Length; i++)
                {
                    sb.Append(hash[i].ToString("x2"));
                }

                string passOn = sb.ToString();

                return await _mySql.QueryFirstOrDefaultAsync<UserAuthenticate>(@"
                        SELECT Id, User, Role FROM `FindMyPet`.`User.Api`
                        WHERE Status = 1 AND `User` = @User AND `Password` = @Pass
                      ",
                    new
                    {
                        User = user,
                        Pass = passOn
                    });
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}


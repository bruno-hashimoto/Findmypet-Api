using System;
using FindMyPet.Models;

namespace FindMyPet.Repository.Interfaces
{
    public interface IPostRepository
    {
        Task<bool> savePost(string image, string description, double latitude, double longitude);

        Task<IEnumerable<Feed>> selectPosts(double? latitude, double? longitude);
    }
}


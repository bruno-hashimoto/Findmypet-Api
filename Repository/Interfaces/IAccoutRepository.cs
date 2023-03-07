using System;
using FindMyPet.Models;

namespace FindMyPet.Repository.Interfaces
{
	public interface IAccoutRepository
    {
        Task<UserAuthenticate> ViewAuthenticate(string user, string pass);
    }
}


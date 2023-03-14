using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using FindMyPet.Models;
using FindMyPet.Repository;
using FindMyPet.Repository.Interfaces;
using FindMyPet.Services;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace FindMyPet.Business
{
	public class ApplicationBusiness : Controller
	{
		private readonly IPostRepository _post;

		public ApplicationBusiness(IPostRepository post)
		{
            _post = post;
		}

		public async Task<bool> newPost(Post newPost)
		{
            return await _post.savePost(newPost.ImageBase, newPost.Description, newPost.Latitude, newPost.Longitude);
        }

		public async Task<IEnumerable<Feed>> requestFeed(double latitude, double longitude)
		{
			return await _post.selectPosts(latitude, longitude);
        }
    }
}


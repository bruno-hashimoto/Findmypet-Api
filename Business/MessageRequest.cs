using System;
using System.Net;
using FindMyPet.Models;
using Microsoft.AspNetCore.Mvc;

namespace FindMyPet.Business
{
	public class MessageRequest : Controller
	{
		public async Task<ReturnSanitize> GetStatus(string text, int code)
		{
            ReturnSanitize message = new ReturnSanitize
            {
                Status = code,
                Message = text
            };

            return message;
        }
	}
}


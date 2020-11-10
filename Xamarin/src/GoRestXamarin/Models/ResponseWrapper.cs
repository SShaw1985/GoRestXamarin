﻿using System;
using System.Net;

namespace GoRestXamarin.Models
{
	public class ResponseWrapper
	{
		public HttpStatusCode StatusCode { get; set; }

		public string Content { get; set; }

		public string ReasonPhrase { get; internal set; }
	}
}

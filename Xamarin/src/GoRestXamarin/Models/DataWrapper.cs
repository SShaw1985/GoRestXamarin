﻿using System;
using System.Collections.Generic;

namespace GoRestXamarin.Models
{
    public class Pagination
    {
        public int total { get; set; }
        public int pages { get; set; }
        public int page { get; set; }
        public int limit { get; set; }
    }

    public class Meta
    {
        public Pagination pagination { get; set; }
    }

    public class Datum
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string gender { get; set; }
        public string status { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }

    public class Root
    {
        public int code { get; set; }
        public Meta meta { get; set; }
        public List<Datum> data { get; set; }
    }

    public class RootSingle
    {
        public int code { get; set; }
        public Meta meta { get; set; }
        public Datum data { get; set; }
    }


    public class RootError
    {
        public int code { get; set; }
        public Meta meta { get; set; }
        public List<Error> data { get; set; }
    }

    public class Error
    {
        public string field { get; set; }
        public string message { get; set; }
    }
}

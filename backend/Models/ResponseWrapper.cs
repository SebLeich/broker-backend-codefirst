﻿using System.Net;

namespace backend.Models
{
    public class ResponseWrapper
    {
        public HttpStatusCode state { get; set; }
        public string error { get; set; }

        public ResponseWrapper()
        {

        }
    }
    public class ResponseWrapper<T>
    {
        public HttpStatusCode state { get; set; }
        public T content { get; set; }
        public string error { get; set; }

        public ResponseWrapper()
        {
            
        }
    }
}
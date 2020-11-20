using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Newtonsoft.Json;

namespace NetworkProgrammingExam
{
    public class Post
    {
        [JsonProperty("userId")]
        public int UserId { get; set; }
        [JsonProperty("id")]
        public int PostId { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("body")]
        public string Body { get; set; }
    }
}

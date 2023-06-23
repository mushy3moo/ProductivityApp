using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityApp.Models
{
    public interface IModel
    {
        [JsonProperty("id")]
        public string Id { get; }
    }
}

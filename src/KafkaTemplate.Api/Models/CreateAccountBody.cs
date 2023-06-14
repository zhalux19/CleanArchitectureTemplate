using Microsoft.AspNetCore.Mvc;

namespace KafkaTemplate.Api.Models
{
    public record CreateAccountBody
    {
        [FromBody]
        public string Name { get; set; }
    }
}

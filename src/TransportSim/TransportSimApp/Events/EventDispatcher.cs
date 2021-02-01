using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace TransportSimApp.Events
{
    public class EventDispatcher
    {
        private readonly Action<string> _log;
        private readonly JsonSerializerSettings _serializerSettings = new JsonSerializerSettings()
        {
            ContractResolver = new DefaultContractResolver() { NamingStrategy = new SnakeCaseNamingStrategy() }, 
            Formatting = Formatting.None,
            NullValueHandling = NullValueHandling.Ignore,
            Converters = new List<JsonConverter>() { new StringEnumConverter() }
        };

        public EventDispatcher(Action<string> log)
        {
            _log = log;
        }

        public void Dispatch(TransportEvent @event)
        {
            var text = JsonConvert.SerializeObject(@event, _serializerSettings);
            _log(text);
        }
    }
}

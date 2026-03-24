using SpeechRecognition.CrossCutting.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace SpeechRecognition.CrossCutting.Framework
{
    public abstract class Entity<TId> : IInternalEventHandler
    {
        [JsonIgnore]
        private readonly Action<object> _applier;
        [JsonIgnore]
        public TId Id { get; protected set; }
        
        protected Entity(Action<object> applier) => _applier = applier;
        
        protected Entity() {}
        
        protected abstract void When(object @event);

        protected void Apply(object @event)
        {
            When(@event);
            _applier(@event);
        }
        public abstract void SetId(string id);
        void IInternalEventHandler.Handle(object @event) => When(@event);
    }
}

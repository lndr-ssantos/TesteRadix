﻿namespace Events.API.Infra.Options
{
    public class RabbitMqOptions
    {
        public string QueueName { get; set; }
        public string HostName { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}

using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace manager_retals.Infrastructure.RabbitMQ
{
    public class RabbitMqConnectionFactory
    {
        private readonly ConnectionFactory _factory;

        public RabbitMqConnectionFactory(string hostName, string userName, string password)
        {
            _factory = new ConnectionFactory
            {
                HostName = hostName,
                UserName = userName,
                Password = password
            };
        }

        public async Task<IConnection> CreateConnectionAsync() => await _factory.CreateConnectionAsync();
    }
}

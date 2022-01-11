﻿using OpenProtocolInterpreter.Alarm;
using OpenProtocolInterpreter.Communication;
using OpenProtocolInterpreter.Emulator.Controller.Events;
using OpenProtocolInterpreter.Job;
using OpenProtocolInterpreter.KeepAlive;
using OpenProtocolInterpreter.Tightening;
using OpenProtocolInterpreter.Vin;
using SimpleTcp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenProtocolInterpreter.Emulator.Controller.Drivers
{
    public class AtlasCopcoControllerDriver
    {
        private readonly SimpleTcpServer _server;
        private readonly MidInterpreter _midInterpreter;
        private readonly IList<string> _connectedClients;
        private readonly IDictionary<int, Func<Mid, Mid>> _autoReplies;
        private readonly Dictionary<int, Action<string, Mid>> _handlers;

        public event EventHandler<string> ClientConnected;
        public event EventHandler<string> ClientDisconnected;
        public event EventHandler<string> LogHandler;
        public event EventHandler<MidMessageEvent> MessageReceived;

        public IEnumerable<string> ConnectedClients { get => _connectedClients; }

        public AtlasCopcoControllerDriver()
        {
            _connectedClients = new List<string>();
            _server = new SimpleTcpServer("127.0.0.1", 4545);
            _server.Settings.IdleClientTimeoutMs = 10000;
            _midInterpreter = new MidInterpreter().UseAllMessages(InterpreterMode.Controller);
            _autoReplies = new Dictionary<int, Func<Mid, Mid>>()
            {
                { Mid0001.MID, mid => OnCommunicationStart((Mid0001)mid) },
                { Mid0034.MID,mid => PositiveAcknowledge(mid) },
                { Mid0038.MID,mid => PositiveAcknowledge(mid) },
                { Mid0050.MID, mid => PositiveAcknowledge(mid) },
                { Mid0051.MID, mid => PositiveAcknowledge(mid) },
                { Mid0060.MID, mid => PositiveAcknowledge(mid) },
                { Mid0070.MID, mid => PositiveAcknowledge(mid) },
                { Mid9999.MID, mid => new Mid9999() }
            };
        }

        public Task StartAsync()
        {
            _server.Events.ClientConnected += OnClientConnected;
            _server.Events.ClientDisconnected += OnClientDisconnected;
            _server.Events.DataReceived += OnDataReceived;
            return _server.StartAsync();
        }

        public async Task SendAsync(string ipPort, Mid mid)
        {
            var data = mid.PackBytes();
            await _server.SendAsync(ipPort, data);
        }

        protected virtual Mid OnCommunicationStart(Mid0001 mid)
        {
            return new Mid0002(1, 1, "New Controller");
        }

        protected virtual Mid PositiveAcknowledge(Mid mid) => new Mid0005(mid.HeaderData.Mid);
        protected virtual Mid NegativeAcknowledge(Mid mid) => new Mid0004(mid.HeaderData.Mid, Error.COMMAND_FAILED);

        private void OnClientConnected(object sender, ClientConnectedEventArgs e)
        {
            _connectedClients.Add(e.IpPort);
            LogHandler?.Invoke(this, $"Client connected from IP and Port {e.IpPort}");
            ClientConnected?.Invoke(this, e.IpPort);
        }

        private void OnClientDisconnected(object sender, ClientDisconnectedEventArgs e)
        {
            _connectedClients.Remove(e.IpPort);
            LogHandler?.Invoke(this, $"Client ({e.IpPort}) disconnected. Reason: {e.Reason}");
            ClientDisconnected?.Invoke(this, e.IpPort);
        }

        private void OnDataReceived(object sender, DataReceivedEventArgs e)
        {
            var mid = _midInterpreter.Parse(e.Data);
            if(_autoReplies.TryGetValue(mid.HeaderData.Mid, out var responseCreator))
            {
                var responseMid = responseCreator(mid);
                var bytes = responseMid.PackBytes();
                _server.Send(e.IpPort, bytes);
            }
            MessageReceived?.Invoke(this, new MidMessageEvent
            {
                ClientIpPort = e.IpPort,
                Mid = mid
            });
        }
    }
}

﻿using System;
using Networking;

namespace SpaceWars
{

    /// <summary>
    /// The main controller for the SpaceWars scoreboard server.
    /// Uses the networking library to maintain a connection with multiple clients
    /// and output an html webpage that shows scores based on the accessed endpoint.
    /// </summary>
    /// <authors>Jiahui Chen, Mitch Talmadge</authors>
    public class ScoreboardServerController
    {
        /// <summary>
        /// The TcpState that the server is using to accept client connections.
        /// </summary>
        private TcpState _tcpState;

        /// <summary>
        /// Called when a client connects to the server.
        /// Useful for logging purposes.
        /// </summary>
        public event Action ClientConnected;

        /// <summary>
        /// Called when a client fails to connect to the server.
        /// Useful for logging purposes.
        /// </summary>
        public event Action ClientConnectFailed;

        /// <summary>
        /// Called when a client disconnects from the server.
        /// Useful for logging purposes.
        /// </summary>
        public event Action ClientDisconnected;

        /// <summary>
        /// Called when this server stops listening for clients.
        /// Useful for logging purposes.
        /// </summary>
        public event Action ServerDisconnected;

        /// <summary>
        /// Creates a new scoreboard server controller that will listen for clients.
        /// </summary>
        public ScoreboardServerController()
        {
            AcceptConnectionsAsync();
        }

        /// <summary>
        /// Starts the process of accepting client connections in a separate thread.
        /// </summary>
        private void AcceptConnectionsAsync()
        {
            _tcpState = ServerNetworking.AwaitClientConnections(80, ClientConnectionEstablished, ClientConnectionFailed);
        }

        /// <summary>
        /// Called when a client establishes a connection with the server.
        /// </summary>
        /// <param name="state">The client's socket state.</param>
        private void ClientConnectionEstablished(SocketState state)
        {

            // Notify listeners of a newly connected client.
            ClientConnected?.Invoke();
        }

        /// <summary>
        /// Called when the server cannot connect to a client.
        /// </summary>
        /// <param name="reason">The reason that the connection failed.</param>
        private void ClientConnectionFailed(string reason)
        {
            ClientConnectFailed?.Invoke();
        }

        /// <summary>
        /// Disconnects from the TcpState that accepts client connections.
        /// This server instance may not be used after calling this method.
        /// </summary>
        public void Disconnect()
        {
            _tcpState?.StopAcceptingClientConnections();
            ServerDisconnected?.Invoke();
        }

    }
}
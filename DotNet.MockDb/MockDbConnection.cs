using System;
using System.Collections.Generic;
using System.Data;

namespace DotNet.MockDb
{
    public class MockDbConnection: IDbConnection
    {
        private bool _isDisposed;
        private bool _isTransactionOpen;
        private List<IDbCommand> _dbCommands;

        public MockDbConnection(string databaseName)
        {
            _isDisposed = false;
            _isTransactionOpen = false;
            Database = databaseName;
            State = ConnectionState.Closed;
            ConnectionTimeout = 30;
            _dbCommands = new List<IDbCommand>();
        }
        
        public void Dispose()
        {
            _isDisposed = true;
        }

        public IDbTransaction BeginTransaction()
        {
            return BeginTransaction(IsolationLevel.ReadCommitted);
        }

        public IDbTransaction BeginTransaction(IsolationLevel il)
        {
            if (_isTransactionOpen)
                throw new InvalidOperationException("A transaction was already open for this connection.");
            _isTransactionOpen = true;
            
            return new MockDbTransaction(this, il);
        }

        public void Close()
        {
            State = ConnectionState.Closed;
        }

        public void ChangeDatabase(string databaseName)
        {
            Database = databaseName;
        }

        public IDbCommand CreateCommand()
        {
            var newCommand = new MockDbCommand {Connection = this};
            _dbCommands.Add(newCommand);
            return newCommand;
        }

        public void Open()
        {
            if (State != ConnectionState.Closed && State != ConnectionState.Broken)
                throw new InvalidOperationException("The connection was already open.");
            State = ConnectionState.Open;
        }

        public string ConnectionString { get; set; }
        public int ConnectionTimeout { get; }
        public string Database { get; private set; }
        public ConnectionState State { get; private set; }
    }
}
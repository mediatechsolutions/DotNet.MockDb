﻿using System.Data;

namespace DotNet.MockDb
{
    public class MockDbCommand: IDbCommand
    {
        private bool _isDisposed;
        private bool _isPrepared;
        private bool _isCancelled;
        private int _numberOfNonQueryExecutions;
        
        public MockDbCommand()
        {
            _isDisposed = false;
            _isPrepared = false;
            _isCancelled = false;
            Parameters = new MockDbParameterCollection();
            _numberOfNonQueryExecutions = 0;
        }
        
        public void Dispose()
        {
            _isDisposed = true;
        }

        public void Prepare()
        {
            _isPrepared = true;
        }

        public void Cancel()
        {
            _isCancelled = true;
        }

        public IDbDataParameter CreateParameter()
        {
            var newParameter = new MockDbParameter();
            Parameters.Add(newParameter);
            return newParameter;
        }

        public int ExecuteNonQuery()
        {
            _numberOfNonQueryExecutions++;
            return 0; //TODO: Add expectations in order to return a specific value.
        }

        public IDataReader ExecuteReader()
        {
            throw new System.NotImplementedException();
        }

        public IDataReader ExecuteReader(CommandBehavior behavior)
        {
            throw new System.NotImplementedException();
        }

        public object ExecuteScalar()
        {
            throw new System.NotImplementedException();
        }

        public IDbConnection Connection { get; set; }
        public IDbTransaction Transaction { get; set; }
        public string CommandText { get; set; }
        public int CommandTimeout { get; set; }
        public CommandType CommandType { get; set; }
        public IDataParameterCollection Parameters { get; }
        public UpdateRowSource UpdatedRowSource { get; set; }
    }
}
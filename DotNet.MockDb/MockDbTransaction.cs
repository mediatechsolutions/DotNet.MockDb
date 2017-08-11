using System;
using System.Data;

namespace DotNet.MockDb
{
    public class MockDbTransaction: IDbTransaction
    {
        private bool _idDisposed;
        private bool _isCommited;
        private bool _isRolledBack;

        public MockDbTransaction(IDbConnection dbConnection, IsolationLevel isolationLevel)
        {
            _idDisposed = false;
            _isCommited = false;
            _isRolledBack = false;

            Connection = dbConnection;
            IsolationLevel = isolationLevel;
        }
        
        public void Dispose()
        {
            _idDisposed = true;
        }

        public void Commit()
        {
            if (_isCommited)
                throw new InvalidOperationException("The transaction is already commited.");

            _isCommited = true;
        }

        public void Rollback()
        {
            if (_isCommited)
                throw new InvalidOperationException("The transaction is already commited.");

            if (_isRolledBack)
                throw new InvalidOperationException("The transaction is already rolled back.");
            
            _isRolledBack = true;
        }

        public IDbConnection Connection { get; }
        public IsolationLevel IsolationLevel { get; }
    }
}
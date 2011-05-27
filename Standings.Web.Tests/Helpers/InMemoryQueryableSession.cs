using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using NHibernate;
using NHibernate.Engine;
using NHibernate.Stat;
using NHibernate.Type;
using Standings.Infrastructure.Persistence;

namespace Standings.Web.Tests.Helpers
{
    public class InMemoryQueryableSession<T> : IQueryableSession<T>
    {
        private readonly List<T> _inMemorySession = new List<T>();
        private readonly EnumerableQuery<T> _enumerableQuery;

        public InMemoryQueryableSession()
        {
            _enumerableQuery = new EnumerableQuery<T>(_inMemorySession);
        }

        #region Implementation of IEnumerable

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region Implementation of IQueryable

        public Expression Expression
        {
            get { return ((IQueryable)_enumerableQuery).Expression; }
        }

        /// <summary>
        /// Gets the type of the element(s) that are returned when the expression tree associated with this instance of <see cref="T:System.Linq.IQueryable"/> is executed.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Type"/> that represents the type of the element(s) that are returned when the expression tree associated with this object is executed.
        /// </returns>
        public Type ElementType
        {
            get { throw new NotImplementedException(); }
        }

        public IQueryProvider Provider
        {
            get { return _enumerableQuery; }
        }

        #endregion

        #region Implementation of IDisposable

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Implementation of ISession

        /// <summary>
        /// Force the <c>ISession</c> to flush.
        /// </summary>
        /// <remarks>
        /// Must be called at the end of a unit of work, before commiting the transaction and closing
        ///             the session (<c>Transaction.Commit()</c> calls this method). <i>Flushing</i> if the process
        ///             of synchronising the underlying persistent store with persistable state held in memory.
        /// </remarks>
        public void Flush()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Disconnect the <c>ISession</c> from the current ADO.NET connection.
        /// </summary>
        /// <remarks>
        /// If the connection was obtained by Hibernate, close it or return it to the connection
        ///             pool. Otherwise return it to the application. This is used by applications which require
        ///             long transactions.
        /// </remarks>
        /// <returns>
        /// The connection provided by the application or <see langword="null"/>
        /// </returns>
        public IDbConnection Disconnect()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Obtain a new ADO.NET connection.
        /// </summary>
        /// <remarks>
        /// This is used by applications which require long transactions
        /// </remarks>
        public void Reconnect()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Reconnect to the given ADO.NET connection.
        /// </summary>
        /// <remarks>
        /// This is used by applications which require long transactions
        /// </remarks>
        /// <param name="connection">An ADO.NET connection</param>
        public void Reconnect(IDbConnection connection)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// End the <c>ISession</c> by disconnecting from the ADO.NET connection and cleaning up.
        /// </summary>
        /// <remarks>
        /// It is not strictly necessary to <c>Close()</c> the <c>ISession</c> but you must
        ///             at least <c>Disconnect()</c> it.
        /// </remarks>
        /// <returns>
        /// The connection provided by the application or <see langword="null"/>
        /// </returns>
        public IDbConnection Close()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Cancel execution of the current query.
        /// </summary>
        /// <remarks>
        /// May be called from one thread to stop execution of a query in another thread.
        ///             Use with care!
        /// </remarks>
        public void CancelQuery()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Does this <c>ISession</c> contain any changes which must be
        ///             synchronized with the database? Would any SQL be executed if
        ///             we flushed this session?
        /// </summary>
        public bool IsDirty()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Is the specified entity (or proxy) read-only?
        /// </summary>
        /// <remarks>
        /// Facade for <see cref="M:NHibernate.Engine.IPersistenceContext.IsReadOnly(System.Object)"/>.
        /// </remarks>
        /// <param name="entityOrProxy">An entity (or <see cref="T:NHibernate.Proxy.INHibernateProxy"/>)</param>
        /// <returns>
        /// <c>true</c> if the entity (or proxy) is read-only, otherwise <c>false</c>.
        /// </returns>
        /// <seealso cref="P:NHibernate.ISession.DefaultReadOnly"/><seealso cref="M:NHibernate.ISession.SetReadOnly(System.Object,System.Boolean)"/>
        public bool IsReadOnly(object entityOrProxy)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Change the read-only status of an entity (or proxy).
        /// </summary>
        /// <remarks>
        /// <para>
        /// Read-only entities can be modified, but changes are not persisted. They are not dirty-checked 
        ///             and snapshots of persistent state are not maintained. 
        /// </para>
        /// <para>
        /// Immutable entities cannot be made read-only.
        /// </para>
        /// <para>
        /// To set the <em>default</em> read-only setting for entities and proxies that are loaded 
        ///             into the session, see <see cref="P:NHibernate.ISession.DefaultReadOnly"/>.
        /// </para>
        /// <para>
        /// This method a facade for <see cref="M:NHibernate.Engine.IPersistenceContext.SetReadOnly(System.Object,System.Boolean)"/>.
        /// </para>
        /// </remarks>
        /// <param name="entityOrProxy">An entity (or <see cref="T:NHibernate.Proxy.INHibernateProxy"/>).</param><param name="readOnly">If <c>true</c>, the entity or proxy is made read-only; if <c>false</c>, it is made modifiable.</param><seealso cref="P:NHibernate.ISession.DefaultReadOnly"/><seealso cref="M:NHibernate.ISession.IsReadOnly(System.Object)"/>
        public void SetReadOnly(object entityOrProxy, bool readOnly)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Return the identifier of an entity instance cached by the <c>ISession</c>
        /// </summary>
        /// <remarks>
        /// Throws an exception if the instance is transient or associated with a different
        ///             <c>ISession</c>
        /// </remarks>
        /// <param name="obj">a persistent instance</param>
        /// <returns>
        /// the identifier
        /// </returns>
        public object GetIdentifier(object obj)
        {
            throw new NotImplementedException();
        }

        public bool Contains(object obj)
        {
            return _inMemorySession.Contains((T)obj);
        }

        /// <summary>
        /// Remove this instance from the session cache.
        /// </summary>
        /// <remarks>
        /// Changes to the instance will not be synchronized with the database.
        ///             This operation cascades to associated instances if the association is mapped
        ///             with <c>cascade="all"</c> or <c>cascade="all-delete-orphan"</c>.
        /// </remarks>
        /// <param name="obj">a persistent instance</param>
        public void Evict(object obj)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Return the persistent instance of the given entity class with the given identifier,
        ///             obtaining the specified lock mode.
        /// </summary>
        /// <param name="theType">A persistent class</param>
        /// <param name="id">A valid identifier of an existing persistent instance of the class</param>
        /// <param name="lockMode">The lock level</param>
        /// <returns>
        /// the persistent instance
        /// </returns>
        public object Load(Type theType, object id, LockMode lockMode)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Return the persistent instance of the given entity class with the given identifier,
        ///             obtaining the specified lock mode, assuming the instance exists.
        /// </summary>
        /// <param name="entityName">The entity-name of a persistent class</param>
        /// <param name="id">a valid identifier of an existing persistent instance of the class </param>
        /// <param name="lockMode">the lock level </param>
        /// <returns>
        /// the persistent instance or proxy 
        /// </returns>
        public object Load(string entityName, object id, LockMode lockMode)
        {
            throw new NotImplementedException();
        }

        public object Load(Type theType, object id)
        {
            return _inMemorySession[(int)id];
        }

        /// <summary>
        /// Return the persistent instance of the given entity class with the given identifier,
        ///             obtaining the specified lock mode.
        /// </summary>
        /// <typeparam name="T">A persistent class</typeparam>
        /// <param name="id">A valid identifier of an existing persistent instance of the class</param>
        /// <param name="lockMode">The lock level</param>
        /// <returns>
        /// the persistent instance
        /// </returns>
        public T1 Load<T1>(object id, LockMode lockMode)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Return the persistent instance of the given entity class with the given identifier,
        ///             assuming that the instance exists.
        /// </summary>
        /// <remarks>
        /// You should not use this method to determine if an instance exists (use a query or
        ///             <see cref="M:NHibernate.ISession.Get``1(System.Object)"/> instead). Use this only to retrieve an instance that you
        ///             assume exists, where non-existence would be an actual error.
        /// </remarks>
        /// <typeparam name="T">A persistent class</typeparam>
        /// <param name="id">A valid identifier of an existing persistent instance of the class</param>
        /// <returns>
        /// The persistent instance or proxy
        /// </returns>
        public T1 Load<T1>(object id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Return the persistent instance of the given <paramref name="entityName"/> with the given identifier,
        ///             assuming that the instance exists.
        /// </summary>
        /// <param name="entityName">The entity-name of a persistent class</param>
        /// <param name="id">a valid identifier of an existing persistent instance of the class </param>
        /// <returns>
        /// The persistent instance or proxy 
        /// </returns>
        /// <remarks>
        /// You should not use this method to determine if an instance exists (use <see cref="M:NHibernate.ISession.Get(System.String,System.Object)"/>
        ///             instead). Use this only to retrieve an instance that you assume exists, where non-existence
        ///             would be an actual error.
        /// </remarks>
        public object Load(string entityName, object id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Read the persistent state associated with the given identifier into the given transient
        ///             instance.
        /// </summary>
        /// <param name="obj">An "empty" instance of the persistent class</param>
        /// <param name="id">A valid identifier of an existing persistent instance of the class</param>
        public void Load(object obj, object id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Persist all reachable transient objects, reusing the current identifier
        ///             values. Note that this will not trigger the Interceptor of the Session.
        /// </summary>
        /// <param name="obj">a detached instance of a persistent class</param><param name="replicationMode"/>
        public void Replicate(object obj, ReplicationMode replicationMode)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Persist the state of the given detached instance, reusing the current
        ///             identifier value.  This operation cascades to associated instances if
        ///             the association is mapped with <tt>cascade="replicate"</tt>.
        /// </summary>
        /// <param name="entityName"/><param name="obj">a detached instance of a persistent class </param><param name="replicationMode"/>
        public void Replicate(string entityName, object obj, ReplicationMode replicationMode)
        {
            throw new NotImplementedException();
        }

        public object Save(object obj)
        {
            var item = (T)obj;
            _inMemorySession.Add(item);
            return item;
        }

        /// <summary>
        /// Persist the given transient instance, using the given identifier.
        /// </summary>
        /// <param name="obj">A transient instance of a persistent class</param>
        /// <param name="id">An unused valid identifier</param>
        public void Save(object obj, object id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Persist the given transient instance, first assigning a generated identifier. (Or
        ///             using the current value of the identifier property if the <tt>assigned</tt>
        ///             generator is used.)
        /// </summary>
        /// <param name="entityName">The Entity name.</param>
        /// <param name="obj">a transient instance of a persistent class </param>
        /// <returns>
        /// the generated identifier 
        /// </returns>
        /// <remarks>
        /// This operation cascades to associated instances if the
        ///             association is mapped with <tt>cascade="save-update"</tt>.
        /// </remarks>
        public object Save(string entityName, object obj)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Either <c>Save()</c> or <c>Update()</c> the given instance, depending upon the value of
        ///             its identifier property.
        /// </summary>
        /// <remarks>
        /// By default the instance is always saved. This behaviour may be adjusted by specifying
        ///             an <c>unsaved-value</c> attribute of the identifier property mapping
        /// </remarks>
        /// <param name="obj">A transient instance containing new or updated state</param>
        public void SaveOrUpdate(object obj)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Either <see cref="M:NHibernate.ISession.Save(System.String,System.Object)"/> or <see cref="M:NHibernate.ISession.Update(System.String,System.Object)"/>
        ///             the given instance, depending upon resolution of the unsaved-value checks
        ///             (see the manual for discussion of unsaved-value checking).
        /// </summary>
        /// <param name="entityName">The name of the entity </param><param name="obj">a transient or detached instance containing new or updated state </param><seealso cref="M:NHibernate.ISession.Save(System.String,System.Object)"/><seealso cref="M:NHibernate.ISession.Update(System.String,System.Object)"/>
        /// <remarks>
        /// This operation cascades to associated instances if the association is mapped
        ///             with <tt>cascade="save-update"</tt>.
        /// </remarks>
        public void SaveOrUpdate(string entityName, object obj)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Update the persistent instance with the identifier of the given transient instance.
        /// </summary>
        /// <remarks>
        /// If there is a persistent instance with the same identifier, an exception is thrown. If
        ///             the given transient instance has a <see langword="null"/> identifier, an exception will be thrown.
        /// </remarks>
        /// <param name="obj">A transient instance containing updated state</param>
        public void Update(object obj)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Update the persistent state associated with the given identifier.
        /// </summary>
        /// <remarks>
        /// An exception is thrown if there is a persistent instance with the same identifier
        ///             in the current session.
        /// </remarks>
        /// <param name="obj">A transient instance containing updated state</param><param name="id">Identifier of persistent instance</param>
        public void Update(object obj, object id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Update the persistent instance with the identifier of the given detached
        ///             instance.
        /// </summary>
        /// <param name="entityName">The Entity name.</param><param name="obj">a detached instance containing updated state </param>
        /// <remarks>
        /// If there is a persistent instance with the same identifier,
        ///             an exception is thrown. This operation cascades to associated instances
        ///             if the association is mapped with <tt>cascade="save-update"</tt>.
        /// </remarks>
        public void Update(string entityName, object obj)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Copy the state of the given object onto the persistent object with the same
        ///             identifier. If there is no persistent instance currently associated with
        ///             the session, it will be loaded. Return the persistent instance. If the
        ///             given instance is unsaved, save a copy of and return it as a newly persistent
        ///             instance. The given instance does not become associated with the session.
        ///             This operation cascades to associated instances if the association is mapped
        ///             with <tt>cascade="merge"</tt>.<br/>
        ///             The semantics of this method are defined by JSR-220.
        /// </summary>
        /// <param name="obj">a detached instance with state to be copied </param>
        /// <returns>
        /// an updated persistent instance 
        /// </returns>
        public object Merge(object obj)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Copy the state of the given object onto the persistent object with the same
        ///             identifier. If there is no persistent instance currently associated with
        ///             the session, it will be loaded. Return the persistent instance. If the
        ///             given instance is unsaved, save a copy of and return it as a newly persistent
        ///             instance. The given instance does not become associated with the session.
        ///             This operation cascades to associated instances if the association is mapped
        ///             with <tt>cascade="merge"</tt>.<br/>
        ///             The semantics of this method are defined by JSR-220.
        ///             <param name="entityName">Name of the entity.</param><param name="obj">a detached instance with state to be copied </param>
        /// <returns>
        /// an updated persistent instance 
        /// </returns>
        /// </summary>
        /// <returns/>
        public object Merge(string entityName, object obj)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Make a transient instance persistent. This operation cascades to associated
        ///             instances if the association is mapped with <tt>cascade="persist"</tt>.<br/>
        ///             The semantics of this method are defined by JSR-220.
        /// </summary>
        /// <param name="obj">a transient instance to be made persistent </param>
        public void Persist(object obj)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Make a transient instance persistent. This operation cascades to associated
        ///             instances if the association is mapped with <tt>cascade="persist"</tt>.<br/>
        ///             The semantics of this method are defined by JSR-220.
        /// </summary>
        /// <param name="entityName">Name of the entity.</param><param name="obj">a transient instance to be made persistent</param>
        public void Persist(string entityName, object obj)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Copy the state of the given object onto the persistent object with the same
        ///             identifier. If there is no persistent instance currently associated with
        ///             the session, it will be loaded. Return the persistent instance. If the
        ///             given instance is unsaved or does not exist in the database, save it and
        ///             return it as a newly persistent instance. Otherwise, the given instance
        ///             does not become associated with the session.
        /// </summary>
        /// <param name="obj">a transient instance with state to be copied</param>
        /// <returns>
        /// an updated persistent instance
        /// </returns>
        public object SaveOrUpdateCopy(object obj)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Copy the state of the given object onto the persistent object with the
        ///             given identifier. If there is no persistent instance currently associated
        ///             with the session, it will be loaded. Return the persistent instance. If
        ///             there is no database row with the given identifier, save the given instance
        ///             and return it as a newly persistent instance. Otherwise, the given instance
        ///             does not become associated with the session.
        /// </summary>
        /// <param name="obj">a persistent or transient instance with state to be copied</param><param name="id">the identifier of the instance to copy to</param>
        /// <returns>
        /// an updated persistent instance
        /// </returns>
        public object SaveOrUpdateCopy(object obj, object id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Remove a persistent instance from the datastore.
        /// </summary>
        /// <remarks>
        /// The argument may be an instance associated with the receiving <c>ISession</c> or a
        ///             transient instance with an identifier associated with existing persistent state.
        /// </remarks>
        /// <param name="obj">The instance to be removed</param>
        public void Delete(object obj)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Remove a persistent instance from the datastore. The <b>object</b> argument may be
        ///             an instance associated with the receiving <see cref="T:NHibernate.ISession"/> or a transient
        ///             instance with an identifier associated with existing persistent state.
        ///             This operation cascades to associated instances if the association is mapped
        ///             with <tt>cascade="delete"</tt>.
        /// </summary>
        /// <param name="entityName">The entity name for the instance to be removed. </param><param name="obj">the instance to be removed </param>
        public void Delete(string entityName, object obj)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Delete all objects returned by the query.
        /// </summary>
        /// <param name="query">The query string</param>
        /// <returns>
        /// Returns the number of objects deleted.
        /// </returns>
        public int Delete(string query)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Delete all objects returned by the query.
        /// </summary>
        /// <param name="query">The query string</param><param name="value">A value to be written to a "?" placeholer in the query</param><param name="type">The hibernate type of value.</param>
        /// <returns>
        /// The number of instances deleted
        /// </returns>
        public int Delete(string query, object value, IType type)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Delete all objects returned by the query.
        /// </summary>
        /// <param name="query">The query string</param><param name="values">A list of values to be written to "?" placeholders in the query</param><param name="types">A list of Hibernate types of the values</param>
        /// <returns>
        /// The number of instances deleted
        /// </returns>
        public int Delete(string query, object[] values, IType[] types)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Obtain the specified lock level upon the given object.
        /// </summary>
        /// <param name="obj">A persistent instance</param><param name="lockMode">The lock level</param>
        public void Lock(object obj, LockMode lockMode)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Obtain the specified lock level upon the given object.
        /// </summary>
        /// <param name="entityName">The Entity name.</param><param name="obj">a persistent or transient instance </param><param name="lockMode">the lock level </param>
        /// <remarks>
        /// This may be used to perform a version check (<see cref="F:NHibernate.LockMode.Read"/>), to upgrade to a pessimistic
        ///             lock (<see cref="F:NHibernate.LockMode.Upgrade"/>), or to simply reassociate a transient instance
        ///             with a session (<see cref="F:NHibernate.LockMode.None"/>). This operation cascades to associated
        ///             instances if the association is mapped with <tt>cascade="lock"</tt>.
        /// </remarks>
        public void Lock(string entityName, object obj, LockMode lockMode)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Re-read the state of the given instance from the underlying database.
        /// </summary>
        /// <remarks>
        /// <para>
        /// It is inadvisable to use this to implement long-running sessions that span many
        ///             business tasks. This method is, however, useful in certain special circumstances.
        /// </para>
        /// <para>
        /// For example,
        /// <list>
        /// <item>
        /// Where a database trigger alters the object state upon insert or update
        /// </item>
        /// <item>
        /// After executing direct SQL (eg. a mass update) in the same session
        /// </item>
        /// <item>
        /// After inserting a <c>Blob</c> or <c>Clob</c>
        /// </item>
        /// </list>
        /// </para>
        /// </remarks>
        /// <param name="obj">A persistent instance</param>
        public void Refresh(object obj)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Re-read the state of the given instance from the underlying database, with
        ///             the given <c>LockMode</c>.
        /// </summary>
        /// <remarks>
        /// It is inadvisable to use this to implement long-running sessions that span many
        ///             business tasks. This method is, however, useful in certain special circumstances.
        /// </remarks>
        /// <param name="obj">a persistent or transient instance</param><param name="lockMode">the lock mode to use</param>
        public void Refresh(object obj, LockMode lockMode)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Determine the current lock mode of the given object
        /// </summary>
        /// <param name="obj">A persistent instance</param>
        /// <returns>
        /// The current lock mode
        /// </returns>
        public LockMode GetCurrentLockMode(object obj)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Begin a unit of work and return the associated <c>ITransaction</c> object.
        /// </summary>
        /// <remarks>
        /// If a new underlying transaction is required, begin the transaction. Otherwise
        ///             continue the new work in the context of the existing underlying transaction.
        ///             The class of the returned <see cref="T:NHibernate.ITransaction"/> object is determined by
        ///             the property <c>transaction_factory</c>
        /// </remarks>
        /// <returns>
        /// A transaction instance
        /// </returns>
        public ITransaction BeginTransaction()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Begin a transaction with the specified <c>isolationLevel</c>
        /// </summary>
        /// <param name="isolationLevel">Isolation level for the new transaction</param>
        /// <returns>
        /// A transaction instance having the specified isolation level
        /// </returns>
        public ITransaction BeginTransaction(IsolationLevel isolationLevel)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates a new <c>Criteria</c> for the entity class.
        /// </summary>
        /// <typeparam name="T">The entity class</typeparam>
        /// <returns>
        /// An ICriteria object
        /// </returns>
        public ICriteria CreateCriteria<T1>() where T1 : class
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates a new <c>Criteria</c> for the entity class with a specific alias
        /// </summary>
        /// <typeparam name="T">The entity class</typeparam><param name="alias">The alias of the entity</param>
        /// <returns>
        /// An ICriteria object
        /// </returns>
        public ICriteria CreateCriteria<T1>(string alias) where T1 : class
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates a new <c>Criteria</c> for the entity class.
        /// </summary>
        /// <param name="persistentClass">The class to Query</param>
        /// <returns>
        /// An ICriteria object
        /// </returns>
        public ICriteria CreateCriteria(Type persistentClass)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates a new <c>Criteria</c> for the entity class with a specific alias
        /// </summary>
        /// <param name="persistentClass">The class to Query</param><param name="alias">The alias of the entity</param>
        /// <returns>
        /// An ICriteria object
        /// </returns>
        public ICriteria CreateCriteria(Type persistentClass, string alias)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Create a new <c>Criteria</c> instance, for the given entity name.
        /// </summary>
        /// <param name="entityName">The name of the entity to Query</param>
        /// <returns>
        /// An ICriteria object
        /// </returns>
        public ICriteria CreateCriteria(string entityName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Create a new <c>Criteria</c> instance, for the given entity name,
        ///             with the given alias.
        /// </summary>
        /// <param name="entityName">The name of the entity to Query</param><param name="alias">The alias of the entity</param>
        /// <returns>
        /// An ICriteria object
        /// </returns>
        public ICriteria CreateCriteria(string entityName, string alias)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates a new <c>IQueryOver&lt;T&gt;</c> for the entity class.
        /// </summary>
        /// <typeparam name="T">The entity class</typeparam>
        /// <returns>
        /// An ICriteria&lt;T&gt; object
        /// </returns>
        public IQueryOver<T1, T1> QueryOver<T1>() where T1 : class
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates a new <c>IQueryOver&lt;T&gt;</c> for the entity class.
        /// </summary>
        /// <typeparam name="T">The entity class</typeparam>
        /// <returns>
        /// An ICriteria&lt;T&gt; object
        /// </returns>
        public IQueryOver<T1, T1> QueryOver<T1>(Expression<Func<T1>> alias) where T1 : class
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Create a new instance of <c>Query</c> for the given query string
        /// </summary>
        /// <param name="queryString">A hibernate query string</param>
        /// <returns>
        /// The query
        /// </returns>
        public IQuery CreateQuery(string queryString)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Create a new instance of <c>Query</c> for the given collection and filter string
        /// </summary>
        /// <param name="collection">A persistent collection</param><param name="queryString">A hibernate query</param>
        /// <returns>
        /// A query
        /// </returns>
        public IQuery CreateFilter(object collection, string queryString)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Obtain an instance of <see cref="T:NHibernate.IQuery"/> for a named query string defined in the
        ///             mapping file.
        /// </summary>
        /// <param name="queryName">The name of a query defined externally.</param>
        /// <returns>
        /// An <see cref="T:NHibernate.IQuery"/> from a named query string.
        /// </returns>
        /// <remarks>
        /// The query can be either in <c>HQL</c> or <c>SQL</c> format.
        /// </remarks>
        public IQuery GetNamedQuery(string queryName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Create a new instance of <see cref="T:NHibernate.ISQLQuery"/> for the given SQL query string.
        /// </summary>
        /// <param name="queryString">a query expressed in SQL</param>
        /// <returns>
        /// An <see cref="T:NHibernate.ISQLQuery"/> from the SQL string
        /// </returns>
        public ISQLQuery CreateSQLQuery(string queryString)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Completely clear the session. Evict all loaded instances and cancel all pending
        ///             saves, updates and deletions. Do not close open enumerables or instances of
        ///             <c>ScrollableResults</c>.
        /// </summary>
        public void Clear()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Return the persistent instance of the given entity class with the given identifier, or null
        ///             if there is no such persistent instance. (If the instance, or a proxy for the instance, is
        ///             already associated with the session, return that instance or proxy.)
        /// </summary>
        /// <param name="clazz">a persistent class</param><param name="id">an identifier</param>
        /// <returns>
        /// a persistent instance or null
        /// </returns>
        public object Get(Type clazz, object id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Return the persistent instance of the given entity class with the given identifier, or null
        ///             if there is no such persistent instance. Obtain the specified lock mode if the instance
        ///             exists.
        /// </summary>
        /// <param name="clazz">a persistent class</param><param name="id">an identifier</param><param name="lockMode">the lock mode</param>
        /// <returns>
        /// a persistent instance or null
        /// </returns>
        public object Get(Type clazz, object id, LockMode lockMode)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Return the persistent instance of the given named entity with the given identifier,
        ///             or null if there is no such persistent instance. (If the instance, or a proxy for the
        ///             instance, is already associated with the session, return that instance or proxy.)
        /// </summary>
        /// <param name="entityName">the entity name </param><param name="id">an identifier </param>
        /// <returns>
        /// a persistent instance or null 
        /// </returns>
        public object Get(string entityName, object id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Strongly-typed version of <see cref="M:NHibernate.ISession.Get(System.Type,System.Object)"/>
        /// </summary>
        public T1 Get<T1>(object id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Strongly-typed version of <see cref="M:NHibernate.ISession.Get(System.Type,System.Object,NHibernate.LockMode)"/>
        /// </summary>
        public T1 Get<T1>(object id, LockMode lockMode)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Return the entity name for a persistent entity
        /// </summary>
        /// <param name="obj">a persistent entity</param>
        /// <returns>
        /// the entity name 
        /// </returns>
        public string GetEntityName(object obj)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Enable the named filter for this current session.
        /// </summary>
        /// <param name="filterName">The name of the filter to be enabled.</param>
        /// <returns>
        /// The Filter instance representing the enabled filter.
        /// </returns>
        public IFilter EnableFilter(string filterName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Retrieve a currently enabled filter by name.
        /// </summary>
        /// <param name="filterName">The name of the filter to be retrieved.</param>
        /// <returns>
        /// The Filter instance representing the enabled filter.
        /// </returns>
        public IFilter GetEnabledFilter(string filterName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Disable the named filter for the current session.
        /// </summary>
        /// <param name="filterName">The name of the filter to be disabled.</param>
        public void DisableFilter(string filterName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Create a multi query, a query that can send several
        ///             queries to the server, and return all their results in a single
        ///             call.
        /// </summary>
        /// <returns>
        /// An <see cref="T:NHibernate.IMultiQuery"/> that can return
        ///             a list of all the results of all the queries.
        ///             Note that each query result is itself usually a list.
        /// </returns>
        public IMultiQuery CreateMultiQuery()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sets the batch size of the session
        /// </summary>
        /// <param name="batchSize"/>
        /// <returns/>
        public ISession SetBatchSize(int batchSize)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the session implementation.
        /// </summary>
        /// <remarks>
        /// This method is provided in order to get the <b>NHibernate</b> implementation of the session from wrapper implementions.
        ///             Implementors of the <seealso cref="T:NHibernate.ISession"/> interface should return the NHibernate implementation of this method.
        /// </remarks>
        /// <returns>
        /// An NHibernate implementation of the <seealso cref="T:NHibernate.Engine.ISessionImplementor"/> interface
        /// </returns>
        public ISessionImplementor GetSessionImplementation()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// An <see cref="T:NHibernate.IMultiCriteria"/> that can return a list of all the results
        ///             of all the criterias.
        /// </summary>
        /// <returns/>
        public IMultiCriteria CreateMultiCriteria()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Starts a new Session with the given entity mode in effect. This secondary
        ///             Session inherits the connection, transaction, and other context
        ///             information from the primary Session. It doesn't need to be flushed
        ///             or closed by the developer.
        /// </summary>
        /// <param name="entityMode">The entity mode to use for the new session.</param>
        /// <returns>
        /// The new session
        /// </returns>
        public ISession GetSession(EntityMode entityMode)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The entity mode in effect for this session.
        /// </summary>
        public EntityMode ActiveEntityMode
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Determines at which points Hibernate automatically flushes the session.
        /// </summary>
        /// <remarks>
        /// For a readonly session, it is reasonable to set the flush mode to <c>FlushMode.Never</c>
        ///             at the start of the session (in order to achieve some extra performance).
        /// </remarks>
        public FlushMode FlushMode
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        /// <summary>
        /// The current cache mode. 
        /// </summary>
        /// <remarks>
        /// Cache mode determines the manner in which this session can interact with
        ///             the second level cache.
        /// </remarks>
        public CacheMode CacheMode
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Get the <see cref="T:NHibernate.ISessionFactory"/> that created this instance.
        /// </summary>
        public ISessionFactory SessionFactory
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets the ADO.NET connection.
        /// </summary>
        /// <remarks>
        /// Applications are responsible for calling commit/rollback upon the connection before
        ///             closing the <c>ISession</c>.
        /// </remarks>
        public IDbConnection Connection
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Is the <c>ISession</c> still open?
        /// </summary>
        public bool IsOpen
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Is the <c>ISession</c> currently connected?
        /// </summary>
        public bool IsConnected
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// The read-only status for entities (and proxies) loaded into this Session.
        /// </summary>
        /// <remarks>
        /// <para>
        /// When a proxy is initialized, the loaded entity will have the same read-only setting
        ///             as the uninitialized proxy, regardless of the session's current setting.
        /// </para>
        /// <para>
        /// To change the read-only setting for a particular entity or proxy that is already in 
        ///             this session, see <see cref="M:NHibernate.ISession.SetReadOnly(System.Object,System.Boolean)"/>.
        /// </para>
        /// <para>
        /// To override this session's read-only setting for entities and proxies loaded by a query,
        ///             see <see cref="M:NHibernate.IQuery.SetReadOnly(System.Boolean)"/>.
        /// </para>
        /// <para>
        /// This method is a facade for <see cref="P:NHibernate.Engine.IPersistenceContext.DefaultReadOnly"/>.
        /// </para>
        /// </remarks>
        /// <seealso cref="M:NHibernate.ISession.IsReadOnly(System.Object)"/><seealso cref="M:NHibernate.ISession.SetReadOnly(System.Object,System.Boolean)"/>
        public bool DefaultReadOnly
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Get the current Unit of Work and return the associated <c>ITransaction</c> object.
        /// </summary>
        public ITransaction Transaction
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Get the statistics for this session.
        /// </summary>
        public ISessionStatistics Statistics
        {
            get { throw new NotImplementedException(); }
        }

        #endregion
    }
}
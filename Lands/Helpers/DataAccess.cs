﻿namespace Lands.Helpers
{
    using Interfaces;
    using Models;
    using SQLite;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Xamarin.Forms;

    public class DataAccess 
    {
        string dbPath;
        SQLiteConnection db;
        public ObservableCollection<UserLocal> Customers { get; set; }
        //private SQLiteConnection connection;
        public DataAccess()
        {
            dbPath = Path.Combine(
                 Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                 "Lands.db3");

            db = new SQLiteConnection(dbPath);

            db.CreateTable<UserLocal>();
            db.CreateTable<TokenResponse>();

            /*var config = DependencyService.Get<IConfig>();
            this.connection = new SQLiteConnection(
                config.Platform,
                Path.Combine(config.DirectoryDB, "Lands.db3"));
            connection.CreateTable<UserLocal>();
            connection.CreateTable<TokenResponse>();*/
        }

        internal TokenResponse GetToken()
        {
            var table = db.Table<TokenResponse>();
            TokenResponse token = new TokenResponse();
            foreach (var s in table)
            {
                token.AccessToken = s.AccessToken;
                token.TokenType = s.TokenType;
                token.TokenResponseId = s.TokenResponseId;
                token.Expires = s.Expires;
                token.ExpiresIn = s.ExpiresIn;
                token.UserName = s.UserName;
                token.Issued = s.Issued;
                token.ErrorDescription = s.ErrorDescription;



                //Console.WriteLine(s.Id + " " + s.Symbol);
            }

            return token;
        }

        public void InsertUser(UserLocal newUser)
        {
            var id_atual = newUser.UserId;
            if (db.Table<UserLocal>().Count() == 0)
            {
                db.Insert(newUser);
            }
            else
            {
                var table = db.Table<UserLocal>();
                foreach (var s in table)
                {
                    db.DeleteAll<UserLocal>();
                }
                db.Insert(newUser);
                db.Execute("Update [UserLocal] set [UserId] = '" + id_atual + "' where [UserId]= '" + newUser.UserId + "'");
            }
        }

        public void UpdateUser(UserLocal newUser)
        {
            db.Execute("Update [UserLocal] set [FirstName] = '" + newUser.FirstName + "', [LastName] = '" + 
                       newUser.LastName + "'," + " [Email] = '" + newUser.Email + "', [Telephone] = '" +
                       newUser.Telephone + "', [ImagePath] = '"+ 
                       newUser.ImagePath + "', [UserTypeId] = '" +
                       newUser.UserTypeId + "' where [UserId]= '" + newUser.UserId + "'");


        }

        public UserLocal GetUser()
        {
            var table = db.Table<UserLocal>();
            UserLocal user = new UserLocal();
            foreach (var s in table)
            {
                user.UserId = s.UserId;
                user.FirstName = s.FirstName;
                user.LastName = s.LastName;
                user.ImagePath = s.ImagePath;
                user.Password = s.Password;
                user.Telephone = s.Telephone;
                user.Email = s.Email;
                user.UserTypeId = s.UserTypeId;

                //Console.WriteLine(s.Id + " " + s.Symbol);
            }

            return user;
        }

        internal void InsertToken(TokenResponse newToken)
        {
            if (db.Table<TokenResponse>().Count() == 0)
            {
                db.Insert(newToken);
            }
            else
            {
                var table = db.Table<TokenResponse>();
                foreach (var s in table)
                {
                    db.DeleteAll<TokenResponse>();
                }
                db.Insert(newToken);
            }
        }

        /*public Task<List<UserLocal>> GetItemsAsync()
        {
            return db.Table<UserLocal>().ToListAsync();
        }
        public Task<List<UserLocal>> GetItemsNotDoneAsync()
        {
            return db.QueryAsync<UserLocal>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
        }

        public Task<UserLocal> GetItemAsync(int id)
        {
            return db.Table<UserLocal>().Where(i => i.UserId == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(UserLocal item)
        {
            if (item.UserId != 0)
            {
                return db.UpdateAsync(item);
            }
            else
            {
                return db.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(UserLocal item)
        {
            return db.DeleteAsync(item);
        }

        /*public void Insert<T>(T model)
        {
            this.connection.Insert(model);
        }

        public void Update<T>(T model)
        {
            this.connection.Update(model);
        }

        public void Delete<T>(T model)
        {
            this.connection.Delete(model);
        }

        public T First<T>(bool WithChildren) where T : class
        {

                return connection.Table<T>().FirstOrDefault();
            
        }

        public List<T> GetList<T>(bool WithChildren) where T : class
        {

                return connection.Table<T>().ToList();
            
        }

        public T Find<T>(int pk, bool WithChildren) where T : class
        {


                return connection.Table<T>().FirstOrDefault(m => m.GetHashCode() == pk);
            
        }

        public void Dispose()
        {
            connection.Dispose();
        }*/
    }
}
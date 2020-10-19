using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Net;
using MySql.Data.MySqlClient;

namespace TeleMedicine
{
    class database
    {

            /// <summary>
            /// Variable declarations
            /// </summary>
            MainWindow parent;
            public bool DbConnected = false;
            
            string servername = "";
            string username = "";
            string password = "";

            string dbname = "";
            string connstring = "";
           

            public MySqlConnection conn;
            MySqlDataAdapter adapter;
           
        
            /// <summary>
            /// Default contructor. 
            /// </summary>
            /// <param name="root"></param>
            /// <param name="_server"></param>
            /// <param name="_username"></param>
            /// <param name="_password"></param>
            /// <param name="_db"></param>
            public database(MainWindow root, string _server, string _username, string _password, string _db)
            {
                parent = root;
                servername = _server;
                username = _username;
                password = _password;
                dbname = _db;
                connstring = "SERVER=" + servername + ";DATABASE=" + dbname + ";User=" + username + ";PASSWORD=" + password + ";convert zero datetime=True ";
            
            }
         
            /// <summary>
            /// Query database and return result in table format
            /// </summary>
            /// <param name="query"></param>
            /// <returns></returns>
            public DataTable queryDB(string query)
            {
                DataTable table = new DataTable();

            if (conn != null)
            {
                if (conn.State.ToString().Equals("Open"))
                {
                    try
                    {

                        adapter = new MySqlDataAdapter(query, connstring);
                        DataSet DS = new DataSet();
                        //get query results in dataset
                        adapter.Fill(DS);

                        if (DS.Tables.Count > 0)
                            table = DS.Tables[0];

                    }
                    catch (Exception rw)
                    {
                        System.Windows.MessageBox.Show("An error occured\n"+rw.Message,"TeleMedicine",System.Windows.MessageBoxButton.OK,System.Windows.MessageBoxImage.Hand);
                    }
                }
                else
                {
                    DbConnected = false;

                }
            }

                return table;
            }

    
        /// <summary>
        /// Attempt to open a connection to the database
        /// </summary>
        public void OpenDB()
            {
                try
                {
                    conn = new MySqlConnection(connstring);
                     conn.StateChange += Conn_StateChange;
                     conn.Open();

            }
            catch (Exception)
                {
                    DbConnected = false;
                }

            }

        /// <summary>
        /// Event for connection status of database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Conn_StateChange(object sender, StateChangeEventArgs e)
        {
            if (conn.State.ToString().Equals("Open"))
            {
                DbConnected = true;
                
            }
            else
            {
                DbConnected = false;
            }

            parent.dbstate(e.CurrentState.ToString());
        }

        /// <summary>
        /// Attempt to close an existing connection to database
        /// </summary>
        public void CloseDB()
            {
                try
                {
                    conn.Close();
                    DbConnected = false;
                }
                catch
                {
                    DbConnected = false;

                }
            }



        }
    }

 
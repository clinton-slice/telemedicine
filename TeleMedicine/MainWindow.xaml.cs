using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Data;
using TeleMedTransition;
using MySql.Data.MySqlClient;
using System.IO;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Threading;


namespace TeleMedicine
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

      
    public partial class MainWindow : Window
    {
        
        //Database Connection details
        string db_server   = "localhost";
        string db_username = "root";
        string db_password = "admin";
        string db_database = "telemedicine";

       //Encryption/Decryption key for password
        public string cipher_text = "#>.Telemed!5102";

         DispatcherTimer timer;
         database db;

        public string Dbstate="Closed";

        /// <summary>
        /// Main Function. Loads up the login page on startup
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

           
            db = new database(this,db_server,db_username,db_password,db_database);

             pages.login newPage = new pages.login(this);
            pageTransitionControl.TransitionType = PageTransitionType.Fade;
            pageTransitionControl.ShowPage(newPage);

            timer = new DispatcherTimer();
            timer.Tick+= new EventHandler(timer_tick);
            timer.Interval = new TimeSpan(0,0,3);
            timer.Start();
         
        }



        /// <summary>
        /// Loads account of user according to thier account category
        /// </summary>
        /// <param name="category"></param>
        /// <param name="id"></param>
        public void loadAccount(string category, int id)
        {
            if (category.Equals("new"))
            {
                pages.welcome newPage = new pages.welcome(this, id);
                pageTransitionControl.TransitionType = PageTransitionType.Fade;
                pageTransitionControl.ShowPage(newPage);
            }
            else if (category.Equals("doctor"))
            {
                pages.doctor.doctor_frame newPage = new pages.doctor.doctor_frame(this, id);
                pageTransitionControl.TransitionType = PageTransitionType.Fade;
                pageTransitionControl.ShowPage(newPage);
            }
            else if (category.Equals("patient"))
            {
                pages.patient.patient_frame newPage = new pages.patient.patient_frame(this, id);
                pageTransitionControl.TransitionType = PageTransitionType.Fade;
                pageTransitionControl.ShowPage(newPage);
            }
            else if (category.Equals("admin"))
            {
                pages.admin.admin_frame newPage = new pages.admin.admin_frame(this, id);
                pageTransitionControl.TransitionType = PageTransitionType.Fade;
                pageTransitionControl.ShowPage(newPage);
            }


        }

      

        /// <summary>
        /// Sign out an account 
        /// </summary>
        /// <param name="id"></param>
        public void signout(int id)
        {

            pages.login newPage = new pages.login(this);
            pageTransitionControl.TransitionType = PageTransitionType.Fade;
            pageTransitionControl.ShowPage(newPage);


        }

        /// <summary>
        /// Timer to check if database is still active and connected
        /// Keeps the form always visible once splashscreen has closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_tick(object sender, EventArgs e)
        {

            if (Dbstate.Equals("Closed"))
            { Thread tid2 = new Thread(new ThreadStart(openDatabase)); tid2.Start(); } 
           else
            { db.conn.Ping(); }

            GC.Collect();
          if(this.WindowState==WindowState.Normal)
                this.Visibility = Visibility.Visible;
        }

       /// <summary>
       /// Calls a function from class database for connection to database 
       /// </summary>
       private void openDatabase()
        {
            db.OpenDB();
        }

        /// <summary>
        /// Get the state of connection to database
        /// </summary>
        /// <param name="state"></param>
        public void dbstate(string state)
        {
            Dbstate = state;
            
        }


        /// <summary>
        /// Call a function in class database for all database queries
        /// </summary>
        /// <param name="_query"></param>
        /// <returns></returns>
        public DataTable query(string _query)
        {
            return db.queryDB(_query);
        }


        /// <summary>
        /// Removes all apostrophe symbol from a text in order to avoid sql injection
        /// </summary>
        /// <param name="original"></param>
        /// <returns></returns>
        public string clean(string original)
        {
            if (original != null)
                return System.Text.RegularExpressions.Regex.Replace(original, "'", "");
            else
                return null;
        }

        /// <summary>
        /// Format a name. Returns Upper case of each word, lower cases for other letters
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string formatname(string name)
        {
            if (name.Length > 0)
            {
                name = name.ToLower();
                char[] _letters = name.ToCharArray();
                _letters[0] = Char.Parse(_letters[0].ToString().ToUpper());
                name = new String(_letters);
                return name;
            }
            else
            { return ""; }

        }

        /// <summary>
        /// Function to update the profile picture of an account
        /// </summary>
        /// <param name="id"></param>
        /// <param name="img"></param>
        /// <returns></returns>
        public bool savePhoto(int id,System.Drawing.Bitmap img)
        {

            if (db.conn != null)
            {
                if (db.DbConnected)
                {
                    MemoryStream ms = new MemoryStream();
                    img.Save(ms, img.RawFormat);
                    byte[] fileByte = ms.ToArray();
                    MySqlCommand comm = new MySqlCommand("update photo set photoblob=@pic where id=" + id, db.conn);
                    comm.Parameters.Add("@pic", fileByte);
                    comm.ExecuteNonQuery();
                    return true;
                }
            }

            return false;
        }




      

        /// <summary>
        /// Function to add drug to database
        /// </summary>
        /// <param name="name"></param>
        /// <param name="amount"></param>
        /// <param name="description"></param>
        /// <param name="img"></param>
        /// <returns></returns>
        public int saveDrug(string name,string amount,string description, Bitmap img)
        {

            if (db.conn != null)
            {
                if (db.DbConnected)
                {
                    MemoryStream ms = new MemoryStream();
                    img.Save(ms, img.RawFormat);
                    byte[] fileByte = ms.ToArray();
                    MySqlCommand comm = new MySqlCommand("insert into drugs (drug_name,cost,description,photo) values (@name,@cost,@des,@pic)", db.conn);
                    comm.Parameters.Add("@name", name);
                    comm.Parameters.Add("@cost", amount);
                    comm.Parameters.Add("@des", description);
                    comm.Parameters.Add("@pic", fileByte);
                    comm.ExecuteNonQuery();

                    DataTable id_qry = query("select max(drug_id) from drugs");
                    if(id_qry.Rows.Count>0)
                    {
                        DataRow row = id_qry.Rows[0];
                        return Int32.Parse(row[0].ToString());
                    }
                    else
                    {
                        return 0;
                    }


                }
            }

            return 0;
        }


 


        /// <summary>
        /// update specific drug information 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="amount"></param>
        /// <param name="description"></param>
        /// <param name="fileByte"></param>
        /// <returns></returns>
        public bool updateDrug(int id, string name, string amount, string description, Byte[] fileByte)
        {

            if (db.conn != null)
            {
                if (db.DbConnected)
                {
                  
                    MySqlCommand comm = new MySqlCommand("update drugs set drug_name=@name,cost=@cost,description=@des,photo=@pic where drug_id=" + id, db.conn);
                    comm.Parameters.Add("@name", name);
                    comm.Parameters.Add("@cost", amount);
                    comm.Parameters.Add("@des", description);
                    comm.Parameters.Add("@pic", fileByte);
                    comm.ExecuteNonQuery();

                    return true;

                }
            }

            return false;
        }





        /// <summary>
        /// change account password
        /// </summary>
        /// <param name="id"></param>
        /// <param name="c_pass"></param>
        /// <param name="n_pass"></param>
        /// <returns></returns>
        public bool changepassword(int id,string c_pass,string n_pass)
        {

            DataTable table = new DataTable();
            MySqlDataAdapter adapter;

            if (db.conn != null)
            {
                if (db.DbConnected)
                {
                    MySqlCommand comm = new MySqlCommand("select password from login where user_id=@id", db.conn);
                    comm.Parameters.Add("@id", id);
                    comm.ExecuteNonQuery();
                    bool verified = false;
                    using (adapter = new MySqlDataAdapter(comm))
                    {
                        adapter.Fill(table);
                        if (table.Rows.Count > 0)
                        {
                            string h_ash = table.Rows[0][0].ToString();
                          
                            if (security.DecryptStringAES(h_ash, cipher_text).Equals(c_pass.Trim()))
                                verified = true;
                        }
                    }

                    if (verified)
                    {
                        n_pass = security.EncryptStringAES(n_pass, cipher_text);
                        comm = new MySqlCommand("update login set password=@npass where user_id=@id", db.conn);
                        comm.Parameters.Add("@id", id);
                        comm.Parameters.Add("@npass", n_pass);
                        comm.ExecuteNonQuery();
                        return true;
                    }

                }
            }

            return false;
        }


     


        [DllImport("gdi32.dll")]
        public static extern bool
            DeleteObject(IntPtr hobj);

        /// <summary>
        /// Converts blob (bytes) from database to image
        /// </summary>
        /// <param name="bits"></param>
        /// <returns></returns>
        public BitmapImage getImg(byte[] bits)
        {

            try
            {
                using (var ms = new System.IO.MemoryStream(bits))
                {
                    var image = new BitmapImage();
                    image.BeginInit();
                    image.CacheOption = BitmapCacheOption.OnLoad; // here
                    image.StreamSource = ms;
                    image.EndInit();
                    return image;
                }
            }
            catch (Exception) { return new BitmapImage() ; }

        }

        /// <summary>
        /// Format date to sql format (YYYY-MM-DD)
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public string reverseDateFormat(string date)
        {
            string newdate = "";

            string[] block = date.Split(new char[] { ' ' });
            string[] split = block[0].Split(new char[] { '/' });
            
            if(split.Length==3)
            {
                newdate = split[2] + "-" + split[0] + "-" + split[1];
            }

            return newdate;
        }

        /// <summary>
        /// Convert AM/PM format to 24hrs format
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public string convertTo24HoursFormat(string time)
        {
            string newtime = "";
             string [] split = time.Split(new char[] { ' ' });
            string [] component= split[0].Split(new char[] { ':' });

             if(split[1].Equals("PM"))
            {
                if (int.Parse(component[0]) < 12)
                    newtime += int.Parse(component[0]) + 12;
                else
                    newtime += component[0];
            }
            else if (split[1].Equals("AM"))
            {
                if (int.Parse(component[0]) == 12)
                    newtime += "00";
                else
                    newtime += component[0];
            }
            else
            {

            }


            return newtime+":"+component[1]+":00";
        }


        /// <summary>
        /// Update doctor's information
        /// </summary>
        /// <param name="id"></param>
        /// <param name="lastname"></param>
        /// <param name="firstname"></param>
        /// <param name="specialization"></param>
        /// <returns></returns>
        public bool updateDoctorInformation(int id,string lastname,string firstname,string specialization)
        {
            if (db.DbConnected)
            {
                MySqlCommand comm = new MySqlCommand("update doctor set lastname=@last,firstname=@first,specialization=@specialize where doctorid=@id", db.conn);
                comm.Parameters.Add("@id", id);
                comm.Parameters.Add("@first", clean(firstname));
                comm.Parameters.Add("@last",clean(lastname));
                comm.Parameters.Add("@specialize", specialization);
                comm.ExecuteNonQuery();
                return true;
            }
            return false;
        }


        /// <summary>
        /// Update patient information
        /// </summary>
        /// <param name="id"></param>
        /// <param name="lastname"></param>
        /// <param name="firstname"></param>
        /// <param name="sex"></param>
        /// <param name="address"></param>
        /// <param name="email"></param>
        /// <param name="phone"></param>
        /// <param name="allergies"></param>
        /// <param name="bloodgroup"></param>
        /// <param name="genotype"></param>
        /// <param name="height"></param>
        /// <param name="weight"></param>
        /// <returns></returns>
        public bool updatePatientInformation(int id, string lastname, string firstname,string sex,string address,string email,string phone,string allergies,string bloodgroup,string genotype,string height,string weight)
        {
            
            if (db.DbConnected)
            {
                MySqlCommand comm = new MySqlCommand("update patient set lastname=@last,firstname=@first,sex=@sex,address=@address,email=@email,phoneno=@phone,allergies=@allergies,bloodgroup=@bloodgroup,genotype=@genotype,height=@height,weight=@weight where patientid=@id", db.conn);
                comm.Parameters.Add("@id", id);
                comm.Parameters.Add("@first", clean(firstname));
                comm.Parameters.Add("@last", clean(lastname));
                comm.Parameters.Add("@sex", sex );
                comm.Parameters.Add("@address", address);
                comm.Parameters.Add("@email",email );
                comm.Parameters.Add("@phone", phone);
                comm.Parameters.Add("@allergies", allergies );
                comm.Parameters.Add("@bloodgroup", bloodgroup );
                comm.Parameters.Add("@genotype", genotype );
                comm.Parameters.Add("@height", height );
                comm.Parameters.Add("@weight", weight );



                comm.ExecuteNonQuery();
                return true;
            }
            return false;
        }




        /// <summary>
        /// Create a new record for a patient
        /// </summary>
        /// <param name="patientid"></param>
        /// <param name="doctorid"></param>
        /// <param name="comment"></param>
        /// <returns></returns>
        public bool addRecordInformation(int patientid,int doctorid,string comment )
        {
            if (db.DbConnected)
            {
                MySqlCommand comm = new MySqlCommand("insert into record (patientid,doctorid,comment,date_added) values(@pt,@dt,@com,now())", db.conn);
                comm.Parameters.Add("@pt", patientid);
                comm.Parameters.Add("@dt", doctorid);
                comm.Parameters.Add("@com", comment);
                 comm.ExecuteNonQuery();
                return true;
            }
            return false;
        }



        /// <summary>
        /// Update an existing patient record
        /// </summary>
        /// <param name="recordid"></param>
        /// <param name="patientid"></param>
        /// <param name="doctorid"></param>
        /// <param name="comment"></param>
        /// <returns></returns>
        public bool updateRecordInformation(int recordid,int patientid, int doctorid, string comment)
        {
            if (db.DbConnected)
            {
                MySqlCommand comm = new MySqlCommand("update record set patientid=@pt,doctorid=@dt,comment=@com where recordid=@id", db.conn);
                comm.Parameters.Add("@pt", patientid);
                comm.Parameters.Add("@dt", doctorid);
                comm.Parameters.Add("@com", comment);
                comm.Parameters.Add("@id", recordid);
                comm.ExecuteNonQuery();
                return true;
            }
            return false;
        }



        /// <summary>
        /// Add result information of a record
        /// </summary>
        /// <param name="recordid"></param>
        /// <param name="detail"></param>
        /// <returns></returns>
        public bool addResultInformation(int recordid, string detail)
        {
            if (db.DbConnected)
            {
                MySqlCommand comm = new MySqlCommand("insert into result (recordid,details) values(@id,@dt)", db.conn);
                comm.Parameters.Add("@id", recordid);
                comm.Parameters.Add("@dt", detail);
                 comm.ExecuteNonQuery();
                return true;
            }
            return false;
        }

 


        /// <summary>
        /// Add prescriptions for a record/diagnosis
        /// </summary>
        /// <param name="recordid"></param>
        /// <param name="detail"></param>
        /// <returns></returns>
        public bool addPrescriptionInformation(int recordid, string detail)
        {
            if (db.DbConnected)
            {
                MySqlCommand comm = new MySqlCommand("insert into prescription (recordid,details) values(@id,@dt)", db.conn);
                comm.Parameters.Add("@id", recordid);
                comm.Parameters.Add("@dt", detail);
                comm.ExecuteNonQuery();
                return true;
            }
            return false;
        }



      
    }
}

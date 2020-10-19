using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Windows.Threading;


namespace TeleMedicine.pages.doctor
{
    /// <summary>
    /// Interaction logic for messages.xaml
    /// </summary>
    public partial class messages : UserControl
    {
        MainWindow parent;
        doctor_frame subparent;
        DispatcherTimer timer;
        bool conversation_loaded = false;
        bool busy = false;
        int currentThread = 0;
        int threadlastmessageid = 0;
        int threadmessagecount = 0;
       public  int reciever_id;
        int refreshConversationListDelay = 2; //4 seconds (2 second equals 1 counts)


        public messages(MainWindow root, doctor_frame _sub)
        {
            parent = root;
            subparent = _sub;
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_tick);
            timer.Interval = new TimeSpan(0, 0, 2);
            timer.Start();

            rectFrame.Visibility= rtb.Visibility = sendBut.Visibility = bodyBox.Visibility = threadLabel.Visibility = messagecountLabel.Visibility = Visibility.Hidden;
            
        }


        private void timer_tick(object sender, EventArgs e)
        {
            if (!busy)
            {
                busy = true;
                if (!conversation_loaded)
                {
                    DataTable qry = parent.query("select max(messageid) from messages where reciever_user_id="+subparent.AccountId+ "  or sender_user_id=" + subparent.AccountId + " group by thread_id desc");

                    if (qry.Rows.Count > 0)
                    {
                        string patch = " where ";
                        foreach (DataRow rw in qry.Rows)
                        {
                            patch += " messageid=" + rw[0];

                            if (qry.Rows.IndexOf(rw) < (qry.Rows.Count - 1))
                                patch += " or ";
                        }

                      
                          qry = parent.query("select sender_user_id,thread_id,content,(select delete_user_id from message_thread where message_thread.thread_id=messages.thread_id) as del_user, if(sender_user_id=" + subparent.AccountId + ",'Yes',reciever_read) as state, (select concat(lastname,' ',firstname) from patient where login_user_id=(if(sender_user_id=" + subparent.AccountId + ",reciever_user_id,sender_user_id))) as name,(select photoblob from photo where id=(if(sender_user_id=" + subparent.AccountId + ",reciever_user_id,sender_user_id))) as photo  from messages" + patch+" order by thread_id desc");

                        foreach (DataRow rw in qry.Rows)
                          {
                            if(!rw["del_user"].ToString().Equals(""))
                            {
                                if (int.Parse(rw["del_user"].ToString()) == subparent.AccountId)
                                    continue;
                            }
                            StackPanel _mSp = new StackPanel();
                            _mSp.Orientation = Orientation.Horizontal;
                            _mSp.Height = 60;
                            _mSp.Name = "Thread_"+rw["thread_id"].ToString();

                            Image profilePix = new Image();
                            profilePix.Source = parent.getImg((byte[])rw["photo"]);
                            profilePix.Stretch = Stretch.UniformToFill;
                            profilePix.Width = 50;
                            profilePix.Height = 50;

                            _mSp.Children.Add(profilePix);


                            StackPanel _cSp = new StackPanel();
                            _cSp.Orientation = Orientation.Vertical;

                            Label name = new Label();
                            name.Content = "  "+rw["name"].ToString();
                            _cSp.Children.Add(name);


                            Label caption= new Label();
                            
                                if (rw["content"].ToString().Length > 15)
                                {
                                    caption.Content = "  " + rw["content"].ToString().Substring(0, 15) + "...";
                                }
                                else
                                {
                                    caption.Content = "  " + rw["content"].ToString();
                                }
                            if (int.Parse(rw["sender_user_id"].ToString()) == subparent.AccountId)
                            {
                                caption.Foreground = Brushes.MidnightBlue;
                            }
                            if (rw["state"].ToString().Equals("No"))
                            {
                                
                                caption.FontWeight = FontWeights.Bold;
                            }
                            _cSp.Children.Add(caption);

                            _mSp.Children.Add(_cSp);
                            conversationlist.Items.Add(_mSp);

                          }
                          
                    }
                    
                    conversation_loaded = true;
                }

                else
                {
                    if (currentThread != 0)
                    {
                        DataTable qry = parent.query("select messageid,sender_user_id,reciever_user_id,content,date_added,(if(sender_user_id=" + subparent.AccountId + ",'You',(select concat(lastname,' ',firstname) as name from patient where patient.login_user_id=sender_user_id))) as sender from messages where thread_id=" +currentThread+" and messageid>"+threadlastmessageid+" and sender_user_id!="+subparent.AccountId);
                        if (qry.Rows.Count > 0)
                        {
                            foreach (DataRow rw in qry.Rows)
                            {
                                reciever_id = (int.Parse(rw["sender_user_id"].ToString()) == subparent.AccountId) ? int.Parse(rw["reciever_user_id"].ToString()) : int.Parse(rw["sender_user_id"].ToString());
                                threadlastmessageid = (int.Parse(rw["sender_user_id"].ToString()) == subparent.AccountId) ? threadlastmessageid : int.Parse(rw["messageid"].ToString());
                                StackPanel messageStack = new StackPanel();
                                messageStack.Orientation = Orientation.Vertical;

                                Label senderLabel = new Label();
                                senderLabel.Content = rw["sender"].ToString();
                                senderLabel.FontWeight = FontWeights.Medium;
                                if (rw["sender"].ToString().Equals("You"))
                                    senderLabel.HorizontalAlignment = HorizontalAlignment.Right;
                                messageStack.Children.Add(senderLabel);

                                Label messageLabel = new Label();
                                messageLabel.Content = rw["content"].ToString();
                                if (rw["sender"].ToString().Equals("You"))
                                    messageLabel.HorizontalAlignment = HorizontalAlignment.Right;
                                messageStack.Children.Add(messageLabel);

                                Label timeLabel = new Label();
                                timeLabel.Content = rw["date_added"].ToString();
                                timeLabel.FontWeight = FontWeights.ExtraLight;
                                timeLabel.FontSize = 10;
                                if (rw["sender"].ToString().Equals("You"))
                                    timeLabel.HorizontalAlignment = HorizontalAlignment.Right;
                                messageStack.Children.Add(timeLabel);


                                bodyBox.Items.Add(messageStack);
                                 threadmessagecount++;
                                messagecountLabel.Content = "Message Count: " + threadmessagecount;
                                updateConversationList(rw["content"].ToString(), currentThread, true);
                            }
                            parent.query("update messages set reciever_read='Yes' where reciever_user_id=" + subparent.AccountId + " and thread_id=" + currentThread);

                            if (bodyBox.SelectedIndex == bodyBox.Items.Count - 2)
                            {
                                bodyBox.SelectedIndex = bodyBox.Items.Count - 1;
                                bodyBox.ScrollIntoView(bodyBox.SelectedItem);
                            }
                        }
                    }
 
                }

                if(refreshConversationListDelay>0)
                { refreshConversationListDelay--; }
                else
                {


                    DataTable qry = parent.query("select max(messageid) from messages where reciever_user_id=" + subparent.AccountId + "  or sender_user_id=" + subparent.AccountId + " group by thread_id desc");

                    if (qry.Rows.Count > 0)
                    {
                        string patch = " where ";
                        foreach (DataRow rw in qry.Rows)
                        {
                            patch += " messageid=" + rw[0];

                            if (qry.Rows.IndexOf(rw) < (qry.Rows.Count - 1))
                                patch += " or ";
                        }


                        qry = parent.query("select sender_user_id,thread_id,content,(select delete_user_id from message_thread where message_thread.thread_id=messages.thread_id) as del_user, if(sender_user_id=" + subparent.AccountId + ",'Yes',reciever_read) as state, (select concat(lastname,' ',firstname) from patient where login_user_id=(if(sender_user_id=" + subparent.AccountId + ",reciever_user_id,sender_user_id))) as name,(select photoblob from photo where id=(if(sender_user_id=" + subparent.AccountId + ",reciever_user_id,sender_user_id))) as photo  from messages" + patch + " order by thread_id desc");

                        foreach (DataRow rw in qry.Rows)
                        {
                            if (!rw["del_user"].ToString().Equals(""))
                            {
                                if (int.Parse(rw["del_user"].ToString()) == subparent.AccountId)
                                    continue;
                            }
                            StackPanel _mSp = new StackPanel();
                            _mSp.Orientation = Orientation.Horizontal;
                            _mSp.Height = 60;
                            _mSp.Name = "Thread_" + rw["thread_id"].ToString();

                            Image profilePix = new Image();
                            profilePix.Source = parent.getImg((byte[])rw["photo"]);
                            profilePix.Stretch = Stretch.UniformToFill;
                            profilePix.Width = 50;
                            profilePix.Height = 50;

                            _mSp.Children.Add(profilePix);


                            StackPanel _cSp = new StackPanel();
                            _cSp.Orientation = Orientation.Vertical;

                            Label name = new Label();
                            name.Content = "  " + rw["name"].ToString();
                            _cSp.Children.Add(name);


                            Label caption = new Label();

                            if (rw["content"].ToString().Length > 15)
                            {
                                caption.Content = "  " + rw["content"].ToString().Substring(0, 15) + "...";
                            }
                            else
                            {
                                caption.Content = "  " + rw["content"].ToString();
                            }
                            if (int.Parse(rw["sender_user_id"].ToString()) == subparent.AccountId)
                            {
                                caption.Foreground = Brushes.MidnightBlue;
                            }
                            if (rw["state"].ToString().Equals("No"))
                            {

                                caption.FontWeight = FontWeights.Bold;
                            }
                            _cSp.Children.Add(caption);

                            _mSp.Children.Add(_cSp);
                            refreshconversationlist(_mSp);

                        }

                    }

                    refreshConversationListDelay = 2;

                }

                busy = false;
            }
        }


        public void resetContent()
        {
            messagecountLabel.Content = "";
            threadLabel.Content = "";
            bodyBox.Items.Clear();
            rtb.Document.Blocks.Clear();
            currentThread = 0;
            threadlastmessageid = 0;
            threadmessagecount = 0;
        }

        public void refreshconversationlist(StackPanel msp)
        {
            bool found = false;
            foreach (StackPanel sp in conversationlist.Items)
            {

                if (msp.Name.Equals("Thread_" + currentThread))
                {
                    found = true;
                    break;
                }
                if (sp.Name.Equals(msp.Name))
                {
                    int index = conversationlist.Items.IndexOf(sp);
                    conversationlist.Items.Remove(sp);
                    conversationlist.Items.Insert(index, msp);
                    found = true;
                    break;
                }
            }

            if(!found)
            {
                conversationlist.Items.Insert(0, msp);
            }
        }

        public void updateConversationList(string content,int thread,bool recieved)
        {
            Label caption = new Label();

            if (content.Length > 15)
            {
                caption.Content = "  " + content.Substring(0, 15) + "...";
            }
            else
            {
                caption.Content = "  " + content;
            }
            if (!recieved)
            {
                caption.Foreground = Brushes.MidnightBlue;
            }
            if (thread!=currentThread && recieved)
            {
                caption.FontWeight = FontWeights.Bold;
            }

            
            foreach(StackPanel sp in conversationlist.Items)
            {
                if(sp.Name.Equals("Thread_"+thread))
                {
                    StackPanel sp2 = sp.Children[1] as StackPanel;
                    sp2.Children.RemoveAt(1);
                    sp2.Children.Add(caption);
                }
            }

        }

        private void conversationlist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            resetContent();
            if (conversationlist.SelectedIndex > -1)
            {
               
               rectFrame.Visibility= rtb.Visibility = sendBut.Visibility = bodyBox.Visibility = threadLabel.Visibility = messagecountLabel.Visibility = Visibility.Visible;
                StackPanel _Tsp = (StackPanel)(sender as ListBox).SelectedItem;
                string thread=_Tsp.Name.Split(new char[] { '_'})[1];
                currentThread = int.Parse(thread);
                DataTable qry = parent.query("select messageid, delete_user_id,sender_user_id,reciever_user_id,content,date_added,(if(sender_user_id=" + subparent.AccountId + ",'You',(select concat(lastname,' ',firstname) as name from patient where patient.login_user_id=sender_user_id))) as sender from messages where thread_id=" + thread);
                
                foreach (DataRow rw in qry.Rows)
                {
                   
                
                    if (!rw["delete_user_id"].ToString().Equals(""))
                    {
                        if (int.Parse(rw["delete_user_id"].ToString()) == subparent.AccountId)
                            continue;
                    }

                   
                    if (threadmessagecount == 0)
                        threadLabel.Content = "Thread started by: " + rw["sender"].ToString();
                    threadmessagecount++;
                    reciever_id = (int.Parse(rw["sender_user_id"].ToString()) == subparent.AccountId) ? int.Parse(rw["reciever_user_id"].ToString()) : int.Parse(rw["sender_user_id"].ToString());
                    threadlastmessageid= (int.Parse(rw["sender_user_id"].ToString()) == subparent.AccountId) ? threadlastmessageid : int.Parse(rw["messageid"].ToString());
                    StackPanel messageStack = new StackPanel();
                    messageStack.Orientation = Orientation.Vertical;
                    
                    Label senderLabel = new Label();
                    senderLabel.Content = rw["sender"].ToString();
                    senderLabel.FontWeight = FontWeights.Medium;
                    if (rw["sender"].ToString().Equals("You"))
                       senderLabel.HorizontalAlignment = HorizontalAlignment.Right;
                    messageStack.Children.Add(senderLabel);

                    TextBlock messageLabel = new TextBlock();
                    messageLabel.TextWrapping = TextWrapping.Wrap;
                    messageLabel.Width = 390;
                    messageLabel.Text = rw["content"].ToString();
                    if (rw["sender"].ToString().Equals("You"))
                    {
                        messageLabel.TextAlignment = TextAlignment.Right;
                        messageLabel.HorizontalAlignment = HorizontalAlignment.Right;
                    }

                    messageStack.Children.Add(messageLabel);

                    Label space = new Label();
                    space.Height = 5;
                    messageStack.Children.Add(space);

                    Label timeLabel = new Label();
                    timeLabel.Content = rw["date_added"].ToString();
                    timeLabel.FontWeight = FontWeights.ExtraLight;
                    timeLabel.FontSize = 10;
                    if (rw["sender"].ToString().Equals("You"))
                        timeLabel.HorizontalAlignment = HorizontalAlignment.Right;
                    messageStack.Children.Add(timeLabel);


                    bodyBox.Items.Add(messageStack);
                    bodyBox.SelectedIndex = bodyBox.Items.Count - 1;
                    bodyBox.ScrollIntoView(bodyBox.SelectedItem);
                }
                messagecountLabel.Content = "Message Count: " + threadmessagecount;

                parent.query("update messages set reciever_read='Yes' where reciever_user_id=" + subparent.AccountId + " and thread_id="+currentThread);
                StackPanel _cSP = _Tsp.Children[1] as StackPanel;
                Label label=_cSP.Children[1] as Label;
                label.FontWeight = FontWeights.Normal;


            }
            else
            {

                rectFrame.Visibility = rtb.Visibility = sendBut.Visibility = bodyBox.Visibility = threadLabel.Visibility = messagecountLabel.Visibility = Visibility.Hidden;
                
            }
        }

        private void sendBut_Click(object sender, RoutedEventArgs e)
        {
            StackPanel messageStack = new StackPanel();
            messageStack.Orientation = Orientation.Vertical;

            Label senderLabel = new Label();
            senderLabel.Content = "You";
            senderLabel.FontWeight = FontWeights.Medium;
            senderLabel.HorizontalAlignment = HorizontalAlignment.Right;
            messageStack.Children.Add(senderLabel);

            TextRange trange = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);

            TextBlock messageLabel = new TextBlock();
            messageLabel.TextWrapping = TextWrapping.Wrap;
            messageLabel.Width = 390;
            messageLabel.Text = trange.Text.Trim();
             messageLabel.TextAlignment = TextAlignment.Right;
                messageLabel.HorizontalAlignment = HorizontalAlignment.Right;
       

            messageStack.Children.Add(messageLabel);


            Label space = new Label();
            space.Height = 5;
            messageStack.Children.Add(space);

            Label timeLabel = new Label();
            timeLabel.Content = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
            timeLabel.FontWeight = FontWeights.ExtraLight;
            timeLabel.FontSize = 10;
             timeLabel.HorizontalAlignment = HorizontalAlignment.Right;
            messageStack.Children.Add(timeLabel);

            parent.query("insert into messages (thread_id,content,sender_user_id,reciever_user_id) values("+currentThread+",'"+messageLabel.Text+"',"+subparent.AccountId+","+reciever_id+") ");
            threadmessagecount++;
            messagecountLabel.Content = "Message Count: " + threadmessagecount;
            parent.query("update message_thread set delete_user_id=NULL where thread_id="+currentThread);
            rtb.Document.Blocks.Clear();
            bodyBox.Items.Add(messageStack);
            bodyBox.SelectedIndex = bodyBox.Items.Count - 1;
            bodyBox.ScrollIntoView(bodyBox.SelectedItem);
            updateConversationList(messageLabel.Text.ToString(), currentThread, false);
        }

        private void addConversation_Click(object sender, RoutedEventArgs e)
        {
            new AddConversation(parent,subparent, this).ShowDialog();
        }

        private void delButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (conversationlist.SelectedIndex > -1)
            {
                string thread;
                StackPanel sp = conversationlist.SelectedItem as StackPanel;
                StackPanel sp2 = sp.Children[1] as StackPanel;
                Label lb = sp2.Children[0] as Label;
                thread = sp.Name.Split(new char[] { '_' })[1];

                if (MessageBox.Show("Delete conversation with " + lb.Content.ToString().Trim() + "?", "TeleMedicine", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {

                    DataTable qry = parent.query("select delete_user_id from message_thread where thread_id=" + thread);
                    DataRow row = qry.Rows[0];
                    if (row["delete_user_id"].ToString().Equals(""))
                    {
                        parent.query("update message_thread set delete_user_id=" + subparent.AccountId + " where thread_id=" + currentThread);
                        parent.query("update messages set delete_user_id=" + subparent.AccountId + " where thread_id=" + currentThread);
                        conversationlist.Items.Remove(sp);
                    }
                    else
                    {
                        parent.query("delete from messages where thread_id=" + currentThread);
                        parent.query("delete from message_thread where thread_id=" + currentThread);
                        conversationlist.Items.Remove(sp);
                    }
                }

            }
            else
                MessageBox.Show("Select a conversion","TeleMedicine",MessageBoxButton.OK,MessageBoxImage.Error);

            
        }




    }
}

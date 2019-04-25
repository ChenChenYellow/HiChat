using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FireSharp.Config;
using FireSharp.Response;
using FireSharp;

using Newtonsoft.Json;
using System.Media;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Xml;
using System.Xml.Serialization;
using System.Data.OleDb;
using System.Data;

namespace HiChat
{
    public static class HiChat
    {

        private static FirebaseConfig config = new FirebaseConfig
        {
            BasePath = @"https://cs-project-ed1af.firebaseio.com/",
            AuthSecret = "zZwpPxSTktqkPix9L8w0IksiHa7nzkMdpfXGvsPD"
        };
        private static OleDbConnection myCon;
        private static DataSet dbSet;
        private static FirebaseClient client;
        private static OleDbDataAdapter adapterMessage, adapterFriendRequest;
        private static readonly string userPath = "/tableSimi/Users/";
        private static readonly string groupIDCounterPath = "/tableSimi/GroupIDCounter";
        private static readonly string groupPath = "/tableSimi/Groups/";
        private static readonly string messagePath = "/List_of_message";
        private static readonly string friendPath = "/List_of_friend";
        private static readonly string userGroupPath = "/List_of_group";
        private static readonly string friendRequestPath = "/List_of_friend_request";
        private static readonly string picturePath = "/Picture";
        private static readonly string appDataPath = Application.StartupPath.Remove(Application.StartupPath.Length - 9) + @"App_data\";
        public static readonly string appDataImagePath = appDataPath + @"Images\";
        public static bool TestConnection()
        {
            client = new FirebaseClient(config);
            if (client != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool Login(string username, string password)
        {
            if (username.Length <= 0)
            {
                return false;
            }
            FirebaseResponse res = client.Get(userPath + username);
            if (res.Body.Equals("null"))
            {
                return false;
            }
            else
            {
                User u = JsonConvert.DeserializeObject<User>(res.Body);
                if (u.Password.Equals(password))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public static bool Register(string username, string password)
        {
            if (!client.Get(userPath + username).Body.Equals("null"))
            {
                return false;
            }
            else
            {
                client.Set(userPath + username, new User(username, password));
                return true;
            }
        }
        public static bool ResetPassword(string username, string oldPassword, string newPassword)
        {
            User u = JsonConvert.DeserializeObject<User>(client.Get(userPath + username).Body);
            if (u.Password.Equals(oldPassword))
            {
                u.Password = newPassword;
                client.Update(userPath + username, u);
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool SetProfileImage(string username, string content)
        {
            User u = JsonConvert.DeserializeObject<User>(client.Get(userPath + username).Body);
            u.Picture = content;
            client.Update(userPath + username, u);
            return true;
        }
        public static string GetProgileImage(string username)
        {
            User u = JsonConvert.DeserializeObject<User>(client.Get(userPath + username).Body);
            return u.Picture;
        }
        public static bool UserExist(string username)
        {
            if (client.Get(userPath + username).Body.Equals("null"))
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        public static List<string> GetFriend(string username)
        {
            FirebaseResponse res = client.Get(userPath + username + friendPath);
            if (res.Body == "null")
            {
                return new List<string>();
            }
            else
            {
                List<string> ret = new List<string>();
                ret = JsonConvert.DeserializeObject<List<string>>(res.Body);
                return ret;
            }
        }
        public static bool AddFriend(string username1, string username2)
        {
            FirebaseResponse res = client.Get(userPath + username1 + friendPath);
            List<string> temp;
            if (res.Body == "null")
            {
                temp = new List<string>();
            }
            else
            {
                temp = JsonConvert.DeserializeObject<List<string>>(res.Body);
            }
            if (!temp.Contains(username2))
            {
                temp.Add(username2);
                client.Set(userPath + username1 + friendPath, temp);
            }
            res = client.Get(userPath + username2 + friendPath);
            if (res.Body == "null")
            {
                temp = new List<string>();
            }
            else
            {
                temp = JsonConvert.DeserializeObject<List<string>>(res.Body);
            }
            if (!temp.Contains(username1))
            {
                temp.Add(username1);
                client.Set(userPath + username2 + friendPath, temp);
            }
            return true;
        }

        public static bool DeleteFriend(string username, string friendUsername)
        {
            FirebaseResponse res = client.Get(userPath + username + friendPath);
            if (res.Body == "null")
            {
                return true;
            }
            List<string> temp = JsonConvert.DeserializeObject<List<string>>(res.Body);
            if (temp.Contains(friendUsername))
            {
                temp.Remove(friendUsername);
                MessageBox.Show(userPath + username + friendPath);
                MessageBox.Show(JsonConvert.SerializeObject(temp));
                client.Set(userPath + username + friendPath, temp);
                return true;
            }
            else
            {
                return true;
            }
        }

        public static Message SendMessage(string sender, string receiver, string content)
        {
            return SendMessage(sender, receiver, content, "Text");
        }
        public static Message SendMessage(string receiver, Message m)
        {
            FirebaseResponse res = client.Get(userPath + receiver + messagePath);
            List<Message> temp;
            if (res.Body == "null")
            {
                temp = new List<Message>();
            }
            else
            {
                temp = JsonConvert.DeserializeObject<List<Message>>(client.Get(userPath + receiver + messagePath).Body);
            }
            temp.Add(m);
            client.Set(userPath + receiver + messagePath, temp);

            return m;
        }
        public static Message SendMessage(string sender_username, string receiver_username, string content, string type)
        {
            return SendMessage(receiver_username, new Message(sender_username, receiver_username, content, DateTime.Now, type, false));
        }
        public static bool AddMessageToLocal(Message m)
        {
            DataRow r = dbSet.Tables["Message"].NewRow();
            r["sender"] = m.Sender;
            r["receiver"] = m.Receiver;
            r["message_date"] = m.Message_date;
            r["type"] = m.Type;
            r["group_message"] = m.GroupMessage;
            if (m.Type == "Text")
            {
                r["content"] = m.Content;

            }
            else
            {
                r["content"] = appDataImagePath + m.Type;
            }
            dbSet.Tables["Message"].Rows.Add(r);
            adapterMessage.Update(dbSet, "Message");
            return true;
        }

        public static List<Message> GetMessage(string username)
        {
            FirebaseResponse res = client.Get(userPath + username);
            if (res.Body.Equals("null"))
            {
                return null;
            }
            else
            {
                User u = JsonConvert.DeserializeObject<User>(res.Body.ToString());
                if (u.List_of_message == null || u.List_of_message.Count == 0)
                {
                    return null;
                }
                else
                {
                    return u.List_of_message;
                }
            }
        }

        public static bool DeleteMessage(string username)
        {
            client.Delete(userPath + username + messagePath);
            return true;
        }

        public static bool SendFriendRequest(string sender_username, string receiver_username)
        {
            User receiver = JsonConvert.DeserializeObject<User>(client.Get(userPath + receiver_username).Body);
            if (receiver.List_of_friend_request == null)
            {
                receiver.List_of_friend_request = new List<Friend_Request>();
            }
            Friend_Request temp = new Friend_Request(sender_username, receiver_username, DateTime.Now);
            receiver.List_of_friend_request.Add(temp);
            client.Update(userPath + receiver_username, receiver);
            return true;
        }

        public static List<Friend_Request> GetFriend_Request(string username)
        {
            FirebaseResponse res = client.Get(userPath + username + friendRequestPath);
            List<Friend_Request> ret = new List<Friend_Request>();
            if (!res.Body.Equals("null"))
            {
                ret = JsonConvert.DeserializeObject<List<Friend_Request>>(res.Body);
            }
            return ret;
        }

        public static bool DeleteFriendRequest(string username)
        {
            client.Delete(userPath + username + friendRequestPath);
            return true;
        }

        public static string SerializeImage(Image i, ImageFormat iformat)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                i.Save(ms, iformat);
                return Convert.ToBase64String(ms.ToArray());
            }
        }
        public static Image DeserializeImage(string i)
        {
            byte[] imageGetBytes = Convert.FromBase64String(i);
            using (MemoryStream ms2 = new MemoryStream(imageGetBytes))
            {
                return Image.FromStream(ms2);
            }
        }

        public static void SaveImage(string content, string path)
        {
            byte[] imageGetBytes = Convert.FromBase64String(content);
            using (MemoryStream ms2 = new MemoryStream(imageGetBytes))
            {
                Image image = Image.FromStream(ms2);
                image.Save(path);
            }
        }
        public static void BackUpFileToFolder(string path, string safeName)
        {
            File.Copy(path, appDataImagePath + safeName);
        }

        public static string SerializeFile(string path)
        {
            return Convert.ToBase64String(File.ReadAllBytes(path));

        }
        public static void DeserializeFile(string downloadToPath, string content)
        {
            File.WriteAllBytes(downloadToPath, Convert.FromBase64String(content));
        }

        public static bool AddGroup(string username, string groupID)
        {
            List<string> temp = JsonConvert.DeserializeObject<List<string>>(client.Get(groupPath + groupID + "/Members").Body);
            if (!temp.Contains(username))
            {
                UserAddGroup(username, groupID);
                temp.Add(username);
                client.Set(groupPath + groupID + "/Members", temp);
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool RegisterGroup(string sender, string receiver, string groupAlias)
        {
            List<string> list = new List<string>();
            list.Add(sender);
            list.Add(receiver);
            int id = JsonConvert.DeserializeObject<int>(client.Get(groupIDCounterPath).Body);
            client.Set(groupIDCounterPath, (id + 1).ToString());
            Group g = new Group(list, id.ToString(), groupAlias);
            client.Update(groupPath + id.ToString(), g);

            UserAddGroup(sender, id.ToString());
            UserAddGroup(receiver, id.ToString());
            return true;
        }
        private static bool UserAddGroup(string username, string groupID)
        {
            FirebaseResponse res = client.Get(userPath + username + userGroupPath);
            List<string> temp;
            if (res.Body == "null")
            {
                temp = new List<string>();
            }
            else
            {
                temp = JsonConvert.DeserializeObject<List<string>>(res.Body);
            }
            temp.Add(groupID);
            client.Set(userPath + username + userGroupPath, temp);
            return true;
        }
        public static Message SendMessageToGroup(string sender, string receiverGroup, string content)
        {
            return SendMessageToGroup(new Message(sender, receiverGroup, content, DateTime.Now, "Text", true));

        }
        public static Message SendMessageToGroup(string sender, string receiverGroup, string content, string type)
        {
            return SendMessageToGroup(new Message(sender, receiverGroup, content, DateTime.Now, type, true));
        }
        public static Message SendMessageToGroup(Message message)
        {
            Group g = JsonConvert.DeserializeObject<Group>(client.Get(groupPath + message.Receiver).Body);
            foreach (string item in g.Members)
            {
                if (item.Equals(message.Sender))
                {
                    continue;
                }
                SendMessage(item, message);
            }
            return message;
        }
        private static bool deleteGroupFromUser(string username, string groupID)
        {
            List<string> temp;
            FirebaseResponse res = client.Get(userPath + username + userGroupPath);
            temp = JsonConvert.DeserializeObject<List<string>>(res.Body);
            if (temp != null)
            {
                temp.Remove(groupID);
                client.Set(userPath + username + userGroupPath, temp);
            }
            return true;
        }
        public static bool QuitGroup(string username, string groupID)
        {
            FirebaseResponse res = client.Get(groupPath + groupID);
            Group g = JsonConvert.DeserializeObject<Group>(res.Body);
            g.Members.Remove(username);
            if (g.Members.Count < 2)
            {
                client.Delete(groupPath + groupID);
                foreach (string item in g.Members)
                {
                    deleteGroupFromUser(item, groupID);
                }
            }
            else
            {
                client.Update(groupPath + groupID, g);
                deleteGroupFromUser(username, groupID);
            }
            return true;
        }
        public static bool ChangeGroupAlias(string groupID, string newname)
        {
            Group g = JsonConvert.DeserializeObject<Group>(client.Get(groupPath + groupID).Body);
            g.Alias = newname;
            client.Update(groupPath + groupID, g);
            return true;
        }
        public static List<int> GetGroupID(string username)
        {
            List<int> ret = new List<int>();
            FirebaseResponse res = client.Get(userPath + username + userGroupPath);
            if (res.Body.Equals("null"))
            {
                return ret;
            }
            else
            {
                ret = JsonConvert.DeserializeObject<List<int>>(res.Body);
                return ret;
            }
        }
        public static List<Group> GetGroupByID(List<int> group_id)
        {
            List<Group> ret = new List<Group>();
            foreach (int item in group_id)
            {
                FirebaseResponse res = client.Get(groupPath + item.ToString());
                Group g = JsonConvert.DeserializeObject<Group>(res.Body);
                ret.Add(g);
            }
            return ret;
        }

        public static bool CreateUserDatabase(ADOX.Catalog catalog)
        {
            ADOX.Table table = new ADOX.Table();
            table.Name = "Message";
            catalog.Tables.Append(table);

            ADOX.Column col = newColumn("sender", ADOX.DataTypeEnum.adWChar, catalog);
            col.DefinedSize = 50;
            col.Attributes = ADOX.ColumnAttributesEnum.adColNullable;
            table.Columns.Append(col);

            col = newColumn("receiver", ADOX.DataTypeEnum.adWChar, catalog);
            col.DefinedSize = 50;
            col.Attributes = ADOX.ColumnAttributesEnum.adColNullable;
            table.Columns.Append(col);

            col = newColumn("content", ADOX.DataTypeEnum.adLongVarWChar, catalog);
            col.DefinedSize = 5000;
            col.Attributes = ADOX.ColumnAttributesEnum.adColNullable;
            table.Columns.Append(col);

            col = newColumn("message_date", ADOX.DataTypeEnum.adDate, catalog);
            table.Columns.Append(col);

            col = newColumn("type", ADOX.DataTypeEnum.adWChar, catalog);
            col.DefinedSize = 50;
            col.Attributes = ADOX.ColumnAttributesEnum.adColNullable;
            table.Columns.Append(col);

            col = newColumn("group_message", ADOX.DataTypeEnum.adBoolean, catalog);
            table.Columns.Append(col);


            table = new ADOX.Table();
            table.Name = "Friend";
            catalog.Tables.Append(table);

            col = newColumn("friend_username", ADOX.DataTypeEnum.adWChar, catalog);
            col.DefinedSize = 50;
            col.Attributes = ADOX.ColumnAttributesEnum.adColNullable;
            table.Columns.Append(col);

            table = new ADOX.Table();
            table.Name = "Group";
            catalog.Tables.Append(table);

            col = newColumn("group_id", ADOX.DataTypeEnum.adWChar, catalog);
            col.DefinedSize = 50;
            col.Attributes = ADOX.ColumnAttributesEnum.adColNullable;
            table.Columns.Append(col);

            table = new ADOX.Table();
            table.Name = "friend_request";
            catalog.Tables.Append(table);

            col = newColumn("sender", ADOX.DataTypeEnum.adWChar, catalog);
            col.DefinedSize = 50;
            col.Attributes = ADOX.ColumnAttributesEnum.adColNullable;
            table.Columns.Append(col);

            col = newColumn("receiver", ADOX.DataTypeEnum.adWChar, catalog);
            col.DefinedSize = 50;
            col.Attributes = ADOX.ColumnAttributesEnum.adColNullable;
            table.Columns.Append(col);

            col = newColumn("request_date", ADOX.DataTypeEnum.adDate, catalog);
            table.Columns.Append(col);

            return true;
        }
        public static OleDbConnection CreateOrOpenUserDataBase(string username)
        {
            ADOX.Catalog catalog = new ADOX.Catalog();

            OleDbConnectionStringBuilder builder = new OleDbConnectionStringBuilder();
            builder.DataSource = appDataPath + username + ".mdb";
            builder.Provider = "Microsoft.Jet.OLEDB.4.0";
            myCon = new OleDbConnection();
            dbSet = new DataSet();
            try
            {
                myCon.ConnectionString = builder.ConnectionString;
                myCon.Open();
            }
            catch (Exception)
            {
                catalog.Create(builder.ConnectionString);
                CreateUserDatabase(catalog);
                myCon.ConnectionString = builder.ConnectionString;
                myCon.Open();
            }
            adapterMessage = new OleDbDataAdapter("select * from Message", myCon);
            adapterMessage.Fill(dbSet, "Message");
            adapterMessage.InsertCommand = new OleDbCommand("insert into [Message]([sender], [receiver], [content], [group_message], [type], [message_date]) Values(@sender, @receiver, @content, @group_message, @type, @message_date);", myCon);
            adapterMessage.InsertCommand.Parameters.Add(new OleDbParameter("@sender", OleDbType.WChar, 50, "sender"));
            adapterMessage.InsertCommand.Parameters.Add(new OleDbParameter("@receiver", OleDbType.WChar, 50, "receiver"));
            OleDbParameter tempPar = new OleDbParameter("@content", OleDbType.LongVarWChar);
            tempPar.SourceColumn = "content";
            adapterMessage.InsertCommand.Parameters.Add(tempPar);
            tempPar = new OleDbParameter("@group_Message", OleDbType.Boolean);
            tempPar.SourceColumn = "group_Message";
            adapterMessage.InsertCommand.Parameters.Add(tempPar);
            adapterMessage.InsertCommand.Parameters.Add(new OleDbParameter("@type", OleDbType.WChar, 50, "type"));
            tempPar = new OleDbParameter("@message_date", OleDbType.Date);
            tempPar.SourceColumn = "message_date";
            adapterMessage.InsertCommand.Parameters.Add(tempPar);
            adapterMessage.AcceptChangesDuringUpdate = true;


            adapterFriendRequest = new OleDbDataAdapter("select * from Friend_Request", myCon);
            adapterFriendRequest.Fill(dbSet, "Friend_Request");
            adapterFriendRequest.InsertCommand = new OleDbCommand("insert into Friend_Request(sender, receiver, request_date) values(@sender, @receiver, @request_date); ", myCon);
            adapterFriendRequest.InsertCommand.Parameters.Add(new OleDbParameter("@sender", OleDbType.WChar, 50, "sender"));
            adapterFriendRequest.InsertCommand.Parameters.Add(new OleDbParameter("@receiver", OleDbType.WChar, 50, "receiver"));
            tempPar = new OleDbParameter("@request_date", OleDbType.Date);
            tempPar.SourceColumn = "request_date";
            adapterFriendRequest.InsertCommand.Parameters.Add(tempPar);
            adapterFriendRequest.AcceptChangesDuringUpdate = true;

            return myCon;
        }

        public static bool DownloadFriend_Request(string username)
        {
            FirebaseResponse res = client.Get(userPath + username + friendRequestPath);
            if (res.Body.Equals("null"))
            {
                return true;
            }
            else
            {
                List<Friend_Request> temp = JsonConvert.DeserializeObject<List<Friend_Request>>(res.Body);
                foreach (Friend_Request item in temp)
                {
                    DataRow row = dbSet.Tables["Friend_request"].NewRow();
                    row["sender"] = item.Sender;
                    row["receiver"] = item.Receiver;
                    row["request_date"] = item.Request_date;
                    dbSet.Tables["Friend_request"].Rows.Add(row);
                }
                adapterFriendRequest.Update(dbSet, "Friend_request");
                DeleteFriendRequest(username);
                return true;
            }
        }
        public static bool DownloadMessage(string username)
        {
            FirebaseResponse res = client.Get(userPath + username + messagePath);
            if (res.Body.Equals("null"))
            {
                return true;
            }
            else
            {
                List<Message> temp = JsonConvert.DeserializeObject<List<Message>>(res.Body);
                foreach (Message item in temp)
                {
                    if (item.Type == "Text")
                    {
                        DataRow row = dbSet.Tables["Message"].NewRow();
                        row["sender"] = item.Sender;
                        row["receiver"] = item.Receiver;
                        row["content"] = item.Content;
                        row["group_Message"] = item.GroupMessage;
                        row["type"] = item.Type;
                        row["message_date"] = item.Message_date;
                        dbSet.Tables["Message"].Rows.Add(row);
                    }
                    else
                    {
                        string extension = Path.GetExtension(item.Type);
                        switch (extension)
                        {
                            case ".jpeg":
                            case ".png":
                            case ".jpg":
                                SaveImage(item.Content, appDataImagePath + item.Type);
                                break;
                            default:
                                DeserializeFile(appDataImagePath + item.Type, item.Content);
                                break;
                        }
                        DataRow row = dbSet.Tables["Message"].NewRow();
                        row["sender"] = item.Sender;
                        row["receiver"] = item.Receiver;
                        row["content"] = appDataImagePath + item.Type;
                        row["group_Message"] = item.GroupMessage;
                        row["type"] = item.Type;
                        row["message_date"] = item.Message_date;
                        dbSet.Tables["Message"].Rows.Add(row);
                    }

                }
                adapterMessage.Update(dbSet, "Message");
                DeleteMessage(username);
                return true;
            }
        }

        public static List<Message> GetMessageOfUser(string other)
        {
            DataRow[] rows = dbSet.Tables["Message"].Select("(sender = '" + other + "' or receiver = '" + other + "') and group_message = 0");
            List<Message> ret = new List<Message>();
            foreach (DataRow r in rows)
            {
                Message message = new Message();
                message.Sender = r["sender"].ToString();
                message.Receiver = r["receiver"].ToString();
                message.Content = r["content"].ToString();
                message.Message_date = DateTime.Parse(r["message_date"].ToString());
                message.Type = r["type"].ToString();
                message.GroupMessage = (bool)r["group_message"];
                ret.Add(message);
            }
            return ret;
        }
        public static List<Message> GetMessageOfGroup(string groupID)
        {
            DataRow[] rows = dbSet.Tables["Message"].Select("receiver = '" + groupID + "' and group_message <> 0");
            List<Message> ret = new List<Message>();
            foreach (DataRow r in rows)
            {
                Message message = new Message();
                message.Sender = r["sender"].ToString();
                message.Receiver = r["receiver"].ToString();
                message.Content = r["content"].ToString();
                message.Message_date = DateTime.Parse(r["message_date"].ToString());
                message.Type = r["type"].ToString();
                message.GroupMessage = (bool)r["group_message"];
                ret.Add(message);
            }
            return ret;
        }



        private static ADOX.Column newColumn(string name, ADOX.DataTypeEnum type, ADOX.Catalog cat)
        {
            ADOX.Column temp = new ADOX.Column();
            temp.Name = name;
            temp.ParentCatalog = cat;
            temp.Type = type;
            return temp;
        }
        public static ImageFormat GetImFormat(this System.Drawing.Image img)
        {
            if (img.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Jpeg))
                return System.Drawing.Imaging.ImageFormat.Jpeg;
            if (img.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Bmp))
                return System.Drawing.Imaging.ImageFormat.Bmp;
            if (img.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Png))
                return System.Drawing.Imaging.ImageFormat.Png;
            if (img.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Emf))
                return System.Drawing.Imaging.ImageFormat.Emf;
            if (img.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Exif))
                return System.Drawing.Imaging.ImageFormat.Exif;
            if (img.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Gif))
                return System.Drawing.Imaging.ImageFormat.Gif;
            if (img.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Icon))
                return System.Drawing.Imaging.ImageFormat.Icon;
            if (img.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.MemoryBmp))
                return System.Drawing.Imaging.ImageFormat.MemoryBmp;
            if (img.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Tiff))
                return System.Drawing.Imaging.ImageFormat.Tiff;
            else
                return System.Drawing.Imaging.ImageFormat.Wmf;
        }

        public static Image CopyImage(string path)
        {
            using (Image im = Image.FromFile(path))
            {
                return new Bitmap(im);
            }
        }

    }
}

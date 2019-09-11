using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace yt_SerializationXML
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            ClearInput();
        }

        private void ClearInput()
        {
            tbUsername.Text = string.Empty;
            cmbSex.SelectedIndex = 0;
            nudAge.Value = 0;
        }

        private void SerializeXML(Users users)
        {
            XmlSerializer xml = new XmlSerializer(typeof(Users));

            using (FileStream fs = new FileStream("Users.xml", FileMode.OpenOrCreate))
            {
                xml.Serialize(fs, users);
            }
        }

        private Users DeserializeXML()
        {
            XmlSerializer xml = new XmlSerializer(typeof(Users));

            using (FileStream fs = new FileStream("Users.xml", FileMode.OpenOrCreate))
            {
                return (Users)xml.Deserialize(fs);
            }
        }

        private void Add(User user)
        {
            ListViewItem LVI = new ListViewItem(user.Username);
            LVI.Tag = user;

            lvUsers.Items.Add(LVI);
        }

        private void btnSerialize_Click(object sender, EventArgs e)
        {
            Users users = new Users();
            
            foreach(ListViewItem item in lvUsers.Items)
            {
                if(item.Tag != null)
                {
                    users.UsersList.Add((User)item.Tag);
                }
            }

            SerializeXML(users);
        }

        private void btnDeserialize_Click(object sender, EventArgs e)
        {
            ClearInput();

            Users users = DeserializeXML();

            foreach(User user in users.UsersList)
            {
                Add(user);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            User user = new User(tbUsername.Text, cmbSex.SelectedIndex, (int)nudAge.Value);

            Add(user);

            ClearInput();
        }

        private void lvUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(lvUsers.SelectedItems.Count == 1)
            {
                User user = (User)lvUsers.SelectedItems[0].Tag;

                if(user != null)
                {
                    tbUsername.Text = user.Username;
                    cmbSex.SelectedIndex = user.Sex;
                    nudAge.Value = user.Age;
                }
            }
            else if (lvUsers.SelectedItems.Count == 0)
            {
                ClearInput();
            }
        }
    }
}

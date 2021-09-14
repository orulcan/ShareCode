using Microsoft.Win32;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppTest
{
    public partial class Login : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
    {
        public Login()
        {
            InitializeComponent();
        } 
        private void button1_Click(object sender, EventArgs e)
        {

            
            

            
        }

        private void Login_Load(object sender, EventArgs e)
        {
            guna2TextBox1.Text = "";
            guna2TextBox1.PasswordChar = '*';
            guna2TextBox1.MaxLength = 14;
        }

        private async void guna2Button2_Click(object sender, EventArgs e)
        {
            //
            string pass = "";
            MongoClient dbClient0 = new MongoClient("mongodb+srv://orulcan:197523789o@cluster0.aksdq.mongodb.net/myFirstDatabase?retryWrites=true&w=majority");
            IMongoDatabase db0 = dbClient0.GetDatabase("ShareCode");
            var users = db0.GetCollection<BsonDocument>("Users");
            var filter = Builders<BsonDocument>.Filter.Eq("Username", guna2TextBox2.Text);
            var find = users.Find(filter).ToList();
            if (find.Count > 0)
            {
                pass = find[0]["Password"].RawValue.ToString();
                if (guna2TextBox1.Text == pass)
                {
                    Form1 frm = new Form1();
                    Login.ActiveForm.Hide();
                    frm.Show();
                }
                else
                {
                    //Hatali sifre
                    kullaniciYok.Text = "Hatalı şifre!";
                    kullaniciYok.Visible = true;
                    await Task.Delay(3000);
                    kullaniciYok.Visible = false;
                }
            }
            else
            {
                //Boyle bir kullanici yok
                kullaniciYok.Visible = true;
                await Task.Delay(3000);
                kullaniciYok.Visible = false;
            }
            
        }

        private void guna2TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                guna2Button2_Click(this, new EventArgs());
            }
        }
    }
}

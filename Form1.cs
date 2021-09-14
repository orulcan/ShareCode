using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinForms.Documents.Model.Code;

namespace AppTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2GradientTileButton1_Click(object sender, EventArgs e)
        {
            string name = "", title = "", date = "", code = "";
            MongoClient dbClient0 = new MongoClient("mongodb+srv://*****:******@cluster0.aksdq.mongodb.net/myFirstDatabase?retryWrites=true&w=majority");
            IMongoDatabase db0 = dbClient0.GetDatabase("ShareCode");
            var mySavedCodes = db0.GetCollection<BsonDocument>("MySavedCodes");
            var filter = Builders<BsonDocument>.Filter.Eq("Name", gunaLabel1.Text);
            var find = mySavedCodes.Find(filter).ToList();
            if (find.Count > 0)
            {
                foreach (var result in find)
                {
                    name = result["Name"].RawValue.ToString();
                    title = result["Title"].RawValue.ToString();
                    code = result["Code"].RawValue.ToString();
                    date = result["Date"].RawValue.ToString();
                }
                gunaLabel5.Text = name;
                gunaLabel6.Text = title;
                radRichTextEditor2.Text = code;
                gunaLabel8.Text = date;
            }
                

            addCodePanel.Visible = false;
            savedPanel.Visible = true;


        }

        private void gunaGradientTileButton1_Click(object sender, EventArgs e)
        {
            addCodePanel.Visible = true;
            savedPanel.Visible = false;
        }

        private async void guna2Button2_Click(object sender, EventArgs e)
        {
            bool check;
            guna2ProgressIndicator1.Visible = true;
            guna2ProgressIndicator1.AutoStart = true;
            MongoClient dbClient0 = new MongoClient("mongodb+srv://******:*******@cluster0.aksdq.mongodb.net/myFirstDatabase?retryWrites=true&w=majority");
            IMongoDatabase db0 = dbClient0.GetDatabase("ShareCode");
            if (guna2RadioButton1.Checked == true)
            {
                 check = true;
            }
            else
            {
                check = false;
            }
            var mySavedCodes = db0.GetCollection<BsonDocument>("MySavedCodes");
            var insertCode = new BsonDocument
            {
                ["Name"] = gunaLabel1.Text,
                ["Title"] = gunaTextBox1.Text,
                ["Code"] = radRichTextEditor1.Text,
                ["Private"] = check,
                ["Date"] = DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss"),
            };
            var insertDatabase = mySavedCodes.InsertOneAsync(insertCode);
            await Task.Delay(2000);
            guna2ProgressIndicator1.Visible = false;
            guna2ProgressIndicator1.AutoStart = false;
            gunaLabel4.Visible = true;
            await Task.Delay(2000);
            gunaLabel4.Visible = false;
        }
    }
}

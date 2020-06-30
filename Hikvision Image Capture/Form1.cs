using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public string Option = "Option1";
        public Form1()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Option = "cameraUrl1";
            requestFrame();
            
        }
        private Bitmap loadedBitmap;

private void requestFrame()
        {
            if (Option == "cameraUrl1")
            {
                Option = @"http://" + textBox1.Text + @":80" + @"/Streaming/channels/1/picture?snapShotImageType=JPEG";
            }
            else if (Option =="cameraUrl2")
            {
                Option = @"http://" + textBox1.Text + @"/ISAPI/Streaming/channels/1/picture";
            }
            string cameraUrl1 = @"http://" + textBox1.Text + @":80" + @"/Streaming/channels/1/picture?snapShotImageType=JPEG";
            string cameraUrl2 = @"http://" + textBox1.Text + @"/ISAPI/Streaming/channels/1/picture";
            var request = System.Net.HttpWebRequest.Create(Option);
            request.Credentials = new NetworkCredential(textBox2.Text, textBox3.Text);
            request.Proxy = null;
            request.BeginGetResponse(new AsyncCallback(finishRequestFrame), request);
        }

        void finishRequestFrame(IAsyncResult result)
        {
            HttpWebResponse response = (result.AsyncState as HttpWebRequest).EndGetResponse(result) as HttpWebResponse;
            Stream responseStream = response.GetResponseStream();

            using (Bitmap frame = new Bitmap(responseStream))
            {
                if (frame != null)
                {
                    
                    loadedBitmap = (Bitmap)frame.Clone();
                    loadedBitmap.Save(@"C:\TEST.JPEG");
                    MessageBox.Show(@"exported to C:\TEST.JPEG");
                    pictureBox1.Image = loadedBitmap;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Option = "cameraUrl2";
            requestFrame();
        }
        //requestFrame(); // call the function
    }
}

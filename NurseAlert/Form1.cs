using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Accord.Video.FFMPEG;

/*
 * 
 *
 * 
 *
 * 
 * 
 */

namespace NurseAlert
{
    public partial class Form1 : Form
    {
        const string subscriptionKey = "f93b867182134d6e968f2fdc61e1fd06";
        const string uriBase =
            "https://westus.api.cognitive.microsoft.com/face/v1.0/detect";
        bool safe = true;


        public Form1()
        {
            //Start
            InitializeComponent();
            
        }

        private async void btnEnter_Click(object sender, EventArgs e)
        {
            ResetAlertBox();
            txtOutput.Text = "Detect faces: ";
            //string imageFilePath = "./a.jpg";
            string imageFilePath = txtRef.Text;

            if (File.Exists(imageFilePath))
            {
                // Execute the REST API call.
                try
                {
                    string fileExtension = Path.GetExtension(imageFilePath);
                    if (fileExtension == ".mp4")
                    {
                        btnEnter.Enabled = false;
                        int i = 0;
                        int frameNum = 0;
                        VideoFileReader reader = new VideoFileReader();
                        reader.Open(imageFilePath);
                        while (safe)
                        {
                            Bitmap videoFrame = reader.ReadVideoFrame(frameNum);
                            if (File.Exists("./input" + i + ".jpg"))
                            {
                                File.Delete("./input" + i + ".jpg");
                            }
                            videoFrame.Save("./input" + i + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                            await MakeAnalysisRequest("./input" + i + ".jpg");
                            i++;
                            frameNum += (/*5 * */(int)reader.FrameRate);
                            if (frameNum > reader.FrameCount) safe = false;
                            System.Threading.Thread.Sleep(1000);
                        }
                        reader.Close();
                        btnEnter.Enabled = true;
                    }
                    else if (fileExtension == ".jpg")
                    {
                        await MakeAnalysisRequest(imageFilePath);
                    }
                }
                catch (Exception ex)
                {
                    txtOutput.AppendText("\n" + ex.Message);
                }
            }
            else
            {
                txtOutput.AppendText("\nInvalid file path.\n");
            }
        }

        async Task MakeAnalysisRequest(string imageFilePath)
        {
            picFrame.Image = Image.FromFile(imageFilePath);
            HttpClient client = new HttpClient();

            // Request headers.
            client.DefaultRequestHeaders.Add(
                "Ocp-Apim-Subscription-Key", subscriptionKey);

            // Request parameters. A third optional parameter is "details".
            string requestParameters = "returnFaceId=true&returnFaceLandmarks=false" +
                "&returnFaceAttributes=age,gender,headPose,smile,facialHair,glasses," +
                "emotion,hair,makeup,occlusion,accessories,blur,exposure,noise";

            // Assemble the URI for the REST API Call.
            string uri = uriBase + "?" + requestParameters;

            HttpResponseMessage response;

            // Request body. Posts a locally stored JPEG image.
            byte[] byteData = GetImageAsByteArray(imageFilePath);

            using (ByteArrayContent content = new ByteArrayContent(byteData))
            {
                // This example uses content type "application/octet-stream".
                // The other content types you can use are "application/json"
                // and "multipart/form-data".
                content.Headers.ContentType =
                    new MediaTypeHeaderValue("application/octet-stream");

                // Execute the REST API call.
                response = await client.PostAsync(uri, content);

                // Get the JSON response.
                string contentString = await response.Content.ReadAsStringAsync();
                //txtOutput.AppendText(JsonPrettyPrint(contentString));
                float[] emotions = JsonParseEmotion(contentString, imageFilePath);

                txtOutput.AppendText("\n");
                txtOutput.AppendText("anger: "    + emotions[0] + Environment.NewLine);
                txtOutput.AppendText("contempt: " + emotions[1] + Environment.NewLine);
                txtOutput.AppendText("disgust: "  + emotions[2] + Environment.NewLine);
                txtOutput.AppendText("fear: "     + emotions[3] + Environment.NewLine);
                txtOutput.AppendText("happiness: "+ emotions[4] + Environment.NewLine);
                txtOutput.AppendText("neutral: "  + emotions[5] + Environment.NewLine);
                txtOutput.AppendText("sadness: "  + emotions[6] + Environment.NewLine);
                txtOutput.AppendText("surprise: " + emotions[7] + Environment.NewLine);
                txtOutput.AppendText("\n");

                bool validEmotions = false;
                for(int i = 0; i < emotions.Length; i++)
                {
                    if (emotions[i] != 0)
                    {
                        validEmotions = true;
                        i = emotions.Length;
                    }
                }
                if(validEmotions == true)
                {

                    if ((emotions[0] > .2) && (emotions[2] > 0) && (emotions[6] > .5))
                    {
                        Alert();
                    }
                    else
                    {
                        Safe();
                    }
                }
            }
        }

        void ResetAlertBox()
        {
            txtAlert.Text = "";
            txtAlert.BackColor = Color.White;
            safe = true;
        }

        void Alert()
        {
            txtAlert.Text = "ALERT";
            txtAlert.BackColor = Color.Red;
            safe = false;
        }
        void Safe()
        {
            txtAlert.Text = "SAFE";
            txtAlert.BackColor = Color.Green;
        }

        static byte[] GetImageAsByteArray(string imageFilePath)
        {
            using (FileStream fileStream =
                new FileStream(imageFilePath, FileMode.Open, FileAccess.Read))
            {
                BinaryReader binaryReader = new BinaryReader(fileStream);
                return binaryReader.ReadBytes((int)fileStream.Length);
            }
        }
        //Get emotion data
        float[] JsonParseEmotion(string jsonStr, string imageFilePath)
        {
            float[] emotions = { 0, 0, 0, 0, 0, 0, 0, 0 };
            try
            {
                JArray jArr = JArray.Parse(jsonStr);
                JObject jObj = JObject.Parse(jArr[0].ToString());

                emotions[0] = (float)jObj.SelectToken("faceAttributes.emotion.anger");
                emotions[1] = (float)jObj.SelectToken("faceAttributes.emotion.contempt");
                emotions[2] = (float)jObj.SelectToken("faceAttributes.emotion.disgust");
                emotions[3] = (float)jObj.SelectToken("faceAttributes.emotion.fear");
                emotions[4] = (float)jObj.SelectToken("faceAttributes.emotion.happiness");
                emotions[5] = (float)jObj.SelectToken("faceAttributes.emotion.neutral");
                emotions[6] = (float)jObj.SelectToken("faceAttributes.emotion.sadness");
                emotions[7] = (float)jObj.SelectToken("faceAttributes.emotion.surprise");
            }
            catch (Exception ex)
            {
                txtOutput.AppendText("\nError: " + ex);
                txtOutput.AppendText("\nError: Face not detected or subscription key may be wrong or expired \n");
                txtOutput.AppendText("File: " + imageFilePath);
                return emotions;
            }
            return emotions;
        }
        static string JsonPrettyPrint(string json)
        {
            if (string.IsNullOrEmpty(json))
                return string.Empty;

            json = json.Replace(Environment.NewLine, "").Replace("\t", "");

            StringBuilder sb = new StringBuilder();
            bool quote = false;
            bool ignore = false;
            int offset = 0;
            int indentLength = 3;

            foreach (char ch in json)
            {
                switch (ch)
                {
                    case '"':
                        if (!ignore) quote = !quote;
                        break;
                    case '\'':
                        if (quote) ignore = !ignore;
                        break;
                }

                if (quote)
                    sb.Append(ch);
                else
                {
                    switch (ch)
                    {
                        case '{':
                        case '[':
                            sb.Append(ch);
                            sb.Append(Environment.NewLine);
                            sb.Append(new string(' ', ++offset * indentLength));
                            break;
                        case '}':
                        case ']':
                            sb.Append(Environment.NewLine);
                            sb.Append(new string(' ', --offset * indentLength));
                            sb.Append(ch);
                            break;
                        case ',':
                            sb.Append(ch);
                            sb.Append(Environment.NewLine);
                            sb.Append(new string(' ', offset * indentLength));
                            break;
                        case ':':
                            sb.Append(ch);
                            sb.Append(' ');
                            break;
                        default:
                            if (ch != ' ') sb.Append(ch);
                            break;
                    }
                }
            }

            return sb.ToString().Trim();
        }
    }
}

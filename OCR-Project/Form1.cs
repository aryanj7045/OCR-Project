using Azure;
using Azure.AI.FormRecognizer;
using Azure.AI.FormRecognizer.DocumentAnalysis;

namespace OCR_Project
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        OpenFileDialog ofd = new OpenFileDialog();

        private void button1_Click(object sender, EventArgs e)
        {
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.ImageLocation = ofd.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string endpoint = "https://ocrmagicbus.cognitiveservices.azure.com/";
            string key = "d67e62468eea497bab4ed0824e4dd5a2";

            AzureKeyCredential credential = new AzureKeyCredential(key);
            DocumentAnalysisClient client = new DocumentAnalysisClient(new Uri(endpoint), credential);

            Stream filestream = File.OpenRead(ofd.FileName);

            AnalyzeDocumentOperation operation = client.AnalyzeDocument(WaitUntil.Completed, "prebuilt-read", filestream);

            AnalyzeResult result = operation.Value;

            foreach (var page in result.Pages)
            {
                foreach (var line in page.Lines)
                {
                    richTextBox1.AppendText(line.Content);
                    richTextBox1.AppendText("\n");
                }
            }
        }
    }
}
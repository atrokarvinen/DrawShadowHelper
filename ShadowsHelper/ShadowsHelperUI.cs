using ImageProcessing;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShadowsHelper
{
    public partial class ShadowsHelperUI : Form
    {
        private int _ScreenWidth;
        private int _ScreenHeight;
        private Bitmap _OriginalImage;
        private readonly ImageProcessor _ImageProcessor;

        public ShadowsHelperUI()
        {
            InitializeComponent();

            Rectangle resolution = Screen.PrimaryScreen.Bounds;
            //Rectangle resolution = new Rectangle(0, 0, 1000, 1000);
            ClientSize = new Size(resolution.Width - 100, resolution.Height - 100);

            _ImageProcessor = new ImageProcessor();

            _OriginalImage = (Bitmap)Image.FromFile(@"Samples\TestOriginal.jpg");
            OriginalPB.Image = _OriginalImage;
        }

        private void InitializeMainScreen()
        {
            _ScreenWidth = ClientSize.Width;
            _ScreenHeight = ClientSize.Height;

            // OriginalPB
            OriginalPB.Location = Prcnt2PxLoc(2, 3);
            OriginalPB.Size = Prcnt2PxSize(47, 80);
            OriginalPB.BorderStyle = BorderStyle.FixedSingle;
            OriginalPB.SizeMode = PictureBoxSizeMode.StretchImage;

            // ResultPB
            ResultPB.Location = Prcnt2PxLoc(51, 3);
            ResultPB.Size = Prcnt2PxSize(47, 80);
            ResultPB.BorderStyle = BorderStyle.FixedSingle;
            ResultPB.SizeMode = PictureBoxSizeMode.StretchImage;

            // OriginalImageLabel
            OriginalImageLabel.Location = Prcnt2PxLoc(2, 1);
            OriginalImageLabel.Size = new Size(97, 13);
            OriginalImageLabel.Text = "OriginalImageLabel";

            // ResultImageLabel
            ResultImageLabel.Location = Prcnt2PxLoc(51, 1);
            ResultImageLabel.Size = new Size(92, 13);
            ResultImageLabel.Text = "ResultImageLabel";

            // LoadImageButton
            LoadImageButton.Location = Prcnt2PxLoc(2, 85);
            LoadImageButton.Size = Prcnt2PxSize(5, 5);
            LoadImageButton.Text = "Load image";

            // ComputeButton
            ComputeButton.Location = Prcnt2PxLoc(10, 85);
            ComputeButton.Size = Prcnt2PxSize(5, 5);
            ComputeButton.Text = "Compute";

            // SaveImageButton
            SaveImageButton.Location = Prcnt2PxLoc(18, 85);
            SaveImageButton.Size = Prcnt2PxSize(5, 5);
            SaveImageButton.Text = "Save image";

            // ShadowLevelsLabel
            ShadowLevelsLabel.Location = Prcnt2PxLoc(26, 87);
            ShadowLevelsLabel.Size = new Size(76, 20);
            ShadowLevelsLabel.Text = "Shadow levels";

            // ShadowLevelsInput
            ShadowLevelsInput.Location = Prcnt2PxLoc(31, 87);
            ShadowLevelsInput.Size = new Size(100, 20);
            ShadowLevelsInput.Text = "5";

            // ProgressBarLabel
            ProgressBarLabel.Location = Prcnt2PxLoc(48, 90);
            ProgressBarLabel.Size = new Size(90, 13);
            ProgressBarLabel.Text = "Processing progress";

            // ProgressBar
            ProgressBar.Location = Prcnt2PxLoc(2, 92);
            ProgressBar.Size = Prcnt2PxSize(96, 2);
        }

        private Point Prcnt2PxLoc(double percentWidth, double percentHeight)
        {
            int width = (int)Math.Round(_ScreenWidth * percentWidth / 100.0);
            int height = (int)Math.Round(_ScreenHeight * percentHeight / 100.0);
            return new Point(width, height);
        }

        private Size Prcnt2PxSize(double percentWidth, double percentHeight)
        {
            Point p = Prcnt2PxLoc(percentWidth, percentHeight);
            return new Size(p.X, p.Y);
        }

        private void ShadowsHelperUI_Resize(object sender, EventArgs e)
        {
            InitializeMainScreen();
        }

        private async void ComputeButton_Click(object sender, EventArgs e)
        {
            Progress<ProgressResult> progressIndicator = new Progress<ProgressResult>(ReportProgress);

            ProcessingArgs args = new ProcessingArgs()
            {
                Image = _OriginalImage,
                ShadowLevels = int.Parse(ShadowLevelsInput.Text),
            };
            Task<Result> processingTask = _ImageProcessor.ProcessAsync(args, progressIndicator);

            Result processingResult = await processingTask;
            PictureBoxSetImage(OriginalPB, processingResult.GrayImage);
            PictureBoxSetImage(ResultPB, processingResult.ResultImage);
        }

        void ReportProgress(ProgressResult value)
        {
            ProgressBar.Value = value.ProgressCount;
            ProgressBarLabel.Text = $"Processing progress: {value.ProgressCount}%";
        }

        private void PictureBoxSetImage(PictureBox pb, Bitmap image)
        {
            if (pb.Image != null && pb.Image != _OriginalImage)
                pb.Image.Dispose();

            pb.Image = image;
        }

        private void LoadImageButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "C:\\";
                openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.tiff, *.png, *.bmp) | *.jpg; *.jpeg; *.tiff; *.png; *.bmp";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    string filePath = openFileDialog.FileName;

                    _OriginalImage = (Bitmap)Image.FromFile(filePath);
                    PictureBoxSetImage(OriginalPB, _OriginalImage);
                }
            }
        }
    }
}


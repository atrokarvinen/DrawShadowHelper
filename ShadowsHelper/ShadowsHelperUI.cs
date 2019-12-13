using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShadowsHelper
{
    public partial class ShadowsHelperUI : Form
    {
        private int _ScreenWidth;
        private int _ScreenHeight;

        public ShadowsHelperUI()
        {
            InitializeComponent();

            Rectangle resolution = Screen.PrimaryScreen.Bounds;
            //Rectangle resolution = new Rectangle(0, 0, 1000, 1000);
            ClientSize = new Size(resolution.Width - 100, resolution.Height - 100);
        }

        private void InitializeMainScreen()
        {
            _ScreenWidth = ClientSize.Width;
            _ScreenHeight = ClientSize.Height;

            // OriginalPB
            OriginalPB.Location = Prcnt2PxLoc(2, 2);
            OriginalPB.Size = Prcnt2PxSize(45, 80);
            OriginalPB.BorderStyle = BorderStyle.FixedSingle;

            // ResultPB
            ResultPB.Location = Prcnt2PxLoc(50, 2);
            ResultPB.Size = Prcnt2PxSize(45, 80);
            ResultPB.BorderStyle = BorderStyle.FixedSingle;

            // OriginalImageLabel
            OriginalImageLabel.Location = Prcnt2PxLoc(2, 0);
            OriginalImageLabel.Size = new Size(97, 13);
            OriginalImageLabel.Text = "OriginalImageLabel";

            // ResultImageLabel
            ResultImageLabel.Location = Prcnt2PxLoc(50, 0);
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
            ShadowLevelsLabel.Location = Prcnt2PxLoc(26, 85);
            ShadowLevelsLabel.Size = new Size(76, 13);
            ShadowLevelsLabel.Text = "Shadow levels";

            // ShadowLevelsInput
            ShadowLevelsInput.Location = Prcnt2PxLoc(31, 85);
            ShadowLevelsInput.Size = new Size(100, 20);
            ShadowLevelsInput.Text = "5";
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
    }
}


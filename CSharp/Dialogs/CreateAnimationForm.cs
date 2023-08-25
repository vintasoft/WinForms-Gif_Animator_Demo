using System;
using System.Drawing;
using System.Windows.Forms;

using Vintasoft.Imaging;
using Vintasoft.Imaging.ImageProcessing;
using Vintasoft.Imaging.ImageProcessing.Color;
using Vintasoft.Imaging.ImageProcessing.Effects;


namespace GifAnimatorDemo
{
    /// <summary>
    /// A form that allows to create a simple animation and insert animation in GIF file.
    /// </summary>
    public partial class CreateAnimationForm : Form
    {

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateAnimationForm"/> class.
        /// </summary>
        public CreateAnimationForm()
        {
            InitializeComponent();

            // init color blending modes
            colorBlendingComboBox.Items.Add(BlendingMode.Brightness);
            colorBlendingComboBox.Items.Add(BlendingMode.Color);
            colorBlendingComboBox.Items.Add(BlendingMode.ColorBurn);
            colorBlendingComboBox.Items.Add(BlendingMode.ColorDodge);
            colorBlendingComboBox.Items.Add(BlendingMode.Contrast);
            colorBlendingComboBox.Items.Add(BlendingMode.Darken);
            colorBlendingComboBox.Items.Add(BlendingMode.Difference);
            colorBlendingComboBox.Items.Add(BlendingMode.Exclusion);
            colorBlendingComboBox.Items.Add(BlendingMode.Gamma);
            colorBlendingComboBox.Items.Add(BlendingMode.HardLight);
            colorBlendingComboBox.Items.Add(BlendingMode.Hue);
            colorBlendingComboBox.Items.Add(BlendingMode.Lighten);
            colorBlendingComboBox.Items.Add(BlendingMode.Luminosity);
            colorBlendingComboBox.Items.Add(BlendingMode.Multiply);
            colorBlendingComboBox.Items.Add(BlendingMode.Overlay);
            colorBlendingComboBox.Items.Add(BlendingMode.Saturation);
            colorBlendingComboBox.Items.Add(BlendingMode.Screen);
            colorBlendingComboBox.Items.Add(BlendingMode.SoftLight);
            colorBlendingComboBox.SelectedItem = BlendingMode.Difference;
        }

        #endregion



        #region Properties

        VintasoftImage _baseImage;
        /// <summary>
        /// Gets or sets an animation base image.
        /// </summary>
        public VintasoftImage BaseImage
        {
            get
            {
                return _baseImage;
            }
            set
            {
                _baseImage = value;
                animatedImageViewer1.Image = _baseImage;
            }
        }

        /// <summary>
        /// Gets a default delay (in 1/100 sec).
        /// </summary>
        public ushort Delay
        {
            get
            {
                return (ushort)delayNumericUpDown.Value;
            }
        }


        #endregion



        #region Methods

        #region UI

        /// <summary>
        /// Handles the Click event of AnimationStartButton object.
        /// </summary>
        private void animationStartButton_Click(object sender, EventArgs e)
        {
            UpdateDelay();

            if (animatedImageViewer1.Image != BaseImage)
            {
                animatedImageViewer1.Images.ClearAndDisposeItems();
                animatedImageViewer1.Image = BaseImage;
            }

            animatedImageViewer1.Images.Clear();
            animatedImageViewer1.Images.AddRange(GetAnimationImages().ToArray());

            // specify that animation must be started
            animatedImageViewer1.Animation = true;

            // upate the UI
            UpdateUI();
        }

        /// <summary>
        /// Handles the Click event of AnimationStopButton object.
        /// </summary>
        private void animationStopButton_Click(object sender, EventArgs e)
        {
            StopAnimation();
        }

        /// <summary>
        /// Handles the ValueChanged event of DelayNumericUpDown object.
        /// </summary>
        private void delayNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            UpdateDelay();
        }

        /// <summary>
        /// Handles the FormClosing event of CreateAnimationDialog object.
        /// </summary>
        private void CreateAnimationDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopAnimation();
        }

        /// <summary>
        /// Handles the Click event of ButtonOk object.
        /// </summary>
        private void buttonOk_Click(object sender, EventArgs e)
        {
            StopAnimation();
            DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// Handles the Click event of ButtonCancel object.
        /// </summary>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            StopAnimation();
            DialogResult = DialogResult.Cancel;
        }

        #endregion


        /// <summary>
        /// Updates the user interface of this form.
        /// </summary>
        private void UpdateUI()
        {
            // tab control
            methodsTabControl.Enabled = !animatedImageViewer1.Animation;

            // controls panel
            animationControlsPanel.Enabled = !animatedImageViewer1.Animation;

            // buttons
            animationStartButton.Enabled = !animatedImageViewer1.Animation;
            animationStopButton.Enabled = animatedImageViewer1.Animation;
            animationStartButton.Visible = !animatedImageViewer1.Animation;
            animationStopButton.Visible = animatedImageViewer1.Animation;
        }

        /// <summary>
        /// Returns the animation images depending on creation method.
        /// </summary>
        /// <returns><see cref="ImageCollection"/> with images of animation.</returns>
        /// <exception cref="NotImplementedException">Thrown if animation creation method is not inmlemented.</exception>
        public ImageCollection GetAnimationImages()
        {
            ImageCollection frames = new ImageCollection();

            // if pixelate method is selected
            if (methodsTabControl.SelectedTab == pixelateTabPage)
            {
                int pixelCellSize = 2;
                int delta = pixelateDeltaTrackBar.Value;
                frames.Add((VintasoftImage)BaseImage.Clone());
                int framesCount = (int)framesNumericUpDown.Value;

                for (int i = 0; i < framesCount; i++)
                {
                    // create and apply pixelate command
                    ChangePixelFormatCommand convertToBgra32 = new ChangePixelFormatCommand(PixelFormat.Bgra32);
                    VintasoftImage currentImage = convertToBgra32.Execute(BaseImage);
                    PixelateCommand pixelate = new PixelateCommand(pixelCellSize);
                    pixelate.ExecuteInPlace(currentImage);

                    // add image to collection
                    frames.Add(currentImage);

                    // increase pixel cell size
                    pixelCellSize += delta;
                }
            }
            // if color blending mode is selected
            else if (methodsTabControl.SelectedTab == colorBlendingTabPage)
            {
                Color color1 = firstColorPanelControl.Color;
                int r1 = color1.R;
                int g1 = color1.G;
                int b1 = color1.B;

                Color color2 = secondColorPanelControl.Color;
                int r2 = color2.R;
                int g2 = color2.G;
                int b2 = color2.B;

                // find the delta values depending on steps count
                int steps = (int)framesNumericUpDown.Value;
                double dr = (r2 - r1) / steps;
                double dg = (g2 - g1) / steps;
                double db = (b2 - b1) / steps;

                double r = r1;
                double g = g1;
                double b = b1;

                // create color blending command
                ColorBlendCommand command = new ColorBlendCommand((BlendingMode)colorBlendingComboBox.SelectedItem, color1);
                for (int i = 0; i < steps; i++)
                {
                    // set command color
                    command.BlendColor = Color.FromArgb(
                        (int)Math.Round(r),
                        (int)Math.Round(g),
                        (int)Math.Round(b));

                    // add processed image to collection
                    frames.Add(command.Execute(BaseImage));

                    // update values before creating the next page
                    r += dr;
                    g += dg;
                    b += db;
                }
            }
            else
            {
                throw new NotImplementedException();
            }

            if (reverseCheckBox.Checked)
            {
                VintasoftImage[] images = frames.ToArray();
                Array.Reverse(images);
                frames.Clear();
                frames.AddRange(images);
            }

            return frames;
        }

        /// <summary>
        /// Stops the animation.
        /// </summary>
        private void StopAnimation()
        {
            if (animationStopButton.Visible)
            {
                animatedImageViewer1.Animation = false;
                animatedImageViewer1.Images.ClearAndDisposeItems();
                animatedImageViewer1.Image = BaseImage;
            }

            // upate the UI
            UpdateUI();
        }

        /// <summary>
        /// Updates the default delay.
        /// </summary>
        private void UpdateDelay()
        {
            animatedImageViewer1.DefaultDelay = (int)delayNumericUpDown.Value * 10;
        }

        #endregion

    }
}

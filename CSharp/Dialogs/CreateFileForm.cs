using System;
using System.Windows.Forms;

using Vintasoft.Imaging;
using Vintasoft.Imaging.Codecs.ImageFiles.Gif;

using DemosCommonCode.Imaging.Codecs;

namespace GifAnimatorDemo
{
    /// <summary>
    /// A form that allows to specify options for creating a new GIF file.
    /// </summary>
    public partial class CreateFileForm : Form
    {

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateFileForm"/> class.
        /// </summary>
        public CreateFileForm()
        {
            InitializeComponent();
            openImageFileDialog.Multiselect = true;

            CodecsFileFilters.SetOpenFileDialogFilter(openImageFileDialog);
        }

        #endregion



        #region Properties

        GifFile _newFile;
        /// <summary>
        /// Gets a new GIF file.
        /// </summary>
        public GifFile NewFile
        {
            get
            {
                return _newFile;
            }
        }

        /// <summary>
        /// Gets a value indicating whether new pages must be added to the new GIF file.
        /// </summary>
        public bool AddPages
        {
            get
            {
                return specifiedSizeRadioButton.Checked && addPagesToNewFileCheckBox.Checked;
            }
        }

        #endregion



        #region Methods

        #region UI

        /// <summary>
        /// Handles the CheckedChanged event of fromImageRadioButton object.
        /// </summary>
        private void fromImageRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            sizeGroupBox.Enabled = !fromImagesRadioButton.Checked;
        }

        /// <summary>
        /// Handles the Click event of buttonOk object.
        /// </summary>
        private void buttonOk_Click(object sender, EventArgs e)
        {
            // if GIF file must be created from images
            if (fromImagesRadioButton.Checked)
            {
                try
                {
                    // show open file dialog
                    if (openImageFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        ImageCollection images = null;
                        int[] delays = null;
                        // load images from selected image file
                        MainForm.LoadImages(openImageFileDialog.FileNames, ref images, ref delays);
                        // if image file does not have images
                        if (images.Count == 0)
                            DialogResult = DialogResult.Cancel;

                        try
                        {
                            // compute the maximum image width and height

                            int maxWidth = images[0].Width;
                            int maxHeight = images[0].Height;
                            for (int i = 1; i < images.Count; i++)
                            {
                                VintasoftImage image = images[i];
                                if (maxWidth < image.Width)
                                    maxWidth = image.Width;
                                if (maxHeight < image.Height)
                                    maxHeight = image.Height;
                            }

                            // create GIF file
                            _newFile = new GifFile(maxWidth, maxHeight);

                            // create a dialog that allows to set the page settings
                            using (AddPagesForm addPagesDlg = new AddPagesForm())
                            {
                                addPagesDlg.InsertIndexVisible = false;
                                if (addPagesDlg.ShowDialog() == DialogResult.OK)
                                {
                                    // set GIF page settings

                                    _newFile.CreatePageFromImageMethod = addPagesDlg.CreatePagesMethod;
                                    _newFile.NewPageAlign = addPagesDlg.NewPagesAlign;
                                }
                            }

                            if (images.Count > 0)
                                // add images to GIF file
                                _newFile.Pages.Insert(0, images);

                            for (int i = 0; i < delays.Length; i++)
                                // set GIF page delay
                                _newFile.Pages[i].Delay = delays[i];
                        }
                        finally
                        {
                            images.ClearAndDisposeItems();
                        }

                        DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        DialogResult = DialogResult.Cancel;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    DialogResult = DialogResult.Cancel;
                }
            }
            // if empty GIF file must be created
            else
            {
                ushort width = (ushort)widthNumericUpDown.Value;
                ushort height = (ushort)heightNumericUpDown.Value;

                // create empty GIF file
                _newFile = new GifFile(width, height);

                DialogResult = DialogResult.OK;
            }
        }

        /// <summary>
        /// Handles the Click event of buttonCancel object.
        /// </summary>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            _newFile = null;
            DialogResult = DialogResult.Cancel;
        }

        #endregion

        #endregion

    }
}

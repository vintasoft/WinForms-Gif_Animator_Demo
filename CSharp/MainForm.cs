using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

using Vintasoft.Imaging;
using Vintasoft.Imaging.Codecs.ImageFiles.Gif;
using Vintasoft.Imaging.ImageProcessing;
using Vintasoft.Imaging.ImageProcessing.Color;
using Vintasoft.Imaging.UI;

using DemosCommonCode;
using DemosCommonCode.Imaging;
using DemosCommonCode.Imaging.Codecs;

namespace GifAnimatorDemo
{
    /// <summary>
    /// The main form of GIF Animator Demo.
    /// </summary>
    public partial class MainForm : Form
    {

        #region Enums

        /// <summary>
        /// Defines available palette changing methods.
        /// </summary>
        enum ChangePaletteMethod
        {
            /// <summary>
            /// Inverts the colors.
            /// </summary>
            Invert,
            /// <summary>
            /// Convert colors to grayscale.
            /// </summary>
            ConvertToGray
        };

        #endregion



        #region Fields

        /// <summary>
        /// Application's title.
        /// </summary>
        string _mainFormTitle = string.Format("VintaSoft GIF Animator Demo v{0}", ImagingGlobalSettings.ProductVersion);

        /// <summary>
        /// Current GIF file.
        /// </summary>
        GifFile _gifFile;

        /// <summary>
        /// A form that allows to create a new GIF file.
        /// </summary>
        CreateFileForm _createGifFileDialog = new CreateFileForm();

        /// <summary>
        /// A form that allows to choose a color.
        /// </summary>
        ColorDialog _effectsColorDialog = new ColorDialog();

        /// <summary>
        /// A form that allows to add a page to a GIF file.
        /// </summary>
        AddPagesForm _addPagesDialog = new AddPagesForm();

        /// <summary>
        /// A form that allows to edit a palette.
        /// </summary>
        PaletteForm _paletteDialog = new PaletteForm();

        /// <summary>
        /// A form that allows to create an animation.
        /// </summary>
        CreateAnimationForm _createAnimationDialog = new CreateAnimationForm();

        /// <summary>
        /// A value indicating whether image collection changes handling is enabled.
        /// </summary>
        bool _isImageCollectionChangingHandlingEnabled = false;

        /// <summary>
        /// HTML page template for animation preview in a browser.
        /// </summary>
        string _htmlPageTemplate =
            "<html>\n<head><title>{0}</title></head>\n<body bgcolor=\"#{1}\">\n<img src=\"file://{2}\" width=\"{3}\" height=\"{4}\">\n</body>\n</html>";

        /// <summary>
        /// The name of the temporary GIF file to be previewed in a browser.
        /// </summary>
        string _tempGifFilename = "vsGifPreview.gif";

        /// <summary>
        /// The name of the temporary HTML file to be previewed in a browser.
        /// </summary>
        string _tempHtmlFilename = "vsGifPreview.html";

        /// <summary>
        /// A value indicating whether page parameters is updating.
        /// </summary>
        bool _isPageParamsUpdating = false;

        #endregion



        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        public MainForm()
        {
            // register the evaluation license for VintaSoft Imaging .NET SDK
            Vintasoft.Imaging.ImagingGlobalSettings.Register("REG_USER", "REG_EMAIL", "EXPIRATION_DATE", "REG_CODE");

            InitializeComponent();

            CloseFile();

            pageImageViewer.ZoomChanged += new EventHandler<ZoomChangedEventArgs>(pageImageViewer_ZoomChanged);

            // set filters in open dialog
            CodecsFileFilters.SetOpenFileDialogFilter(openImageFileDialog);

            animationDefaultDelayNumericUpDown.Value = 10;
            animatedImageViewer.AnimationFinished += new EventHandler(animatedImageViewer_AnimationFinished);

            UpdatePageAndFrame(null);

            _tempGifFilename = Path.Combine(Path.GetTempPath(), _tempGifFilename);
            _tempHtmlFilename = Path.Combine(Path.GetTempPath(), _tempHtmlFilename);

            pagesThumbnailViewer.Images.ImageCollectionChanged += new EventHandler<ImageCollectionChangeEventArgs>(pagesThumbnailViewer_ImageCollectionChanged);

            // set the initial directory in open file dialog
            DemosTools.SetTestFilesFolder(openGifFileDialog);
            DemosTools.SetTestFilesFolder(openImageFileDialog);
        }

        #endregion



        #region Properties

        /// <summary>
        /// Gets the focused GIF page.
        /// </summary>
        internal GifPage FocusedPage
        {
            get
            {
                if (pagesThumbnailViewer.FocusedIndex < 0)
                    return null;

                return (GifPage)pagesThumbnailViewer.Images[pagesThumbnailViewer.FocusedIndex].Tag;
            }
        }

        /// <summary>
        /// Gets or sets a maximum value of progress bar.
        /// </summary>
        internal int ProgressMaxValue
        {
            get
            {
                return actionProgressBar.Maximum;
            }
            set
            {
                actionProgressBar.Maximum = value;
            }
        }

        /// <summary>
        /// Gets or sets a viewers background color.
        /// </summary>
        internal Color BackgroundColor
        {
            get
            {
                return pagesThumbnailViewer.BackColor;
            }
            set
            {
                pagesThumbnailViewer.BackColor = value;
                pageImageViewer.BackColor = value;
                frameImageViewer.BackColor = value;
                animatedImageViewer.BackColor = value;
            }
        }

        bool _isGifFileLoading = false;
        /// <summary>
        /// Gets or sets a value indicating whether GIF file is loading.
        /// </summary>
        internal bool IsGifFileLoading
        {
            get
            {
                return _isGifFileLoading;
            }
            set
            {
                _isGifFileLoading = value;
                UpdateUI();
            }
        }

        bool _isGifFileSaving = false;
        /// <summary>
        /// Gets or sets a value indicating whether GIF file is saving.
        /// </summary>
        internal bool IsGifFileSaving
        {
            get
            {
                return _isGifFileSaving;
            }
            set
            {
                _isGifFileSaving = value;
                UpdateUI();
            }
        }

        bool _isGifFileOptimizing = false;
        /// <summary>
        /// Gets or sets a value indicating whether GIF file optimization is in progress.
        /// </summary>
        internal bool IsGifFileOptimizing
        {
            get
            {
                return _isGifFileOptimizing;
            }
            set
            {
                _isGifFileOptimizing = value;
                UpdateUI();
            }
        }

        #endregion



        #region Methods

        #region UI

        #region Main Form

        /// <summary>
        /// Handles the FormClosing event of MainForm object.
        /// </summary>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopAnimation();
            viewerToolStrip.ImageViewer = null;
        }

        #endregion


        #region 'File' menu

        /// <summary>
        /// Handles the Click event of NewToolStripMenuItem object.
        /// </summary>
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IsGifFileLoading = true;

            // create new GIF file
            NewFile();

            IsGifFileLoading = false;
        }

        /// <summary>
        /// Handles the Click event of OpenToolStripMenuItem object.
        /// </summary>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IsGifFileLoading = true;

            openGifFileDialog.Multiselect = false;
            if (openGifFileDialog.ShowDialog() == DialogResult.OK)
            {
                // open an existing GIF file
                OpenFile(openGifFileDialog.FileName);
            }

            IsGifFileLoading = false;
        }

        /// <summary>
        /// Handles the Click event of AddPagesToolStripMenuItem object.
        /// </summary>
        private void addPagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IsGifFileLoading = true;

            openImageFileDialog.Multiselect = true;
            if (openImageFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // add pages to a GIF file
                    InsertPages(openImageFileDialog.FileNames, _gifFile.Pages.Count);
                }
                catch (Exception ex)
                {
                    DemosTools.ShowErrorMessage(ex);
                }
            }

            IsGifFileLoading = false;
        }

        /// <summary>
        /// Handles the Click event of SaveToolStripMenuItem object.
        /// </summary>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IsGifFileSaving = true;

            try
            {
                // save changes in GIF file
                _gifFile.SaveChanges();

                UpdateVersionInformation();
            }
            catch (Exception ex)
            {
                DemosTools.ShowErrorMessage(ex);
            }

            IsGifFileSaving = false;
        }

        /// <summary>
        /// Handles the Click event of SaveAsToolStripMenuItem object.
        /// </summary>
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IsGifFileSaving = true;

            try
            {
                if (saveGifFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // save GIF file to the new file and switch GIF file to the new file
                    Save(saveGifFileDialog.FileName, true);

                    UpdateFileInformation(saveGifFileDialog.FileName);
                }
            }
            catch (Exception ex)
            {
                DemosTools.ShowErrorMessage(ex);
            }

            IsGifFileSaving = false;
        }

        /// <summary>
        /// Handles the Click event of SaveToToolStripMenuItem object.
        /// </summary>
        private void saveToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IsGifFileSaving = true;

            try
            {
                if (saveGifFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // save GIF file to the new file without switching GIF file to the new file
                    Save(saveGifFileDialog.FileName, false);

                    UpdateFileInformation(saveGifFileDialog.FileName);
                }
            }
            catch (Exception ex)
            {
                DemosTools.ShowErrorMessage(ex);
            }

            IsGifFileSaving = false;
        }

        /// <summary>
        /// Handles the Click event of PackToolStripMenuItem object.
        /// </summary>
        private void packToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IsGifFileSaving = true;

            SetStatus("Pack..");
            try
            {
                int lengthBeforeOptimze = _gifFile.Length;

                // pack GIF file
                _gifFile.Pack();

                int lengthAfterOptimze = _gifFile.Length;
                UpdatePageAndFrame();
                UpdateVersionInformation();

                string status = string.Format("Pack: {0}->{1} bytes", lengthBeforeOptimze, lengthAfterOptimze);
                if (lengthBeforeOptimze > lengthAfterOptimze)
                {
                    status += string.Format(" (compression {0:F2}%)", (1 - (double)lengthAfterOptimze / lengthBeforeOptimze) * 100);
                }
                SetStatus(status);
            }
            catch (Exception ex)
            {
                DemosTools.ShowErrorMessage(ex);
                SetStatus("");
            }

            IsGifFileSaving = false;
        }

        /// <summary>
        /// Handles the Click event of GifFilePropertiesStripMenuItem object.
        /// </summary>
        private void gifFilePropertiesStripMenuItem_Click(object sender, EventArgs e)
        {
            using (PropertyGridForm dlg = new PropertyGridForm(_gifFile, "GIF File Properties"))
            {
                // show dialog with GIF file properties
                dlg.ShowDialog();
            }
        }

        /// <summary>
        /// Handles the Click event of CloseToolStripMenuItem object.
        /// </summary>
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // close current file
            CloseFile();
        }

        /// <summary>
        /// Handles the Click event of ExitToolStripMenuItem object.
        /// </summary>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // close file
            CloseFile();
            // exit application
            Close();
        }

        #endregion


        #region 'View' menu

        /// <summary>
        /// Handles the CheckedChanged event of HiqualityRenderingToolStripMenuItem object.
        /// </summary>
        private void hiqualityRenderingToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            ImageRenderingQuality renderingQuality;
            if (hiqualityRendringToolStripMenuItem.Checked)
                renderingQuality = ImageRenderingQuality.High;
            else
                renderingQuality = ImageRenderingQuality.Low;

            // set image rendering quality
            pageImageViewer.RenderingQuality = renderingQuality;
            frameImageViewer.RenderingQuality = renderingQuality;
            animatedImageViewer.RenderingQuality = renderingQuality;
        }

        /// <summary>
        /// Handles the Click event of WhiteToolStripMenuItem object.
        /// </summary>
        private void whiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // set viewers background color to the white color
            BackgroundColor = Color.White;
        }

        /// <summary>
        /// Handles the Click event of ControlToolStripMenuItem object.
        /// </summary>
        private void controlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // set viewers background color to the Control color
            BackgroundColor = SystemColors.Control;
        }

        /// <summary>
        /// Handles the Click event of SelectToolStripMenuItem object.
        /// </summary>
        private void selectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // set viewers background color to the selected color
            if (backgroundColorDialog.ShowDialog() == DialogResult.OK)
                BackgroundColor = backgroundColorDialog.Color;
        }

        /// <summary>
        /// Handles the Click event of ThumbnailViewerSettingsToolStripMenuItem object.
        /// </summary>
        private void thumbnailViewerSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (ThumbnailViewerSettingsForm dlg = new ThumbnailViewerSettingsForm(pagesThumbnailViewer))
            {
                // show the thumbnail viewer settings
                dlg.ShowDialog();
            }
        }

        #endregion


        #region 'Pages' menu

        /// <summary>
        /// Handles the Click event of OptimizePagesToolStripMenuItem object.
        /// </summary>
        private void optimizePagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IsGifFileOptimizing = true;

            SetStatus("Optimize...");

            int lengthBeforeOptimze = _gifFile.Length;

            // optimize pages of GIF file
            _gifFile.Pages.Optimize();

            int lengthAfterOptimze = _gifFile.Length;

            string status;
            // if GIF file length is changed after optimization
            if (lengthAfterOptimze == lengthBeforeOptimze)
            {
                status = "Optimize: not required.";
            }
            // if GIF file length is changed after optimization
            else
            {
                status = string.Format("Optimize: {0}->{1} bytes", lengthBeforeOptimze, lengthAfterOptimze);
                if (lengthBeforeOptimze > lengthAfterOptimze)
                {
                    status += string.Format(" (compression {0:F2}%)", (1 - (double)lengthAfterOptimze / lengthBeforeOptimze) * 100);
                }
                UpdateAllPageImages();
            }
            SetStatus(status);

            IsGifFileOptimizing = false;
        }

        /// <summary>
        /// Handles the Click event of DeoptimizePagesToolStripMenuItem object.
        /// </summary>
        private void deoptimizePagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IsGifFileOptimizing = true;

            // deoptimize pages of GIF file
            _gifFile.Pages.Deoptimize();

            UpdatePageAndFrame();

            IsGifFileOptimizing = false;
        }

        /// <summary>
        /// Handles the Click event of InvertColorsToolStripMenuItem object.
        /// </summary>
        private void invertColorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // invert palette colors on all pages
            ChangePaletteForAllPages(ChangePaletteMethod.Invert);
        }

        /// <summary>
        /// Handles the Click event of ConvertPaletteToGrayToolStripMenuItem object.
        /// </summary>
        private void convertPaletteToGrayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // change the palette to the grayscale palette for all pages
            ChangePaletteForAllPages(ChangePaletteMethod.ConvertToGray);
        }

        /// <summary>
        /// Handles the Click event of GlobalPaletteToolStripMenuItem object.
        /// </summary>
        private void globalPaletteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_gifFile.LogicalScreenPalette == null)
            {
                MessageBox.Show("There is no global palette.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                _paletteDialog.PaletteViewer.Palette = _gifFile.LogicalScreenPalette;
                _paletteDialog.CanChangePalette = true;

                // show the global palette editor form 
                if (_paletteDialog.ShowDialog() == DialogResult.OK
                    && _paletteDialog.PaletteViewer.IsPaletteChanged)
                {
                    _gifFile.LogicalScreenPalette = _paletteDialog.PaletteViewer.Palette;
                    UpdateAllPageImages();
                }
            }
        }

        /// <summary>
        /// Handles the Click event of DifferenceAllPagesToolStripMenuItem object.
        /// </summary>
        private void differenceAllPagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // apply color blend command with "Difference" mode to all pages
            ExecuteColorBlendCommandOnAllPages(BlendingMode.Difference);
        }

        /// <summary>
        /// Handles the Click event of HueAllPagesToolStripMenuItem object.
        /// </summary>
        private void hueAllPagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // apply color blend command with "Hue" mode to all pages
            ExecuteColorBlendCommandOnAllPages(BlendingMode.Hue);
        }

        /// <summary>
        /// Handles the Click event of RemoveAllToolStripMenuItem object.
        /// </summary>
        private void removeAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // remove all pages
            pagesThumbnailViewer.Images.ClearAndDisposeItems();

            // upate the UI
            UpdateUI();
        }

        /// <summary>
        /// Handles the Click event of ReverseToolStripMenuItem object.
        /// </summary>
        private void reverseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // get an array of GIF pages
            GifPage[] pages = _gifFile.Pages.ToArray();

            // clear page collection in GIF file
            _gifFile.Pages.Clear();

            // reverse the array with GIF pages
            Array.Reverse(pages);

            for (int i = 0; i < pages.Length; i++)
            {
                // add page to the GIF file
                _gifFile.Pages.Add(pages[i]);
            }

            // reload pages
            ReloadPages(true);
        }

        /// <summary>
        /// Handles the Click event of RemoveLocalPalettesToolStripMenuItem object.
        /// </summary>
        private void removeLocalPalettesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // indicates that at least one page contains a local palette
            bool containsLocalPalette = false;
            for (int i = 0; i < _gifFile.Pages.Count; i++)
            {
                if (_gifFile.Pages[i].HasLocalPalette)
                {
                    containsLocalPalette = true;
                    break;
                }
            }

            if (containsLocalPalette)
            {
                SetStatus("Remove local palette from pages...");
                int lengthBefore = _gifFile.Length;
                // remove pages local palettes
                _gifFile.Pages.RemoveLocalPalettes();
                int lengthAfter = _gifFile.Length;

                string status;
                if (lengthAfter == lengthBefore)
                {
                    status = "";
                }
                else
                {
                    status = string.Format("Remove local palette from pages: {0}->{1} bytes", lengthBefore, lengthAfter);
                    if (lengthBefore > lengthAfter)
                        status += string.Format(" (compression {0:F2}%)", (1 - (double)lengthAfter / lengthBefore) * 100);
                }
                UpdateAllPageImages();
                SetStatus(status);
            }
        }

        #endregion


        #region 'Page' menu

        /// <summary>
        /// Handles the Click event of MoveUpToolStripMenuItem object.
        /// </summary>
        private void moveUpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // swap the current and previous pages
            MoveCurrentPageTo(pagesThumbnailViewer.FocusedIndex - 1);
        }

        /// <summary>
        /// Handles the Click event of MoveDownToolStripMenuItem object.
        /// </summary>
        private void moveDownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // swap the current and next pages
            MoveCurrentPageTo(pagesThumbnailViewer.FocusedIndex + 1);
        }

        /// <summary>
        /// Handles the Click event of InsertToolStripMenuItem object.
        /// </summary>
        private void insertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IsGifFileLoading = true;

            openImageFileDialog.Multiselect = true;
            if (openImageFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // insert the new page before selected page
                    InsertPages(openImageFileDialog.FileNames, pagesThumbnailViewer.FocusedIndex);
                }
                catch (Exception ex)
                {
                    DemosTools.ShowErrorMessage(ex);
                }
            }

            IsGifFileLoading = false;
        }

        /// <summary>
        /// Handles the Click event of RemovePageToolStripMenuItem object.
        /// </summary>
        private void removePageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // remove selected page from the GIF file
            pagesThumbnailViewer.Images.RemoveAt(pagesThumbnailViewer.FocusedIndex);

            // upate the UI
            UpdateUI();
        }

        /// <summary>
        /// Handles the Click event of ReplacePageStripMenuItem object.
        /// </summary>
        private void replacePageStripMenuItem_Click(object sender, EventArgs e)
        {
            IsGifFileLoading = true;

            openImageFileDialog.Multiselect = false;
            if (openImageFileDialog.ShowDialog() == DialogResult.OK)
            {
                // replace selected page with new image
                ReplacePage(openImageFileDialog.FileName, pagesThumbnailViewer.FocusedIndex);
            }

            IsGifFileLoading = false;
        }

        /// <summary>
        /// Handles the Click event of DifferencePageToolStripMenuItem object.
        /// </summary>
        private void differencePageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // apply color blend command with "Difference" mode to all pages
            ExecuteColorBlendCommandOnCurrentPage(BlendingMode.Difference);
        }

        /// <summary>
        /// Handles the Click event of HuePageToolStripMenuItem object.
        /// </summary>
        private void huePageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // apply color blend command with "Hue" mode to all pages
            ExecuteColorBlendCommandOnCurrentPage(BlendingMode.Hue);
        }

        /// <summary>
        /// Handles the Click event of FramePaletteToolStripMenuItem object.
        /// </summary>
        private void framePaletteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!FocusedPage.HasLocalPalette &&
                MessageBox.Show("This frame has no local palette, set it?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            _paletteDialog.CanChangePalette = true;
            PaletteViewer viewer = _paletteDialog.PaletteViewer;
            viewer.Palette = FocusedPage.FramePalette;
            // show palette editor form for selected frame
            if (_paletteDialog.ShowDialog() == DialogResult.OK && viewer.IsPaletteChanged)
            {
                // update focused page palette
                FocusedPage.FramePalette = viewer.Palette;
                UpdateDependedPageImages(FocusedPage);
            }
        }

        /// <summary>
        /// Handles the Click event of SavePageAsToolStripMenuItem object.
        /// </summary>
        private void savePageAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // save page to a file
            SaveImageFileForm.SaveImageToFile(pageImageViewer.Image, ImagingEncoderFactory.Default);
        }

        /// <summary>
        /// Handles the Click event of SaveFrameAsToolStripMenuItem object.
        /// </summary>
        private void saveFrameAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // save frame to a file
            SaveImageFileForm.SaveImageToFile(frameImageViewer.Image, ImagingEncoderFactory.Default);
        }

        /// <summary>
        /// Handles the Click event of DeoptimizePageToolStripMenuItem object.
        /// </summary>
        private void deoptimizePageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IsGifFileOptimizing = true;

            // de-optimize selected page
            FocusedPage.Deoptimize();

            UpdatePageAndFrame();

            IsGifFileOptimizing = false;
        }

        #endregion


        #region 'Animation' menu

        /// <summary>
        /// Handles the Click event of StartToolStripMenuItem object.
        /// </summary>
        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (toolsTabControl.SelectedTab != animationTabPage)
                toolsTabControl.SelectedTab = animationTabPage;
            // start animation in image viewer
            StartAnimation();
        }

        /// <summary>
        /// Handles the Click event of StopToolStripMenuItem object.
        /// </summary>
        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // stop animation in image viewer
            StopAnimation();
        }

        /// <summary>
        /// Handles the Click event of PreviewInBrowserToolStripMenuItem object.
        /// </summary>
        private void previewInBrowserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Color backColor = BackgroundColor;
            if (backColor == SystemColors.Control)
                backColor = Color.White;

            backgroundColorDialog.Color = backColor;
            if (backgroundColorDialog.ShowDialog() == DialogResult.OK)
            {
                backColor = backgroundColorDialog.Color;
                try
                {
                    // save GIF file to a temp file
                    _gifFile.Save(_tempGifFilename);

                    // browser background color
                    string backColorString = string.Format("{0}{1}{2}",
                        backColor.R.ToString("X2"),
                        backColor.G.ToString("X2"),
                        backColor.B.ToString("X2"));
                    // generate HTML code
                    string htmlCode = string.Format(_htmlPageTemplate,
                        _mainFormTitle,
                        backColorString,
                        _tempGifFilename,
                        _gifFile.LogicalScreenWidth,
                        _gifFile.LogicalScreenHeight);
                    // save HTML code to a temp HTML file
                    File.WriteAllText(_tempHtmlFilename, htmlCode, Encoding.Unicode);

                    // execute HTML file
                    ProcessStartInfo processInfo = new ProcessStartInfo(_tempHtmlFilename);
                    processInfo.UseShellExecute = true;
                    Process.Start(processInfo);
                }
                catch (Exception ex)
                {
                    DemosTools.ShowErrorMessage(ex);
                }
            }
        }

        /// <summary>
        /// Handles the Click event of CreateAnimationToolStripMenuItem object.
        /// </summary>
        private void createAnimationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (VintasoftImage baseImage = FocusedPage.GetImage())
            {
                _createAnimationDialog.BaseImage = baseImage;

                // if animation is created
                if (_createAnimationDialog.ShowDialog() == DialogResult.OK)
                {
                    // create image collection from animation images
                    ImageCollection images = _createAnimationDialog.GetAnimationImages();
                    try
                    {
                        _addPagesDialog.InsertIndexVisible = true;
                        _addPagesDialog.InsertIndexMaxValue = _gifFile.Pages.Count;
                        _addPagesDialog.InsertIndex = pagesThumbnailViewer.FocusedIndex;

                        // if pages must be added to GIF file
                        if (_addPagesDialog.ShowDialog() == DialogResult.OK)
                        {
                            // insert images in GIF file
                            _gifFile.CreatePageFromImageMethod = _addPagesDialog.CreatePagesMethod;
                            _gifFile.NewPageAlign = _addPagesDialog.NewPagesAlign;
                            int insertIndex = _addPagesDialog.InsertIndex;
                            _gifFile.Pages.Insert(insertIndex, images);

                            // set delays
                            for (int i = 0; i < images.Count; i++)
                            {
                                _gifFile.Pages[insertIndex + i].Delay = _createAnimationDialog.Delay;
                            }

                            ReloadPages(true);
                        }
                    }
                    catch
                    {
                        images.ClearAndDisposeItems();
                    }
                }
            }
        }

        #endregion


        #region 'Help' menu

        /// <summary>
        /// Handles the Click event of AboutToolStripMenuItem object.
        /// </summary>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (AboutBoxForm dlg = new AboutBoxForm())
            {
                // show "About" dialog
                dlg.ShowDialog();
            }
        }

        #endregion


        #region Image Viewer

        /// <summary>
        /// Handles the FocusedIndexChanged event of AnimatedImageViewer object.
        /// </summary>
        private void animatedImageViewer_FocusedIndexChanged(object sender, FocusedIndexChangedEventArgs e)
        {
            UpdateUI();
        }

        /// <summary>
        /// Handles the ZoomChanged event of PageImageViewer object.
        /// </summary>
        private void pageImageViewer_ZoomChanged(object sender, ZoomChangedEventArgs e)
        {
            frameImageViewer.SizeMode = pageImageViewer.SizeMode;
            frameImageViewer.Zoom = pageImageViewer.Zoom;
            animatedImageViewer.SizeMode = pageImageViewer.SizeMode;
            animatedImageViewer.Zoom = pageImageViewer.Zoom;
        }

        #endregion


        #region Thumbnail Viewer

        /// <summary>
        /// Handles the FocusedIndexChanged event of PagesThumbnailViewer object.
        /// </summary>
        private void pagesThumbnailViewer_FocusedIndexChanged(object sender, Vintasoft.Imaging.UI.FocusedIndexChangedEventArgs e)
        {
            if (toolsTabControl.SelectedTab == pageAndFrameTabPage)
                UpdatePageAndFrame();

            if (toolsTabControl.SelectedTab == animationTabPage)
            {
                if (pagesThumbnailViewer.FocusedIndex < animatedImageViewer.Images.Count)
                {
                    animatedImageViewer.FocusedIndex = pagesThumbnailViewer.FocusedIndex;
                }
            }

            // upate the UI
            UpdateUI();
        }

        /// <summary>
        /// Handles the ImageCollectionChanged event of PagesThumbnailViewer object.
        /// </summary>
        private void pagesThumbnailViewer_ImageCollectionChanged(object sender, ImageCollectionChangeEventArgs e)
        {
            if (!_isImageCollectionChangingHandlingEnabled)
                return;

            int firstIndex = int.MaxValue;
            switch (e.Action)
            {
                case ImageCollectionChangeAction.Clear:
                    // remove all pages
                    _gifFile.Pages.Clear();
                    break;

                case ImageCollectionChangeAction.Reorder:
                case ImageCollectionChangeAction.SwapImages:
                    // resort pages
                    for (int i = 0; i < e.Images.Length; i++)
                    {
                        GifPage page = (GifPage)e.Images[i].Tag;
                        if (page != _gifFile.Pages[i])
                        {
                            // find old index of page
                            int oldIndex = -1;
                            for (int j = 0; j < _gifFile.Pages.Count; j++)
                            {
                                if (page == _gifFile.Pages[j])
                                {
                                    oldIndex = j;
                                    break;
                                }
                            }
                            // remove page at old index
                            _gifFile.Pages.RemoveAt(oldIndex);
                            // insert page at new index
                            _gifFile.Pages.Insert(i, page);

                            firstIndex = Math.Min(firstIndex, Math.Min(oldIndex, i));
                        }
                    }
                    break;

                case ImageCollectionChangeAction.RemoveImages:
                    // remove pages
                    for (int i = 0; i < e.Images.Length; i++)
                    {
                        int pageIndex = _gifFile.Pages.IndexOf((GifPage)e.Images[i].Tag);
                        if (pageIndex >= 0)
                        {
                            firstIndex = Math.Min(firstIndex, pageIndex);
                            _gifFile.Pages.RemoveAt(pageIndex);
                        }
                    }
                    break;
            }

            // update 32-bit pages
            if (firstIndex < _gifFile.Pages.Count)
            {
                for (int i = firstIndex; i < _gifFile.Pages.Count; i++)
                {
                    if (_gifFile.Pages[i].BitsPerPixel == 32)
                    {
                        UpdatePageImage(i);
                    }
                    else
                    {
                        break;
                    }
                }
                SetStatus("");
            }

            UpdatePageAndFrame();

            if (toolsTabControl.SelectedTab == animationTabPage)
            {
                // update animation tab page
                animatedImageViewer.Images.Clear();
                if (pagesThumbnailViewer.Images.Count > 0)
                {
                    animatedImageViewer.Images.AddRange(pagesThumbnailViewer.Images.ToArray());
                    animatedImageViewer.FocusedIndex = pagesThumbnailViewer.FocusedIndex;
                }
            }
            UpdateUI();
        }

        #endregion


        #region Tools tab control

        /// <summary>
        /// Handles the SelectedIndexChanged event of ToolsTabControl object.
        /// </summary>
        private void toolsTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            // if 'Animation' tab page is selected
            if (toolsTabControl.SelectedTab == animationTabPage)
            {
                // update it
                animatedImageViewer.Visible = true;
                animatedImageViewer.Images.AddRange(pagesThumbnailViewer.Images.ToArray());
                pageImageViewer.MasterViewer = null;
                animatedImageViewer.FocusedIndex = pagesThumbnailViewer.FocusedIndex;
                viewerToolStrip.ImageViewer = animatedImageViewer;
            }
            else
            {
                // update 'Page and Frame' tab page
                animatedImageViewer.Visible = false;
                StopAnimation();
                animatedImageViewer.Images.Clear();
                pageImageViewer.MasterViewer = pagesThumbnailViewer;
                viewerToolStrip.ImageViewer = pageImageViewer;
                UpdatePageAndFrame();
            }

            // upate the UI
            UpdateUI();
        }

        #region Page and frame

        /// <summary>
        /// Handles the TextChanged event of PageCommentsTextBox object.
        /// </summary>
        private void pageCommentsTextBox_TextChanged(object sender, EventArgs e)
        {
            if (FocusedPage != null)
            {
                // update page comment text
                FocusedPage.Comments = pageCommentsTextBox.Text;
                UpdateLengthInformation(FocusedPage);
            }
        }

        /// <summary>
        /// Handles the ValueChanged event of FrameDelayNumericUpDown object.
        /// </summary>
        private void frameDelayNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!_isPageParamsUpdating)
            {
                // update page delay
                FocusedPage.Delay = (ushort)pageDelayNumericUpDown.Value;
            }
        }

        /// <summary>
        /// Handles the CheckedChanged event of TransparencyCheckBox object.
        /// </summary>
        private void transparencyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            transparencyIndexGroupBox.Enabled = transparencyCheckBox.Checked;
            if (!_isPageParamsUpdating)
            {
                // enable/disable page color transparency
                FocusedPage.HasTransparentColor = transparencyCheckBox.Checked;
                UpdateDependedPageImages(FocusedPage);
            }
        }

        /// <summary>
        /// Handles the Click event of ChangeTrasparentColorIndexButton object.
        /// </summary>
        private void changeTrasparentColorIndexButton_Click(object sender, EventArgs e)
        {
            _paletteDialog.CanChangePalette = false;
            PaletteViewer viewer = _paletteDialog.PaletteViewer;
            viewer.Palette = FocusedPage.FramePalette;
            viewer.SelectedColorIndex = FocusedPage.TransparentColorIndex;

            // show selected frame palette viewer form
            if (_paletteDialog.ShowDialog() == DialogResult.OK &&
                viewer.SelectedColorIndex != FocusedPage.TransparentColorIndex)
            {
                FocusedPage.TransparentColorIndex = viewer.SelectedColorIndex;
                UpdateDependedPageImages(FocusedPage);
            }
        }

        /// <summary>
        /// Handles the Click event of SetDelayForAllPagesButton object.
        /// </summary>
        private void setDelayForAllPagesButton_Click(object sender, EventArgs e)
        {
            // set delay for all pages
            for (int i = 0; i < _gifFile.Pages.Count; i++)
            {
                _gifFile.Pages[i].Delay = (ushort)pageDelayNumericUpDown.Value;
            }
        }

        #endregion


        #region Animation

        /// <summary>
        /// Handles the Click event of AnimationPlayButton object.
        /// </summary>
        private void animationPlayButton_Click(object sender, EventArgs e)
        {
            // start animation in image viewer
            StartAnimation();
        }

        /// <summary>
        /// Handles the Click event of AnimationStopButton object.
        /// </summary>
        private void animationStopButton_Click(object sender, EventArgs e)
        {
            // stop animation in image viewer
            StopAnimation();
        }

        /// <summary>
        /// Handles the AnimationFinished event of AnimatedImageViewer object.
        /// </summary>
        private void animatedImageViewer_AnimationFinished(object sender, EventArgs e)
        {
            // upate the UI
            UpdateUI();
        }

        /// <summary>
        /// Handles the CheckedChanged event of AnimationRepeatCheckBox object.
        /// </summary>
        private void animationRepeatCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            // enable/disable animation repeat
            animatedImageViewer.AnimationRepeat = animationRepeatCheckBox.Checked;
        }

        /// <summary>
        /// Handles the ValueChanged event of AnimationDefaultDelayNumericUpDown object.
        /// </summary>
        private void animationDefaultDelayNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            // set default animation delay
            animatedImageViewer.DefaultDelay = (int)animationDefaultDelayNumericUpDown.Value * 10;
        }

        /// <summary>
        /// Handles the CheckedChanged event of AnimationReverseCheckBox object.
        /// </summary>
        private void animationReverseCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            // reverse animation
            animatedImageViewer.AnimationReverse = animationReverseCheckBox.Checked;
        }

        /// <summary>
        /// Handles the Click event of AnimationNextPageButton object.
        /// </summary>
        private void animationNextPageButton_Click(object sender, EventArgs e)
        {
            // show next page in viewer
            MoveTo(animatedImageViewer.FocusedIndex + 1);
        }

        /// <summary>
        /// Handles the Click event of AnimationPrevPageButton object.
        /// </summary>
        private void animationPrevPageButton_Click(object sender, EventArgs e)
        {
            // show previous page in viewer
            MoveTo(animatedImageViewer.FocusedIndex - 1);
        }

        /// <summary>
        /// Handles the Click event of AnimationLastPageButton object.
        /// </summary>
        private void animationLastPageButton_Click(object sender, EventArgs e)
        {
            // show last page of file in viewer
            MoveTo(animatedImageViewer.Images.Count - 1);
        }

        /// <summary>
        /// Handles the Click event of AnimationFirstPageButton object.
        /// </summary>
        private void animationFirstPageButton_Click(object sender, EventArgs e)
        {
            // show first page of file in viewer
            MoveTo(0);
        }

        #endregion

        #endregion

        #endregion


        #region UI state

        /// <summary>
        /// Updates the user interface of this form.
        /// </summary>
        private void UpdateUI()
        {
            // get the current status of application
            bool isGifFileLoaded = _gifFile != null;
            bool isGifFileLoading = IsGifFileLoading;
            bool isGifFileSaving = IsGifFileSaving;
            bool isGifFileOptimize = IsGifFileOptimizing;
            bool isGifFileEmpty = true;
            bool isMultipageGifFile = false;
            bool isLoadFirstPage = true;
            bool isLoadLastPage = true;

            if (isGifFileLoaded)
            {
                isGifFileEmpty = _gifFile.Pages.Count <= 0;
                isMultipageGifFile = _gifFile.Pages.Count > 1;

                if (toolsTabControl.SelectedTab == animationTabPage)
                    isLoadFirstPage = animatedImageViewer.FocusedIndex <= 0;
                else
                    isLoadFirstPage = pagesThumbnailViewer.FocusedIndex <= 0;

                if (toolsTabControl.SelectedTab == animationTabPage)
                    isLoadLastPage = animatedImageViewer.FocusedIndex == _gifFile.Pages.Count - 1;
                else
                    isLoadLastPage = pagesThumbnailViewer.FocusedIndex == _gifFile.Pages.Count - 1;
            }

            // "File" menu
            fileToolStripMenuItem.Enabled = !isGifFileLoading && !isGifFileSaving &&
                !animatedImageViewer.Animation && !isGifFileOptimize;
            addPagesToolStripMenuItem.Enabled = isGifFileLoaded;
            saveToolStripMenuItem.Enabled = isGifFileLoaded && !isGifFileEmpty;
            saveAsToolStripMenuItem.Enabled = isGifFileLoaded && !isGifFileEmpty;
            saveToToolStripMenuItem.Enabled = isGifFileLoaded && !isGifFileEmpty;
            packToolStripMenuItem.Enabled = isGifFileLoaded && !isGifFileEmpty;
            logicalScreenToolStripMenuItem.Enabled = isGifFileLoaded;
            closeToolStripMenuItem.Enabled = isGifFileLoaded;
            viewerToolStrip.CanOpenFile = !animatedImageViewer.Animation;

            // "Pages" menu
            pagesToolStripMenuItem.Enabled = !isGifFileLoading && !isGifFileSaving && !isGifFileEmpty &&
                !animatedImageViewer.Animation && !isGifFileOptimize;

            // "Page" menu
            pageToolStripMenuItem.Enabled = isGifFileLoaded && !isGifFileLoading && !isGifFileSaving &&
                !isGifFileEmpty && !animatedImageViewer.Animation && !isGifFileOptimize;
            moveUpToolStripMenuItem.Enabled = isMultipageGifFile && !isLoadFirstPage;
            moveDownToolStripMenuItem.Enabled = isMultipageGifFile && !isLoadLastPage;

            // "Animation" menu
            animationToolStripMenuItem.Enabled = isGifFileLoaded && !isGifFileLoading && !isGifFileSaving &&
                !isGifFileEmpty && !isGifFileOptimize;
            startToolStripMenuItem.Enabled = isMultipageGifFile && !animatedImageViewer.Animation;
            animationStartButton.Visible = !animatedImageViewer.Animation;
            animationStopButton.Visible = animatedImageViewer.Animation;
            pagesThumbnailViewer.AllowDrag = !animatedImageViewer.Animation;
            pagesThumbnailViewer.DisableThumbnailContextMenu = animatedImageViewer.Animation;
            stopToolStripMenuItem.Enabled = isMultipageGifFile && animatedImageViewer.Animation;
            createAnimationToolStripMenuItem.Enabled = !animatedImageViewer.Animation;

            // tools tab control
            toolsTabControl.Enabled = isGifFileLoaded && !isGifFileLoading && !isGifFileSaving && !isGifFileEmpty &&
                !isGifFileOptimize;
            animationControlPanel.Enabled = isMultipageGifFile;
            setDelayForAllPagesButton.Enabled = isMultipageGifFile;
            animationFirstPageButton.Enabled = !isLoadFirstPage && !isGifFileEmpty && isGifFileLoaded;
            animationPrevPageButton.Enabled = !isLoadFirstPage && !isGifFileEmpty && isGifFileLoaded;
            animationNextPageButton.Enabled = !isLoadLastPage && !isGifFileEmpty && isGifFileLoaded;
            animationLastPageButton.Enabled = !isLoadLastPage && !isGifFileEmpty && isGifFileLoaded;
            if (!toolsTabControl.Enabled && animatedImageViewer.Animation)
                StopAnimation();

            // viewerToolStrip
            viewerToolStrip.Enabled = !isGifFileLoading && !isGifFileSaving && !isGifFileOptimize;
            viewerToolStrip.SaveButtonEnabled = !isGifFileEmpty && isGifFileLoaded && !animatedImageViewer.Animation;

            // pages thumbnail viewer
            pagesThumbnailViewer.Enabled = isGifFileLoaded && !IsGifFileLoading && !isGifFileSaving && !isGifFileOptimize;
        }

        /// <summary>
        /// Updates the information about GIF file.
        /// </summary>
        /// <param name="filename">GIF file name.</param>
        private void UpdateFileInformation(string filename)
        {
            Text = string.Format("{0} - {1}", _mainFormTitle, Path.GetFileName(filename));

            UpdateVersionInformation();
        }

        /// <summary>
        /// Updates the information about GIF file version.
        /// </summary>
        private void UpdateVersionInformation()
        {
            string versionString = null;
            switch (_gifFile.Version)
            {
                case GifFileVersion.Version87a:
                    versionString = "87a";
                    break;
                case GifFileVersion.Version89a:
                    versionString = "89a";
                    break;
            }
            gifFileInformationLabel.Text = string.Format("Version: {0}.", versionString);
        }

        /// <summary>
        /// Updates the action progress in status strip.
        /// </summary>
        /// <param name="value">The amount of progress.</param>
        private void OnProgress(int value)
        {
            actionProgressBar.Value = value;
            actionProgressBar.Visible = value != ProgressMaxValue;
            statusStrip.Refresh();
        }

        /// <summary>
        /// Updates the "Page and Frame" tab page.
        /// </summary>
        private void UpdatePageAndFrame()
        {
            UpdatePageAndFrame(FocusedPage);
        }

        /// <summary>
        /// Sets specified page to the viewers in "Page and Frame" tab page 
        /// and updates information in it.
        /// </summary>
        /// <param name="page">Page to be displayed.</param>
        private void UpdatePageAndFrame(GifPage page)
        {
            if (page != null)
            {
                if (frameImageViewer.Image == null)
                {
                    frameImageViewer.Image = page.GetFrame();
                }
                else
                {
                    frameImageViewer.Image.SetImage(page.GetFrame());
                }
            }
            else
            {
                VintasoftImage image = frameImageViewer.Image;
                frameImageViewer.Image = null;
                if (image != null)
                    image.Dispose();
            }

            _isPageParamsUpdating = true;

            if (page != null)
            {
                pageGroupBox.Text = string.Format("Page ({0} bpp), size {1}x{2} pixels", page.BitsPerPixel, page.Width, page.Height);
                frameGroupBox.Text = string.Format("Frame (8 bpp), size {0}x{1} pixels", page.FrameWidth, page.FrameHeight);
                framePositionLabel.Text = string.Format("X={0}; Y={1}", page.FrameLeftPosition, page.FrameTopPosition);
                disposalMethodLabel.Text = string.Format("{0}", page.DisposalMethod);

                if (page.IsImageDependOnPreviousPages)
                    pageDependingsLabel.Text = "Previous pages";
                else
                    pageDependingsLabel.Text = "Independent";

                pageDelayNumericUpDown.Value = page.Delay;
                pageCommentsTextBox.Text = page.Comments;
                tranparentColorIndexLabel.Text = page.TransparentColorIndex.ToString();
                transparencyCheckBox.Checked = page.HasTransparentColor;
            }
            else
            {
                pageGroupBox.Text = "Page";
                frameGroupBox.Text = "Frame";
                framePositionLabel.Text = "";
                pageDelayNumericUpDown.Value = 0;
                pageCommentsTextBox.Text = "";
                disposalMethodLabel.Text = "";
                pageDependingsLabel.Text = "";
                tranparentColorIndexLabel.Text = "0";
                transparencyCheckBox.Checked = false;
            }
            _isPageParamsUpdating = false;
            UpdateLengthInformation(page);
        }

        /// <summary>
        /// Updates the page length information.
        /// </summary>
        /// <param name="page">The page.</param>
        private void UpdateLengthInformation(GifPage page)
        {
            if (page == null)
                pageLengthLabel.Text = "";
            else
                pageLengthLabel.Text = string.Format("{0} bytes", page.Length);
        }

        /// <summary>
        /// Sets the status in status strip.
        /// </summary>
        /// <param name="status">The status.</param>
        private void SetStatus(string status)
        {
            statusLabel.Text = status;
            statusStrip.Refresh();
        }

        #endregion


        #region GIF file manipulation

        /// <summary>
        /// Creates a new GIF file.
        /// </summary>
        private void NewFile()
        {
            if (_createGifFileDialog.ShowDialog() == DialogResult.OK)
            {
                CloseFile();

                _gifFile = _createGifFileDialog.NewFile;

                try
                {
                    // if pages must be added to GIF file
                    if (_createGifFileDialog.AddPages)
                    {
                        openImageFileDialog.Multiselect = true;

                        // if image file is selected
                        if (openImageFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            // insert pages to GIF file
                            InsertPages(openImageFileDialog.FileNames, 0);
                        }
                    }
                    // if pages must NOT be added to GIF file
                    else
                    {
                        if (_gifFile.Pages.Count > 0)
                            ReloadPages(true);
                    }
                }
                catch (Exception ex)
                {
                    DemosTools.ShowErrorMessage(ex);
                }

                UpdateFileInformation("gifFile1.gif");
            }
        }

        /// <summary>
        /// Opens an existing GIF file.
        /// </summary>
        /// <param name="filename">The name of the file to open.</param>
        public void OpenFile(string filename)
        {
            CloseFile();
            try
            {
                _gifFile = new GifFile(filename);
            }
            catch (Exception e)
            {
                DemosTools.ShowErrorMessage(e);
                return;
            }

            UpdateFileInformation(filename);
            ReloadPages(false);
        }

        /// <summary>
        /// Saves the file.
        /// </summary>
        /// <param name="filename">The name of the file to save to.</param>
        /// <param name="switchToNewSource">Indicates whether to switch to the source after saving.</param>
        private void Save(string filename, bool switchToNewSource)
        {
            try
            {
                ProgressMaxValue = 100;

                if (switchToNewSource)
                {
                    _gifFile.SaveChanges(filename, gifFile_SavingProgress);
                }
                else
                {
                    _gifFile.Save(filename, gifFile_SavingProgress);
                }
            }
            catch (Exception ex)
            {
                DemosTools.ShowErrorMessage(ex);
            }
        }

        /// <summary>
        /// GIF file saving is in progress.
        /// </summary>
        private void gifFile_SavingProgress(object sender, ProgressEventArgs e)
        {
            OnProgress(e.Progress);
        }

        /// <summary>
        /// Closes the current GIF file.
        /// </summary>
        private void CloseFile()
        {
            _isImageCollectionChangingHandlingEnabled = false;

            Text = _mainFormTitle;
            gifFileInformationLabel.Text = "";
            pagesThumbnailViewer.Images.ClearAndDisposeItems();

            if (_gifFile != null)
            {
                _gifFile.Dispose();
                _gifFile = null;
            }

            VintasoftImage lastFrame = frameImageViewer.Image;
            frameImageViewer.Image = null;
            if (lastFrame != null)
                lastFrame.Dispose();

            SetStatus("");

            // upate the UI
            UpdateUI();
        }

        #endregion


        #region Pages manipulation

        /// <summary>
        /// Inserts pages into GIF file starting from specified index.
        /// </summary>
        /// <param name="files">Images that are inserted.</param>
        /// <param name="insertIndex">Insert start index.</param>
        private void InsertPages(string[] files, int insertIndex)
        {
            // configure page adding form
            _addPagesDialog.InsertIndexVisible = false;
            _addPagesDialog.InsertIndexMaxValue = _gifFile.Pages.Count;
            _addPagesDialog.InsertIndex = insertIndex;

            if (_addPagesDialog.ShowDialog() == DialogResult.OK)
            {
                _gifFile.CreatePageFromImageMethod = _addPagesDialog.CreatePagesMethod;
                _gifFile.NewPageAlign = _addPagesDialog.NewPagesAlign;
                insertIndex = _addPagesDialog.InsertIndex;

                // create image collection from file names
                ImageCollection images = null;
                int[] delays = null;
                LoadImages(files, ref images, ref delays);

                // add images
                if (images.Count > 0)
                    _gifFile.Pages.Insert(insertIndex, images);
                // sets delays
                for (int i = 0; i < delays.Length; i++)
                    _gifFile.Pages[insertIndex + i].Delay = delays[i];
                // dispose images
                images.ClearAndDisposeItems();
                // reload pages
                ReloadPages(true);
            }
        }

        /// <summary>
        /// Loads image files to an <see cref="ImageCollection"/>.
        /// </summary>
        /// <param name="files">Images that are loaded.</param>
        /// <param name="images">Image collection where images will be loaded.</param>
        /// <param name="delays">Delays for animation.</param>
        internal static void LoadImages(string[] files, ref ImageCollection images, ref int[] delays)
        {
            images = new ImageCollection();
            List<int> delaysList = new List<int>();
            for (int i = 0; i < files.Length; i++)
            {
                string filename = files[i];
                try
                {
                    if (Path.GetExtension(filename).ToUpperInvariant() == ".GIF")
                    {
                        // open GIF file
                        using (GifFile file = new GifFile(filename))
                        {
                            // add GIF pages
                            for (int j = 0; j < file.Pages.Count; j++)
                            {
                                images.Add(file.Pages[j].GetImage());
                                delaysList.Add(file.Pages[j].Delay);
                            }
                        }
                    }
                    else
                    {
                        images.Add(filename);
                        delaysList.Add(0);
                    }
                }
                catch (Exception ex)
                {
                    DemosTools.ShowErrorMessage(ex);
                }
            }
            delays = delaysList.ToArray();
        }

        /// <summary>
        /// Replaces specified page with another image. 
        /// </summary>
        /// <param name="image">New <see cref="VintasoftImage"/> to be placed.</param>
        /// <param name="index">Index of the replaced page.</param>
        /// <param name="changeIndex">Indicates whether to focus the new image after replacement.</param>
        private void ReplacePage(VintasoftImage image, int index, bool changeIndex)
        {
            // save page properties
            GifPage page = _gifFile.Pages[index];
            string comments = page.Comments;
            int delay = page.Delay;

            // remove old page
            _gifFile.Pages.RemoveAt(index);

            // insert new page
            _gifFile.Pages.Insert(index, image);
            GifPage newPage = _gifFile.Pages[index];

            newPage.Delay = delay;
            newPage.Comments = comments;

            if (changeIndex)
            {
                // change focused page index
                pagesThumbnailViewer.FocusedIndex = index;
            }

            VintasoftImage pageImage = pagesThumbnailViewer.Images[index];
            pageImage.Tag = newPage;
            pageImage.SetImage(newPage.GetImage());

            UpdatePageAndFrame();
        }

        /// <summary>
        /// Replaces specified page with another image. 
        /// </summary>
        /// <param name="filename">Name of the image to be placed.</param>
        /// <param name="index">Index of page to be replaced.</param>
        private void ReplacePage(string filename, int index)
        {
            VintasoftImage image;
            try
            {
                // open the image
                image = new VintasoftImage(filename);
            }
            catch (Exception ex)
            {
                DemosTools.ShowErrorMessage(ex);
                return;
            }
            try
            {
                _addPagesDialog.InsertIndexVisible = false;

                if (_addPagesDialog.ShowDialog() == DialogResult.OK)
                {
                    // get parameters specified in the dialog
                    _gifFile.CreatePageFromImageMethod = _addPagesDialog.CreatePagesMethod;
                    _gifFile.NewPageAlign = _addPagesDialog.NewPagesAlign;

                    // replace page
                    ReplacePage(image, index, true);
                }
            }
            finally
            {
                image.Dispose();
            }
        }

        /// <summary>
        /// Swaps two pages.
        /// </summary>
        /// <param name="newIndex">Index of page that is focused after the swap.</param>
        private void MoveCurrentPageTo(int newIndex)
        {
            pagesThumbnailViewer.Images.Swap(pagesThumbnailViewer.FocusedIndex, newIndex);
            pagesThumbnailViewer.FocusedIndex = newIndex;
        }

        /// <summary>
        /// Changes the palette using a specified method.
        /// </summary>
        /// <param name="imagePalette">The <see cref="Palette"/>.</param>
        /// <param name="method">Pallete changing method.</param>
        /// <returns>Palette with changes.</returns>
        private Palette ChangePalette(Palette imagePalette, ChangePaletteMethod method)
        {
            switch (method)
            {
                case ChangePaletteMethod.Invert:
                    imagePalette.Invert();
                    break;
                case ChangePaletteMethod.ConvertToGray:
                    imagePalette.ConvertToGrayColors();
                    break;
            }
            return imagePalette;
        }

        /// <summary>
        /// Changes the palette for all pages.
        /// </summary>
        /// <param name="method">Palette changing method.</param>
        private void ChangePaletteForAllPages(ChangePaletteMethod method)
        {
            if (_gifFile.LogicalScreenPalette != null)
            {
                _gifFile.LogicalScreenPalette = ChangePalette(_gifFile.LogicalScreenPalette, method);
            }

            for (int i = 0; i < _gifFile.Pages.Count; i++)
            {
                GifPage page = _gifFile.Pages[i];
                if (page.HasLocalPalette)
                {
                    page.FramePalette = ChangePalette(page.FramePalette, method);
                }
            }
            UpdateAllPageImages();
        }

        /// <summary>
        /// Reloads GIF file pages.
        /// </summary>
        /// <param name="needShowPageAndFrame">Indicates whether to update "Page and Frame" tab page.</param>
        private void ReloadPages(bool needShowPageAndFrame)
        {
            _isImageCollectionChangingHandlingEnabled = false;

            fileToolStripMenuItem.Enabled = false;

            UpdatePageAndFrame(null);
            Application.DoEvents();

            int focusedIndex = pagesThumbnailViewer.FocusedIndex;
            pagesThumbnailViewer.Images.ClearAndDisposeItems();

            if (_gifFile.Pages.Count > 0)
            {
                // update status
                SetStatus(string.Format("Rendering ({0} pages)...", _gifFile.Pages.Count));
                ProgressMaxValue = _gifFile.Pages.Count - 1;
                OnProgress(0);
                DateTime dt = DateTime.Now;

                VintasoftImage[] images = new VintasoftImage[_gifFile.Pages.Count];
                for (int i = 0; i < images.Length; i++)
                {
                    // get page images
                    GifPage page = _gifFile.Pages[i];
                    VintasoftImage pageImage = page.GetImage();
                    pageImage.Tag = page;
                    images[i] = pageImage;
                    OnProgress(i);
                }

                // update images in thumbnail viewer
                pagesThumbnailViewer.Images.AddRange(images);
                SetStatus(string.Format("Rendering ({0} pages): {1} ms.", _gifFile.Pages.Count, (DateTime.Now - dt).TotalMilliseconds));
            }

            if (focusedIndex != pagesThumbnailViewer.FocusedIndex &&
                focusedIndex >= 0 &&
                focusedIndex < pagesThumbnailViewer.Images.Count)
            {
                pagesThumbnailViewer.FocusedIndex = focusedIndex;
            }

            if (toolsTabControl.SelectedTab == animationTabPage)
            {
                // update animation tab page
                animatedImageViewer.Images.Clear();
                if (pagesThumbnailViewer.Images.Count > 0)
                {
                    animatedImageViewer.Images.AddRange(pagesThumbnailViewer.Images.ToArray());
                    animatedImageViewer.FocusedIndex = pagesThumbnailViewer.FocusedIndex;
                }
            }

            if (needShowPageAndFrame)
                UpdatePageAndFrame();

            _isImageCollectionChangingHandlingEnabled = true;

            fileToolStripMenuItem.Enabled = true;

            ProgressMaxValue = 100;

            // upate the UI
            UpdateUI();
        }

        /// <summary>
        /// Changes the page displayed in viewers.
        /// </summary>
        /// <param name="index">Index of the page to be displayed.</param>
        private void MoveTo(int index)
        {
            animatedImageViewer.SetFocusedIndex(index);
            pagesThumbnailViewer.FocusedIndex = index;

            // upate the UI
            UpdateUI();
        }

        /// <summary>
        /// Updates images of the specified page and dependent pages.
        /// </summary>
        /// <param name="page">Page where the update starts.</param>
        private void UpdateDependedPageImages(GifPage page)
        {
            int index = _gifFile.Pages.IndexOf(page);
            UpdatePageImage(index);
            UpdatePageAndFrame();
            for (int i = index + 1; i < _gifFile.Pages.Count; i++)
            {
                if (_gifFile.Pages[i].IsImageDependOnPreviousPages)
                    UpdatePageImage(i);
                else
                    break;
            }
            SetStatus("");
        }

        /// <summary>
        /// Updates images of all pages.
        /// </summary>
        private void UpdateAllPageImages()
        {
            for (int i = 0; i < _gifFile.Pages.Count; i++)
                UpdatePageImage(i);
            UpdatePageAndFrame();
            SetStatus("");
        }

        /// <summary>
        /// Updates the specified page image.
        /// </summary>
        /// <param name="index">Page index.</param>
        private void UpdatePageImage(int index)
        {
            SetStatus(string.Format("Rendering page {0}...", index));
            pagesThumbnailViewer.Images[index].SetImage(_gifFile.Pages[index].GetImage());
        }

        #endregion


        #region Effects

        /// <summary>
        /// Executes the processing command on all pages.
        /// </summary>
        /// <param name="command">The command to execute.</param>
        private void ExecuteProcessingCommandOnAllPages(ProcessingCommandBase command)
        {
            for (int i = 0; i < _gifFile.Pages.Count; i++)
            {
                ExecuteProcessingCommand(command, i, false);
                if (i == pagesThumbnailViewer.FocusedIndex)
                    UpdatePageAndFrame();
            }
        }

        /// <summary>
        /// Executes the color blend command on all pages.
        /// </summary>
        /// <param name="mode">Color blending mode.</param>
        private void ExecuteColorBlendCommandOnAllPages(BlendingMode mode)
        {
            if (_effectsColorDialog.ShowDialog() == DialogResult.OK)
                ExecuteProcessingCommandOnAllPages(new ColorBlendCommand(mode, _effectsColorDialog.Color));
        }

        /// <summary>
        /// Executes the processing command on the current page.
        /// </summary>
        /// <param name="command">The command to execute.</param>
        private void ExecuteProcessingCommandOnCurrentPage(ProcessingCommandBase command)
        {
            ExecuteProcessingCommand(command, pagesThumbnailViewer.FocusedIndex, true);
            UpdatePageAndFrame();
        }

        /// <summary>
        /// Executes the color blend command on the current page.
        /// </summary>
        /// <param name="mode">Color blending mode.</param>
        private void ExecuteColorBlendCommandOnCurrentPage(BlendingMode mode)
        {
            if (_effectsColorDialog.ShowDialog() == DialogResult.OK)
                ExecuteProcessingCommandOnCurrentPage(new ColorBlendCommand(mode, _effectsColorDialog.Color));
        }

        /// <summary>
        /// Executes the processing command on specified page.
        /// </summary>
        /// <param name="command">The command to execute.</param>
        /// <param name="pageIndex">Processing page index.</param>
        /// <param name="changeIndex">Indicates whether to change focused page after processing.</param>
        private void ExecuteProcessingCommand(ProcessingCommandBase command, int pageIndex, bool changeIndex)
        {
            // get image of GIF page
            using (VintasoftImage pageImage = _gifFile.Pages[pageIndex].GetImage())
            {
                // if command does not support image pixel format
                if (!command.IsPixelFormatSupported(pageImage.PixelFormat))
                {
                    ChangePixelFormatCommand changePixelFormatCommand = new ChangePixelFormatCommand(PixelFormat.Bgra32);
                    // convert image to Bgra32 pixel format
                    changePixelFormatCommand.ExecuteInPlace(pageImage);
                }

                // apply processing command to the image
                command.ExecuteInPlace(pageImage);

                _gifFile.CreatePageFromImageMethod = CreatePageMethod.UseGlobalOrLocalPalette;
                _gifFile.NewPageAlign = PageAlignMode.Center;
                // replace image of GIF page
                ReplacePage(pageImage, pageIndex, changeIndex);
            }
        }

        #endregion


        #region Animation

        /// <summary>
        /// Starts the animation in image viewer.
        /// </summary>
        private void StartAnimation()
        {
            // for each GIF page
            for (int i = 0; i < _gifFile.Pages.Count; i++)
            {
                // save info about page delay
                animatedImageViewer.Delays[i] = _gifFile.Pages[i].Delay * 10;
            }

            // enable animation
            animatedImageViewer.Animation = true;
            animationStartButton.Focus();
            pagesThumbnailViewer.ShortcutDelete = Shortcut.None;

            // upate the UI
            UpdateUI();
        }

        /// <summary>
        /// Stops the animation in image viewer.
        /// </summary>
        private void StopAnimation()
        {
            // disable animation
            animatedImageViewer.Animation = false;
            animationStartButton.Focus();
            pagesThumbnailViewer.ShortcutDelete = Shortcut.Del;

            pagesThumbnailViewer.FocusedIndex = animatedImageViewer.FocusedIndex;

            // upate the UI
            UpdateUI();
        }


        #endregion

        #endregion

    }
}

using System;
using System.Windows.Forms;
using Vintasoft.Imaging.Codecs.ImageFiles.Gif;

namespace GifAnimatorDemo
{
    /// <summary>
    /// A form that allows to specify parameters for new pages.
    /// </summary>
    public partial class AddPagesForm : Form
    {

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AddPagesForm"/> class.
        /// </summary>
        public AddPagesForm()
        {
            InitializeComponent();
            createPageMethodComboBox.Items.Add(CreatePageMethod.UseGlobalOrLocalPalette);
            createPageMethodComboBox.Items.Add(CreatePageMethod.UseOnlyGlobalPalette);
            createPageMethodComboBox.SelectedIndex = 0;
        }

        #endregion



        #region Properties

        /// <summary>
        /// Gets or sets a maximum value of insert index.
        /// </summary>
        public int InsertIndexMaxValue
        {
            get
            {
                return (int)insertIndexNumericUpDown.Maximum;
            }
            set
            {
                insertIndexNumericUpDown.Maximum = value;
            }
        }


        /// <summary>
        /// Gets or sets an insert index.
        /// </summary>
        public int InsertIndex
        {
            get
            {
                return (int)insertIndexNumericUpDown.Value;
            }
            set
            {
                insertIndexNumericUpDown.Value = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Insert Index is visible.
        /// </summary>
        public bool InsertIndexVisible
        {
            get
            {
                return insertIndexGroupBox.Visible;
            }
            set
            {
                insertIndexGroupBox.Visible = value;
            }
        }

        /// <summary>
        /// Gets a <see cref="CreatePageMethod"/>.
        /// </summary>
        public CreatePageMethod CreatePagesMethod
        {
            get
            {
                return (CreatePageMethod)createPageMethodComboBox.SelectedItem;
            }
        }

        /// <summary>
        /// Gets a <see cref="PageAlignMode"/>.
        /// </summary>
        public PageAlignMode NewPagesAlign
        {
            get
            {
                if (centerPositionRadioButton.Checked)
                    return PageAlignMode.Center;
                if (leftTopPositionRadioButton.Checked)
                    return PageAlignMode.LeftTop;
                if (topPositionRadioButton.Checked)
                    return PageAlignMode.Top;
                if (rightTopPositionRadioButton.Checked)
                    return PageAlignMode.RightTop;
                if (rightPositionRadioButton.Checked)
                    return PageAlignMode.Right;
                if (rightBottomPositionRadioButton.Checked)
                    return PageAlignMode.RightButtom;
                if (bottomPositionRadioButton.Checked)
                    return PageAlignMode.Bottom;
                if (letfBottomPositionRadioButton.Checked)
                    return PageAlignMode.LeftBottom;
                if (leftPositionRadioButton.Checked)
                    return PageAlignMode.Left;
                return PageAlignMode.LeftTop;
            }
        }

        #endregion



        #region Methods

        #region UI

        /// <summary>
        /// Handles the Click event of ButtonOk object.
        /// </summary>
        private void buttonOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// Handles the Click event of ButtonCancel object.
        /// </summary>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        #endregion

        #endregion

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Karta_Pracy_SMT
{
    public partial class NewLotForm : Form
    {
        Form opener;
        public NewLotForm()
        {
            InitializeComponent();
            opener = ParentForm;
        }



        private void textBoxLotNo_TextChanged(object sender, EventArgs e)
        {
            if (textBoxLotNo.Text.Length > 5)
            {
                LotData currentLotData = SqlOperations.GetLotData(textBoxLotNo.Text);

                labelModel.Text = "Model: " + currentLotData.Model;
                labelOrderedQty.Text = "Ilość " + currentLotData.OrderedQty.ToString();
                labelRankA.Text = "Rank A" + Environment.NewLine + currentLotData.RankA;
                labelRankB.Text = "Rank B" + Environment.NewLine + currentLotData.RankB;

            }
        }
    }
}

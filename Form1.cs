using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace BulkRename
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
        }

        string fileNames { get; set; }
        List<string> fileNamesList = new List<string>();
        string a { get; set; }
        List<string> fileNamesListnoext = new List<string>();
        List<ListViewItem> filenomi = new List<ListViewItem>();
        List<string> risultato = new List<string>();
        string [] fileNamesArray2 { get; set; }
        string[] fileNamesArraydef { get; set; }
        public void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            fileNames = string.Join("\n", openFileDialog1.FileNames);
            string[] fileNamesArray = openFileDialog1.FileNames;
            fileNamesArray2 = fileNamesArray;
            string a = textBox3.Text;
            for (int i = 0; i < fileNamesArray.Length; i++)
            {
                fileNamesList.Add(Path.GetFileName(fileNamesArray[i]));
            }

            if (openFileDialog1.FileName == "openFileDialog1")
            {
                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Clear();
                MessageBox.Show("Non hai inserito nessun file.\n\n\t\t Uscita dal programma.", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Environment.Exit(1);
            }

            for (int i = 0; i < fileNamesArray.Length; i++)
            {
                string s;
                s = System.IO.Path.GetFileNameWithoutExtension(fileNamesArray[i]);
                fileNamesListnoext.Add(s);
            }

            for (int i = 0; i < fileNamesArray.Length; i++)
            {
                dataGridView1.Rows.Add(fileNamesListnoext[i]);
            }

            if (dataGridView1.Rows[0].Cells[0].Value != "") { button1.Enabled = false; }

            dataGridView1.AutoResizeColumns();
            dataGridView1.ClearSelection();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }


        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == comboBox1.Items[0])
            {
                textBox1.ReadOnly = true;
                textBox1.Enabled = false;
                textBox2.ReadOnly = true;
                textBox2.Enabled = false;
                textBox3.ReadOnly = false;
                textBox3.Enabled = true;
            }

            if (comboBox1.SelectedItem == comboBox1.Items[1])
            {
                textBox1.ReadOnly = true;
                textBox1.Enabled = false;
                textBox2.ReadOnly = true;
                textBox2.Enabled = false;
                textBox3.ReadOnly = false;
                textBox3.Enabled = true;
            }
            if (comboBox1.SelectedItem == comboBox1.Items[2])
            {
                textBox1.ReadOnly = false;
                textBox1.Enabled = true;
                textBox2.ReadOnly = false;
                textBox2.Enabled = true;
                textBox3.ReadOnly = true;
                textBox3.Enabled = false;
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if ((Convert.ToString(dataGridView1[1, 0].Value) == ""))
                {
                    button3.PerformClick();
                }
            }
            catch (System.ArgumentOutOfRangeException) { 
                MessageBox.Show("Non hai inserito nessun file.\n\n\t\t Uscita dal programma.", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Environment.Exit(1);
            }
            int j = 0;
            string[] fileNamesTmp = fileNamesArray2;
            fileNamesArraydef = fileNamesArray2;
            List<string> fileNamesListTmp = new List<string>();
            try { fileNamesListTmp = fileNamesTmp.ToList(); }
            catch (System.ArgumentNullException) { 
                MessageBox.Show("Non hai inserito nessun file.\n\n\t\t Uscita dal programma.", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Environment.Exit(1);
            }
            foreach (string s1 in fileNamesArraydef)
            {
                int ind = fileNamesArray2[j].LastIndexOf("\\");
                int ind2 = fileNamesArray2[j].LastIndexOf(".");
                int ind3 = fileNamesArray2[j].Length;

                try
                {
                    fileNamesArraydef[j] = fileNamesArray2[j].Substring(0, ind) + "\\" + risultato[j] + fileNamesArray2[j].Substring(ind2, fileNamesArray2[j].Length - ind2);
                }

                catch (System.ArgumentOutOfRangeException)
                {
                    MessageBox.Show("Non hai inserito alcuna modifica.\n\n\t\t Uscita dal programma.", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    System.Environment.Exit(1);
                }
                    j++;
            }

            DialogResult dres = MessageBox.Show("Se ci sono file che otterranno lo stesso nome, non verrano rinominati tutti e l'operazione non andrà a buon fine. Assicurati che tutti i file abbiano nomi diversi dopo la modifica, altrimenti l'operazione non andrà a buon fine.\n\nL'operazione è irreversibile.\n\nVuoi continuare?", "Attenzione!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dres == DialogResult.Yes)
            {
                j = 0;
                foreach (string s2 in fileNamesArraydef)
                {
                    try
                    {
                        System.IO.File.Move(@fileNamesListTmp[j], @fileNamesArraydef[j]);
                        j++;
                    }
                    catch (System.IO.IOException)
                    {
                        MessageBox.Show("Esiste almeno un file con lo stesso nome del risultato, l'operazione non può essere eseguita.\n\n\t\t Uscita dal programma.", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        System.Environment.Exit(1);
                    }
                }
            }
            if (dres == DialogResult.No)
            {
                MessageBox.Show("Hai deciso di uscire dal programma.", "Uscita dal programma", MessageBoxButtons.OK, MessageBoxIcon.Information);
                try 
                {
                    GC.Collect();
                    System.Environment.Exit(1); 
                }
                catch (System.ComponentModel.Win32Exception) { System.Environment.Exit(1); }
            }


            MessageBox.Show("File rinominati correttamente.");

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        public void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();

            dataGridView1.Columns.Add("File", "File");
            dataGridView1.Columns.Add("Modifica", "Modifica");
            dataGridView1.Columns.Add("Risultato", "Risultato");

            risultato.Clear();
            a = textBox3.Text;

            try
            {
                for (int i = 0; i < fileNamesArray2.Length; i++)
                {

                    if (comboBox1.SelectedItem == comboBox1.Items[0])
                    {

                        risultato.Add(Convert.ToString(fileNamesListnoext[i]) + a);

                    }
                    if (comboBox1.SelectedItem == comboBox1.Items[1])
                    {
                        try { risultato.Add(Convert.ToString(fileNamesListnoext[i].Remove(fileNamesListnoext[i].Length - Convert.ToInt32(textBox3.Text)))); }
                        catch (System.ArgumentOutOfRangeException)
                        {
                            MessageBox.Show("Stai cercando di rimuovere più caratteri di quelli esistenti.\n\n\t\t\tUscita dal programma.", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            GC.Collect();
                            System.Environment.Exit(1);
                        }
                        a = "- " + Convert.ToInt32(textBox3.Text);

                    }
                    if (comboBox1.SelectedItem == comboBox1.Items[2])
                    {
                        a = textBox1.Text;
                        try { risultato.Add(Convert.ToString(fileNamesListnoext[i].Remove(fileNamesListnoext[i].Length - Convert.ToInt32(textBox2.Text))) + a); }
                        catch (System.FormatException)
                        {
                            MessageBox.Show("Non hai inserito alcuna modifica.\n\n\t\t Uscita dal programma.", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            System.Environment.Exit(1);
                        }

                        catch (System.ArgumentOutOfRangeException)
                        {
                            MessageBox.Show("Stai cercando di rimuovere più caratteri di quelli esistenti.\n\n\t\t\tUscita dal programma.", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            GC.Collect();
                            System.Environment.Exit(1);
                        }
                    }
                }
            }

            catch (System.NullReferenceException) {
                MessageBox.Show("Non hai inserito nessun file.\n\n\t\t Uscita dal programma.", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Environment.Exit(1);
            }

            for (int i = 0; i < fileNamesArray2.Length; i++)
            {
                try
                {
                    dataGridView1.Rows.Add(fileNamesListnoext[i], a, risultato[i]);
                }

                catch (System.ArgumentOutOfRangeException)
                {
                    MessageBox.Show("Non hai inserito alcuna modifica.\n\n\t\t Uscita dal programma.", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    System.Environment.Exit(1);
                }
            }

            dataGridView1.AutoResizeColumns();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void objectListView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Aggiungi: aggiungi una stringa di testo ai nomi.\nRimuovi: rimuovi un certo numero di caratteri dalla fine dei nomi.\nModifica: rimuovi un certo numero di caratteri e inseriscine altri per sostituirli.\n\nSviluppato da: Emanuele Mazzucchi.\n\nCC BY-SA\nQuest'opera è stata rilasciata con licenza Creative Commons Attribuzione - Condividi allo stesso modo 4.0 Internazionale. Per leggere una copia della licenza visita il sito web http://creativecommons.org/licenses/by-sa/4.0/.", "Informazioni", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}

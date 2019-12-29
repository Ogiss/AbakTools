namespace Enova.Forms.CRM
{
    partial class KontrahentOgolnePanel
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.kodPocztKorTextBox = new System.Windows.Forms.TextBox();
            this.adresDoKorBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.ulicaKorTextBox = new System.Windows.Forms.TextBox();
            this.faksKorTextBox = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.telefonKorTextBox = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.nrDomuKorTextBox = new System.Windows.Forms.TextBox();
            this.nrlokaluKorTextBox = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.miejscowośćKorTextBox = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.pocztaKorTextBox = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rabatTextBox = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.terminTextBox = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.blokadaSprzedażyCheckBox = new System.Windows.Forms.CheckBox();
            this.blokadaCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.kodPocztowyTextBox = new System.Windows.Forms.TextBox();
            this.adresBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.ulicaTextBox = new System.Windows.Forms.TextBox();
            this.faksTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.telefonuTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.nrDomuTextBox = new System.Windows.Forms.TextBox();
            this.nrLokaluTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.miejscowośćTextBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.pocztaTextBox = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.przedstawicielComboBox = new System.Windows.Forms.ComboBox();
            this.label30 = new System.Windows.Forms.Label();
            this.nazwaTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.kodTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.nipTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.BindingSource)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.adresDoKorBindingSource)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.adresBindingSource)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // BindingSource
            // 
            this.BindingSource.DataSource = typeof(Enova.API.CRM.Kontrahent);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.kodPocztKorTextBox);
            this.groupBox4.Controls.Add(this.label19);
            this.groupBox4.Controls.Add(this.label20);
            this.groupBox4.Controls.Add(this.ulicaKorTextBox);
            this.groupBox4.Controls.Add(this.faksKorTextBox);
            this.groupBox4.Controls.Add(this.label21);
            this.groupBox4.Controls.Add(this.telefonKorTextBox);
            this.groupBox4.Controls.Add(this.label22);
            this.groupBox4.Controls.Add(this.label23);
            this.groupBox4.Controls.Add(this.nrDomuKorTextBox);
            this.groupBox4.Controls.Add(this.nrlokaluKorTextBox);
            this.groupBox4.Controls.Add(this.label25);
            this.groupBox4.Controls.Add(this.label26);
            this.groupBox4.Controls.Add(this.miejscowośćKorTextBox);
            this.groupBox4.Controls.Add(this.label28);
            this.groupBox4.Controls.Add(this.pocztaKorTextBox);
            this.groupBox4.Location = new System.Drawing.Point(3, 370);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(715, 135);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Adres korespondencyjny";
            // 
            // kodPocztKorTextBox
            // 
            this.kodPocztKorTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.adresDoKorBindingSource, "KodPocztowy", true));
            this.kodPocztKorTextBox.Location = new System.Drawing.Point(108, 49);
            this.kodPocztKorTextBox.Name = "kodPocztKorTextBox";
            this.kodPocztKorTextBox.Size = new System.Drawing.Size(110, 20);
            this.kodPocztKorTextBox.TabIndex = 20;
            // 
            // adresDoKorBindingSource
            // 
            this.adresDoKorBindingSource.DataSource = typeof(Enova.API.Core.Adres);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(8, 23);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(34, 13);
            this.label19.TabIndex = 6;
            this.label19.Text = "Ulica:";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(390, 104);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(50, 13);
            this.label20.TabIndex = 30;
            this.label20.Text = "Nr faksu:";
            // 
            // ulicaKorTextBox
            // 
            this.ulicaKorTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.adresDoKorBindingSource, "Ulica", true));
            this.ulicaKorTextBox.Location = new System.Drawing.Point(108, 23);
            this.ulicaKorTextBox.Name = "ulicaKorTextBox";
            this.ulicaKorTextBox.Size = new System.Drawing.Size(220, 20);
            this.ulicaKorTextBox.TabIndex = 17;
            // 
            // faksKorTextBox
            // 
            this.faksKorTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.adresDoKorBindingSource, "Faks", true));
            this.faksKorTextBox.Location = new System.Drawing.Point(467, 101);
            this.faksKorTextBox.Name = "faksKorTextBox";
            this.faksKorTextBox.Size = new System.Drawing.Size(224, 20);
            this.faksKorTextBox.TabIndex = 24;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(390, 26);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(50, 13);
            this.label21.TabIndex = 8;
            this.label21.Text = "Nr domu:";
            // 
            // telefonKorTextBox
            // 
            this.telefonKorTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.adresDoKorBindingSource, "Telefon", true));
            this.telefonKorTextBox.Location = new System.Drawing.Point(108, 101);
            this.telefonKorTextBox.Name = "telefonKorTextBox";
            this.telefonKorTextBox.Size = new System.Drawing.Size(220, 20);
            this.telefonKorTextBox.TabIndex = 23;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(558, 26);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(52, 13);
            this.label22.TabIndex = 9;
            this.label22.Text = "Nr lokalu:";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(6, 104);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(62, 13);
            this.label23.TabIndex = 27;
            this.label23.Text = "Nr telefonu:";
            // 
            // nrDomuKorTextBox
            // 
            this.nrDomuKorTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.adresDoKorBindingSource, "NrDomu", true));
            this.nrDomuKorTextBox.Location = new System.Drawing.Point(467, 23);
            this.nrDomuKorTextBox.Name = "nrDomuKorTextBox";
            this.nrDomuKorTextBox.Size = new System.Drawing.Size(74, 20);
            this.nrDomuKorTextBox.TabIndex = 18;
            // 
            // nrlokaluKorTextBox
            // 
            this.nrlokaluKorTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.adresDoKorBindingSource, "NrLokalu", true));
            this.nrlokaluKorTextBox.Location = new System.Drawing.Point(617, 23);
            this.nrlokaluKorTextBox.Name = "nrlokaluKorTextBox";
            this.nrlokaluKorTextBox.Size = new System.Drawing.Size(74, 20);
            this.nrlokaluKorTextBox.TabIndex = 19;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(8, 52);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(77, 13);
            this.label25.TabIndex = 12;
            this.label25.Text = "Kod pocztowy:";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(390, 52);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(71, 13);
            this.label26.TabIndex = 14;
            this.label26.Text = "Miejscowość:";
            // 
            // miejscowośćKorTextBox
            // 
            this.miejscowośćKorTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.adresDoKorBindingSource, "Miejscowosc", true));
            this.miejscowośćKorTextBox.Location = new System.Drawing.Point(467, 49);
            this.miejscowośćKorTextBox.Name = "miejscowośćKorTextBox";
            this.miejscowośćKorTextBox.Size = new System.Drawing.Size(224, 20);
            this.miejscowośćKorTextBox.TabIndex = 21;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(8, 78);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(43, 13);
            this.label28.TabIndex = 16;
            this.label28.Text = "Poczta:";
            // 
            // pocztaKorTextBox
            // 
            this.pocztaKorTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.adresDoKorBindingSource, "Poczta", true));
            this.pocztaKorTextBox.Location = new System.Drawing.Point(108, 75);
            this.pocztaKorTextBox.Name = "pocztaKorTextBox";
            this.pocztaKorTextBox.Size = new System.Drawing.Size(220, 20);
            this.pocztaKorTextBox.TabIndex = 22;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rabatTextBox);
            this.groupBox3.Controls.Add(this.label27);
            this.groupBox3.Controls.Add(this.terminTextBox);
            this.groupBox3.Controls.Add(this.label18);
            this.groupBox3.Controls.Add(this.label17);
            this.groupBox3.Controls.Add(this.label16);
            this.groupBox3.Controls.Add(this.blokadaSprzedażyCheckBox);
            this.groupBox3.Controls.Add(this.blokadaCheckBox);
            this.groupBox3.Location = new System.Drawing.Point(3, 157);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(715, 65);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Warunki płatności";
            // 
            // rabatTextBox
            // 
            this.rabatTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.BindingSource, "Rabat", true));
            this.rabatTextBox.Location = new System.Drawing.Point(456, 20);
            this.rabatTextBox.Name = "rabatTextBox";
            this.rabatTextBox.Size = new System.Drawing.Size(100, 20);
            this.rabatTextBox.TabIndex = 8;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(411, 24);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(39, 13);
            this.label27.TabIndex = 32;
            this.label27.Text = "Rabat:";
            // 
            // terminTextBox
            // 
            this.terminTextBox.Location = new System.Drawing.Point(316, 21);
            this.terminTextBox.Name = "terminTextBox";
            this.terminTextBox.Size = new System.Drawing.Size(74, 20);
            this.terminTextBox.TabIndex = 7;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(248, 23);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(62, 13);
            this.label18.TabIndex = 6;
            this.label18.Text = "Termin(dni):";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(105, 24);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(99, 13);
            this.label17.TabIndex = 7;
            this.label17.Text = "Blokada sprzedaży:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(11, 24);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(49, 13);
            this.label16.TabIndex = 6;
            this.label16.Text = "Blokada:";
            // 
            // blokadaSprzedażyCheckBox
            // 
            this.blokadaSprzedażyCheckBox.AutoSize = true;
            this.blokadaSprzedażyCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.BindingSource, "BlokadaSprzedaży", true));
            this.blokadaSprzedażyCheckBox.Location = new System.Drawing.Point(210, 23);
            this.blokadaSprzedażyCheckBox.Name = "blokadaSprzedażyCheckBox";
            this.blokadaSprzedażyCheckBox.Size = new System.Drawing.Size(15, 14);
            this.blokadaSprzedażyCheckBox.TabIndex = 6;
            this.blokadaSprzedażyCheckBox.UseVisualStyleBackColor = true;
            this.blokadaSprzedażyCheckBox.CheckedChanged += new System.EventHandler(this.blokadaSprzedażyCheckBox_CheckedChanged);
            // 
            // blokadaCheckBox
            // 
            this.blokadaCheckBox.AutoSize = true;
            this.blokadaCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.BindingSource, "Blokada", true));
            this.blokadaCheckBox.Location = new System.Drawing.Point(66, 23);
            this.blokadaCheckBox.Name = "blokadaCheckBox";
            this.blokadaCheckBox.Size = new System.Drawing.Size(15, 14);
            this.blokadaCheckBox.TabIndex = 5;
            this.blokadaCheckBox.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.kodPocztowyTextBox);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.ulicaTextBox);
            this.groupBox2.Controls.Add(this.faksTextBox);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.telefonuTextBox);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.nrDomuTextBox);
            this.groupBox2.Controls.Add(this.nrLokaluTextBox);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.miejscowośćTextBox);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.pocztaTextBox);
            this.groupBox2.Location = new System.Drawing.Point(3, 228);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(715, 136);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Adres";
            // 
            // kodPocztowyTextBox
            // 
            this.kodPocztowyTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.adresBindingSource, "KodPocztowy", true));
            this.kodPocztowyTextBox.Location = new System.Drawing.Point(108, 49);
            this.kodPocztowyTextBox.Name = "kodPocztowyTextBox";
            this.kodPocztowyTextBox.Size = new System.Drawing.Size(110, 20);
            this.kodPocztowyTextBox.TabIndex = 12;
            // 
            // adresBindingSource
            // 
            this.adresBindingSource.DataSource = typeof(Enova.API.Core.Adres);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Ulica:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(390, 104);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(50, 13);
            this.label15.TabIndex = 30;
            this.label15.Text = "Nr faksu:";
            // 
            // ulicaTextBox
            // 
            this.ulicaTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.adresBindingSource, "Ulica", true));
            this.ulicaTextBox.Location = new System.Drawing.Point(108, 23);
            this.ulicaTextBox.Name = "ulicaTextBox";
            this.ulicaTextBox.Size = new System.Drawing.Size(220, 20);
            this.ulicaTextBox.TabIndex = 9;
            // 
            // faksTextBox
            // 
            this.faksTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.adresBindingSource, "Faks", true));
            this.faksTextBox.Location = new System.Drawing.Point(467, 101);
            this.faksTextBox.Name = "faksTextBox";
            this.faksTextBox.Size = new System.Drawing.Size(224, 20);
            this.faksTextBox.TabIndex = 16;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(390, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Nr domu:";
            // 
            // telefonuTextBox
            // 
            this.telefonuTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.adresBindingSource, "Telefon", true));
            this.telefonuTextBox.Location = new System.Drawing.Point(108, 101);
            this.telefonuTextBox.Name = "telefonuTextBox";
            this.telefonuTextBox.Size = new System.Drawing.Size(220, 20);
            this.telefonuTextBox.TabIndex = 15;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(558, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Nr lokalu:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(11, 104);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(62, 13);
            this.label14.TabIndex = 27;
            this.label14.Text = "Nr telefonu:";
            // 
            // nrDomuTextBox
            // 
            this.nrDomuTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.adresBindingSource, "NrDomu", true));
            this.nrDomuTextBox.Location = new System.Drawing.Point(467, 23);
            this.nrDomuTextBox.Name = "nrDomuTextBox";
            this.nrDomuTextBox.Size = new System.Drawing.Size(74, 20);
            this.nrDomuTextBox.TabIndex = 10;
            // 
            // nrLokaluTextBox
            // 
            this.nrLokaluTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.adresBindingSource, "NrLokalu", true));
            this.nrLokaluTextBox.Location = new System.Drawing.Point(617, 23);
            this.nrLokaluTextBox.Name = "nrLokaluTextBox";
            this.nrLokaluTextBox.Size = new System.Drawing.Size(74, 20);
            this.nrLokaluTextBox.TabIndex = 11;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 52);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Kod pocztowy:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(390, 52);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(71, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Miejscowość:";
            // 
            // miejscowośćTextBox
            // 
            this.miejscowośćTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.adresBindingSource, "Miejscowosc", true));
            this.miejscowośćTextBox.Location = new System.Drawing.Point(467, 49);
            this.miejscowośćTextBox.Name = "miejscowośćTextBox";
            this.miejscowośćTextBox.Size = new System.Drawing.Size(224, 20);
            this.miejscowośćTextBox.TabIndex = 13;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(11, 78);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(43, 13);
            this.label9.TabIndex = 16;
            this.label9.Text = "Poczta:";
            // 
            // pocztaTextBox
            // 
            this.pocztaTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.adresBindingSource, "Poczta", true));
            this.pocztaTextBox.Location = new System.Drawing.Point(108, 75);
            this.pocztaTextBox.Name = "pocztaTextBox";
            this.pocztaTextBox.Size = new System.Drawing.Size(220, 20);
            this.pocztaTextBox.TabIndex = 14;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.przedstawicielComboBox);
            this.groupBox1.Controls.Add(this.label30);
            this.groupBox1.Controls.Add(this.nazwaTextBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.kodTextBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.nipTextBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(715, 148);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Data identyfikacyjne";
            // 
            // przedstawicielComboBox
            // 
            this.przedstawicielComboBox.DisplayMember = "Value";
            this.przedstawicielComboBox.FormattingEnabled = true;
            this.przedstawicielComboBox.Location = new System.Drawing.Point(108, 115);
            this.przedstawicielComboBox.Name = "przedstawicielComboBox";
            this.przedstawicielComboBox.Size = new System.Drawing.Size(121, 21);
            this.przedstawicielComboBox.TabIndex = 4;
            this.przedstawicielComboBox.ValueMember = "Value";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(25, 118);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(77, 13);
            this.label30.TabIndex = 8;
            this.label30.Text = "Przedstawiciel:";
            // 
            // nazwaTextBox
            // 
            this.nazwaTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.BindingSource, "Nazwa", true));
            this.nazwaTextBox.Location = new System.Drawing.Point(106, 46);
            this.nazwaTextBox.Multiline = true;
            this.nazwaTextBox.Name = "nazwaTextBox";
            this.nazwaTextBox.Size = new System.Drawing.Size(583, 63);
            this.nazwaTextBox.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Kod:";
            // 
            // kodTextBox
            // 
            this.kodTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.BindingSource, "Kod", true));
            this.kodTextBox.Location = new System.Drawing.Point(106, 20);
            this.kodTextBox.Name = "kodTextBox";
            this.kodTextBox.Size = new System.Drawing.Size(220, 20);
            this.kodTextBox.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(490, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "NIP:";
            // 
            // nipTextBox
            // 
            this.nipTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.BindingSource, "NIP", true));
            this.nipTextBox.Location = new System.Drawing.Point(524, 20);
            this.nipTextBox.Name = "nipTextBox";
            this.nipTextBox.Size = new System.Drawing.Size(165, 20);
            this.nipTextBox.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Nazwa:";
            // 
            // KontrahentOgolnePanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "KontrahentOgolnePanel";
            this.Size = new System.Drawing.Size(722, 508);
            this.Load += new System.EventHandler(this.KontrahentOgolnePanel_Load);
            ((System.ComponentModel.ISupportInitialize)(this.BindingSource)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.adresDoKorBindingSource)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.adresBindingSource)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox kodPocztKorTextBox;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox ulicaKorTextBox;
        private System.Windows.Forms.TextBox faksKorTextBox;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox telefonKorTextBox;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox nrDomuKorTextBox;
        private System.Windows.Forms.TextBox nrlokaluKorTextBox;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.TextBox miejscowośćKorTextBox;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.TextBox pocztaKorTextBox;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox rabatTextBox;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TextBox terminTextBox;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.CheckBox blokadaSprzedażyCheckBox;
        private System.Windows.Forms.CheckBox blokadaCheckBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox kodPocztowyTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox ulicaTextBox;
        private System.Windows.Forms.TextBox faksTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox telefonuTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox nrDomuTextBox;
        private System.Windows.Forms.TextBox nrLokaluTextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox miejscowośćTextBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox pocztaTextBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox przedstawicielComboBox;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.TextBox nazwaTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox kodTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox nipTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.BindingSource adresDoKorBindingSource;
        private System.Windows.Forms.BindingSource adresBindingSource;
    }
}

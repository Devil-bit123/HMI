using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Linq.Expressions;
using System.Threading;
using System.Runtime.Remoting.Contexts;

namespace HMI
{
    public partial class Form1 : Form
    {

        string dato="";
        string[] info;
        int valPotAnalog;
        double voltaje;

        public Form1()
        {
            InitializeComponent();
            if (serialPort1.IsOpen)
            {
                serialPort1.Close();
                serialPort1.Dispose();
                
            }
            else
            {
                string[] puertos = SerialPort.GetPortNames();
                comboBox1.Items.AddRange(puertos);

            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            serialPort1.Close();
            serialPort1.Dispose();
           
            try 
            {
                serialPort1.PortName = comboBox1.Text;
                serialPort1.Open();
                CheckForIllegalCrossThreadCalls = false;
                if (serialPort1.IsOpen == true)
                {
                    radioButton1.Text = "ON";
                    radioButton1.Checked = true;
                    MessageBox.Show("CONECTADO CON EL ARDUINO");
                    button1.Enabled = false;
                    timer1.Start();
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("ERROR DE CONEXION CON EL ARDUINO: "+ err);
            }
        }

           
            
        

        private void Form1_Load(object sender, EventArgs e)
        {
            
            // TODO: esta línea de código carga datos en la tabla 'datosArdDataSet.sensores' Puede moverla o quitarla según sea necesario.
            this.sensoresTableAdapter.Fill(this.datosArdDataSet.sensores);
            radioButton1.Enabled = false;
            
            
        }

        private void ver()
        {
            datosArdEntities1 contextDB = new datosArdEntities1();
            var valores = from values in contextDB.sensores
                          select values;
            dataGridView1.DataSource = valores.ToList();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            serialPort1.Close();
            serialPort1.Dispose();
            radioButton1.Text = "OFF";
            radioButton1.Checked = false;
            lblAnalog.Text = "";
            lblDistancia.Text = "";
            lblTemp.Text = "";
            lblVolts.Text = "";
            progressBar1.Value = 0;
            MessageBox.Show("DESCONECTADO DEL ARDUINO");
            button1.Enabled = true;
         
        }

        

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {

          
            
           
            dato = serialPort1.ReadLine();            
            info = dato.Split(';');      


            try
            {


                info = dato.Split(';');
                if (info.Length <= 3)
                {
                    lblAnalog.Text = info[0].ToString();
                    lblTemp.Text=info[1].ToString();
                    lblDistancia.Text = info[2].ToString();
                    int progressVolt = Convert.ToInt32(info[0]);
                    double voltA = Convert.ToDouble(info[0]);
                    double voltD = Math.Round(((voltA*5)/1023),2);
                    lblVolts.Text = voltD.ToString();
                    progressBar1.Value = progressVolt;
                }
                else
                {
                    lblAnalog.Text = "";
                    lblTemp.Text = "";
                    lblDistancia.Text = "";
                }
                                
                

            }
            catch (Exception err)
            {
                MessageBox.Show("Error de conversion de valor analogo: "+err);
            }
          
            
           
           
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            serialPort1.Close();
            serialPort1.Dispose();
            radioButton1.Text = "OFF";
            radioButton1.Checked = false;
            lblAnalog.Text = "";
            lblDistancia.Text = "";
            lblTemp.Text = "";
            lblVolts.Text = "";
            progressBar1.Value = 0;
            MessageBox.Show("DESCONECTADO DEL ARDUINO");
            button1.Enabled = true;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            serialPort1.Close();
            serialPort1.Dispose();
            radioButton1.Text = "OFF";
            radioButton1.Checked = false;
            lblAnalog.Text = "";
            lblDistancia.Text = "";
            lblTemp.Text = "";
            lblVolts.Text = "";
            progressBar1.Value = 0;
            MessageBox.Show("DESCONECTADO DEL ARDUINO");
            button1.Enabled = true;
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void lblAnalog_Click(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {
            if (progressBar1.Value > 1023)
            {
                progressBar1.Value = 0;
            }

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ver();
            datosArdEntities1 contextDB = new datosArdEntities1();
            if(lblAnalog.Text!=null || lblDistancia.Text!=null || lblTemp.Text != null)
            {
                sensores dato = new sensores();
                dato.valorVoltaje = lblAnalog.Text;
                dato.valorDistancia = lblDistancia.Text;
                dato.valorTemperatura = lblTemp.Text;
                dato.Fecha = DateTime.Now.ToString();
                contextDB.sensores.Add(dato);
                try
                {
                    if (contextDB.SaveChanges() == 1)
                    {
                        lblError.Text = "OK";

                    }
                }
                catch (Exception)
                {
                    lblError.Text = "EROR DE INSERCION";
                }
            }

            
        }

        
    }
}

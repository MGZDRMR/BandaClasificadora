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

namespace NightProject
{
    public partial class Form1 : Form
    {
        SerialPort arduino;
        sensors sensores;
        System.Windows.Forms.Timer loop;
        String datos;
        public Form1()
        {
            InitializeComponent();
            arduino = new SerialPort();
            arduino.BaudRate = 9600;
            arduino.Parity = Parity.None;
            arduino.StopBits = StopBits.One;
            arduino.DataBits = 8;

            loop = new System.Windows.Forms.Timer();
            loop.Interval = 500;
            loop.Tick += Loop_Tick;
            loop.Start();

            sensores = new sensors();

            CargarPuertosCOM();
        }

        private void CargarPuertosCOM()
        {
            string[] puertosDisponibles = SerialPort.GetPortNames();
            combito.Items.AddRange(puertosDisponibles);
        }

        private void Loop_Tick(object sender, EventArgs e)
        {
            char band = 'O';
            int contc = 0;
            string contch = contc.ToString();

            contText.Text = sensores.str_contador;
            dister.Text = sensores.str_distancia+" CM";
            labelpequeño.Text = sensores.str_peque;
            labelmediano.Text = sensores.str_medi;
            labelgrande.Text = sensores.str_gde;

            if (sensores.distancia <= 9)
            {
                tmn.Text = "Grande";
               
            }
            if (sensores.distancia >= 5 && sensores.distancia <= 8)
            {
                tmn.Text = "Mediano";
            }
            if (sensores.distancia >= 8 && sensores.distancia <= 10)
            {
               
                tmn.Text = "Chico";

            }
            if (sensores.distancia >= 11)
            {
                tmn.Text = "En Espera";
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btn_conect_Click(object sender, EventArgs e)
        {
            if(btn_conect.Text == "Conectar" && !arduino.IsOpen){
                try
                {
                    arduino.PortName = combito.SelectedItem.ToString();
                    arduino.Open();
                    arduino.DataReceived += Arduino_DataReceived;
                    btn_conect.Text = "Desconectar";
                    states.Text = "BANDA CONECTADA";
                }
                catch(Exception error) {

                    MessageBox.Show("No se pudo realizar la conexión");
                    
                }
            }
            else if(btn_conect.Text == "Desconectar" && arduino.IsOpen)
            {

                arduino.Close();
                btn_conect.Text = "Conectar";
            }
        }



        private void Arduino_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            datos = arduino.ReadLine();
            string[] substrings = datos.Split('/');
            if(substrings.Length == 7)
            {
                if (substrings[0] == "~")
                {
                    sensores.str_contador = substrings[1];
                    sensores.str_peque = substrings[2];
                    sensores.str_medi = substrings[3];
                    sensores.str_gde = substrings[4];
                    sensores.str_distancia = substrings[5];
                    sensores.obtiene();
                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            arduino.Write("O");
            vel.Text = "APAGADO";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            arduino.Write("L");
            vel.Text = "LENTO";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            arduino.Write("N");
            vel.Text = "NORMAL";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            arduino.Write("R");
            vel.Text = "RAPIDO";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            arduino.Write("M");
            vel.Text = "VELOCIDAD MÁXIMA";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Desarrollado por TEAM MANTHRA para el Instituto Tecnologico Superior de Cintalapa de Agosto - Noviembre 2023\n\n" +
                "Programación y diseño por:\n\nManuel Angel Gómez Caballero\n" +
                "\nArte por:\n\nCarlos Eduardo Villanueva Ramirez\n" +
                "\nEnsamblaje, pintura y documentación:\n\nJesús Alexis López Jiménez\nAbner Mauricio Moeales Garcia" +
                "\nJosé Eduardo Clemente Morales\nDiego Armando López Hernandez", "Acerca de NIGHT PROJECT");

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}

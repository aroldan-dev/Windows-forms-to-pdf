using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _03_Trivial
{
    public partial class Form1 : Form
    {
        private String opcionSeleccionada;
        private int posicionCorrecta;
        private int modoJuego = 0;

        private int contAciertos;
        private int cantPreguntas = 6;
        private int contPreguntas = 0;
        private String[] arrayJugadores;
        private String[] arrayEquipos;

        public Form1()
        {
            InitializeComponent();
            initializeData();
            iniciarNuevaPartida();

        }






        public void initializeData()
        {
            PicSiguiente.BackColor = Color.Transparent;
            LblNumPregunta.BackColor = Color.Transparent;
            LblAciertos.BackColor = Color.Transparent;
            LblPreguntaCont.BackColor = Color.Transparent;
            GrpBoxPrincipal.Visible = true;
            GrpBoxPrincipal.BackColor = Color.Transparent;
            PicSiguiente.Enabled = false;
            arrayJugadores = new string[] { "Lewandowski", "Borja Iglesias", "Vinicius Jr", "Iago Aspas", "Chimy Ávila", "Álvaro Morata", "Joselu", "Álex Baena", "Nico Williams", "Samu Castillejo", "Stuani", "Falcao", "Negredo", "Oyarzabal", "Isco", "Pablo Maffeo" };
            arrayEquipos = new string[] { "Fc Barcelona", "Real Betis", "Real Madrid", "Celta de Vigo", "Osasuna", "Átletico de Madrid", "Espanyol", "Villareal", "Athletic de Bilbao", "Valencia", "Girona", "Rayo Vallecano", "Cádiz", "Real Sociedad", "Sevilla", "Mallorca" };
        }






        private void MnuPartidaNueva_Click(object sender, EventArgs e)
        {
            iniciarNuevaPartida();
        }

        private void iniciarNuevaPartida()
        {
            contAciertos = 0;
            contPreguntas = 0;
            TxtAciertosCont.Text = "/" + cantPreguntas;
            preguntaNueva();
            PicSiguiente.Focus();
            LblPreguntaCont.Text = "1";
        }

        private void rellenarPreguntas(String[] arrayAdivinar, String[] arrayOpciones)
        {

            //Objeto random que genera un numero aleatorio
            Random r = new Random();
            //Numero que almacena jugador elegido y equipo
            posicionCorrecta = r.Next(0, arrayAdivinar.Length);
            TxtPregunta.Text = arrayAdivinar[posicionCorrecta];

            //TextBox con opciones para rellenar
            Button[] opciones = new Button[4] { BtnOpcion1, BtnOpcion2, BtnOpcion3, BtnOpcion4 };
            //Sacamos un segundo numero para rellenar aleatoriamente un hueco con el equipo correcto;
            int randomNumber1 = r.Next(0, opciones.Length);
            opciones[randomNumber1].Text = arrayOpciones[posicionCorrecta];

            //Sacamos el resto de los 3 huecos y los rellenamos con numeros que no se repitan
            bool repetido = true;
            int[] numAleatorios = new int[3];//Almacenaremos los 3 numeros no repetidos en el array
            //Sacamos los 3 numeros sin repetirse
            do
            {
                int n1 = r.Next(0, arrayOpciones.Length), n2 = r.Next(0, arrayOpciones.Length), n3 = r.Next(0, arrayOpciones.Length);
                if (n1 != n2 && n1 != posicionCorrecta && n2 != n3 && n2 != posicionCorrecta &&
                    n3 != n1 && n3 != posicionCorrecta)
                {
                    repetido = false;
                    numAleatorios[0] = n1;
                    numAleatorios[1] = n2;
                    numAleatorios[2] = n3;


                }
            } while (repetido);

            //Rellenamos los textBox con las opciones incorrectas distintas
            int cont = 0;
            foreach (Button bt in opciones)
            {

                if (!bt.Text.Equals(arrayOpciones[posicionCorrecta]))
                {
                    bt.Text = arrayOpciones[numAleatorios[cont]];
                    cont++;
                }
            }


        }

        private void preguntaNueva()
        {
            GrpBoxPrincipal.Enabled = true;
            BtnOpcion1.Enabled = true;
            BtnOpcion2.Enabled = true;
            BtnOpcion3.Enabled = true;
            BtnOpcion4.Enabled = true;
            PicSiguiente.Enabled = false;
            if (modoJuego == 1)
            {
                rellenarPreguntas(arrayJugadores, arrayEquipos);
            }
            else if (modoJuego == 2)
            {
                rellenarPreguntas(arrayEquipos, arrayJugadores);
            }

            TxtPregunta.BackColor = Color.White;


        }



        private void BtnOpcion_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            opcionSeleccionada = b.Text;
            if (modoJuego == 1)
            {
                comprobarIntento(posicionCorrecta, opcionSeleccionada, arrayEquipos);
            }
            else if (modoJuego == 2)
            {
                comprobarIntento(posicionCorrecta, opcionSeleccionada, arrayJugadores);
            }

            PicSiguiente.Enabled = true;
        }

        private void comprobarIntento(int posicionCorrecta, String opcionSeleccionada, String[] arrayOpciones)
        {
            int posicionJugador = posicionCorrecta;
            if (arrayOpciones[posicionCorrecta].Equals(opcionSeleccionada))
            {
                TxtPregunta.BackColor = Color.Green;
                contAciertos++;
                TxtAciertosCont.Text = contAciertos + "/" + cantPreguntas;
            }
            else
            {
                TxtPregunta.BackColor = Color.Red;

            }
            BtnOpcion1.Enabled = false;
            BtnOpcion2.Enabled = false;
            BtnOpcion3.Enabled = false;
            BtnOpcion4.Enabled = false;

            PicSiguiente.Enabled = true;

        }

        private void PicSiguiente_Click(object sender, EventArgs e)
        {
            if (contPreguntas < (cantPreguntas - 1))
            {
                TxtPregunta.BackColor = Color.White;
                preguntaNueva();
                contPreguntas++;
                LblPreguntaCont.Text = (contPreguntas + 1).ToString();
            }
            else
            {
                GrpBoxPrincipal.Enabled = false;
                PicSiguiente.Enabled = true;
                finPartida();
            }

        }

        private void finPartida()
        {
            double porcentajeAciertos = (double)((double)contAciertos / (double)cantPreguntas) * 100;


            MessageBox.Show("Partida finalizada acertaste el " + porcentajeAciertos.ToString("N2") + "% de las preguntas\n" +
                "pulse nueva partida para jugar de nuevo", "Partida Finalizada", MessageBoxButtons.OK);
        }

        private void MnuPartidaSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void iniciarNuevaPartidaEquipoJugador()
        {
            contAciertos = 0;
            contPreguntas = 0;
            TxtAciertosCont.Text = "/" + cantPreguntas;
            preguntaNueva();
            PicSiguiente.Focus();
            LblPreguntaCont.Text = "1";
        }

        private void MnuOpciones_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem clicked = (ToolStripMenuItem)sender;
            clicked.Checked = true;
            if (clicked.Name.Equals("MnuOpcionesJugadores"))
            {
                modoJuego = 1;
                
            }
            else if (clicked.Name.Equals("MnuOpcionesEquipos"))
            {
                modoJuego = 2;
            }
            else if (clicked.Name.Equals("MnuOpcionesEscribirRespuesta"))
            {
                modoJuego = 3;
            }
        }
    }


}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace mroSoft
{
    class TextBoxButton : Button
    {
        private TextBox textBox;
        private bool editing = false;

        public TextBoxButton()
        {
            // Configurar el botón
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 4;
            this.Size = new Size(150, 40);
            this.BackColor = Color.White;
            this.ForeColor = Color.Black;

            // Configurar el TextBox
            textBox = new TextBox();
            textBox.Size = new Size(this.Width - 20, this.Height - 20); // Ajusta el tamaño del TextBox
            textBox.Location = new Point(5, 5); // Posición del TextBox dentro del botón
            textBox.BorderStyle = BorderStyle.None;
            textBox.BackColor = Color.White;

            // Configurar la fuente del TextBox
            textBox.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            textBox.ForeColor = System.Drawing.Color.Black;
            // Manejar eventos
            this.Click += Button_Click;
            textBox.LostFocus += TextBox_LostFocus;

            // Agregar el TextBox al control del botón
            this.Controls.Add(textBox);
            textBox.Hide();
        }



        private void Button_Click(object sender, EventArgs e)
        {
            // Al hacer clic en el botón, mostrar el TextBox para editar el texto
            textBox.Show();
            textBox.Focus();
            editing = true;
        }

        private void TextBox_LostFocus(object sender, EventArgs e)
        {
            // Cuando el TextBox pierde el foco, ocultarlo y actualizar el texto del botón
            textBox.Hide();
            editing = false;
            this.Text = textBox.Text;
        }

       protected override void OnPaint(PaintEventArgs pevent)
{
    base.OnPaint(pevent);

    // Dibujar el borde redondeado como en el botón original
    Rectangle rectSurface = this.ClientRectangle;
    int borderRadius = 4; // Puedes ajustar el radio del borde
    using (GraphicsPath pathSurface = GetFigurePath(rectSurface, borderRadius))
    using (Pen penBorder = new Pen(Color.Black, 2)) // Cambia el color del borde a negro
    {
        pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        // Botón surface
        this.Region = new Region(pathSurface);
        // Dibujar el borde
        pevent.Graphics.DrawPath(penBorder, pathSurface);
    }
}





        private GraphicsPath GetFigurePath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            float curveSize = radius * 2F;

            path.StartFigure();
            path.AddArc(rect.X, rect.Y, curveSize, curveSize, 180, 90);
            path.AddArc(rect.Right - curveSize, rect.Y, curveSize, curveSize, 270, 90);
            path.AddArc(rect.Right - curveSize, rect.Bottom - curveSize, curveSize, curveSize, 0, 90);
            path.AddArc(rect.X, rect.Bottom - curveSize, curveSize, curveSize, 90, 90);
            path.CloseFigure();

            return path;
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            // Actualizar el texto del TextBox si el botón está siendo editado
            if (editing)
            {
                textBox.Text = this.Text;
            }
        }
    }
}